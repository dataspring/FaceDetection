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
using SURFFeatureExample;
using Newtonsoft.Json;




namespace SURFFeatureExample
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

                IEnumerable<UserAlbumInstanceDetail> batchPhotos = _albumInstanceDetailSvc.GetPhotosForBatchProcssing();
                Console.WriteLine("About to process {0} records", batchPhotos.Count().ToString());

                if (batchPhotos.Count() > 0)
                {
                    if (verboseShow)
                        Console.WriteLine("Total records retrieved for processing : {0}", batchPhotos.Count().ToString());

                    foreach(UserAlbumInstanceDetail batchPhoto in batchPhotos)
                    {
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
                            Console.WriteLine("Error processing AlbumInstanceKey : {0}", batchPhoto.UserAlbumInstanceKey);
                            Console.Error.WriteLine("Exception");
                            Console.Error.WriteLine(e.Message);
                            Console.Error.WriteLine("Inner Exception");
                            Console.Error.WriteLine(e.InnerException.ToString());
                        }
                        finally
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


            //static void Main()
            //{
            //    Console.WriteLine("About to match FaceBoy with YesBoy");
            //   DrawMatches.testCall("FaceBoy.jpg", "YesBoy.jpg");
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

                    Console.WriteLine("Starting SURFFeatureExample @ {0}", DateTime.Now.ToString("dd MMM yyyy hh:mm:ss"));
                    //run the main routine
                    Run(args, verboseShow);
                    Console.WriteLine("Fininsing SURFFeatureExample @ {0} with exit code : {1}", DateTime.Now.ToString("dd MMM yyyy hh:mm:ss"), Environment.ExitCode);
                    return Environment.ExitCode;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine("Exception");
                    Console.Error.WriteLine(e.Message);
                    Console.Error.WriteLine("Inner Exception");
                    Console.Error.WriteLine(e.InnerException.ToString());
                    //Trace.TraceError(e.ToString());

                    Console.WriteLine("Aborting SURFFeatureExample @ {0} with exit code : {1}", DateTime.Now.ToString("dd MMM yyyy hh:mm:ss"), (Environment.ExitCode != 0 ? Environment.ExitCode : 100));
                    return Environment.ExitCode != 0
                         ? Environment.ExitCode : 100;
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
            }
    }
}

