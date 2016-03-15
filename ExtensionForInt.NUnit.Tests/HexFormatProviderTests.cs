using System;
using System.Globalization;
using System.Threading;
using NUnit.Framework;

namespace ExtensionForInt.NUnit.Tests
{
    [TestFixture]
    public class HexFormatProviderTests
    {
        [TestCase(0, Result = "0")]
        [TestCase(7, Result = "7")]
        [TestCase(28, Result = "1C")]
        [TestCase(15, Result = "F")]  
        [TestCase(256, Result = "100")]
        [TestCase(-11, Result = "FFFFFFF5")]
        [TestCase(-10000, Result = "FFFFD8F0")]
        [TestCase(25.2, ExpectedException = typeof(ArgumentException))]
        public string Format_Test(object value)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            IFormatProvider formatProvider = new HexFormatProvider();
            return string.Format(formatProvider, "{0:hex}", value);
        }

        [TestCase(-10000, "{0:X}", Result = "FFFFD8F0")]
        [TestCase(-10000, "{0:q}", ExpectedException = typeof(FormatException))]
        [TestCase(-10000, "{0:D}", Result = "-10000")]
        [TestCase(.473, "{0:P}", Result = "47.30 %")]
        [TestCase(4.73, "{0:C}", Result = "¤4.73")]
        public string ParentFormat_Test(object value, string formatString)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            IFormatProvider formatProvider = new HexFormatProvider();
            return string.Format(formatProvider, formatString, value);
        }
    }
}
