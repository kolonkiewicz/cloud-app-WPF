using cloud.Core;
using cloud.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace cloud.MVVM.ViewModel
{
    class UserViewModel : ObservableObject
    {

        private string _username;

        public string Username
        {
            get 
            { 
                return _username; 
            }
            set
            { 
                _username = value; 
                OnPropertyChanged(nameof(Username));    
            }
        }

        private string _name;
        public string Name
        {
            get 
            { 
                return _name; 
            }
            set
            { 
                _name = value; 
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _surname;
        public string Surname
        {
            get 
            { 
                return _surname; 
            }
            set
            { 
                _surname = value; 
                OnPropertyChanged(nameof(Surname));
            }
        }

        private string _password;

        public string Password
        {
            get 
            { 
                return _password;
            }
            set 
            { 
                _password = value; 
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _email;

        public string Email
        {
            get
            { 
                return _email; 
            }
            set
            { 
                _email = value; 
                OnPropertyChanged(nameof(Email));   
            }
        }

        private int _id;

        public int ID
        {
            get 
            { 
                return _id; 
            }
            set 
            { 
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _alertwelcome;

        public string AlertWelcome
        {
            get
            { 
                return _alertwelcome; 
            }
            set 
            { 
                _alertwelcome = value;
                OnPropertyChanged();
            }
        }


        private MainViewModel _mainViewModel;
        public RelayCommand Loqout { get; set; }

        public UserViewModel( int id, MainViewModel mainViewModel) {
            
            ID = id;
            FIlltext();
            _mainViewModel = mainViewModel;

            Loqout = new RelayCommand( o => 
            { 
                _mainViewModel.Isloged = false;
                _mainViewModel.ShowLoginView.Execute(null);
            });

        }

        public void FIlltext()
        {
            using CloudFileContext context = new CloudFileContext();

            var user = context.Users.Where(u => u.Id == ID).FirstOrDefault();

            if ( user != null )
            {
                AlertWelcome = "Witaj, " + user.Name + "!!!" ; 
                Username = user.Username;
                Name = user.Name;
                Surname = user.Surname;
                Email = user.Email;
            }

            Password = Randompass();
        }

        public string Randompass()
        {
            string randompass = "" ;

            Random rand = new Random();

            int randomnumber = rand.Next( 4, 11 );

            for ( int i = 0; i < randomnumber; i++  ){
                randompass += "*";
            }

            return randompass;
        }
        
    }
}
