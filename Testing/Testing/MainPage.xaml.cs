using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.Data;
using Xamarin.Forms;

namespace Testing
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
        }

        internal async void btnCheckUrl_Clicked(object sender, EventArgs e)
        {
           var url = entryUrl.Text;
           await Navigation.PushAsync(new HttpClientPage(url));
        }

        internal async void btnOpenUrl_Clicked(object sender, EventArgs e)
        {
            var url = entryUrl.Text;

            Properties.AppProperties["webUrl"] = url;
            await Application.Current.SavePropertiesAsync();

            if (await DisplayAlert("Czy na pewno?", "Czy na pewno chcesz otworzyć stronę? " + url, "TAK", "NIE"))
            {
                await Navigation.PushAsync(new WebViewPage(url));
            }
            else
            {
                await DisplayAlert("Dobrze", "Nie otwieram strony", "OK");
            }
        }

        private async void btnProperties_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PropertiesPage());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClassesPage());
        }
    }
}
