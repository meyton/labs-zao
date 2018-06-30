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
	public partial class AddStudentPage : ContentPage
	{
        private Class _class;
		public AddStudentPage(Class selectedClass)
		{
            _class = selectedClass;
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (entryFirstName == null || string.IsNullOrWhiteSpace(entryFirstName.Text))
            {
                await DisplayAlert("Błąd", "Nie wszystkie pola zostały wypełnione", "OK");
                return;
            }

            var firstName = entryFirstName.Text;
            var lastName = entryLastName.Text;
            var albumNumber = int.Parse(entryAlbum.Text);

            var newStudent = new Student()
            {
                FirstName = firstName,
                LastName = lastName,
                AlbumNumber = albumNumber,
                ClassID = _class.ID
            };

            await App.LocalDB.SaveItem(newStudent);
            await DisplayAlert("Sukces", "Dane zostały zapisane", "OK");
            await Navigation.PopAsync();
        }
    }
}