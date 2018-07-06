using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Testing.Utils
{
    public class NavigationService
    {
        public static async Task NavigateTo(Page page)
        {
            await Application.Current.MainPage.FadeTo(0.2, 1000);
            await Application.Current.MainPage.Navigation.PushAsync(page);
            await Application.Current.MainPage.FadeTo(1.0, 500);
        }

        public static async Task Pop()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
