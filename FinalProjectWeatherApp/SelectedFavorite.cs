using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using System.Net;
using System;

namespace FinalProjectWeatherApp
{
    [Activity(Label = "SelectedFavorite")]
    public class SelectedFavorite : Activity
    {
        public string imageString = "http://openweathermap.org/img/w/";
        public string imgString2 = ".png";
        public string iconWebAddress;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SelectFavorite);
            // Create your application here
            GetFavorite(); //call our method to make another API call based on the selected item from our ListActivity

            Button button = FindViewById<Button>(Resource.Id.returnHome); //calls a method to start an Intent to return to Main Activity
            button.Click += ReturnHome;
        }
        private async void GetFavorite()
        {
            Class newClass = await APICall.GetClass(FavoriteList.favoriteZip.Trim());
            if (newClass != null)
            {
                //pretty much the same as main activity. 
                FindViewById<TextView>(Resource.Id.favCity).Text = newClass.Title;
                FindViewById<TextView>(Resource.Id.favMainWeather).Text = newClass.MainWeather;
                FindViewById<TextView>(Resource.Id.favDescription).Text = newClass.Description;
                FindViewById<TextView>(Resource.Id.favTemp).Text = newClass.Temperature;
                FindViewById<TextView>(Resource.Id.favTempMin).Text = newClass.Temp_min;
                FindViewById<TextView>(Resource.Id.favTempMax).Text = newClass.Temp_max;
                ImageView iView = FindViewById<ImageView>(Resource.Id.favImageView);
                iconWebAddress = imageString + newClass.Icon + imgString2;
                var image = GetImage(iconWebAddress);
                iView.SetImageBitmap(image);

            }
        }
        private Bitmap GetImage(string iconWebAddress)
        {
            Bitmap image = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(iconWebAddress);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    image = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }
            return image;
        }
        private void ReturnHome(object sender, EventArgs e)
        {
            var returnHome = new Intent(this, typeof(MainActivity));
            StartActivity(returnHome); //tied to a button click, takes us back to main activity
        }
    }
}