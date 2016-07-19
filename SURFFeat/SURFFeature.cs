//----------------------------------------------------------------------------
//  Copyright (C) 2004-2013 by EMGU. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SURFFeatureExample
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         long matchTime; int Inliers = 10, OutLiers;
         string mImage = "C:\\inetpub\\wwwroot\\memomface\\UserContent\\Member\\2\\1f99e8d4-be1d-4ca1-b094-67a45e298f65.JPG";
         string oImage = "C:\\inetpub\\wwwroot\\memomface\\UserContent\\Album\\1\\45d2c5b1-2079-408a-af48-e60edad56f5f--IMG_0185-3.JPG";

         //using(Image<Gray, Byte> modelImage = new Image<Gray, byte>("FaceBoy.jpg"))
         //using (Image<Gray, Byte> observedImage = new Image<Gray, byte>("YesBoy.jpg"))
         using (Image<Gray, Byte> modelImage = new Image<Gray, byte>(mImage))
         using (Image<Gray, Byte> observedImage = new Image<Gray, byte>(oImage))
         {
            //Image<Bgr, byte> result = DrawMatches.Draw(modelImage, observedImage, out matchTime);
            Image<Bgr, byte> result = BruteForceMatcher.Draw(modelImage, observedImage, out matchTime, out Inliers, out OutLiers);
            ImageViewer.Show(result, String.Format("Matched using {0} in {1} milliseconds", GpuInvoke.HasCuda ? "GPU" : "CPU", matchTime));
         }
      }
   }
}
