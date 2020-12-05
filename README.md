# Persia.NET Core

[![Build Status](https://github.com/nibro7778/mydiary/workflows/dotnet-core-build/badge.svg)](https://github.com/shahabfar/Persia-.NET-Core)

Persia .NET Core is a class library to convert Persian, Gregorian, and Arabic (Hijri) dates to each other. This library has been developed based on .NET Core 3.1

![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)

## Install via NuGet

To install Persia.NET Core, run the following command in the Package Manager Console:

[![NuGet version (Persia.NetCore)](https://img.shields.io/nuget/v/Persia.NetCore.svg?style=flat-square)](https://www.nuget.org/packages/Persia.NetCore/)

```
PM> Install-Package Persia.NetCore
```

You can also view the [package page](https://www.nuget.org/packages/Persia.NetCore/) on NuGet.


## Usage

#### Converting Gregorian to Shamsi

``` C#
Persia.SolarDate solarDate = Persia.Calendar.ConvertToPersian(DateTime.Now);
// getting the simple format of persian date
string str = solarDate.ToString();
```
##### SolarDate Class Properties
| Property | Type | Description |
| --- | --- | --- |
| ArrayType | Array of integers | an array of 6 elements consisting of year, month, day, hour, minute and second|
| DayOfWeek | int | day number in week|
| DaysPast  | int | days past from the beginning of Shamsi year|
| DaysRemain | int | days remain to the end of Shamsi year|
| IsLeapYear | bool | is the year is leap|


SolarDate can display the date string as follow:

| Output | ToString param |
| --- | --- |
| 1389/09/30 | ToString() |
| 89/09/30 | ToString("L") | 
| ۱۳۸۹/۹/۳۰  | ToString("D") |
| ۸۹/۹/۳۰ | ToString("d") |
| سی ام  آذر ۱۳۸۹ | ToString("F") |
| سی ام  آذر ۸۹ | ToString("f") |
| سه شنبه  ۱۳۸۹/۹/۳۰ | ToString("W") |
| سه شنبه  ۸۹/۹/۳۰ | ToString("w") |
| سه شنبه  سی ام  آذر ۱۳۸۹ | ToString("S") |
| سه شنبه  سی ام  آذر ۸۹ | ToString("s") |
| ۳۰ آذر ۱۳۸۹ | ToString("M") |
| ۳۰ آذر ۸۹ | ToString("m") |
| سه شنبه  ۳۰ آذر ۱۳۸۹ | ToString("N") |
| سه شنبه  ۳۰ آذر ۸۹ | ToString("n") |
| سه شنبه  ۳۰ آذر | ToString("g") |
| آذر ۱۳۸۹ | ToString("E") |
| آذر ۸۹ | ToString("e") |
| ۳۰ آذر | ToString("Q") |
|سی ام  آذر | ToString("q") |
|  ۱۹ : ۵۰ | ToString("H") |
|ساعت   ۱۹ : ۵۰ | ToString("R") |
| ۷ : ۵۰ بعد از ظهر | ToString("HH") |
|   ۷ : ۵۰  ب. ظ | ToString("hh") |
| ساعت   ۷ : ۵۰ بعد از ظهر | ToString("T") |
| ساعت   ۷ : ۵۰ ب. ظ | ToString("t") |

Date and Time can be displayed together:

``` C#
string str = solarDate.ToString("H,w");
```
ToRelativeDateString() is another method which display the date as the number of days past from the spesidic date. the following example:

``` C#
string str = solarDate.c ("D,5");
```
shows days past from specific date up untill 5 days. The default number of days past is 30 days.
Considering above example the complete output format of ToRelativeDateString() method are shown in table bellow.

| Output | Parameter |
| --- | --- |
| X روز پیش (پیش فرض 30 روز) | ToRelativeDateString("D") |
|  امروز| ToRelativeDateString("T") | 
| امروز، دیروز | ToRelativeDateString("Y") | 
| امروز، دیروز، x  روز پیش (پیش فرض یک هفته) | ToRelativeDateString("TY") | 
| اکنون (کمتر از پنج دقیقه، پیش فرض پنج دقیقه) | ToRelativeDateString("N") | 
| X دقیقه پیش (کمتر از 60 دقیقه، پیش فرض 60 دقیقه) | ToRelativeDateString("M") | 
|X  ساعت پیش (کمتر از 24 ساعت، پیش فرض 24 ساعت)  | ToRelativeDateString("H") | 
| کمتر از یک ساعت پیش | ToRelativeDateString("h") | 
| کمتر از یک دقیقه پیش | ToRelativeDateString("m") | 
| اکنون، x دقیقه پیش | ToRelativeDateString("n") | 
|اکنون، x دقیقه پیش، x ساعت پیش  | ToRelativeDateString("p") | 
|اکنون، x دقیقه پیش، امروز  | ToRelativeDateString("t") | 


#### Converting Shamsi to Gregorian

``` C#
DateTime ConvertToGregorian(SolarDate solarDate)
DateTime ConvertToGregorian(LunarDate lunarDate)
DateTime ConvertToGregorian(int year, int month, int day, DateType dateType)
DateTime ConvertToGregorian(int year, int month, int day, int hour, int minute, int
second, DateType dateType)
```

#### Converting Shamsi to Arabic

``` C#
LunarDate ConvertToIslamic(DateTime date)
LunarDate ConvertToIslamic(SolarDate solarDate)
LunarDate ConvertToIslamic(int year, int month, int day, DateType dateType)
```
LunarDate class has following properties:
| Property | Type | Description |
| --- | --- | --- |
| ArrayType | Array of integers | an array of 6 elements consisting of year, month, day, hour, minute and second|
| DayOfWeek | int | day number in week|
| IsLeapYear | bool | is the year is leap|

LunarDate can display the date string as follow:

| Output | ToString param |
| --- | --- |
| 1432/03/28| ToString() |
| ۲۸ /۱۴۳۲/۳ | ToString("H") | 
| ۲۸ ربیع الاول ۱۴۳۲  | ToString("M") |
|پنجشنبه  ۲۸ ربیع الاول ۱۴۳۲ | ToString("D") |
|الخمیس  ۲۸ ربیع الاول ۱۴۳۲| ToString("N") |


#### Converting Latin numbers to Persian

``` C#
String persianNumber = Persia.PersianWord.ConvertToPersianNumber("12345");
```
