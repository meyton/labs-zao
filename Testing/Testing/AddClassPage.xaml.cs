using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.Model.Sqlite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Testing
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddClassPage : ContentPage
	{
		public AddClassPage ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (entryName == null || string.IsNullOrWhiteSpace(entryName.Text))
            {
                await DisplayAlert("Błąd", "Nie wszystkie pola zostały wypełnione", "OK");
                return;
            }

            var className = entryName.Text;

            var newClass = new Class() { Name = className };

            await App.LocalDB.SaveItem(newClass);
            await DisplayAlert("Sukces", "Dane zostały zapisane", "OK");
            await Navigation.PopAsync();
        }
    }
}