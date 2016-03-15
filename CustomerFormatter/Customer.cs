using System;
using System.Globalization;
using System.Linq;

namespace CustomerFormatter
{
    public class Customer : IFormattable
    {
        public string Name { get; set; }
        
        public string Phone { get; set; }

        public decimal Revenue { get; set; }

        public Customer(string name, string phone, decimal revenue)
        {
            Name = name;
            Phone = phone;
            Revenue = revenue;
        }
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "G";
            }

            if (formatProvider == null)
            {
                formatProvider = CultureInfo.CurrentCulture;
            }

            switch (format.ToUpperInvariant())
            {
                case "G":
                case "N":
                    return $"name: {Name??"empty"}";
                case "P":
                    return $"phone: {Phone??"empty"}";
                case "R":
                    return $"revenue: {Revenue.ToString("C", formatProvider)}";
                default:
                    throw new FormatException($"Format {format} isn't supported");
            }
        }
    }

    public class CustomerFormatProvider : IFormatProvider, ICustomFormatter
    {
      public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            string[] formatArray;
            Customer customer = arg as Customer;

            if (customer == null || !CheckFormat(format, out formatArray))
            {
                try
                {
                    return CheckForOtherFormats(format, arg);
                }
                catch (FormatException exception)
                {
                    throw new FormatException($"Format {format} isn't valid!", exception);
                }
            }

            return GetFormattedString(customer, formatArray);
        }

        private string CheckForOtherFormats(string format, object arg)
        {
            if (arg is IFormattable)
                return ((IFormattable)arg).ToString(format, CultureInfo.InvariantCulture);

            return arg?.ToString() ?? string.Empty;
        }

        public string GetFormattedString( Customer customer,string[] formatArray )
        {
            string result = "Customer information:";

            for (int i = 0; i < formatArray.Length; i++)
            {
                result += GetProperty(customer, formatArray[i]);
            }

            result = result.Substring(0, result.Length - 1);
            return result;
        }

        private bool CheckFormat(string format, out string[] formatArray)
        {
            string[] allowFormatters = "n p r".Split();

            if (string.IsNullOrEmpty(format))
            {
                throw new FormatException($"Format {format} isn't valid!");
            }

            formatArray = new string[format.Length];

            for (int i = 0; i < format.Length; i++)
            {
                formatArray[i] = format[i].ToString();
                if (!allowFormatters.Contains(formatArray[i]))
                {
                    return false; 
                }
            }

            return true;
        }

        private static string GetProperty(Customer customer, string formatLetter)
        {
            switch (formatLetter)
            {
                case "n":
                    return $" name: {customer.Name??"empty"},";
                case "p":
                    return $" phone: {customer.Phone??"empty"},";
                case "r":
                    return $" revenue: {customer.Revenue.ToString("C", CultureInfo.CurrentCulture)},";
                default:
                    return "";
            }
        }
    }
}
