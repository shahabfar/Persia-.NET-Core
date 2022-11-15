using System;

namespace Persia_.NET_Core
{
    public sealed class SolarDate
    {
        public int[] ArrayType { set; get; }
        internal DateTime dateTime { set; get; }
        public int DayOfWeek { set; get; }
        public int DaysPast
        {
            get
            {
                var date = Calendar.ConvertToGregorian(ArrayType[0], 1, 1, DateType.Persian);
                var span = dateTime - date;
                return span.Days;
            }
        }
        public int DaysRemain
        {
            get
            {
                var date = Calendar.ConvertToGregorian(ArrayType[0]+1, 1, 1, DateType.Persian);
                var span = date - dateTime;
                return span.Days;
            }
        }

        readonly string[] _solarMonths = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };

        readonly string[] _days = {"یکم","دوم","سوم","چهارم","پنجم","ششم","هفتم","هشتم","نهم","دهم","یازدهم","دوازدهم","سیزدهم",
												   "چهاردهم","پانزدهم","شانزدهم","هفدهم","هجدهم","نوزدهم","بیستم","بیست و یکم","بیست و دوم",
												   "بیست و سوم","بیست و چهارم","بیست و پنجم","بیست و ششم","بیست و هفتم","بیست و هشتم","بیست و نهم","سی ام","سی و یکم"};

        readonly string[] _weekDays = { "شنبه", "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه" };

        public bool IsLeapYear => ((ArrayType[0] - (ArrayType[0] > 0 ? 474 : 473)) % 2820 + 474 + 38) * 682 % 2816 < 682;

        public new string ToString()
        {
            var str1 = ArrayType[1].ToString();
            var str2 = ArrayType[2].ToString();
            if (ArrayType[1] < 10)
                str1 = ArrayType[1].ToString().Insert(0, "0");
            if (ArrayType[2] < 10)
                str2 = ArrayType[2].ToString().Insert(0, "0");

            return $"{ArrayType[0]}/{str1}/{str2}";
        }

