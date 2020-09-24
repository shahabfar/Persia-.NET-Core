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


        public static string ConvertToLatinNumber_(string num)
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

        private static string ConvertToPersianNumber(string num)
        {
            if (string.IsNullOrWhiteSpace(num)) return string.Empty;

            return string.Create(num.Length, num, (chars, context) =>
            {
                for (var i = 0; i < num.Length; i++)
                {
                    chars[i] = context[i] switch
                    {
                        '0' => '\u06F0',
                        '\u0660' => '\u06F0',
                        '1' => '\u06F1',
                        '\u0661' => '\u06F1',
                        '2' => '\u06F2',
                        '\u0662' => '\u06F2',
                        '3' => '\u06F3',
                        '\u0663' => '\u06F3',
                        '4' => '\u06F4',
                        '\u0664' => '\u06F4',
                        '5' => '\u06F5',
                        '\u0665' => '\u06F5',
                        '6' => '\u06F6',
                        '\u0666' => '\u06F6',
                        '7' => '\u06F7',
                        '\u0667' => '\u06F7',
                        '8' => '\u06F8',
                        '\u0668' => '\u06F8',
                        '9' => '\u06F9',
                        '\u0669' => '\u06F9',
                        _ => chars[i]
                    };
                }
            });
        }

        private static string ConvertToLatinNumber(string num)
        {
            if (string.IsNullOrWhiteSpace(num)) return string.Empty;

            return string.Create(num.Length, num, (chars, context) =>
            {
                for (var i = 0; i < num.Length; i++)
                {
                    chars[i] = context[i] switch
                    {
                        '\u06F0' => '0', //٠
                        '\u0660' => '0', //۰
                        '\u06F1' => '1', //١
                        '\u0661' => '1', //۱
                        '\u06F2' => '2', //٢
                        '\u0662' => '2', //۲
                        '\u06F3' => '3', //٣
                        '\u0663' => '3', //۳
                        '\u06F4' => '4', //۴
                        '\u0664' => '4', //٤
                        '\u06F5' => '5', //۵
                        '\u0665' => '5', //٥
                        '\u06F6' => '6', //۶
                        '\u0666' => '6', //٦
                        '\u06F7' => '7', //٧
                        '\u0667' => '7', //۷
                        '\u06F8' => '8', //٨
                        '\u0668' => '8', //۸
                        '\u06F9' => '9', //٩
                        '\u0669' => '9', //۹
                        _ => chars[i]
                    };
                }
            });
        }

    }
}

