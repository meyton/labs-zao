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
	public partial class EditStudentPage : ContentPage
	{
        private Student _student;
		public EditStudentPage (Student student)
		{
            _student = student;
			InitializeComponent ();
            lblStudent.Text = $"Editing: {student.FirstName} {student.LastName}";
            entryAlbum.Text = student.AlbumNumber.ToString();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var albumNumber = int.Parse(entryAlbum.Text);

            _student.AlbumNumber = albumNumber;

            await App.LocalDB.SaveItem(_student);
            await DisplayAlert("OK", "Zmiany zostały zapisane", "OK");
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (await DisplayAlert("Warning", "Czy na pewno chcesz usunąć studenta z bazy?", "TAK", "NIE"))
            {
                await App.LocalDB.DeleteItem(_student);
                await Navigation.PopAsync();
            }
        }
    }
}