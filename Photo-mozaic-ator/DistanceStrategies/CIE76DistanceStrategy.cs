﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Photo_mozaic_ator.DistanceStrategies
{
    class CIE76DistanceStrategy : IDistanceStrategy
    {
        // CIE76 color distance
        // rgb -> XYZ -> Lab
        // compare 2 Lab colors with eucleideian norm

        public int Distance(Color a, Color b)
        {
            var AinLAB = rgb2lab(a.R, a.G,a.B);
            var BinLAB = rgb2lab(b.R, b.G,b.B);


            var redDifference = AinLAB[0] - BinLAB[0];
            var greenDifference = AinLAB[1] - BinLAB[1];
            var blueDifference = AinLAB[2] - BinLAB[2];

            // x1000 is for keeping accuracy
            return (int)((redDifference * redDifference + greenDifference * greenDifference + blueDifference * blueDifference)*1000);
        }


        static float Gamma(float x)
        {
            return x > 0.04045f ? MathF.Pow((x + 0.055f) / 1.055f, 2.4f) : x / 12.92f;
        }

        public static float[] rgb2lab(float var_R, float var_G, float var_B)
        {

            float[] arr = new float[3];
            float B = Gamma(var_B);
            float G = Gamma(var_G);
            float R = Gamma(var_R);
            float X = 0.412453f * R + 0.357580f * G + 0.180423f * B;
            float Y = 0.212671f * R + 0.715160f * G + 0.072169f * B;
            float Z = 0.019334f * R + 0.119193f * G + 0.950227f * B;

            X /= 0.95047f;
            Y /= 1.0f;
            Z /= 1.08883f;

            float FX = X > 0.008856f ? MathF.Pow(X, 1.0f / 3.0f) : (7.787f * X + 0.137931f);
            float FY = Y > 0.008856f ? MathF.Pow(Y, 1.0f / 3.0f) : (7.787f * Y + 0.137931f);
            float FZ = Z > 0.008856f ? MathF.Pow(Z, 1.0f / 3.0f) : (7.787f * Z + 0.137931f);
            arr[0] = Y > 0.008856f ? (116.0f * FY - 16.0f) : (903.3f * Y);
            arr[1] = 500f * (FX - FY);
            arr[2] = 200f * (FY - FZ);
            return arr;

        }
    }
}
