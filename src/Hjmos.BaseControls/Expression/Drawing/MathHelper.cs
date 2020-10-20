using System;


namespace Hjmos.BaseControls.Expression.Drawing
{
    internal static class MathHelper
    {


        public static bool IsVerySmall(double value) => Math.Abs(value) < 1E-06;

    }
}
