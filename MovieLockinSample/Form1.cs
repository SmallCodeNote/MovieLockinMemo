using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MovieLockinClass;
using OpenCvSharp;

namespace MovieLockinSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_TestMovieCreate_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "MP4|*.mp4";
            sfd.FileName = "test.mp4";
            if (sfd.ShowDialog() != DialogResult.OK) return;

            var writer = new VideoWriter(sfd.FileName, FourCC.XVID, 30, new OpenCvSharp.Size(640, 480));

            // 2. 丸の描画
            double brightness = 10;
            var circleColor = new Scalar(brightness, brightness, brightness);
            var circleRadius = 20;
            var center = new OpenCvSharp.Point(320, 240); // 画像の中央

            using (var frame = new Mat(new OpenCvSharp.Size(640, 480), MatType.CV_8UC3))
            using (var frame2 = new Mat(new OpenCvSharp.Size(640, 480), MatType.CV_8UC3))
            {
                frame2.SetTo(Scalar.Black); // 背景
                Cv2.Circle(frame2, center, circleRadius, circleColor, -1, LineTypes.AntiAlias, 0);

                int j = 0;
                for (int i = 0; i < 300; i++) // 10秒分のフレーム
                {
                    frame.SetTo(Scalar.DarkGray); // 背景
                    frame.Line(0, 0, frame.Width, frame.Height, Scalar.Gray, 20);

                    // 丸を描画
                    if (j >= 10)
                    {
                        Cv2.AddWeighted(frame, 1, frame2, 1, 0, frame);
                    }

                    if (j >= 20) j = 0;

                    j++;
                    // フレームをMP4ファイルに追加
                    writer.Write(frame);
                }
            }

            writer.Release();
        }

        private void button_TestProcess_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "MP4|*.mp4";
            if (ofd.ShowDialog() != DialogResult.OK) return;

            using (Mat m = MovieLockin.GetModulatedImage(ofd.FileName))
            {

                if (pictureBox1.Image != null) pictureBox1.Image.Dispose();

                pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(m);

            }
        }
    }
}
