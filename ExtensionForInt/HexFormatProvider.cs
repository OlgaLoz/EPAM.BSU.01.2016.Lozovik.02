using System;
using System.Globalization;

namespace ExtensionForInt
{
    public class HexFormatProvider : IFormatProvider, ICustomFormatter
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
            if (format == null)
            {
                throw new FormatException("Format string can't be null!");
            }
            
            if (arg == null || arg.GetType() != typeof(int) || format != "hex")
            {
                if (arg != null && arg.GetType() != typeof(int) && format == "hex")
                {
                    throw new ArgumentException($"Format {format} can be apply only for int!");
                }

                try
                {
                    return CheckForOtherFormats(format, arg);
                }
                catch (FormatException exception)
                {
                    throw new FormatException($"Format {format} isn't valid!", exception);
                }
            }

            int valueToConvert = (int)arg;
            return ConvertToHex(valueToConvert);
        }

        private string CheckForOtherFormats(string format, object arg)
        {
                if (arg is IFormattable)
                    return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);

                return arg?.ToString() ?? string.Empty;
        }

        private static string ConvertToHex(int valueToConvert)
        {
            string[] signs = "0 1 2 3 4 5 6 7 8 9 A B C D E F".Split();
            const int BASE = 16;
            if (valueToConvert == 0)
            {
                return "0";
            }

            long value = valueToConvert;
            string result = "";

            if (valueToConvert < 0)
            {
                value = uint.MaxValue + valueToConvert + 1;
            }           

            while (value != 0)
            {
                long modulo = value % BASE;
                value = value / BASE;
                result = signs[modulo] + result;
            }

            return result;
        }

    }
}
