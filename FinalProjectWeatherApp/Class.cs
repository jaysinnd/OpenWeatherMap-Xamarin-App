using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FinalProjectWeatherApp
{
    public class Class
    {
        public string Title { get; set; } = " ";
        public string Temperature { get; set; } = " ";
        public string Wind { get; set; } = " ";
        public string Temp_min { get; set; } = " ";
        public string Temp_max { get; set; } = " ";
        public string Description { get; set; } = " ";
        public string MainWeather { get; set; } = " ";
        public string Icon { get; set; }
        public int ListCnt { get; set; }
    }
}