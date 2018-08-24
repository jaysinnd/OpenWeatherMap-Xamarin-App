using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FinalProjectWeatherApp
{
    [Activity(Label = "Favorite List")]
    public class FavoriteList : ListActivity
    {
        ISharedPreferences favoriteCities = Application.Context.GetSharedPreferences("MyFavorites", FileCreationMode.Private);
        string[] items = new string[MainActivity.listCounter]; //list of items used later
        public static string favoriteZip;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FavoriteLayout);

            // Create your application here

           //protected local variables we will need to help count through the list iterations and set string variables
            int countItems = 0;
            int positionItems = 0;
            
            foreach(var item in items) //iterate through our list of items and set the correct values
            {
                string getString = "item" + countItems.ToString(); //set the variable
                items[positionItems] = favoriteCities.GetString(getString, null);
                countItems++;
                positionItems++; //add one to our protected variables
            }

            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);
        
        }
        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            string selectCity = items[position]; //set the selected position as a string variable to use later to get usable zip
            favoriteZip = selectCity.Split()[1]; //split the string stored as a favorite, and using '1' to get the Right-side of the string, to use as a search zipcode
            Toast.MakeText(this, favoriteZip.Trim(), ToastLength.Short).Show();
            var viewSelectedFavorite = new Intent(this, typeof(SelectedFavorite)); 
            StartActivity(viewSelectedFavorite); //start new Activity that shows our selected favorite city
        }


    }

}