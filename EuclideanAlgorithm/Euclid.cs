using System;
using System.Diagnostics;
using static System.Math;

namespace EuclideanAlgorithm
{
    public static class Euclid
    {
        #region Public methods to find CGD
        public static double GCD(int a, int b, out int result)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            CheckArguments(a, b);
            result = FindGCD(a, b);
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public static double GCD(int a, int b, int c, out int result)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            CheckArguments(a, b, c);
            result = FindGCD(a, b);
            result = FindGCD(result, c);
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public static double GCD(out int result, params int[] numbers)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            CheckArguments(numbers);
            result = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                result = FindGCD(result, numbers[i]);
            }
            return stopwatch.Elapsed.TotalMilliseconds;
        }
        #endregion

        #region Public methods to binary find CGD
        public static double GCDBin(int a, int b, out int result)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            CheckArguments(a, b);
            result = FindBinGCD(a, b);
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public static double GCDBin(int a, int b, int c, out int result)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            CheckArguments(a, b, c);
            result = FindBinGCD(a, b);
            result = FindBinGCD(result, c);
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public static double GCDBin(out int result, params int[] numbers)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            CheckArguments(numbers);
            result = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                result = FindBinGCD(result, numbers[i]);
            }
            return stopwatch.Elapsed.TotalMilliseconds;
        }
        #endregion

        #region  Euclid and Stein algorithms
        private static int FindGCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return Abs(a);
        }

        private static int FindBinGCD(int a, int b)
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
                return 2 * FindBinGCD(a/2, b/2);
            }

            if (a % 2 == 0 && b % 2 == 1)
            {
                return FindBinGCD(a / 2, b );
            }

            if (a % 2 == 1 && b % 2 == 0)
            {
                return FindBinGCD(a, b / 2);
            }

            return FindBinGCD(a, Abs(a - b));
        }
        #endregion

        #region Checkers
        private static void CheckArguments(int a, int b)
        {
            if (a == 0 && b == 0)
            {
                throw new ArgumentException("Though one of the numbers shouldn't be zero!");
            }
        }

        private static void CheckArguments(int a, int b, int c)
        {
            if (a == 0 && b == 0 && c==0)
            {
                throw new ArgumentException("Though one of the numbers shouldn't be zero!");
            }
        }

        private static void CheckArguments(params int[] numbers)
        {
            bool allZero = true;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] !=0)
                {
                    allZero = false;
                    break;
                }
            }
            if (allZero)
            {
                throw new ArgumentException("Though one of the numbers shouldn't be zero!");
            }
        }
        #endregion
    }
}
