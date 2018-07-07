using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Share;
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

        private async void btnProperties_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ClassesPage());
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg",
                SaveToAlbum = true
            });

            if (file == null)
                return;
            
            img.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entryUrl.Text))
                return;

            var compressionQuality = int.Parse(entryUrl.Text);

            if (!CrossMedia.IsSupported)
                return;

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
                return;

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions()
            {
                CompressionQuality = compressionQuality,
                SaveMetaData = true
            });

            if (file == null)
                return;
            
            img.Source = ImageSource.FromFile(file.Path);

            var st = file.GetStream();
            await DisplayAlert("Size", $"Size is {st.Length}", "OK");
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            if (!CrossConnectivity.IsSupported)
                return;

            if (CrossConnectivity.Current.IsConnected)
            {
                var types = CrossConnectivity.Current.ConnectionTypes;
                
            }

            if (!CrossShare.IsSupported)
                return;

            CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage()
            {
                Title = "Przepis na mięso",
                Text = "[X] Sól i pieprz\n[ ] Mięso\n[ ] Sos"
            });
        }
    }
}
