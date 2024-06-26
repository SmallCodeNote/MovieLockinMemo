﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;

namespace MovieLockinClass
{
    static class MovieLockin
    {
        static public Mat GetModulatedImage(string filename)
        {
            List<double> frameBrightness =  GetModulation( filename);

            using (var capture = new VideoCapture(filename))
            {
                return GetModulatedImage(capture, frameBrightness);
            }
        }

        static public Mat GetModulatedImage(VideoCapture capture, List<double> frameBrightness)
        {
            Mat frame = new Mat();
            Mat sumFrame = new Mat();
            int frameCount = 0;

            while (true)
            {
                capture.Read(frame);
                if (frame.Empty())
                    break;

                Cv2.CvtColor(frame, frame, ColorConversionCodes.BGR2GRAY);
                frame.ConvertTo(frame, MatType.CV_64FC1);
                frame = frame * frameBrightness[frameCount];

                if (sumFrame.Empty())
                {
                    frame.CopyTo(sumFrame);
                }
                else
                {
                    Cv2.Add(sumFrame, frame, sumFrame);
                }
                frameCount++;
            }


            double maxValue;
            Point maxLocation;
            Cv2.MinMaxLoc(sumFrame, out _, out maxValue, out _, out maxLocation);

            sumFrame *= 255.0 / maxValue;

            sumFrame.ConvertTo(sumFrame, MatType.CV_8UC1);
            frame.Dispose();
            return sumFrame;
        }

        static public Mat GetModulationMask(string filename)
        {
            using (var capture = new VideoCapture(filename))
            {
                return GetModulationMask(capture);
            }
        }

        static public Mat GetModulationMask(VideoCapture capture)
        {
            using (Mat frame = new Mat())
            using (Mat minFrame = new Mat())
            using (Mat maxFrame = new Mat())
            {
                while (true)
                {
                    capture.Read(frame);
                    if (frame.Empty())
                        break;

                    Cv2.CvtColor(frame, frame, ColorConversionCodes.BGR2GRAY);
                    if (minFrame.Empty())
                    {
                        frame.CopyTo(minFrame); minFrame.SetTo(255);
                        frame.CopyTo(maxFrame); maxFrame.SetTo(0);
                    }
                    else
                    {
                        Cv2.Max(frame, maxFrame, maxFrame);
                        Cv2.Min(frame, minFrame, minFrame);
                    }
                }

                Mat mask = new Mat();
                Cv2.Subtract(maxFrame, minFrame, mask);

                double minVal, maxVal;
                Cv2.MinMaxLoc(mask, out minVal, out maxVal);

                Cv2.Threshold(mask, mask, maxVal / 2, 255, ThresholdTypes.Binary);

                return mask;
            }
        }

        static public List<double> GetModulation(string filename)
        {
            using (Mat mask = GetModulationMask(filename))
            {
                using (var capture = new VideoCapture(filename))
                {
                    return GetModulation(capture, mask);
                }
            }
        }

        static public List<double> GetModulation(VideoCapture capture, Mat mask)
        {
            var frameBrightness = new List<double>();
            using (Mat frame = new Mat())
            {
                // Set the brightness high and low values
                double brightnessHigh = 255.0;
                double brightnessLow = 0.0;

                while (true)
                {
                    capture.Read(frame);
                    if (frame.Empty())
                        break;

                    Cv2.CvtColor(frame, frame, ColorConversionCodes.BGR2GRAY);

                    // Apply the mask to the frame
                    Cv2.InRange(frame, new Scalar(brightnessLow), new Scalar(brightnessHigh), mask);
                    Mat maskedFrame = new Mat();
                    frame.CopyTo(maskedFrame, mask);

                    // Calculate the brightness of the masked area
                    Scalar meanBrightness = Cv2.Mean(maskedFrame);
                    frameBrightness.Add(meanBrightness[0]);
                }
            }

            // Normalize the frameBrightness list
            double avg = frameBrightness.Average();
            frameBrightness = frameBrightness.Select(x => x > avg ? 1.0 : -1.0).ToList();
            avg = frameBrightness.Average();
            frameBrightness = frameBrightness.Select(x => (x - avg)).ToList();
            double sum = frameBrightness.Sum(x => Math.Abs(x));
            frameBrightness = frameBrightness.Select(x => x / sum).ToList();

            avg = frameBrightness.Average();
            sum = frameBrightness.Sum(x => Math.Abs(x));

            return frameBrightness;

        }

    }
}
