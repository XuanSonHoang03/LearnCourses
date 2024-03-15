using System.Drawing;

namespace LearnCourses.Service
{
    public class Captcha
    {
        public static Bitmap Generate(int w, int h, out string validate)
        {
            Bitmap bmp = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Graphics graphics = Graphics.FromImage(bmp);
            var brush = new[] { Brushes.Brown, Brushes.LawnGreen, Brushes.Blue, Brushes.Coral };
            var rand = new Random((int)DateTime.Now.Ticks);
            validate = null;
            using (Graphics g = Graphics.FromImage(bmp))
            {
                for (int i = 0; i < 4; i++)
                {
                    var text = Convert.ToChar(rand.Next(97, 122)).ToString();
                    validate += text;
                    if (i == 0) g.TranslateTransform(bmp.Width / 2, bmp.Height / 2);
                    g.RotateTransform(40 * 5 * rand.Next(1, 6));
                    var font = new Font("Arial", h * 0.25f + 8 * rand.Next(1, 6), FontStyle.Bold);
                    SizeF textSize = g.MeasureString(text, font);
                    g.DrawString(text, font, brush[i], 15 * (i + 1) - (textSize.Width / 2), -(textSize.Height / 2));
                }
            }

            return bmp;
        }
    }
}
