using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.IO;
using System.Threading.Tasks;
using MemomMvc52.Areas.MemomWeb.Models;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using Memom.Service;
using Memom.Entities.Models;
using System.Globalization;
using Memom.FaceDetection;

namespace MemomMvc52.Areas.MemomWeb.Controllers
{
    [RoutePrefix("api")]
    [Authorize]
    public class FileController : ApiController
    {

        MemberService _memberSvc;
        UserService _userSvc;
        AlbumService _albumSvc;
        AlbumInstanceService _albumInstanceSvc;

        public FileController(MemberService memberSvc, UserService userSvc, AlbumService albumSvc, AlbumInstanceService albumInstanceSvc)
        {
            this._memberSvc = memberSvc;
            this._userSvc = userSvc;
            this._albumSvc = albumSvc;
            this._albumInstanceSvc = albumInstanceSvc;
        }

        private static readonly string ServerUploadFolderAlbum = HttpContext.Current.Server.MapPath("~/UserContent/Album/");
        private static readonly string ServerUploadFolder = HttpContext.Current.Server.MapPath("~/UserContent/Member/");
        private static readonly string ServerBinFolder = HttpContext.Current.Server.MapPath("~/bin/");

        [HttpPost, Route("UpdateFaceForMember", Name = "api-UpdateFaceForMember")]
        public UpdateFaceForMemberResult UpdateFaceForMember(FaceForMember faceInput)
        {
            Member memb = null; string oldFace = "";
            UpdateFaceForMemberResult resultSent = new UpdateFaceForMemberResult();
            resultSent.IsUpdateOk = false;


            // check member id
            if (!string.IsNullOrEmpty(faceInput.MemberId))
                memb = _memberSvc.FindMember(User.Identity.Name, Convert.ToInt32(faceInput.MemberId));
            else
            {   
                resultSent.ResultMsg = "Member Id is missing";
                return resultSent;
            }
            // check face image present
            if (string.IsNullOrEmpty(faceInput.FaceImage))
            {
                resultSent.ResultMsg = "Face Image is missing";
                return resultSent;
            }

            //start update
            if (memb != null)
            {
                oldFace = memb.FaceImage;
                memb.FaceImage = faceInput.FaceImage;
                int count = _memberSvc.UpdateMemberReplaceFace(memb, faceInput.FaceImage, oldFace);
                    resultSent.IsUpdateOk = true;
                    resultSent.ResultMsg = "FaceImage updated successfully";
                    return resultSent;
            }
            else
            {
                resultSent.ResultMsg = "Member Id does not belong to the logged in user";
                return resultSent;
            }

        }



