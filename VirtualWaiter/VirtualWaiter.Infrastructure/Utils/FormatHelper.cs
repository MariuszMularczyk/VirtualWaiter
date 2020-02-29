using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VirtualWaiter.Utils
{
    public class FormatHelper
    {
        private static string formatPLN = SettingsHelper.FormatPLN;
        private static string formatPLNfractional = SettingsHelper.FormatPLNFractional;

        public static string FormatPLN(float amount, bool isFractional = false)
        {
            return FormatPLN((double)amount, isFractional);   // higher precision
        }
        public static string FormatPLN<T>(T amount, bool isFractional = false)
        {
            var nfi = (NumberFormatInfo)CultureInfo.GetCultureInfo("pl-PL").NumberFormat.Clone();
            decimal parsedAmount;
            bool conversionSucceeded = decimal.TryParse(amount.ToString(), out parsedAmount);
            string currentFormatPLN = isFractional ? formatPLNfractional : formatPLN;
            return conversionSucceeded ? decimal.Round(parsedAmount, 2).ToString(currentFormatPLN, nfi) : amount.ToString();
        }

        public static string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value between 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }

        public static string FormatIBAN(string iban)
        {
            int modulo = iban.Length % 4;
            if (modulo != 0)
                iban = new string(' ', modulo) + iban;
            return Regex.Replace(iban, ".{4}", "$0 ").Trim();
        }
    }
}
