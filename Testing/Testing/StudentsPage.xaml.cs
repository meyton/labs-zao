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
	public partial class StudentsPage : ContentPage
	{
        private Class _class;
		public StudentsPage(Class selectedClass)
		{
            _class = selectedClass;
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            lvItems.ItemsSource = await App.LocalDB.GetStudentsByClassID(_class.ID);
            lvItems.ItemTapped -= LvItems_ItemTapped;
            lvItems.ItemTapped += LvItems_ItemTapped;
        }

        private async void LvItems_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var student = (Student)e.Item;
            await Navigation.PushAsync(new EditStudentPage(student));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddStudentPage(_class));
        }
    }
}