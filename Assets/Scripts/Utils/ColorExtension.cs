using UnityEngine;
using System.Collections;
using System;

namespace Utils
{

    public static class ColorExtension
    {
        //Converts an RGB color to an HSV color.
        public static Vector3 ConvertRgbToHsv(double r, double b, double g)
        {
            double delta, min;
            double h = 0, s, v;


            min = Math.Min(Math.Min(r, g), b);
            v = Math.Max(Math.Max(r, g), b);
            delta = v - min;

            if (v.Equals(0))
                s = 0;
            else
                s = delta / v;

            if (s.Equals(0))
                h = 360;
            else
            {
                if (r.Equals(v))
                    h = (g - b) / delta;
                else if (g.Equals(v))
                    h = 2 + (b - r) / delta;
                else if (b.Equals(v))
                    h = 4 + (r - g) / delta;

                h *= 60;
                if (h <= 0.0)
                    h += 360;
            }

            Vector3 hsvColor = new Vector3();
            hsvColor.x = (float)(360 - h);
            hsvColor.y = (float)s;
            hsvColor.z = (float)(v / 255);

            return hsvColor;

        }

        /// <summary>
        /// Converts an HSV color to an RGB color. 
        /// </summary>
        /// <param name="h">0~360</param>
        /// <param name="s">0~1</param>
        /// <param name="v">0~1</param>
        /// <param name="alpha">0~1</param>
        public static Color ConvertHsvToRgb(double h, double s, double v, float alpha)
        {
            double r, g, b;

            if (s.Equals(0))
            {
                r = v;
                g = v;
                b = v;
            }

            else
            {
                int i;
                double f, p, q, t;


                if (h.Equals(360))
                    h = 0;
                else
                    h = h / 60;

                i = (int)(h);
                f = h - i;

                p = v * (1.0 - s);
                q = v * (1.0 - (s * f));
                t = v * (1.0 - (s * (1.0f - f)));


                switch (i)
                {
                    case 0:
                        r = v;
                        g = t;
                        b = p;
                        break;

                    case 1:
                        r = q;
                        g = v;
                        b = p;
                        break;

                    case 2:
                        r = p;
                        g = v;
                        b = t;
                        break;

                    case 3:
                        r = p;
                        g = q;
                        b = v;
                        break;

                    case 4:
                        r = t;
                        g = p;
                        b = v;
                        break;

                    default:
                        r = v;
                        g = p;
                        b = q;
                        break;
                }

            }

            return new Color((float)r, (float)g, (float)b, alpha);

        }
    }
}