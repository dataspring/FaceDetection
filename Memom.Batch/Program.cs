using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using Ninject;

using Memom.Service;
using Repository.Pattern.DataContext;
using Memom.Entities.Models;
using Repository.Pattern.Ef6.Factories;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Memom.Batch;
using Newtonsoft.Json;

using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;
using Emgu.CV.GPU;
using System.Data.SqlTypes;




namespace Memom.Batch
{

        static class Program
        {
            

            static void Run(string[] args, bool verboseShow)
            {

                // Dependency Injection to reuse all stuff developed so far
                IKernel kernel = new StandardKernel();
                DependencyInjection(kernel);

                // service initializatons
                var _albumInstanceDetailSvc = kernel.Get<IAlbumInstanceDetailService>();
                var _albumInstanceSvc = kernel.Get<IAlbumInstanceService>();
                var _memberSvc = kernel.Get<IMemberService>();

                //class variable intializations

                string MatchFolderPath, MatchFile;
                bool processCatch;

                IEnumerable<UserAlbumInstanceDetail> batchPhotos = _albumInstanceDetailSvc.GetPhotosForBatchProcssing();
                Console.WriteLine("About to process {0} records", batchPhotos.Count().ToString());

                if (batchPhotos.Count() > 0)
                {
                    if (verboseShow)
                        Console.WriteLine("Total records retrieved for processing : {0}", batchPhotos.Count().ToString());

                    foreach(UserAlbumInstanceDetail batchPhoto in batchPhotos)
                    {
                        processCatch = false;
                        long ticks = DateTime.Now.Ticks;
                        if (verboseShow)
                            Console.WriteLine("Ticks-{0} : Processig record AlbumInstanceKey : {1}, MmeberKey : {2}, FaceToFind : {3}....", ticks.ToString(), batchPhoto.UserAlbumInstanceKey, batchPhoto.MemberKey, batchPhoto.FaceImage);
                        UserAlbumInstance photoImage = null;
                        Member member = null;
                        FindMatchResult findMatchResult = null;

                        MatchFolderPath = MatchFile = "";
                        photoImage = _albumInstanceSvc.FindAlbumInstance(batchPhoto.UserAlbumInstanceKey);
                        member = _memberSvc.FindMember(batchPhoto.MemberKey);

                        MatchInput matchInput = new MatchInput();
                        matchInput.FindInFile = new FileInfo(photoImage.AbsolutePath);
                        matchInput.MatchFile = new FileInfo(member.AbsoultePath + batchPhoto.FaceImage);
                        matchInput.WebFolderPath = photoImage.FolderPath +  "MatchFiles";

                        if (verboseShow)
                            Console.WriteLine(JsonConvert.SerializeObject(matchInput, Formatting.Indented));
                        try
                        {
                            findMatchResult = BruteForceMatcher.FindMatchInSource(matchInput);
                        }
                        catch (Exception e)
                        {
                            processCatch = true;
                            Console.WriteLine("Error processing AlbumInstanceKey : {0}", batchPhoto.UserAlbumInstanceKey);
                            Console.Error.WriteLine("Exception");
                            Console.Error.WriteLine(e.Message);
                            if (e.InnerException != null)
                            {
                                Console.Error.WriteLine("Inner Exception");
                                Console.Error.WriteLine(e.InnerException.ToString());
                            }

                            //fix to update error for individual record processing
                            batchPhoto.ProcessedOn = (DateTime)SqlDateTime.MinValue;
                            batchPhoto.Remarks = "Error Processing : " + e.Message;
                            batchPhoto.Processed = true;
                            _albumInstanceDetailSvc.Update(batchPhoto);
                            //----------------------------------------------------
                        }
                        finally
                        {
                            if (!processCatch)
                            {
                                if (findMatchResult != null)
                                {
                                    if (findMatchResult.Matched)
                                    {
                                        batchPhoto.FaceMatchFile = findMatchResult.MatchedFaceFile;
                                    }
                                    batchPhoto.Inliers = findMatchResult.Inliers;
                                    batchPhoto.OpenCVMethod = findMatchResult.OpenCvMethod;
                                    batchPhoto.FaceFound = findMatchResult.Matched;
                                    batchPhoto.FolderPath = findMatchResult.FolderPath;
                                    batchPhoto.AbsolutePath = findMatchResult.AbsolutePath;
                                    batchPhoto.ProcessedOn = DateTime.Now;
                                    batchPhoto.Processed = true;

                                    _albumInstanceDetailSvc.Update(batchPhoto);

                                    if (verboseShow)
                                        Console.WriteLine("Ticks-{0} : Processed successfully AlbumInstanceKey : {1}, MmeberKey : {2}, FaceToFind : {3}....", ticks.ToString(), batchPhoto.UserAlbumInstanceKey, batchPhoto.MemberKey, batchPhoto.FaceImage);

                                }
                            }
                        }

                    }
                }


            }

