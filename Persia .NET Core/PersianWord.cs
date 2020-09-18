namespace Persia_.NET_Core
{
    public abstract class PersianWord
    {
        private PersianWord()
        {            
        }

        public static string ToPersianString(object value)
        {
            var str = value;
            var strOut = string.Empty;
            var length = str.ToString().Length;
            var nLen = length;
            if (nLen == 0)
                return str.ToString();
            for (var i = 0; i < nLen; i++)
            {
                var ch = str.ToString()[i];
                if ((48 <= ch) && (ch <= 57))
                    ch = (char)(ch + 1728);
                if (ch == 46)
                    ch = (char)47;
                strOut += ch;
            }
            return strOut.Replace("ي", "ی").Replace("ك", "ک");
        }


        public static string ConvertToLatinNumber(string num)
        {
            var strOut = "";
            var nLen = num.Length;
            if (nLen == 0)
                return num;
            for (var i = 0; i < nLen; i++)
            {
                var ch = num[i];
                if ((1776 <= ch) && (ch <= 1785))
                {
                    ch = (char)(ch - 1728);
                }
                strOut += ch;
            }
            return strOut;
        }

    }
}
