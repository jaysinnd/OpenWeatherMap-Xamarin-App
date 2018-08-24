using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Net;
using Android.Graphics;
using Android.Content;

namespace FinalProjectWeatherApp
{
    /// <summary>
    /// Developer:              Jason Dean
    /// Date:                   7/20/2018
    /// Application:            Weather App
    /// Purpose:                Using the Openweathermap.org free API, we allow the user to input a zipcode
    ///                         that will then return the current weather conditions for that area based on zipcode
    ///                         entered. We also give the user the ability to add favorite cities that they can then click on
    ///                         and it wil automatically pull the current weather information and display it to the user.
    /// </summary>
    [Activity(Label = "Weather App", MainLauncher = true)]
    public class MainActivity : Activity
    {
        //list of public variables we will use
        public static int listCounter = 0;
        public static string favoriteZip;
        public string currentZip;
        public string currentCity;
        public string item = "item";
        public string zipCode = "Zip";
        public string imageString = "http://openweathermap.org/img/w/";
        public string imgString2 = ".png";
        public string iconWebAddress;
        ISharedPreferences favoriteCities = Application.Context.GetSharedPreferences("MyFavorites", FileCreationMode.Private);
        // added from Favorites
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource

            //set instances of our buttons tied with button click methods
            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.getZipCode);
            
            button.Click += Button_Click;

            Button favButton = FindViewById<Button>(Resource.Id.addToFavs);
            favButton.Click += Favorites;
            
            Button goToFavList = FindViewById<Button>(Resource.Id.goToFavs);
            goToFavList.Click += FavoritesList;
            
        }
        private async void Button_Click(object sender, EventArgs e)
        {
            EditText getZip = FindViewById<EditText>(Resource.Id.theZipCode);
            Toast.MakeText(this, "Please wait while your City is found", ToastLength.Short).Show();
            if (!String.IsNullOrEmpty(getZip.Text))
            {
                Class newClass = await APICall.GetClass(getZip.Text); //call our API class where the magic happens!
                if (newClass != null)
                {
                    //set the returned data from the class to our text fields in our Main axml file
                    FindViewById<TextView>(Resource.Id.txtCity).Text = newClass.Title;
                    FindViewById<TextView>(Resource.Id.txtMainWeather).Text = newClass.MainWeather;
                    FindViewById<TextView>(Resource.Id.txtDescription).Text = newClass.Description;
                    FindViewById<TextView>(Resource.Id.txtTemp).Text = newClass.Temperature;
                    FindViewById<TextView>(Resource.Id.txtTempMin).Text = newClass.Temp_min;
                    FindViewById<TextView>(Resource.Id.txtTempMax).Text = newClass.Temp_max;
                    ImageView imgView = FindViewById<ImageView>(Resource.Id.imageView);
                    iconWebAddress = imageString + newClass.Icon + imgString2;
                    var image = GetImage(iconWebAddress);
                    imgView.SetImageBitmap(image);
                    currentCity = newClass.Title; //set our local variable to that of our Class Value. to be used later.
                }
            }
            else
            {
                Toast.MakeText(this, "You must enter a zip code", ToastLength.Short).Show();
            }
            currentZip = getZip.Text;
            getZip.Text = "";

        }

        private void Favorites(object sender, EventArgs e)
        {
            //instead of using an IF statement to loop through this over and over, 
            //we will set an int variable to 0, and increment it each time a user adds a favorite.
            //this will be a public static int so that we can pass it to the next activity without problems
            //thus, we can also use it to set the length of how many items are in the string. Magic ;)
            TextView favCity = FindViewById<TextView>(Resource.Id.txtCity);
            if (!String.IsNullOrEmpty(favCity.Text))
            {
                string items = item + listCounter.ToString(); //another concantenated string that we will use to set our values. (no long IF statements)
                string zipper = zipCode + listCounter.ToString();//same here.
                ISharedPreferencesEditor cityEdit = favoriteCities.Edit();
                cityEdit.PutString(items, currentCity + " " + currentZip);
                cityEdit.PutString(zipper, currentZip);
                cityEdit.Apply();
                Toast.MakeText(this, FindViewById<TextView>(Resource.Id.txtCity).Text + " Added To Favorites List", ToastLength.Short).Show();
                listCounter++; //increment by 1 each time, giving us a count in the next activity, and 
                FavClass fClass = new FavClass();
                fClass.ItemNumber = listCounter;
            }
            else
            {
                Toast.MakeText(this, "You must enter a zip code to find a city to add", ToastLength.Short).Show();
            }   

        }
        private void FavoritesList(object sender, EventArgs e)
        {
            var viewFavorites = new Intent(this, typeof(FavoriteList));
            StartActivity(viewFavorites); //tied to a button click, takes us to the FavoritesList activity to allow users to view their favorites
        }
        private void SaveCity()
        {
          

        }
        //METHOD FOR CREATING IMAGE ICON-------------------*****
        private Bitmap GetImage(string iconWebAddress)
        {
            //here we use the Bitmap function to read the data for an image
            //we will pull the image down from the API call using a web address and concantenating the ID string
            //returned from the API, stored in our Class, and use it to decipher the image.
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
    }
    
}

