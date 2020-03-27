using System;
using System.Drawing;

namespace bayes
{
    static class Filter
    {
        private static Bitmap Gauss(Bitmap btm)
        {
            Bitmap btmf = new Bitmap(btm.Width, btm.Height);

            double[][] kernel = new double[3][];
            kernel[0] = new double[] { -1, -1, -1 };
            kernel[1] = new double[] { -1, 8, -1 };
            kernel[2] = new double[] { -1, -1, -1 };

            for (int i = 0; i < btm.Width; i++)
            {
                for (int j = 0; j < btm.Height; j++)
                {
                    double R = 0;
                    double G = 0;
                    double B = 0;
                    for (int k = -1; k < kernel.Length - 1; k++)
                    {
                        for (int n = -1; n < kernel[0].Length - 1; n++)
                        {
                            if (i - k < 0 || j - n < 0 || i - k >= btm.Width || j - n >= btm.Height)
                            {
                                R -= 255;
                                G -= 255;
                                B -= 255;
                            }
                            else
                            {
                                Color pxl = btm.GetPixel(i - k, j - n);
                                R += kernel[k + 1][n + 1] * pxl.R;
                                G += kernel[k + 1][n + 1] * pxl.G;
                                B += kernel[k + 1][n + 1] * pxl.B;
                            }
                        }
                    }

                    if (R < 0)
                        R = 0;
                    else if (R > 225)
                        R = 255;

                    if (G < 0)
                        G = 0;
                    else if (G > 225)
                        G = 255;

                    if (B < 0)
                        B = 0;
                    else if (B > 225)
                        B = 255;

                    btmf.SetPixel(i, j, Color.FromArgb((int)R, (int)G, (int)B));
                }
            }
            return btmf;
        }

        public static void MyFilter(string path)
        {
            Console.WriteLine("Please wait...");
            Bitmap original = new Bitmap(path);
            Bitmap btm = Gauss(original);

            for (int i = 4; i < btm.Width - 4; i += 5)
            {
                for (int j = 4; j < btm.Height - 4; j += 5)
                {
                    double PointBrightness = 0;
                    double NeighbourBrightness = 0;

                    for (int k = -13; k < 14; k++)
                    {
                        for (int n = -13; n < 14; n++)
                        {
                            if (i - k < 0 || j - n < 0 || i - k >= btm.Width || j - n >= btm.Height)
                            {
                                NeighbourBrightness += 0;
                            }
                            else
                            {
                                Color pxl = btm.GetPixel(i - k, j - n);
                                NeighbourBrightness += pxl.GetBrightness();
                            }
                        }
                    }
                    for (int k = -4; k < 5; k++)
                    {
                        for (int n = -4; n < 5; n++)
                        {
                            if (i - k < 0 || j - n < 0 || i - k >= btm.Width || j - n >= btm.Height)
                            {
                                PointBrightness += 0;
                                NeighbourBrightness -= 0;
                            }
                            else
                            {
                                Color pxl = btm.GetPixel(i - k, j - n);
                                PointBrightness += pxl.GetBrightness();
                                NeighbourBrightness -= pxl.GetBrightness();
                            }
                        }
                    }
                    PointBrightness /= 81;
                    NeighbourBrightness /= 648;

                    if (PointBrightness - NeighbourBrightness > 0.05 )
                    {
                        for (int k = -4; k < 5; k++)
                        {
                            original.SetPixel(i - 4, j - k, Color.Blue);
                            original.SetPixel(i + 4, j - k, Color.Blue);
                            original.SetPixel(i - k, j - 4, Color.Blue);
                            original.SetPixel(i - k, j + 4, Color.Blue);
                        }
                    }
                }
            }
            original.Save("after.jpg");
            Console.WriteLine("Done!");
        }
    }
}
