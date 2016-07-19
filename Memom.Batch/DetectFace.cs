//----------------------------------------------------------------------------
//  Copyright (C) 2004-2013 by EMGU. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
#if !IOS
using Emgu.CV.GPU;
#endif

namespace Memom.Batch
{
    public static class FaceDetection
   {
      public static void Detect(Image<Bgr, Byte> image, String faceFileName, String eyeFileName, List<Rectangle> faces, List<Rectangle> eyes, out long detectionTime)
      {
         Stopwatch watch;

         #if !IOS
         if (GpuInvoke.HasCuda)
         {
            using (GpuCascadeClassifier face = new GpuCascadeClassifier(faceFileName))
            using (GpuCascadeClassifier eye = new GpuCascadeClassifier(eyeFileName))
            {
               watch = Stopwatch.StartNew();
               using (GpuImage<Bgr, Byte> gpuImage = new GpuImage<Bgr, byte>(image))
               using (GpuImage<Gray, Byte> gpuGray = gpuImage.Convert<Gray, Byte>())
               {
                  Rectangle[] faceRegion = face.DetectMultiScale(gpuGray, 1.1, 10, Size.Empty);
                  faces.AddRange(faceRegion);
                  foreach (Rectangle f in faceRegion)
                  {
                     using (GpuImage<Gray, Byte> faceImg = gpuGray.GetSubRect(f))
                     {
                        //For some reason a clone is required.
                        //Might be a bug of GpuCascadeClassifier in opencv
                        using (GpuImage<Gray, Byte> clone = faceImg.Clone())
                        {
                           Rectangle[] eyeRegion = eye.DetectMultiScale(clone, 1.1, 10, Size.Empty);

                           foreach (Rectangle e in eyeRegion)
                           {
                              Rectangle eyeRect = e;
                              eyeRect.Offset(f.X, f.Y);
                              eyes.Add(eyeRect);
                           }
                        }
                     }
                  }
               }
               watch.Stop();
            }
         }
         else
         #endif
         {
            //Read the HaarCascade objects
            using (CascadeClassifier face = new CascadeClassifier(faceFileName))
            using (CascadeClassifier eye = new CascadeClassifier(eyeFileName))
            {
               watch = Stopwatch.StartNew();
               using (Image<Gray, Byte> gray = image.Convert<Gray, Byte>()) //Convert it to Grayscale
               {
                  //normalizes brightness and increases contrast of the image
                  gray._EqualizeHist();

                  //Detect the faces  from the gray scale image and store the locations as rectangle
                  //The first dimensional is the channel
                  //The second dimension is the index of the rectangle in the specific channel
                  Rectangle[] facesDetected = face.DetectMultiScale(
                     gray,
                     1.1,
                     10,
                     new Size(20, 20),
                     Size.Empty);
                  faces.AddRange(facesDetected);

                  foreach (Rectangle f in facesDetected)
                  {
                     //Set the region of interest on the faces
                     gray.ROI = f;
                     Rectangle[] eyesDetected = eye.DetectMultiScale(
                        gray,
                        1.1,
                        10,
                        new Size(20, 20),
                        Size.Empty);
                     gray.ROI = Rectangle.Empty;

                     foreach (Rectangle e in eyesDetected)
                     {
                        Rectangle eyeRect = e;
                        eyeRect.Offset(f.X, f.Y);
                        eyes.Add(eyeRect);
                     }
                  }
               }
               watch.Stop();
            }
         }
         detectionTime = watch.ElapsedMilliseconds;
      }

      public class FaceDetetctResult
      {
          public int FacesDetected
          {
              get
              {
                  return FaceImages.Count;
              }
          }
          public List<string> FaceImages { get; set; }
          public string FolderPath { get; set; }
          public string AbsFolderPath { get; set; }
          public string SourceImage { get; set; }
          public string SuperImposedImage { get; set; }
          public bool Outcome { get; set; }
          public string ErrorMsg { get; set; }
          public string ProcessedWith { get; set; }
          public long TimeTaken { get; set; }

          public string FileExtension { get; set; }

          public FaceDetetctResult()
          {
              this.Outcome = false; //error
              this.FaceImages = new List<string>();
          }

          public string SourceImagePath()
          {
              return AbsFolderPath + "\\" + SourceImage;
          }

          public string NewFaceImagePath(string FileName)
          {
              return AbsFolderPath + "\\" + FileName;
          }

          public string NewFaceName()
          {
              return Guid.NewGuid().ToString() + FileExtension;
          }

          public string GetAllFaceImageNames()
          {
              if (FaceImages.Count > 0)
                  return String.Join(",", FaceImages.ToArray());
              else
                  return null;
          }
          public string Remarks()
          {
              return "Completed face and eye detection using " + ProcessedWith + " in " + TimeTaken.ToString();
          }
      }

      public static FaceDetetctResult DetectFaceSave(string sourceFace, string AbsFolderPath, string FolderPath, string OpenCvXmlPath, string FileExtension)
      {
          long detectionTime;
          string newFace;
          FaceDetetctResult faceProcess = new FaceDetetctResult();
          faceProcess.SourceImage = sourceFace;
          faceProcess.FolderPath = FolderPath;
          faceProcess.AbsFolderPath = AbsFolderPath;
          faceProcess.FileExtension = FileExtension;
          string haarCascadeFace = OpenCvXmlPath +  "haarcascade_frontalface_default.xml";
          string haarCascadeEye = OpenCvXmlPath + "haarcascade_eye.xml";

          try
          {
              Image<Bgr, Byte> image = new Image<Bgr, byte>(faceProcess.SourceImagePath()); //Read the files as an 8-bit Bgr image  

              List<Rectangle> faces = new List<Rectangle>();
              List<Rectangle> eyes = new List<Rectangle>();
              FaceDetection.Detect(image, haarCascadeFace, haarCascadeEye, faces, eyes, out detectionTime);
              foreach (Rectangle face in faces)
              {
                  newFace = faceProcess.NewFaceName();
                  faceProcess.FaceImages.Add(newFace);
                  image.Copy(face).Save(faceProcess.NewFaceImagePath(newFace));
                  newFace = string.Empty;
                  ///do the original stuff
                  image.Draw(face, new Bgr(Color.Red), 2);
              }

              foreach (Rectangle eye in eyes)
                  image.Draw(eye, new Bgr(Color.Blue), 2);

              //save rectange drawn image - super imposed image
              newFace = string.Empty;
              newFace = faceProcess.NewFaceName();

              faceProcess.SuperImposedImage = newFace;
              image.Save(faceProcess.NewFaceImagePath(newFace));

              faceProcess.ProcessedWith = GpuInvoke.HasCuda ? "GPU" : "CPU";
              faceProcess.TimeTaken = detectionTime;

              //display the image 
              //ImageViewer.Show(image, String.Format(
              //   "Completed face and eye detection using {0} in {1} milliseconds",
              //   GpuInvoke.HasCuda ? "GPU" : "CPU",
              //   detectionTime));

              faceProcess.Outcome = true; //success
              return faceProcess;

          }
          catch (Exception ex)
          {
              faceProcess.Outcome = false; //error
              faceProcess.ErrorMsg = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
              return faceProcess;
          }

      }




   }
}
