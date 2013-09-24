using System.Text.RegularExpressions;

namespace Skadoosh.Store.Common
{
    public static class StringExtenstions
    {
        public static bool IsValidEmail(this string str)
        {
            string exp = "^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$;";
            return Regex.IsMatch(str, exp);
        }
    }
}