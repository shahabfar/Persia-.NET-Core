using System;


//    ("`-''-/").___..--''"`-._
//     `6_ 6  )   `-.  (     ).`-.__.`)
//     (_Y_.)'  ._   )  `._ `. ``-..-'
//   _..`--'_..-_/  /--'_.' ,'
//  (il),-''  (li),'  ((!.-'


namespace Persia_.NET_Core
{
    public enum DateType { Persian, Gerigorian, Islamic };

    public static class Calendar
    {
        private const float GregorianEpoch = 1721425.5f;
        private const float PersianEpoch   = 1948320.5f;
        private const float IslamicEpoch   = 1948439.5f;
        private const float Tolerance      = 0.00001f;

        public static SolarDate ConvertToPersian(LunarDate lunarDate)
        {
            return ConvertToPersian(null, lunarDate.ArrayType[0], lunarDate.ArrayType[1], lunarDate.ArrayType[2], false);
        }

        public static SolarDate ConvertToPersian(DateTime date)
        {
            return ConvertToPersian(date, 0, 0, 0, true);
        }

        public static SolarDate ConvertToPersian(int year, int month, int day, DateType dateType)
        {
            return ConvertToPersian(null, year, month, day, dateType == DateType.Gerigorian);
        }

        private static SolarDate ConvertToPersian(DateTime? date, int year, int month, int day, bool isDateTime)
        {
            var solarDate = new SolarDate();
            float jd;
            if (isDateTime)
            {
                if (date.HasValue)
                {
                    jd = gregorian_to_jd(date.Value.Year, date.Value.Month, date.Value.Day);
                    solarDate.dateTime = date.Value;
                }
                else
                    throw new NullReferenceException();
            }
            else
            {
                jd = islamic_to_jd(year, month, day);
                date = ConvertToGregorian(year, month, day, DateType.Islamic);
                solarDate.dateTime = date.Value;
            }

            var solar = jd_to_persian(jd);
            var weekday = Convert.ToInt32(date.Value.DayOfWeek);
            weekday = weekday >= 6 ? weekday - 6 : weekday + 1;
            solarDate.DayOfWeek = weekday;
            solarDate.ArrayType = isDateTime ? new[] { solar[0], solar[1], solar[2], date.Value.Hour, date.Value.Minute, date.Value.Second } : new[] { solar[0], solar[1], solar[2], 0, 0, 0 };
            return solarDate;
        }

        public static LunarDate ConvertToIslamic(DateTime date)
        {
            return ConvertToIslamic(date, 0, 0, 0, true);
        }

        public static LunarDate ConvertToIslamic(int year, int month, int day, DateType dateType)
        {
            return ConvertToIslamic(DateTime.Now, year, month, day, dateType != DateType.Persian);
        }

        public static LunarDate ConvertToIslamic(SolarDate solarDate)
        {
            return ConvertToIslamic(DateTime.Now, solarDate.ArrayType[0], solarDate.ArrayType[1], solarDate.ArrayType[2], false);
        }

        private static LunarDate ConvertToIslamic(DateTime? date, int year, int month, int day, bool isDateTime)
        {
            var lunarDate = new LunarDate();
            float jd;
            if (isDateTime)
            {
                if (date.HasValue)
                    jd = gregorian_to_jd(date.Value.Year, date.Value.Month, date.Value.Day);
                else
                    throw new NullReferenceException();
            }
            else // Persian
            {
                jd = persian_to_jd(year, month, day);
                date = ConvertToGregorian(year, month, day, DateType.Persian);
            }

            var lunar   = jd_to_islamic(jd);
            var weekday = Convert.ToInt32(date.Value.DayOfWeek);

            weekday = weekday >= 6 ? weekday - 6 : weekday + 1;
            lunarDate.DayOfWeek = weekday;
            lunarDate.ArrayType = isDateTime ? new[] { lunar[0], lunar[1], lunar[2], date.Value.Hour, date.Value.Minute, date.Value.Second } : new[] { lunar[0], lunar[1], lunar[2], 0, 0, 0 };

            var cal = new System.Globalization.HijriCalendar();
            lunarDate.ArrayType[2] = cal.GetDayOfMonth(date.Value.Date);
            lunarDate.ArrayType[1] = cal.GetMonth(date.Value.Date);

            return lunarDate;
        }

        public static DateTime ConvertToGregorian(LunarDate lunarDate)
        {
            return ConvertToGregorian(lunarDate.ArrayType[0], lunarDate.ArrayType[1], lunarDate.ArrayType[2], lunarDate.ArrayType[3], lunarDate.ArrayType[4], lunarDate.ArrayType[5], true);
        }

        public static DateTime ConvertToGregorian(SolarDate solarDate)
        {
            return ConvertToGregorian(solarDate.ArrayType[0], solarDate.ArrayType[1], solarDate.ArrayType[2], solarDate.ArrayType[3], solarDate.ArrayType[4], solarDate.ArrayType[5], false);
        }

        public static DateTime ConvertToGregorian(int year, int month, int day, DateType dateType)
        {
            return ConvertToGregorian(year, month, day, 0, 0, 0, dateType == DateType.Islamic);
        }

