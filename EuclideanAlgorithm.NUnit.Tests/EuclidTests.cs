using System;
using System.Diagnostics;
using NUnit.Framework;
using static EuclideanAlgorithm.Euclid;

namespace EuclideanAlgorithm.NUnit.Tests
{
    [TestFixture]
    public class EuklidTests
    {
        [TestCase(-5, -10, Result = 5)]
        [TestCase(-5, 10, Result = 5)]
        [TestCase(5, -10, Result = 5)]
        [TestCase(5, 10, Result = 5)]
        [TestCase(48, 0, Result = 48)]
        [TestCase(0, 48, Result = 48)]
        [TestCase(24, 32, Result = 8)]
        [TestCase(1, 7, Result = 1)]
        [TestCase(0, 0, ExpectedException = typeof(ArgumentException))]
        [TestCase(17, 13, Result = 1)]

        public int GCD_TwoNumbers(int a, int b)
        {
            int result;
            Debug.WriteLine(Gcd(a, b, out result));
            return result;
        }

        [TestCase(-5, -10, 10, Result = 5)]
        [TestCase(-5, 10, -10, Result = 5)]
        [TestCase(5, -10, 5, Result = 5)]
        [TestCase(8, 246, 26, Result = 2)]
        [TestCase(48, 0, 52, Result = 4)]
        [TestCase(52, 48, 0, Result = 4)]
        [TestCase(0, 0, 52, Result = 52)]
        [TestCase(0, 48, 0, Result = 48)]
        [TestCase(0, 0, 0, ExpectedException = typeof(ArgumentException))]
        [TestCase(17, 13, 8, Result = 1)]

        public int GCD_ThreeNumbers(int a, int b, int c)
        {
            int result;
            Debug.WriteLine(Gcd(a, b, c, out result));
            return result;
        }

        [TestCase(-5, -10, 10, 5, 45, 100, 35, Result = 5)]
        [TestCase(-5, 10, -10, -25, 1000, 655, 205, Result = 5)]
        [TestCase(8, 246, 26, 38, 82, 256, Result = 2)]
        [TestCase(48, 0, 52, 0, 0, Result = 4)]
        [TestCase(0, 48, 52, 16, 96, Result = 4)]
        [TestCase(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ExpectedException = typeof(ArgumentException))]
        [TestCase(17, 13, 8, 102, 3698, 258, 20, Result = 1)]

        public int GCD_ManyNumbers(params int[] numbers)
        {
            int result;
            Debug.WriteLine(Gcd(out result, numbers));
            return result;
        }

        [TestCase(-5, -10, Result = 5)]
        [TestCase(-5, 10, Result = 5)]
        [TestCase(5, -10, Result = 5)]
        [TestCase(5, 10, Result = 5)]
        [TestCase(48, 0, Result = 48)]
        [TestCase(0, 48, Result = 48)]
        [TestCase(24, 32, Result = 8)]
        [TestCase(0, 0, ExpectedException = typeof(ArgumentException))]
        [TestCase(17, 13, Result = 1)]

        public int GCDBin_TwoNumbers(int a, int b)
        {
            int result;
            Debug.WriteLine(GcdBin(a, b, out result));
            return result;
        }

        [TestCase(-5, -10, 10, Result = 5)]
        [TestCase(-5, 10, -10, Result = 5)]
        [TestCase(5, -10, 5, Result = 5)]
        [TestCase(8, 246, 26, Result = 2)]
        [TestCase(48, 0, 52, Result = 4)]
        [TestCase(52, 48, 0, Result = 4)]
        [TestCase(0, 0, 52, Result = 52)]
        [TestCase(0, 48, 0, Result = 48)]
        [TestCase(0, 0, 0, ExpectedException = typeof(ArgumentException))]
        [TestCase(17, 13, 8, Result = 1)]

        public int GCDBin_ThreeNumbers(int a, int b, int c)
        {
            int result;
            Debug.WriteLine(GcdBin(a, b, c, out result));
            return result;
        }

        [TestCase(-5, -10, 10, 5, 45, 100, 35, Result = 5)]
        [TestCase(-5, 10, -10, -25, 1000, 655, 205, Result = 5)]
        [TestCase(8, 246, 26, 38, 82, 256, Result = 2)]
        [TestCase(48, 0, 52, 0, 0, Result = 4)]
        [TestCase(0, 48, 52, 16, 96, Result = 4)]
        [TestCase(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ExpectedException = typeof(ArgumentException))]
        [TestCase(17, 13, 8, 102, 3698, 258, 20, Result = 1)]

        public int GCDBin_ManyNumbers(params int[] numbers)
        {
            int result;
            Debug.WriteLine(GcdBin(out result, numbers));
            return result;
        }
    }
}
