﻿#region MIT.
// 
// Gorgon.
// Copyright (C) 2012 Michael Winsor
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
// Created: Friday, June 15, 2012 9:55:35 AM
// 
#endregion

using System;

namespace GorgonLibrary.Math
{
	/// <summary>
	/// Extensions for mathematical operations on single floating point values.
	/// </summary>
	public static class GorgonMathExtensions
	{
		#region Constants.
		private const float DegConvert = ((float)System.Math.PI / 180.0f);		// Constant containing the value used to convert degrees to radians.
		private const float RadConvert = (180.0f / (float)System.Math.PI);		// Constant containing the value used to convert radians to degrees.

		/// <summary>
		/// PI
		/// </summary>
		public const float PI = 3.141593f;
		#endregion

		#region Methods.
		/// <summary>
		/// Function to return the maximum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The larger of the two values.</returns>
		public static byte Max(this byte value1, byte value2)
		{
			return (value1 > value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the minimum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The smaller of the two values.</returns>
		public static byte Min(this byte value1, byte value2)
		{
			return (value1 < value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the maximum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The larger of the two values.</returns>
		public static ushort Max(this ushort value1, ushort value2)
		{
			return (value1 > value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the minimum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The smaller of the two values.</returns>
		public static ushort Min(this ushort value1, ushort value2)
		{
			return (value1 < value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the maximum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The larger of the two values.</returns>
		public static short Max(this short value1, short value2)
		{
			return (value1 > value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the minimum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The smaller of the two values.</returns>
		public static short Min(this short value1, short value2)
		{
			return (value1 < value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the maximum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The larger of the two values.</returns>
		public static uint Max(this uint value1, uint value2)
		{
			return (value1 > value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the minimum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The smaller of the two values.</returns>
		public static uint Min(this uint value1, uint value2)
		{
			return (value1 < value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the maximum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The larger of the two values.</returns>
		public static int Max(this int value1, int value2)
		{
			return (value1 > value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the minimum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The smaller of the two values.</returns>
		public static int Min(this int value1, int value2)
		{
			return (value1 < value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the maximum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The larger of the two values.</returns>
		public static ulong Max(this ulong value1, ulong value2)
		{
			return (value1 > value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the minimum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The smaller of the two values.</returns>
		public static ulong Min(this ulong value1, ulong value2)
		{
			return (value1 < value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the maximum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The larger of the two values.</returns>
		public static long Max(this long value1, long value2)
		{
			return (value1 > value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the minimum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The smaller of the two values.</returns>
		public static long Min(this long value1, long value2)
		{
			return (value1 < value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the maximum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The larger of the two values.</returns>
		public static float Max(this float value1, float value2)
		{			
			return (value1 > value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the minimum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The smaller of the two values.</returns>
		public static float Min(this float value1, float value2)
		{
			return (value1 < value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the maximum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The larger of the two values.</returns>
		public static double Max(this double value1, double value2)
		{
			return (value1 > value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the minimum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The smaller of the two values.</returns>
		public static double Min(this double value1, double value2)
		{
			return (value1 < value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the maximum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The larger of the two values.</returns>
		public static decimal Max(this decimal value1, decimal value2)
		{
			return (value1 > value2) ? value1 : value2;
		}

		/// <summary>
		/// Function to return the minimum value between this value and another value.
		/// </summary>
		/// <param name="value1">This value to test.</param>
		/// <param name="value2">The secondary value to test.</param>
		/// <returns>The smaller of the two values.</returns>
		public static decimal Min(this decimal value1, decimal value2)
		{
			return (value1 < value2) ? value1 : value2;
		}
		
		/// <summary>
		/// Function to return the absolute value of a floating point value.
		/// </summary>
		/// <param name="value">Value to evaluate.</param>
		/// <returns>The absolute value of <paramref name="value"/>.</returns>
		public static short Abs(this short value)
		{
			return System.Math.Abs(value);
		}

		/// <summary>
		/// Function to return the absolute value of a floating point value.
		/// </summary>
		/// <param name="value">Value to evaluate.</param>
		/// <returns>The absolute value of <paramref name="value"/>.</returns>
		public static int Abs(this int value)
		{
			return System.Math.Abs(value);
		}

		/// <summary>
		/// Function to return the absolute value of a floating point value.
		/// </summary>
		/// <param name="value">Value to evaluate.</param>
		/// <returns>The absolute value of <paramref name="value"/>.</returns>
		public static long Abs(this long value)
		{
			return System.Math.Abs(value);
		}

		/// <summary>
		/// Function to return the absolute value of a floating point value.
		/// </summary>
		/// <param name="value">Value to evaluate.</param>
		/// <returns>The absolute value of <paramref name="value"/>.</returns>
		public static double Abs(this double value)
		{
			return System.Math.Abs(value);
		}

		/// <summary>
		/// Function to return the absolute value of a floating point value.
		/// </summary>
		/// <param name="value">Value to evaluate.</param>
		/// <returns>The absolute value of <paramref name="value"/>.</returns>
		public static decimal Abs(this decimal value)
		{
			return System.Math.Abs(value);
		}

		/// <summary>
		/// Function to return the absolute value of a floating point value.
		/// </summary>
		/// <param name="value">Value to evaluate.</param>
		/// <returns>The absolute value of <paramref name="value"/>.</returns>
		public static float Abs(this float value)
		{
			return System.Math.Abs(value);
		}

		/// <summary>
		/// Function to round a floating point value.
		/// </summary>
		/// <param name="value">Value to round.</param>
		/// <returns>The rounded floating point value.</returns>
		/// <remarks>This uses MidpointRounding.ToEven (Bankers rounding) as the default for consistency with the System.Math.Round method.</remarks>
		public static float Round(this float value)
		{
			return Round(value, 0, MidpointRounding.ToEven);
		}

		/// <summary>
		/// Function to round a floating point value.
		/// </summary>
		/// <param name="value">Value to round.</param>
		/// <param name="decimalCount">Number of decimal places to return.</param>
		/// <returns>The rounded floating point value.</returns>
		/// <remarks>This uses MidpointRounding.ToEven (Bankers rounding) as the default for consistency with the System.Math.Round method.</remarks>
		public static float Round(this float value, int decimalCount)
		{
			return Round(value, decimalCount, MidpointRounding.ToEven);
		}

		/// <summary>
		/// Function to round a floating point value.
		/// </summary>
		/// <param name="value">Value to round.</param>
		/// <param name="decimalCount">Number of decimal places to return.</param>
		/// <param name="rounding">Determines how to round mid point numbers.</param>
		/// <returns>The rounded floating point value.</returns>
		public static float Round(this float value, int decimalCount, MidpointRounding rounding)
		{
			return (float)System.Math.Round(value, decimalCount, rounding);
		}

		/// <summary>
		/// Function to round a floating point value.
		/// </summary>
		/// <param name="value">Value to round.</param>
		/// <returns>The rounded floating point value.</returns>
		/// <remarks>This uses MidpointRounding.ToEven (Bankers rounding) as the default for consistency with the System.Math.Round method.</remarks>
		public static decimal Round(this decimal value)
		{
			return Round(value, 0, MidpointRounding.ToEven);
		}

		/// <summary>
		/// Function to round a floating point value.
		/// </summary>
		/// <param name="value">Value to round.</param>
		/// <param name="decimalCount">Number of decimal places to return.</param>
		/// <returns>The rounded floating point value.</returns>
		/// <remarks>This uses MidpointRounding.ToEven (Bankers rounding) as the default for consistency with the System.Math.Round method.</remarks>
		public static decimal Round(this decimal value, int decimalCount)
		{
			return Round(value, decimalCount, MidpointRounding.ToEven);
		}

		/// <summary>
		/// Function to round a floating point value.
		/// </summary>
		/// <param name="value">Value to round.</param>
		/// <param name="decimalCount">Number of decimal places to return.</param>
		/// <param name="rounding">Determines how to round mid point numbers.</param>
		/// <returns>The rounded floating point value.</returns>
		public static decimal Round(this decimal value, int decimalCount, MidpointRounding rounding)
		{
			return System.Math.Round(value, decimalCount, rounding);
		}

		/// <summary>
		/// Function to round a floating point value.
		/// </summary>
		/// <param name="value">Value to round.</param>
		/// <returns>The rounded floating point value.</returns>
		/// <remarks>This uses MidpointRounding.ToEven (Bankers rounding) as the default for consistency with the System.Math.Round method.</remarks>
		public static double Round(this double value)
		{
			return Round(value, 0, MidpointRounding.ToEven);
		}

		/// <summary>
		/// Function to round a floating point value.
		/// </summary>
		/// <param name="value">Value to round.</param>
		/// <param name="decimalCount">Number of decimal places to return.</param>
		/// <returns>The rounded floating point value.</returns>
		/// <remarks>This uses MidpointRounding.ToEven (Bankers rounding) as the default for consistency with the System.Math.Round method.</remarks>
		public static double Round(this double value, int decimalCount)
		{
			return Round(value, decimalCount, MidpointRounding.ToEven);
		}

		/// <summary>
		/// Function to round a floating point value.
		/// </summary>
		/// <param name="value">Value to round.</param>
		/// <param name="decimalCount">Number of decimal places to return.</param>
		/// <param name="rounding">Determines how to round mid point numbers.</param>
		/// <returns>The rounded floating point value.</returns>
		public static double Round(this double value, int decimalCount, MidpointRounding rounding)
		{
			return System.Math.Round(value, decimalCount, rounding);
		}

		/// <summary>
		/// Function to convert this radian value into degrees.
		/// </summary>
		/// <param name="radians">Radian value to convert.</param>
		/// <returns>The angle in degrees.</returns>
		public static float Degrees(this float radians)
		{
			return radians * RadConvert;
		}

		/// <summary>
		/// Function to convert this degree angle value into radians.
		/// </summary>
		/// <param name="degrees">Degree angle value to convert.</param>
		/// <returns>The angle in radians.</returns>
		public static float Radians(this float degrees)
		{
			return degrees * DegConvert;
		}

		/// <summary>
		/// Function to convert this radian value into degrees.
		/// </summary>
		/// <param name="radians">Radian value to convert.</param>
		/// <returns>The angle in degrees.</returns>
		public static decimal Degrees(this decimal radians)
		{
			return radians * (decimal)RadConvert;
		}

		/// <summary>
		/// Function to convert this degree angle value into radians.
		/// </summary>
		/// <param name="degrees">Degree angle value to convert.</param>
		/// <returns>The angle in radians.</returns>
		public static decimal Radians(this decimal degrees)
		{
			return degrees * (decimal)DegConvert;
		}


		/// <summary>
		/// Function to convert this radian value into degrees.
		/// </summary>
		/// <param name="radians">Radian value to convert.</param>
		/// <returns>The angle in degrees.</returns>
		public static double Degrees(this double radians)
		{
			return radians * RadConvert;
		}

		/// <summary>
		/// Function to convert this degree angle value into radians.
		/// </summary>
		/// <param name="degrees">Degree angle value to convert.</param>
		/// <returns>The angle in radians.</returns>
		public static double Radians(this double degrees)
		{
			return degrees * DegConvert;
		}

		/// <summary>
		/// Function to determine if this floating point value is equal to another within a given delta range.
		/// </summary>
		/// <param name="left">Left value to compare.</param>
		/// <param name="right">Right value to compare.</param>
		/// <returns>TRUE if equal, FALSE if not.</returns>
		public static bool EqualsEpsilon(this float left, float right)
		{
			return Abs(right - left) <= 1e-06f;
		}

		/// <summary>
		/// Function to determine if this floating point value is equal to another within a given delta range.
		/// </summary>
		/// <param name="left">Left value to compare.</param>
		/// <param name="right">Right value to compare.</param>
		/// <param name="delta">Delta to account for error between the two values.</param>
		/// <returns>TRUE if equal, FALSE if not.</returns>
		public static bool EqualsEpsilon(this float left, float right, float delta)
		{
			return Abs(right - left) <= delta;
		}

        /// <summary>
        /// Function to determine if this double floating point value is equal to another within a given delta range.
        /// </summary>
        /// <param name="left">Left value to compare.</param>
        /// <param name="right">Right value to compare.</param>
        /// <returns>TRUE if equal, FALSE if not.</returns>
        public static bool EqualsEpsilon(this double left, double right)
        {
            return Abs(right - left) <= 1e-06f;
        }

        /// <summary>
        /// Function to determine if this double floating point value is equal to another within a given delta range.
        /// </summary>
        /// <param name="left">Left value to compare.</param>
        /// <param name="right">Right value to compare.</param>
        /// <param name="delta">Delta to account for error between the two values.</param>
        /// <returns>TRUE if equal, FALSE if not.</returns>
        public static bool EqualsEpsilon(this double left, double right, double delta)
        {
            return Abs(right - left) <= delta;
        }

        /// <summary>
		/// Function to return the square root of this floating point value.
		/// </summary>
		/// <param name="value">Value to get the square root of.</param>
		/// <returns>The square root of the value.</returns>
		public static double InverseSqrt(this double value)
		{
			return 1.0 / System.Math.Sqrt(value);
		}

		/// <summary>
		/// Function to return the square root of this floating point value.
		/// </summary>
		/// <param name="value">Value to get the square root of.</param>
		/// <returns>The square root of the value.</returns>
		public static float InverseSqrt(this float value)
		{
			return 1.0f / (float)System.Math.Sqrt(value);
		}

		/// <summary>
		/// Function to return the square root of this floating point value.
		/// </summary>
		/// <param name="value">Value to get the square root of.</param>
		/// <returns>The square root of the value.</returns>
		public static double Sqrt(this double value)
		{
			return System.Math.Sqrt(value);
		}

		/// <summary>
		/// Function to return the square root of this floating point value.
		/// </summary>
		/// <param name="value">Value to get the square root of.</param>
		/// <returns>The square root of the value.</returns>
		public static float Sqrt(this float value)
		{
			return (float)System.Math.Sqrt(value);
		}

		/// <summary>
		/// Function to return the sin of an angle, in radians.
		/// </summary>
		/// <param name="angle">Angle, in radians, to retrieve the sin value from.</param>
		/// <returns>The sin value of the angle.</returns>
		public static float Sin(this float angle)
		{
			return (float)System.Math.Sin(angle);
		}

		/// <summary>
		/// Function to return the cosine of an angle, in radians.
		/// </summary>
		/// <param name="angle">Angle, in radians, to retrieve the cosine value from.</param>
		/// <returns>The cosine value of the angle.</returns>
		public static float Cos(this float angle)
		{
			return (float)System.Math.Cos(angle);
		}

		/// <summary>
		/// Function to return the tangent of an angle, in radians.
		/// </summary>
		/// <param name="angle">Angle, in radians, to retrieve the tangent from.</param>
		/// <returns></returns>
		public static float Tan(this float angle)
		{
			return (float)System.Math.Tan(angle);
		}
				
		/// <summary>
		/// Function to return the arc sin of an angle, in radians.
		/// </summary>
		/// <param name="angle">Angle, in radians, to retrieve the arc sin value from.</param>
		/// <returns>The sin value of the angle.</returns>
		public static float ASin(this float angle)
		{
			return (float)System.Math.Asin(angle);
		}

		/// <summary>
		/// Function to return the arccosine of an angle, in radians.
		/// </summary>
		/// <param name="angle">Angle,in radians, to retrieve the arc cosine value from.</param>
		/// <returns>The cosine value of the angle.</returns>
		public static float ACos(this float angle)
		{
			return (float)System.Math.Acos(angle);
		}

		/// <summary>
		/// Function to return the arc tangent of an angle, in radians.
		/// </summary>
		/// <param name="angle">Angle,in radians, to retrieve the arc tangent value from.</param>
		/// <returns>The arc tangent of the angle.</returns>
		public static float ATan(this float angle)
		{
			return (float)System.Math.Atan(angle);
		}

		/// <summary>
		/// Function to return the arc tangent of a slope.
		/// </summary>
		/// <param name="y">Vertical slope value to retrieve the arc tangent from.</param>
		/// <param name="x">Horizontal slope value to retrieve the arc tangent from.</param>
		/// <returns>The arc tangent of the slope.</returns>
		public static float ATan(this float y, float x)
		{
			return (float)System.Math.Atan2(y, x);
		}

		/// <summary>
		/// Function to return the sin of an angle, in radians.
		/// </summary>
		/// <param name="angle">Angle, in radians, to retrieve the sin value from.</param>
		/// <returns>The sin value of the angle.</returns>
		public static decimal Sin(this decimal angle)
		{
			return (decimal)System.Math.Sin((double)angle);
		}

		/// <summary>
		/// Function to return the cosine of an angle, in radians.
		/// </summary>
		/// <param name="angle">Angle, in radians, to retrieve the cosine value from.</param>
		/// <returns>The cosine value of the angle.</returns>
		public static decimal Cos(this decimal angle)
		{
			return (decimal)System.Math.Cos((double)angle);
		}

		/// <summary>
		/// Function to return the tangent of an angle, in radians.
		/// </summary>
		/// <param name="angle">Angle, in radians, to retrieve the tangent from.</param>
		/// <returns></returns>
		public static decimal Tan(this decimal angle)
		{
			return (decimal)System.Math.Tan((double)angle);
		}
				
		/// <summary>
		/// Function to return the arc sin of an angle, in radians.
		/// </summary>
		/// <param name="angle">Angle, in radians, to retrieve the arc sin value from.</param>
		/// <returns>The sin value of the angle.</returns>
		public static decimal ASin(this decimal angle)
		{
			return (decimal)System.Math.Asin((double)angle);
		}

		/// <summary>
		/// Function to return the arccosine of an angle, in radians.
		/// </summary>
		/// <param name="angle">Angle,in radians, to retrieve the arc cosine value from.</param>
		/// <returns>The cosine value of the angle.</returns>
		public static decimal ACos(this decimal angle)
		{
			return (decimal)System.Math.Acos((double)angle);
		}

		/// <summary>
		/// Function to return the arc tangent of an angle, in radians.
		/// </summary>
		/// <param name="angle">Angle,in radians, to retrieve the arc tangent value from.</param>
		/// <returns>The arc tangent of the angle.</returns>
		public static decimal ATan(this decimal angle)
		{
			return (decimal)System.Math.Atan((double)angle);
		}

		/// <summary>
		/// Function to return the arc tangent of a slope.
		/// </summary>
		/// <param name="y">Vertical slope value to retrieve the arc tangent from.</param>
		/// <param name="x">Horizontal slope value to retrieve the arc tangent from.</param>
		/// <returns>The arc tangent of the slope.</returns>
		public static decimal ATan(this decimal y, decimal x)
		{
			return (decimal)System.Math.Atan2((double)y, (double)x);
		}

		/// <summary>
		/// Function to return the sin of an angle, in radians.
		/// </summary>
		/// <param name="angle">Angle, in radians, to retrieve the sin value from.</param>
		/// <returns>The sin value of the angle.</returns>
		public static double Sin(this double angle)
		{
			return System.Math.Sin(angle);
		}

		/// <summary>
		/// Function to return the cosine of an angle, in radians.
		/// </summary>
		/// <param name="angle">Angle, in radians, to retrieve the cosine value from.</param>
		/// <returns>The cosine value of the angle.</returns>
		public static double Cos(this double angle)
		{
			return System.Math.Cos(angle);
		}

		/// <summary>
		/// Function to return the tangent of an angle, in radians.
		/// </summary>
		/// <param name="angle">Angle, in radians, to retrieve the tangent from.</param>
		/// <returns></returns>
		public static double Tan(this double angle)
		{
			return System.Math.Tan(angle);
		}
				
		/// <summary>
		/// Function to return the arc sin of an angle, in radians.
		/// </summary>
		/// <param name="angle">Angle, in radians, to retrieve the arc sin value from.</param>
		/// <returns>The sin value of the angle.</returns>
		public static double ASin(this double angle)
		{
			return System.Math.Asin(angle);
		}

		/// <summary>
		/// Function to return the arccosine of an angle, in radians.
		/// </summary>
		/// <param name="angle">Angle,in radians, to retrieve the arc cosine value from.</param>
		/// <returns>The cosine value of the angle.</returns>
		public static double ACos(this double angle)
		{
			return System.Math.Acos(angle);
		}

		/// <summary>
		/// Function to return the arc tangent of an angle, in radians.
		/// </summary>
		/// <param name="angle">Angle,in radians, to retrieve the arc tangent value from.</param>
		/// <returns>The arc tangent of the angle.</returns>
		public static double ATan(this double angle)
		{
			return System.Math.Atan(angle);
		}

		/// <summary>
		/// Function to return the arc tangent of a slope.
		/// </summary>
		/// <param name="y">Vertical slope value to retrieve the arc tangent from.</param>
		/// <param name="x">Horizontal slope value to retrieve the arc tangent from.</param>
		/// <returns>The arc tangent of the slope.</returns>
		public static double ATan(this double y, double x)
		{
			return System.Math.Atan2(y, x);
		}

		/// <summary>
		/// Function to take e raised to the power passed in.
		/// </summary>
		/// <param name="power">Value to take e from.</param>
		/// <returns><c>e</c> raised to the power specified.</returns>
		public static double Exp(this double power)
		{
			return System.Math.Exp(power);
		}

		/// <summary>
		/// Function to raise a value to a specified power.
		/// </summary>
		/// <param name="value">Value to raise.</param>
		/// <param name="power">Power to raise up to.</param>
		/// <returns>The value raised to the power.</returns>
		public static double Pow(this double value, double power)
		{
			return System.Math.Pow(value, power);
		}

		/// <summary>
		/// Function to take e raised to the power passed in.
		/// </summary>
		/// <param name="power">Value to take e from.</param>
		/// <returns><c>e</c> raised to the power specified.</returns>
		public static decimal Exp(this decimal power)
		{
			return (decimal)System.Math.Exp((double)power);
		}

		/// <summary>
		/// Function to raise a value to a specified power.
		/// </summary>
		/// <param name="value">Value to raise.</param>
		/// <param name="power">Power to raise up to.</param>
		/// <returns>The value raised to the power.</returns>
		public static decimal Pow(this decimal value, decimal power)
		{
			return (decimal)System.Math.Pow((double)value, (double)power);
		}

		/// <summary>
		/// Function to take e raised to the power passed in.
		/// </summary>
		/// <param name="power">Value to take e from.</param>
		/// <returns><c>e</c> raised to the power specified.</returns>
		public static float Exp(this float power)
		{
			return (float)System.Math.Exp(power);
		}

		/// <summary>
		/// Function to raise a value to a specified power.
		/// </summary>
		/// <param name="value">Value to raise.</param>
		/// <param name="power">Power to raise up to.</param>
		/// <returns>The value raised to the power.</returns>
		public static float Pow(this float value, float power)
		{
			return (float)System.Math.Pow(value, power);
		}

        /// <summary>
        /// Function to perform a fast floor operation on a floating point value.
        /// </summary>
        /// <param name="value">Floating point value to floor.</param>
        /// <returns>The floored value.</returns>
        public static float FastFloor(this float value)
        {
	        var result = (int)value;

            return (value < result) ? result - 1 : result;
        }

        /// <summary>
        /// Function to perform a fast ceiling operation on a floating point value.
        /// </summary>
        /// <param name="value">Floating point value to ceiling.</param>
        /// <returns>The ceiling value.</returns>
        public static float FastCeiling(this float value)
        {
	        var result = (int)value;

	        return (value > result) ? result + 1 : result;
        }

		/// <summary>
		/// Function to return the sign of an int32 value.
		/// </summary>
		/// <param name="value">Value to evaluate.</param>
		/// <returns>0 if the value is 0, -1 if the value is less than 0, and 1 if the value is greater than 0.</returns>
		public static int Sign(this int value)
		{
			if (value == 0)
			{
				return 0;
			}

			return value < 0 ? -1 : 1;
		}

		/// <summary>
		/// Function to return the sign of an int64 value.
		/// </summary>
		/// <param name="value">Value to evaluate.</param>
		/// <returns>0 if the value is 0, -1 if the value is less than 0, and 1 if the value is greater than 0.</returns>
		public static int Sign(this long value)
		{
			if (value == 0)
			{
				return 0;
			}

			return value < 0 ? -1 : 1;
		}

		/// <summary>
		/// Function to return the sign of an signed byte value.
		/// </summary>
		/// <param name="value">Value to evaluate.</param>
		/// <returns>0 if the value is 0, -1 if the value is less than 0, and 1 if the value is greater than 0.</returns>
		public static int Sign(this sbyte value)
		{
			if (value == 0)
			{
				return 0;
			}

			return value < 0 ? -1 : 1;
		}

		/// <summary>
		/// Function to return the sign of an int16 value.
		/// </summary>
		/// <param name="value">Value to evaluate.</param>
		/// <returns>0 if the value is 0, -1 if the value is less than 0, and 1 if the value is greater than 0.</returns>
		public static int Sign(this short value)
		{
			if (value == 0)
			{
				return 0;
			}

			return value < 0 ? -1 : 1;
		}

		/// <summary>
		/// Function to return the sign of a decimal value.
		/// </summary>
		/// <param name="value">Value to evaluate.</param>
		/// <returns>0 if the value is 0, -1 if the value is less than 0, and 1 if the value is greater than 0.</returns>
		public static int Sign(this decimal value)
		{
			if (value == 0)
			{
				return 0;
			}

			return value < 0 ? -1 : 1;
		}

		/// <summary>
		/// Function to return the sign of a single floating point value.
		/// </summary>
		/// <param name="value">Value to evaluate.</param>
		/// <returns>0 if the value is 0, -1 if the value is less than 0, and 1 if the value is greater than 0.</returns>
		public static int Sign(this float value)
		{
			if (value.EqualsEpsilon(0))
			{
				return 0;
			}

			return value < 0 ? -1 : 1;
		}

		/// <summary>
		/// Function to return the sign of a double floating point value.
		/// </summary>
		/// <param name="value">Value to evaluate.</param>
		/// <returns>0 if the value is 0, -1 if the value is less than 0, and 1 if the value is greater than 0.</returns>
		public static int Sign(this double value)
		{
			if (value.EqualsEpsilon(0))
			{
				return 0;
			}

			return value < 0 ? -1 : 1;
		}
		#endregion
	}
}
