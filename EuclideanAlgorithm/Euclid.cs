using System;
using System.Diagnostics;
using System.Linq;
using static System.Math;

namespace EuclideanAlgorithm
{
    public static class Euclid
    {
        private delegate int TwoNumbersDelegate(int a, int b);

        #region Public methods to find CGD
        public static double Gcd(int a, int b, out int result)
        {
            return TwoNumbers(a, b, out result, FindGcd);
        }

        public static double Gcd(int a, int b, int c, out int result)
        {
            return ThreeNumbers(a, b, c, out result, FindGcd);
        }

        public static double Gcd(out int result, params int[] numbers)
        {
            return ManyNumbers(FindGcd, out result, numbers);
        }
        #endregion

        #region Public methods to binary find CGD
        public static double GcdBin(int a, int b, out int result)
        {
            return TwoNumbers(a, b, out result, FindBinGcd);
        }

        public static double GcdBin(int a, int b, int c, out int result)
        {
            return ThreeNumbers(a, b, c, out result, FindBinGcd);
        }

        public static double GcdBin(out int result, params int[] numbers)
        {
            return ManyNumbers(FindBinGcd, out result, numbers);
        }
        #endregion

        #region  Euclid and Stein algorithms
        private static int FindGcd(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return Abs(a);
        }

        private static int FindBinGcd(int a, int b)
        {
            if (a < 0)
            {
                a = Abs(a);
            }

            if (b < 0)
            {
                b = Abs(b);
            }

            if (a == 0)
            {
                return b;
            }

            if (b == 0)
            {
                return a;
            }

            if (a == b)
            {
                return a;
            }

            if (a == 1 || b == 1)
            {
                return 1;
            }

            if (a % 2 == 0 && b % 2 == 0)
            {
                return 2 * FindBinGcd(a/2, b/2);
            }

            if (a % 2 == 0 && b % 2 == 1)
            {
                return FindBinGcd(a / 2, b );
            }

            if (a % 2 == 1 && b % 2 == 0)
            {
                return FindBinGcd(a, b / 2);
            }

            return FindBinGcd(a, Abs(a - b));
        }
        #endregion

        #region Private methods to find GCD
        private static double TwoNumbers(int a, int b, out int result, TwoNumbersDelegate twoNumbersGcd)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            if (a == 0 && b == 0)
            {
                throw new ArgumentException("Though one of the numbers shouldn't be zero!");
            }
            result = twoNumbersGcd(a, b);
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        private static double ThreeNumbers(int a, int b, int c, out int result, TwoNumbersDelegate twoNumbersGcd)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            if (a == 0 && b == 0 && c == 0)
            {
                throw new ArgumentException("Though one of the numbers shouldn't be zero!");
            }

            result = twoNumbersGcd(a, b);
            result = twoNumbersGcd(result, c);
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        private static double ManyNumbers(TwoNumbersDelegate twoNumbers, out int result, params int[] numbers)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            bool allZero = numbers.All(number => number == 0);
            if (allZero)
            {
                throw new ArgumentException("Though one of the numbers shouldn't be zero!");
            }

            result = numbers.Aggregate(0, (current, t) => twoNumbers(current, t));

            return stopwatch.Elapsed.TotalMilliseconds;
        }
        #endregion
    }
}