        public string ToString(string dateTimeFormatSpecifier)
        {
            string dateFormat;
            var str = dateTimeFormatSpecifier.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (str.Length == 2)
                dateFormat = ToString(str[0]) + "  " + ToString(str[1]);
            else
            {
                switch (dateTimeFormatSpecifier)
                {
                    case "D":
                        dateFormat = PersianWord.ToPersianString(ArrayType[0]) + "/" +
                                     PersianWord.ToPersianString(ArrayType[1]) + "/" + PersianWord.ToPersianString(ArrayType[2]);
                        break;
                    case "d":
                        dateFormat = PersianWord.ToPersianString(ArrayType[0].ToString().Remove(0, 2)) + "/" +
                                     PersianWord.ToPersianString(ArrayType[1]) + "/" + PersianWord.ToPersianString(ArrayType[2]);
                        break;
                    case "F":
                        dateFormat = PersianWord.ToPersianString(ArrayType[0]) + " " + _days[ArrayType[2] - 1] + " " +
                                     _solarMonths[ArrayType[1] - 1];
                        break;
                    case "f":
                        dateFormat = PersianWord.ToPersianString(ArrayType[0].ToString().Remove(0, 2)) + " " +
                                     _days[ArrayType[2] - 1] + " " + _solarMonths[ArrayType[1] - 1];
                        break;
                    case "W":
                        dateFormat = PersianWord.ToPersianString(ArrayType[0]) + "/" + PersianWord.ToPersianString(ArrayType[1]) +
                                     "/" + PersianWord.ToPersianString(ArrayType[2]) + " " + _weekDays[DayOfWeek];
                        break;
                    case "w":
                        dateFormat = PersianWord.ToPersianString(ArrayType[0].ToString().Remove(0, 2)) + "/" +
                                     PersianWord.ToPersianString(ArrayType[1]) + "/" + PersianWord.ToPersianString(ArrayType[2]) +
                                     " " + _weekDays[DayOfWeek];
                        break;
                    case "S":
                        dateFormat = _weekDays[DayOfWeek] + " " + _days[ArrayType[2] - 1] + " " +
                                     _solarMonths[ArrayType[1] - 1] + " " + PersianWord.ToPersianString(ArrayType[0]) + " ";
                        break;
                    case "s":
                        dateFormat = _weekDays[DayOfWeek] + " " + _days[ArrayType[2] - 1] + " " +
                                     _solarMonths[ArrayType[1] - 1] + " " +
                                     PersianWord.ToPersianString(ArrayType[0].ToString().Remove(0, 2)) + " ";
                        break;
                    case "M":
                        dateFormat = PersianWord.ToPersianString(ArrayType[2]) + " " + _solarMonths[ArrayType[1] - 1] + " " +
                                     PersianWord.ToPersianString(ArrayType[0]);
                        break;
                    case "m":
                        dateFormat = PersianWord.ToPersianString(ArrayType[2]) + " " + _solarMonths[ArrayType[1] - 1] + " " +
                                     PersianWord.ToPersianString(ArrayType[0].ToString().Remove(0, 2));
                        break;
                    case "N":
                        dateFormat = _weekDays[DayOfWeek] + " " + PersianWord.ToPersianString(ArrayType[2]) + " " +
                                     _solarMonths[ArrayType[1] - 1] + " " + PersianWord.ToPersianString(ArrayType[0]);
                        break;
                    case "n":
                        dateFormat = _weekDays[DayOfWeek] + " " + PersianWord.ToPersianString(ArrayType[2]) + " " +
                                     _solarMonths[ArrayType[1] - 1] + " " +
                                     PersianWord.ToPersianString(ArrayType[0].ToString().Remove(0, 2));
                        break;
                    case "g":
                        dateFormat = _weekDays[DayOfWeek] + " " + PersianWord.ToPersianString(ArrayType[2]) + " " +
                                     _solarMonths[ArrayType[1] - 1];
                        break;
                    case "E":
                        dateFormat = _solarMonths[ArrayType[1] - 1] + " " + PersianWord.ToPersianString(ArrayType[0]);
                        break;
                    case "e":
                        dateFormat = _solarMonths[ArrayType[1] - 1] + " " +
                                     PersianWord.ToPersianString(ArrayType[0].ToString().Remove(0, 2));
                        break;
                    case "Q":
                        dateFormat = PersianWord.ToPersianString(ArrayType[2]) + " " + _solarMonths[ArrayType[1] - 1];
                        break;
                    case "q":
                        dateFormat = _days[ArrayType[2] - 1] + " " + _solarMonths[ArrayType[1] - 1];
                        break;
                    case "L":
                        dateFormat = ToString().Remove(0, 2);
                        break;
                    case "H":
                        dateFormat = PersianWord.ToPersianString(ArrayType[3]) + ":" + PersianWord.ToPersianString(ArrayType[4]) +
                                     ":" + PersianWord.ToPersianString(ArrayType[5]);
                        break;
                    case "R":
                        dateFormat = PersianWord.ToPersianString(ArrayType[3]) + ":" + PersianWord.ToPersianString(ArrayType[4]) +
                                     ":" + PersianWord.ToPersianString(ArrayType[5]) + " " + "ساعت";
                        break;
                    case "HH":
                        string noon;
                        if (ArrayType[3] > 12)
                        {
                            ArrayType[3] = ArrayType[3] - 12;
                            noon = "بعد از ظهر";
                        }
                        else if (ArrayType[3] == 12)
                            noon = "بعد از ظهر";
                        else
                            noon = "قبل از ظهر";
                        dateFormat = PersianWord.ToPersianString(ArrayType[3]) + ":" + PersianWord.ToPersianString(ArrayType[4]) +
                                     ":" + PersianWord.ToPersianString(ArrayType[5]) + " " + noon;
                        break;
                    case "hh":
                        if (ArrayType[3] > 12)
                        {
                            ArrayType[3] = ArrayType[3] - 12;
                            noon = "ب ظ";
                        }
                        else if (ArrayType[3] == 12)
                            noon = "ب ظ";
                        else
                            noon = "ق ظ";
                        dateFormat = PersianWord.ToPersianString(ArrayType[3]) + ":" + PersianWord.ToPersianString(ArrayType[4]) +
                                     ":" + PersianWord.ToPersianString(ArrayType[5]) + " " + noon;
                        break;
                    case "T":
                        if (ArrayType[3] > 12)
                        {
                            ArrayType[3] = ArrayType[3] - 12;
                            noon = "بعد از ظهر";
                        }
                        else if (ArrayType[3] == 12)
                            noon = "بعد از ظهر";
                        else
                            noon = "قبل از ظهر";
                        dateFormat = "ساعت" + " " + PersianWord.ToPersianString(ArrayType[3]) + ":" +
                                     PersianWord.ToPersianString(ArrayType[4]) + ":" + PersianWord.ToPersianString(ArrayType[5]) +
                                     " " + noon;
                        break;
                    case "t":
                        if (ArrayType[3] > 12)
                        {
                            ArrayType[3] = ArrayType[3] - 12;
                            noon = "ب ظ";
                        }
                        else if (ArrayType[3] == 12)
                            noon = "ب ظ";
                        else
                            noon = "ق ظ";
                        dateFormat = "ساعت" + " " + PersianWord.ToPersianString(ArrayType[3]) + ":" +
                                     PersianWord.ToPersianString(ArrayType[4]) + ":" + PersianWord.ToPersianString(ArrayType[5]) +
                                     " " + noon;
                        break;
                    default:
                        dateFormat = PersianWord.ToPersianString(ArrayType[0]) + "/" + PersianWord.ToPersianString(ArrayType[1]) +
                                     "/" + PersianWord.ToPersianString(ArrayType[2]);
                        break;
                }
            }
            return dateFormat;
        }

