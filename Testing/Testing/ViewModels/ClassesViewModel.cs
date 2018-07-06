using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Testing.Model.Sqlite;

namespace Testing.ViewModels
{
    public class ClassesViewModel : BaseViewModel
    {
        public KeyValuePair<string, string> Test { get; set; }
        public ObservableCollection<Class> Classes { get; set; }

        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged(nameof(Title));
                }
            }
        }

        public ClassesViewModel()
        {
            Title = "Classes";
            Test = new KeyValuePair<string, string>("klucz", "wartość");
            Classes = new ObservableCollection<Class>();
            Task.Run(async () => await Init());
        }

        private async Task Init()
        {
            var classes = await App.LocalDB.GetItems<Class>();
            foreach (var c in classes)
            {
                await Task.Delay(1000);
                Classes.Add(c);
            }
            await Task.Delay(3000);
            Classes.RemoveAt(2);
        }
    }
}