            static void DependencyInjection(IKernel kernel)
            {
                kernel.Bind<IDataContextAsync>().To<MemomContext>().InSingletonScope();
                kernel.Bind<IRepositoryProvider>()
                    .To<RepositoryProvider>()
                    .InSingletonScope()
                    .WithConstructorArgument(new object[] { new RepositoryFactories() });

                kernel.Bind<IUnitOfWorkAsync>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<IRepositoryAsync<Album>>().To<Repository<Album>>().InSingletonScope();
                kernel.Bind<IRepositoryAsync<UserAlbumInstance>>().To<Repository<UserAlbumInstance>>().InSingletonScope();
                kernel.Bind<IRepositoryAsync<UserAlbumInstanceDetail>>().To<Repository<UserAlbumInstanceDetail>>().InSingletonScope();
                kernel.Bind<IRepositoryAsync<UserAccount>>().To<Repository<UserAccount>>().InSingletonScope();
                kernel.Bind<IRepositoryAsync<Member>>().To<Repository<Member>>().InSingletonScope();
                kernel.Bind<IAppDbStoredProcedures>().To<MemomContext>().InSingletonScope();

                kernel.Bind<IAlbumService>().To<AlbumService>().InSingletonScope();
                kernel.Bind<IAlbumInstanceService>().To<AlbumInstanceService>().InSingletonScope();
                kernel.Bind<IAlbumInstanceDetailService>().To<AlbumInstanceDetailService>().InSingletonScope();
                kernel.Bind<IMemberService>().To<MemberService>().InSingletonScope();
                kernel.Bind<IUserService>().To<UserService>().InSingletonScope();

            }


            [STAThread]
            //static void Main()
            //{
            //    Application.EnableVisualStyles();
            //    Application.SetCompatibleTextRenderingDefault(false);
            //    long matchTime; int Inliers = 10, OutLiers;

            //    Console.WriteLine("Processing....");

            //    MatchInput matchInput = new MatchInput();
            //    matchInput.FindInFile = new FileInfo("C:\\inetpub\\wwwroot\\memomface\\UserContent\\Album\\1\\21f9c02b-5f88-4c70-b009-74f12f3c172a--IMG_0186-3.JPG");
            //    matchInput.MatchFile = new FileInfo("C:\\inetpub\\wwwroot\\memomface\\UserContent\\Member\\2\\1f99e8d4-be1d-4ca1-b094-67a45e298f65.JPG");
            //    matchInput.WebFolderPath = "C:\\inetpub\\wwwroot\\memomface\\UserContent\\Album\\1\\" + "MatchFiles";

            //    FindMatchResult findMatchResult = null;