        public string ToRelativeDateString(string specifier)
        {
            string format;
            var n = 0;
            var m = 0;
            var d = 0;

            var str = specifier.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            switch (str.Length)
            {
                case 1:
                    specifier = str[0];
                    break;
                case 2:
                    specifier = str[0];
                    n = int.Parse(str[1]);
                    break;
                case 3:
                    specifier = str[0];
                    n = int.Parse(str[1]);
                    m = int.Parse(str[2]);
                    break;
                case 4:
                    specifier = str[0];
                    n = int.Parse(str[1]);
                    m = int.Parse(str[2]);
                    d = int.Parse(str[3]);
                    break;
            }

            switch (specifier)
            {
                case "D":
                    var span = DateTime.Now - dateTime;
                    n = n == 0 ? 30 : n;
                    if (span.Days <= n && span.Days > 0)
                        format = $"{PersianWord.ToPersianString(span.Days)} روز پیش";
                    else
                        format = PersianWord.ToPersianString(ArrayType[0]) + "/" + PersianWord.ToPersianString(ArrayType[1]) + "/" + PersianWord.ToPersianString(ArrayType[2]);
                    break;
                case "T":
                    if (dateTime.Date == DateTime.Now.Date)
                        format = "امروز";
                    else
                        format = PersianWord.ToPersianString(ArrayType[0]) + "/" + PersianWord.ToPersianString(ArrayType[1]) + "/" + PersianWord.ToPersianString(ArrayType[2]);
                    break;
                case "Y":
                    if (dateTime.Date == DateTime.Now.Date)
                        format = "امروز";
                    else if(dateTime.Date.AddDays(1) == DateTime.Now.Date)
                        format = "دیروز";
                    else
                        format = PersianWord.ToPersianString(ArrayType[0]) + "/" + PersianWord.ToPersianString(ArrayType[1]) + "/" + PersianWord.ToPersianString(ArrayType[2]);
                    break;
                case "TY":
                    if (dateTime.Date == DateTime.Now.Date)
                        format = "امروز";
                    else if (dateTime.Date.AddDays(1) == DateTime.Now.Date)
                        format = "دیروز";
                    else
                    {
                        n = n == 0 ? 7 : n;
                        format = ToRelativeDateString($"D,{n}");
                    }
                    break;
                case "N":
                    span = DateTime.Now - dateTime;
                    n = n == 0 ? 5 : n > 5 ? 5 : n;
                    if (span.TotalMinutes <= n && span.TotalMinutes > 0)
                        format = "اکنون";
                    else
                        format = PersianWord.ToPersianString(ArrayType[0]) + "/" + PersianWord.ToPersianString(ArrayType[1]) + "/" + PersianWord.ToPersianString(ArrayType[2]);
                    break;
                case "M":
                    span = DateTime.Now - dateTime;
                    n = n == 0 ? 60 : n > 60 ? 60 : n;
                    if (span.TotalMinutes <= n && span.TotalMinutes > 0)
                        format = string.Format("{0} دقیقه پیش", PersianWord.ToPersianString(span.Minutes));
                    else
                        format = PersianWord.ToPersianString(ArrayType[0]) + "/" + PersianWord.ToPersianString(ArrayType[1]) + "/" + PersianWord.ToPersianString(ArrayType[2]);
                    break;
                case "H":
                    span = DateTime.Now - dateTime;
                    n = n == 0 ? 24 : n > 24 ? 24 : n;
                    if (span.TotalHours <= n && span.TotalHours > 0)
                        format = string.Format("{0} ساعت پیش", PersianWord.ToPersianString(span.Hours));
                    else
                        format = PersianWord.ToPersianString(ArrayType[0]) + "/" + PersianWord.ToPersianString(ArrayType[1]) + "/" + PersianWord.ToPersianString(ArrayType[2]);
                    break;
                case "h":
                    span = DateTime.Now - dateTime;
                    if (span.TotalHours <= 1 && span.TotalHours > 0)
                        format = "کمتر از یک ساعت پیش";
                    else
                        format = PersianWord.ToPersianString(ArrayType[0]) + "/" + PersianWord.ToPersianString(ArrayType[1]) + "/" + PersianWord.ToPersianString(ArrayType[2]);
                    break;
                case "m":
                    span = DateTime.Now - dateTime;
                    if (span.TotalMinutes <= 1 && span.TotalMinutes > 0)
                        format = "کمتر از یک دقیقه پیش";
                    else
                        format = PersianWord.ToPersianString(ArrayType[0]) + "/" + PersianWord.ToPersianString(ArrayType[1]) + "/" + PersianWord.ToPersianString(ArrayType[2]);
                    break;
                case "n":
                    span = DateTime.Now - dateTime;
                    n = n == 0 ? 5 : n > 5 ? 5 : n;
                    m = m == 0 ? 60 : m > 60 ? 60 : m;
                    if (span.TotalMinutes <= n && span.TotalMinutes > 0)
                        format = "اکنون";
                    else if (span.TotalMinutes <= m && span.TotalMinutes > 0)
                        format = ToRelativeDateString(string.Format("M,{0}",m));
                    else
                        format = PersianWord.ToPersianString(ArrayType[0]) + "/" + PersianWord.ToPersianString(ArrayType[1]) + "/" + PersianWord.ToPersianString(ArrayType[2]);
                    break;
                case "p":
                    span = DateTime.Now - dateTime;
                    n = n == 0 ? 5  : n > 5 ? 5 : n;
                    m = m == 0 ? 60 : m > 60 ? 60 : m;
                    d = d == 0 ? 24 : d > 24 ? 24 : d;
                    if (span.TotalMinutes <= n && span.TotalMinutes > 0)
                        format = "اکنون";
                    else if (span.TotalMinutes <= m && span.TotalMinutes > 0)
                        format = ToRelativeDateString(string.Format("M,{0}", m));
                    else if (span.TotalHours <= d && span.TotalHours > 0)
                        format = ToRelativeDateString(string.Format("H,{0}", d));
                    else
                        format = PersianWord.ToPersianString(ArrayType[0]) + "/" + PersianWord.ToPersianString(ArrayType[1]) + "/" + PersianWord.ToPersianString(ArrayType[2]);
                    break;
                case "t":
                    span = DateTime.Now - dateTime;
                    n = n == 0 ? 5 : n > 5 ? 5 : n;
                    m = m == 0 ? 60 : m > 60 ? 60 : m;
                    if (span.TotalMinutes <= n && span.TotalMinutes > 0)
                        format = "اکنون";
                    else if (span.TotalMinutes <= m && span.TotalMinutes > 0)
                        format = ToRelativeDateString(string.Format("M,{0}", m));
                    else if (DateTime.Now.Date == dateTime.Date)
                        format = ToRelativeDateString("Y");
                    else
                        format = PersianWord.ToPersianString(ArrayType[0]) + "/" + PersianWord.ToPersianString(ArrayType[1]) + "/" + PersianWord.ToPersianString(ArrayType[2]);
                    break;
                default:
                    format = PersianWord.ToPersianString(ArrayType[0]) + "/" + PersianWord.ToPersianString(ArrayType[1]) + "/" + PersianWord.ToPersianString(ArrayType[2]);
                    break;
            }

            return format;
        }

    }
}