        [HttpPost, Route("UploadMemberandFace", Name = "api-UploadMemberandFace")]
        [ValidateMimeMultipartContentFilter]
        public async Task<MemeberFaceUploadResult> UploadMemberandFace()
        {
            Member _memberRec = new Member();
            FileInfo fileInfo;
            FaceDetection.FaceDetetctResult faceRes = new FaceDetection.FaceDetetctResult();

            try
            {
                //--- insert record in to Member table and create a directory - before reading the file -------------
                _memberRec.Name = "Name";
                _memberRec.DisplayName = "DisplayName";
                _memberRec.DateOfBirth = DateTime.ParseExact("15/08/1947", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _memberRec.Relation = "Relation";
                _memberRec.IsActive = true;
                _memberRec.IsFaceDetected = false;
                _memberRec.IsFaceTagged = false;
                _memberRec.Created = DateTime.Now;
                _memberRec.UserKey = _userSvc.UserDetails(User.Identity.Name).Key;
                _memberSvc.Insert(_memberRec);

                string subPath = _memberRec.Key.ToString(); // your code goes here
                bool exists = System.IO.Directory.Exists(ServerUploadFolder + subPath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(ServerUploadFolder + subPath);
                //-------------------------------------------------------------------------------------------------------

                var streamProvider = new CustomMultipartFormDataStreamProvider(ServerUploadFolder + subPath);
                await Request.Content.ReadAsMultipartAsync(streamProvider);
                fileInfo = new FileInfo(streamProvider.FileData.Select(entry => entry.LocalFileName).FirstOrDefault());

                //---------------------------- open cv routine to do face detection -------------------------------
                
                faceRes = FaceDetection.DetectFaceSave(fileInfo.Name, ServerUploadFolder + subPath, "", ServerBinFolder, fileInfo.Extension);

                if (faceRes.Outcome)
                {

                    //-------------------Face detection successful and continue ----------------------------

                    _memberRec.Name = streamProvider.FormData["Name"];
                    _memberRec.DisplayName = streamProvider.FormData["DisplayName"];
                    _memberRec.DateOfBirth = DateTime.ParseExact(streamProvider.FormData["DateOfBirth"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    _memberRec.Relation = streamProvider.FormData["Relation"];
                    _memberRec.IsFaceDetected = faceRes.FacesDetected > 0 ? true: false;
                    _memberRec.IsFaceTagged = true;

                    _memberRec.FaceImage = faceRes.FacesDetected > 0 ? faceRes.FaceImages.FirstOrDefault() : null;
                    _memberRec.AbsoultePath = ServerUploadFolder + subPath + "\\";
                    _memberRec.DetectedFaceImage = faceRes.FacesDetected > 0 ? faceRes.SuperImposedImage : fileInfo.Name;
                    _memberRec.AllDetectedFaceImages = faceRes.GetAllFaceImageNames();
                    _memberRec.UnDetectedFaceImage = fileInfo.Name;
                    _memberRec.DetectedFaceCount = faceRes.FacesDetected;
                    _memberRec.FolderPath = "/UserContent/Member/" + subPath + "/";
                    _memberRec.OriginalFaceFileName = streamProvider.GetOriginalFileName;
                    _memberRec.FaceDetectionRemarks = faceRes.Remarks();

                    if (faceRes.FacesDetected > 0)
                        _memberSvc.UpdateMemberAddFace(_memberRec, faceRes.FaceImages.FirstOrDefault());
                    else
                        _memberSvc.Update(_memberRec); 
                    
                }
                else
                {
                    _memberRec.Name = streamProvider.FormData["Name"];
                    _memberRec.DisplayName = streamProvider.FormData["DisplayName"];
                    _memberRec.DateOfBirth = DateTime.ParseExact(streamProvider.FormData["DateOfBirth"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    _memberRec.Relation = streamProvider.FormData["Relation"];
                    _memberRec.IsFaceDetected = false;
                    _memberRec.IsFaceTagged = false;
                    _memberRec.DetectedFaceCount = faceRes.FacesDetected;
                    _memberRec.AbsoultePath = ServerUploadFolder + subPath + "\\";

                    _memberRec.DetectedFaceImage = fileInfo.Name;
                    _memberRec.UnDetectedFaceImage = fileInfo.Name;
                    _memberRec.FolderPath = "/UserContent/Member/" + subPath + "/";
                    _memberRec.OriginalFaceFileName = streamProvider.GetOriginalFileName;
                    _memberRec.FaceDetectionRemarks = String.IsNullOrEmpty(faceRes.ErrorMsg) ? "Some Exception" : faceRes.ErrorMsg;

                    _memberSvc.Update(_memberRec);
                }

                return new MemeberFaceUploadResult
                {
                    IsAddOk = true,
                    FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName).FirstOrDefault(),
                    ErrorMsg = !faceRes.Outcome ? faceRes.ErrorMsg : faceRes.Remarks(),
                    UpdatedTimestamp = DateTime.UtcNow,
                    FacesDetected = faceRes.FacesDetected,
                    MemberId = _memberRec.Key.ToString(),
                    IsFaceDetectionOk = faceRes.Outcome
                };
            }
            catch(Exception ex)
            {
                if (_memberRec.Key > 0) _memberSvc.Delete(_memberRec);
                return new MemeberFaceUploadResult
                {
                    IsAddOk = false,
                    FileNames = string.Empty,
                    ErrorMsg = ex.Message,
                    FacesDetected = faceRes.FacesDetected,
                    IsFaceDetectionOk = faceRes.Outcome
                };
            }

             
        }


        [HttpPost, Route("UploadReplacementPhoto", Name = "api-UploadReplacementPhoto")]
        [ValidateMimeMultipartContentFilter]
        public async Task<MemeberFaceUploadResult> UploadReplacementPhoto()
        {
            Member _memberRec; string oldFace = "";
            string stringMemberId, subPath;
            int intMemberId;
            FileInfo fileInfo = null;
            FaceDetection.FaceDetetctResult faceRes = new FaceDetection.FaceDetetctResult();

            try
            {
                var streamProvider = new CustomMultipartFormDataStreamProvider(ServerUploadFolder);
                await Request.Content.ReadAsMultipartAsync(streamProvider);
                fileInfo = new FileInfo(streamProvider.FileData.Select(entry => entry.LocalFileName).FirstOrDefault());

                if (fileInfo == null) throw (new Exception("Cannot fine uploaded file"));

                stringMemberId = streamProvider.FormData["MemberId"];
                if (string.IsNullOrEmpty(stringMemberId))
                {
                    if (File.Exists(fileInfo.FullName)) File.Delete(fileInfo.FullName);
                    return new MemeberFaceUploadResult
                    {
                        IsAddOk = false,
                        FileNames = string.Empty,
                        ErrorMsg = "MemberId is missing",
                        FacesDetected = faceRes.FacesDetected,
                        IsFaceDetectionOk = faceRes.Outcome
                    };
                }

                intMemberId = Convert.ToInt32(stringMemberId);

                _memberRec = _memberSvc.FindMember(User.Identity.Name, intMemberId);

                if (_memberSvc == null) 
                {
                    if (File.Exists(fileInfo.FullName)) File.Delete(fileInfo.FullName);
                    return new MemeberFaceUploadResult
                    {
                        IsAddOk = false,
                        FileNames = string.Empty,
                        ErrorMsg = "Unauthorized Access",
                        FacesDetected = faceRes.FacesDetected,
                        IsFaceDetectionOk = faceRes.Outcome
                    };
                }

                // recraete the stream with subpath gotten from member retrieved
                subPath = intMemberId.ToString();
                File.Move(fileInfo.FullName, ServerUploadFolder + subPath + "\\" + fileInfo.Name);
                oldFace = _memberRec.FaceImage;


                //---------------------------- open cv routine to do face detection -------------------------------

                faceRes = FaceDetection.DetectFaceSave(fileInfo.Name, ServerUploadFolder + subPath, "", ServerBinFolder, fileInfo.Extension);

                if (faceRes.Outcome)
                {

                    //-------------------Face detection successful and continue ----------------------------

                    _memberRec.IsFaceDetected = faceRes.FacesDetected > 0 ? true : false;
                    _memberRec.IsFaceTagged = true;

                    _memberRec.FaceImage = faceRes.FacesDetected > 0 ? faceRes.FaceImages.FirstOrDefault() : null;
                    _memberRec.AbsoultePath = ServerUploadFolder + subPath + "\\";

                    _memberRec.DetectedFaceImage = faceRes.FacesDetected > 0 ? faceRes.SuperImposedImage : fileInfo.Name;
                    _memberRec.AllDetectedFaceImages = faceRes.GetAllFaceImageNames();
                    _memberRec.UnDetectedFaceImage = fileInfo.Name;
                    _memberRec.DetectedFaceCount = faceRes.FacesDetected;
                    _memberRec.FolderPath = "/UserContent/Member/" + subPath + "/";
                    _memberRec.OriginalFaceFileName = streamProvider.GetOriginalFileName;
                    _memberRec.FaceDetectionRemarks = faceRes.Remarks();

                    if (_memberRec.FaceImage != null)
                    {
                        int count = _memberSvc.UpdateMemberReplaceFace(_memberRec, _memberRec.FaceImage, oldFace);
                    }
                    else
                    {
                        _memberSvc.Update(_memberRec);
                    }
                }
                else
                {
                    _memberRec.IsFaceDetected = false;
                    _memberRec.IsFaceTagged = false;
                    _memberRec.DetectedFaceCount = faceRes.FacesDetected;
                    _memberRec.AbsoultePath = ServerUploadFolder + subPath + "\\";

                    _memberRec.DetectedFaceImage = fileInfo.Name;
                    _memberRec.UnDetectedFaceImage = fileInfo.Name;
                    _memberRec.FolderPath = "/UserContent/Member/" + subPath + "/";
                    _memberRec.OriginalFaceFileName = streamProvider.GetOriginalFileName;
                    _memberRec.FaceDetectionRemarks = String.IsNullOrEmpty(faceRes.ErrorMsg) ? "Some Exception" : faceRes.ErrorMsg;

                    _memberSvc.Update(_memberRec);
                }

                return new MemeberFaceUploadResult
                {
                    IsAddOk = true,
                    FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName).FirstOrDefault(),
                    ErrorMsg = !faceRes.Outcome ? faceRes.ErrorMsg : faceRes.Remarks(),
                    UpdatedTimestamp = DateTime.UtcNow,
                    FacesDetected = faceRes.FacesDetected,
                    MemberId = _memberRec.Key.ToString(),
                    IsFaceDetectionOk = faceRes.Outcome
                };
            }
            catch (Exception ex)
            {
                if (fileInfo != null)
                {
                    if (File.Exists(fileInfo.FullName)) File.Delete(fileInfo.FullName);
                }
                return new MemeberFaceUploadResult
                {
                    IsAddOk = false,
                    FileNames = string.Empty,
                    ErrorMsg = ex.Message,
                    FacesDetected = faceRes.FacesDetected,
                    IsFaceDetectionOk = faceRes.Outcome,
                    UpdatedTimestamp = DateTime.UtcNow
                };
            }


        }


        [HttpPost, Route("AddAlbumAndPhotos", Name = "api-AddAlbumAndPhotos")]
        [ValidateMimeMultipartContentFilter]
        public async Task<AddAlbumAndPhotosResult> AddAlbumAndPhotos()
        {
            Album _albumRec = new Album();
            //IEnumerable<FileInfoAddPhotos> fileInfos;
            IEnumerable<FileInfo> fileInfos;
            FaceDetection.FaceDetetctResult faceRes = new FaceDetection.FaceDetetctResult();

            try
            {
                //--- insert record in to Member table and create a directory - before reading the file -------------
                _albumRec.Name = "Name";
                _albumRec.Description = "Description";
                _albumRec.DisplayOrder = 0;
                _albumRec.IsAttached = false;
                _albumRec.UserKey = _userSvc.UserDetails(User.Identity.Name).Key;
                _albumRec.Created = DateTime.Now;


                _albumSvc.Insert(_albumRec);

                string subPath = _albumRec.Key.ToString(); // your code goes here
                bool exists = System.IO.Directory.Exists(ServerUploadFolderAlbum + subPath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(ServerUploadFolderAlbum + subPath);
                //-------------------------------------------------------------------------------------------------------

                var streamProvider = new CustomMultipartFormDataStreamProvider(ServerUploadFolderAlbum + subPath);
                await Request.Content.ReadAsMultipartAsync(streamProvider);
                fileInfos = streamProvider.FileData.Select(entry =>
                            {
                                FileInfo fileInfo = new FileInfo(entry.LocalFileName);
                                return fileInfo;
                            });

                //---------------------------- open cv routine to do face detection -------------------------------
                // done in a batch


                // ---------- insert into albuminstances table for teh photos added in a loop -----------------------
                if (fileInfos.Count() > 0)
                {
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        UserAlbumInstance _albumInstance = new UserAlbumInstance();
                        _albumInstance.AlbumKey = _albumRec.Key;

                        _albumInstance.PhotoFile = fileInfo.Name;
                        _albumInstance.PhotoId = Guid.Parse(fileInfo.Name.Substring(0, 36).ToString());
                        _albumInstance.FolderPath = "/UserContent/Album/" + _albumRec.Key + "/";
                        _albumInstance.AbsolutePath = fileInfo.FullName;
                        _albumInstance.IsActive = true;
                        _albumInstance.AnyFacesTagged = false;
                        _albumInstance.PhotosSized = false;
                        _albumInstance.FacesDetected = 0;
                        _albumInstance.FileUploadStatus = "Success";
                        _albumInstance.IpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                        _albumInstance.OriginalFile = fileInfo.Name.Substring(37);
                        _albumInstance.ImageType = fileInfo.Extension;
                        _albumInstance.Created = DateTime.Now;

                        _albumInstanceSvc.Insert(_albumInstance, _albumRec.UserKey);
                    }
                }


                if (fileInfos.Count() > 0)
                {

                    //-------------------Face detection successful and continue ----------------------------

                    _albumRec.Name = streamProvider.FormData["Name"];
                    _albumRec.Description = streamProvider.FormData["Description"];
                    _albumRec.DisplayOrder = string.IsNullOrEmpty(streamProvider.FormData["DisplayOrder"]) ? 0 : Convert.ToInt32(streamProvider.FormData["DisplayOrder"]);
                    _albumRec.SetupEmail = User.Identity.Name;
                    _albumRec.IsAttached = true;
                    _albumRec.Remarks = "In Last session " + fileInfos.Count().ToString() + " uploaded on " + DateTime.UtcNow.ToString();
                    _albumRec.LastUpdated = DateTime.Now;


                    _albumSvc.Update(_albumRec);
                }

                return new AddAlbumAndPhotosResult
                {
                    IsAddOk = true,
                    FileNames = "Total of " + fileInfos.Count().ToString() + " has been added",
                    ErrorMsg = "",
                    UpdatedTimestamp = DateTime.UtcNow,
                    MemberId = _albumRec.UserKey.ToString()
                };
            }
            catch (Exception ex)
            {
                if (_albumRec.Key > 0) _albumSvc.Delete(_albumRec);
                return new AddAlbumAndPhotosResult
                {
                    IsAddOk = false,
                    FileNames = string.Empty,
                    ErrorMsg = ex.Message,
                    UpdatedTimestamp = DateTime.UtcNow,
                    MemberId = _albumRec.UserKey.ToString()
                };
            }


        }

    }

    public class ValidateMimeMultipartContentFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {

        }
    }

    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        private string originalFileName;
        public CustomMultipartFormDataStreamProvider(string path)
            : base(path)
        {
            originalFileName = String.Empty;
        }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            string fileName;
            if (!string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName))
            {
                //fileName = headers.ContentDisposition.FileName;
                //fileName = Guid.NewGuid().ToString() + "-" + headers.ContentDisposition.DispositionType;
                originalFileName = headers.ContentDisposition.FileName;
                fileName = Guid.NewGuid().ToString() + "--" + headers.ContentDisposition.FileName;
            }
            else
            {
                fileName = Guid.NewGuid().ToString() + "." + headers.ContentDisposition.DispositionType; 
            }

            originalFileName = originalFileName.Replace("\"", string.Empty);

            return fileName.Replace("\"", string.Empty); //this is here because Chrome submits files in quotation marks which get treated as part of the filename and get escaped
        }

        public string GetOriginalFileName
        {
            get { return originalFileName; }
        }
    }

    public class FileInfoAddPhotos
    {
        public FileInfo PhotoFile { get; set; }
        public string OriginalFileName { get; set; }
    }
}
