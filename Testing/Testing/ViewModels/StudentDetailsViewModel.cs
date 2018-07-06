using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Testing.Model.Sqlite;
using Testing.Utils;
using Xamarin.Forms;

namespace Testing.ViewModels
{
    public class StudentDetailsViewModel : BaseViewModel
    {
        public Command<string> SaveStudentCommand { get; private set; }

        private int _studentId;
        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    RaisePropertyChanged(nameof(FirstName));
                }
            }
        }

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    RaisePropertyChanged(nameof(LastName));
                }
            }
        }

        private int _albumNumber;

        public int AlbumNumber
        {
            get => _albumNumber;
            set
            {
                if (_albumNumber != value)
                {
                    _albumNumber = value;
                    RaisePropertyChanged(nameof(AlbumNumber));
                }
            }
        }

        public StudentDetailsViewModel()
        {
            _studentId = 1;
            FirstName = string.Empty;
            LastName = string.Empty;
            SaveStudentCommand = new Command<string>(async (s) => await SaveStudent(s));
            Init();
        }

        private async Task SaveStudent(string s)
        {
            var student = new Student()
            {
                FirstName = FirstName,
                LastName = LastName,
                AlbumNumber = AlbumNumber,
                ClassID = 1
            };

            if (s == "UPDATE")
            {
                student.ID = _studentId;
            }

            await App.LocalDB.SaveItem(student);

            await AlertService.DisplayAlert("Sukces", "Dane zostały zapisane");
            await NavigationService.Pop();
        }

        private async void Init()
        {
            await Task.Delay(1000);
            var student = await App.LocalDB.GetItemByID<Student>(_studentId);
            FirstName = student.FirstName;
            LastName = student.LastName;
            AlbumNumber = student.AlbumNumber;
        }
    }
}
