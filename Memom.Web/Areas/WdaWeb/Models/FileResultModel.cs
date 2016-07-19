using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemomMvc52.Areas.MemomWeb.Models
{
    public class FileResultModel
    {
            public IEnumerable<string> FileNames { get; set; }
            public string Description { get; set; }
            public DateTime CreatedTimestamp { get; set; }
            public DateTime UpdatedTimestamp { get; set; }
            public string DownloadLink { get; set; }
            public IEnumerable<string> ContentTypes { get; set; }
            public IEnumerable<string> Names { get; set; }
    }

    public class MemeberFaceUploadResult
    {
        public string FileNames { get; set; }
        public string ErrorMsg { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
        public int FacesDetected { get; set; }
        public string MemberId { get; set; }
        public bool IsFaceDetectionOk { get; set; }
        public bool IsAddOk { get; set; }

    }

    public class AddAlbumAndPhotosResult
    {
        public string FileNames { get; set; }
        public string ErrorMsg { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
        public string MemberId { get; set; }
        public bool IsAddOk { get; set; }

    }

    public class UpdateFaceForMemberResult
    {
        public string ResultMsg { get; set; }
        public bool IsUpdateOk { get; set; }

    }

    public class FaceForMember
    {
        public string MemberId {get; set;}
        public string FaceImage { get; set; }
    }
}