            //    findMatchResult = BruteForceMatcher.FindMatchInSource(matchInput);
            //    Console.WriteLine("{0}, {1}, {2}", findMatchResult.Inliers, findMatchResult.Matched, findMatchResult.Outliers);
            //    Console.WriteLine("Press enter key to quit....");
            //    Console.Read();

                
            //    //using (Image<Gray, Byte> modelImage = new Image<Gray, byte>("C:\\inetpub\\wwwroot\\memomface\\UserContent\\Album\\1\\21f9c02b-5f88-4c70-b009-74f12f3c172a--IMG_0186-3.JPG"))
            //    //using (Image<Gray, Byte> observedImage = new Image<Gray, byte>("C:\\inetpub\\wwwroot\\memomface\\UserContent\\Member\\2\\1f99e8d4-be1d-4ca1-b094-67a45e298f65.JPG"))
            //    ////using (Image<Gray, Byte> modelImage = new Image<Gray, byte>("FaceBoy.jpg"))
            //    ////using (Image<Gray, Byte> observedImage = new Image<Gray, byte>("YesBoy.jpg"))
            //    ////using (Image<Gray, Byte> observedImage = new Image<Gray, byte>("NoBoy3.jpg"))
            //    //{
            //    //    //Image<Bgr, byte> result = DrawMatches.Draw(modelImage, observedImage, out matchTime);
            //    //    Image<Bgr, byte> result = BruteForceMatcher.Draw(modelImage, observedImage, out matchTime, out Inliers, out OutLiers);
            //    //    ImageViewer.Show(result, String.Format("Matched using {0} in {1} milliseconds", GpuInvoke.HasCuda ? "GPU" : "CPU", matchTime));
            //    //}
            //}

            static int Main(string[] args)
            {

                bool verboseShow = false;


                //-------------logger Setup-------------------------------------------
                FileStream ostrm = null;
                StreamWriter writer = null;
                TextWriter oldOut = Console.Out;

                //----------------------------------------------------------------------------

                // TODO Use a more robust arguments parser
                if (args.Any(arg => arg.Equals("/v") || arg.Equals("-v"))) // verbose?
                {
                    Trace.Listeners.Add(new ConsoleTraceListener(true));
                    verboseShow = true;
                }

                try
                {
                    ostrm = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "FaceBatchLog.txt", FileMode.Append, FileAccess.Write);
                    writer = new StreamWriter(ostrm);
                    writer.AutoFlush = true;
                    Console.SetOut(writer);
                    Console.SetError(writer);

                    Console.WriteLine("Starting Memom.Batch @ {0}", DateTime.Now.ToString("dd MMM yyyy hh:mm:ss"));
                    //run the main routine
                    Run(args, verboseShow);
                    Console.WriteLine("Fininsing Memom.Batch @ {0} with exit code : {1}", DateTime.Now.ToString("dd MMM yyyy hh:mm:ss"), Environment.ExitCode);
                    Environment.ExitCode = 0;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine("In main catch...");
                    Console.Error.WriteLine("Exception");
                    Console.Error.WriteLine(e.Message);
                    if (e.InnerException != null)
                    {
                        Console.Error.WriteLine("Inner Exception");
                        Console.Error.WriteLine(e.InnerException.ToString());
                    }
                    //Trace.TraceError(e.ToString());

                    Console.WriteLine("Aborting Memom.Batch @ {0} with exit code : {1}", DateTime.Now.ToString("dd MMM yyyy hh:mm:ss"), (Environment.ExitCode != 0 ? Environment.ExitCode : 100));
                    Environment.ExitCode = 100;
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Flush();
                        writer.Close();
                    }

                    if (ostrm != null)
                        ostrm.Close();

                    Console.SetOut(oldOut);
                    Console.SetError(oldOut);
                    //qp - stands for quit prompt
                    if (args.Any(arg => arg.Equals("/qp") || arg.Equals("-qp")))
                    {
                        Console.WriteLine("Press enter key to quit....");
                        Console.Read();
                    }
                    
                }
                return Environment.ExitCode;
            }
    }
}

