using System;
using System.Globalization;
using System.Threading;
using NUnit.Framework;

namespace CustomerFormatter.Nunit.Tests
{
    [TestFixture]
    public class CustomerFormatProviderTests
    {
        [TestCase("{0:n8p7r}", ExpectedException = typeof(FormatException))]
        [TestCase("{0:qwert}", ExpectedException = typeof(FormatException))]
        [TestCase("{0:npr}", Result = "Customer information: name: Ann, phone: 123, revenue: ¤100.00")]
        [TestCase("{0:rnp}", Result = "Customer information: revenue: ¤100.00, name: Ann, phone: 123")]
        [TestCase("{0:np}", Result = "Customer information: name: Ann, phone: 123")]
        [TestCase("{0:nr}", Result = "Customer information: name: Ann, revenue: ¤100.00")]
        [TestCase("{0:nn}", Result = "Customer information: name: Ann, name: Ann")]
        [TestCase("{0:n}", Result = "Customer information: name: Ann")]
        [TestCase("{0:p}", Result = "Customer information: phone: 123")]
        public string Format_ByFormatString(string formatString)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Customer customer = new Customer("Ann", "123", 100);
            return string.Format(new CustomerFormatProvider(), formatString, customer);
        }

        [TestCase("{0:G}", Result = "name: Ann")]
        [TestCase("{0:P}", Result = "phone: 123")]
        [TestCase("{0:N}", Result = "name: Ann")]
        [TestCase("{0:R}", Result = "revenue: ¤100.00")]
        public string ParentFormat_ByFormatString(string formatString)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Customer customer = new Customer("Ann", "123", 100);
            return string.Format(new CustomerFormatProvider(), formatString, customer);
        }

        [TestCase("{0:np}", Result = "Customer information: name: empty, phone: empty")]
        [TestCase("{0:nr}", Result = "Customer information: name: empty, revenue: ¤0.00")]
        [TestCase("{0:nn}", Result = "Customer information: name: empty, name: empty")]
        [TestCase("{0:n}", Result = "Customer information: name: empty")]
        [TestCase("{0:p}", Result = "Customer information: phone: empty")]
        [TestCase("{0:P}", Result = "phone: empty")]
        [TestCase("{0:N}", Result = "name: empty")]
        public string Format_ObjectWithNullPropert(string formatString)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Customer customer = new Customer(null, null,0);
            CustomerFormatProvider provider = new CustomerFormatProvider();
            return string.Format(provider, formatString, customer);
        }
    }

    [TestFixture]
    public class CustomerTests
    {
        [TestCase("G", Result = "name: Ann")]
        [TestCase("n", Result = "name: Ann")]
        [TestCase("p", Result = "phone: 123")]
        [TestCase("R", Result = "revenue: ¤100.00")]
        [TestCase("F", ExpectedException = typeof(FormatException))]
        public string ToString_ByFormatStringAndInvariantCulture(string formatString)
        {
            CultureInfo cultureInfo = CultureInfo.InvariantCulture;
            Customer customer = new Customer("Ann", "123", 100);
            return customer.ToString(formatString, cultureInfo);
        }

        [TestCase(Result = "name: empty")]
        public string ToString_WithoutParametrs()
        {
            Customer customer = new Customer(null, "123", 100);
            return customer.ToString();
        }

        [TestCase("N", Result = "name: Ann")]
        [TestCase("P", Result = "phone: + 375(29)88 - 881 - 652")]
        [TestCase("r", Result = "revenue: ¤100.00")]
        [TestCase("F", ExpectedException = typeof(FormatException))]
        public string ToString_ByFormatString()
        {
            Customer customer = new Customer("Ann", "+375(29)88-881-652", 100);
            return customer.ToString();
        }
    }
}