        public static DateTime ConvertToGregorian(int year, int month, int day, int hour, int minute, int second, DateType dateType)
        {
            return ConvertToGregorian(year, month, day, hour, minute, second, dateType == DateType.Islamic);
        }

        private static DateTime ConvertToGregorian(int year, int month, int day, int hour, int minute, int second, bool isSlamic)
        {
            var jd = !isSlamic ? persian_to_jd(year, month, day) : islamic_to_jd(year, month, day);
            var miladi = jd_to_gregorian(jd);
            var date = new DateTime(miladi[0], miladi[1], miladi[2], hour, minute, second);
            return date;
        }

        private static float gregorian_to_jd(int year, int month, int day)
        {
            return (float)(GregorianEpoch - 1 +
                365 * (year - 1) +
                Math.Floor((double)((year - 1) / 4)) - Math.Floor((double)((year - 1) / 100)) +
                Math.Floor((double)((year - 1) / 400)) +
                Math.Floor((double)((367 * month - 362) / 12 +
                (month <= 2 ? 0 : leap_gregorian(year) ? -1 : -2)) + day));
        }

        private static bool leap_gregorian(int year)
        {
            return year % 4 == 0 && !(year % 100 == 0 && year % 400 != 0);
        }

        //private static bool leap_persian(int year)
        //{
         //   return ((((((year - ((year > 0) ? 474 : 473)) % 2820) + 474) + 38) * 682) % 2816) < 682;
        //}

        private static int[] jd_to_persian(float jd)
        {
            float ycycle;

            jd = (float)(Math.Floor(jd) + 0.5);

            var depoch = jd - persian_to_jd(475, 1, 1);
            var cycle = (float)Math.Floor(depoch / 1029983);
            var cyear = (depoch % 1029983);
            if (Math.Abs(cyear - 1029982) < Tolerance)
                ycycle = 2820;
            else
            {
                var aux1 = (float)Math.Floor(cyear / 366);
                var aux2 = (cyear % 366);
                ycycle = (float)Math.Floor(((2134 * aux1) + (2816 * aux2) + 2815) / 1028522) + aux1 + 1;
            }

            var year = (int)(ycycle + (2820 * cycle) + 474);
            if (year <= 0)
                year--;

            var yday = (jd - persian_to_jd(year, 1, 1)) + 1;
            var month = (int)((yday <= 186) ? Math.Ceiling(yday / 31) : Math.Ceiling((yday - 6) / 30));
            var day = (int)(jd - persian_to_jd(year, month, 1)) + 1;
            var res = new[] { year, month, day };
            return res;
        }

        private static float persian_to_jd(int year, int month, int day)
        {
            float epbase = year - (year >= 0 ? 474 : 473);
            var   epyear = 474 + (epbase % 2820);

            return day +
                (month <= 7 ? (month - 1) * 31 : (month - 1) * 30 + 6) + (float)Math.Floor((epyear * 682 - 110) / 2816) +
                                                 (epyear - 1) * 365 + (float)Math.Floor(epbase / 2820) * 1029983 + (PersianEpoch - 1);
        }

        private static int[] jd_to_gregorian(float jd)
        {
            var wjd         = (float)(Math.Floor(jd - 0.5) + 0.5);
            var depoch = wjd - GregorianEpoch;
            var quadricent  = (float)Math.Floor(depoch / 146097);
            var dqc    = (depoch % 146097);
            var cent        = (float)Math.Floor(dqc / 36524);
            var dcent  = dqc % 36524;
            var quad        = (float)Math.Floor(dcent / 1461);
            var dquad  = dcent % 1461;
            var yindex      = (float)Math.Floor(dquad / 365);
            var year        = (int)(quadricent * 400 + (cent * 100) + (quad * 4) + yindex);
            if (!(Math.Abs(cent - 4) < Tolerance || Math.Abs(yindex - 4) < Tolerance))
                year++;

            var yearday = wjd - gregorian_to_jd(year, 1, 1);
            float leapadj = wjd < gregorian_to_jd(year, 3, 1) ? 0 : leap_gregorian(year) ? 1 : 2;
            var month     = (int)Math.Floor(((yearday + leapadj) * 12 + 373) / 367);
            var day       = (int)(wjd - gregorian_to_jd(year, month, 1) + 1);

            var res = new[] { year, month, day };
            return res;
        }

        //private static bool leap_Islamic(int year)
        //{
         //   return ((year * 11) + 14 % 30) < 11;
        //}

        private static float islamic_to_jd(int year, int month, int day)
        {
            return (float)(day + Math.Ceiling(29.5 * (month - 1)) + (year - 1) * 354 +
                    Math.Floor((double)((3 + 11 * year) / 30)) + IslamicEpoch) - 1;
        }

        private static int[] jd_to_islamic(float jd)
        {
            jd        = (float) (Math.Floor(jd) + 0.5);
            var year  = (int) Math.Floor((30 * (jd - IslamicEpoch) + 10646) / 10631);
            var month = (int) Math.Min(12, Math.Ceiling((jd - (29 + islamic_to_jd(year, 1, 1))) / 29.5) + 1);
            var day   = (int) (jd - islamic_to_jd(year, month, 1) + 1);
            var res = new[] { year, month, day };
            return res;
        }
    }
}
