using cloud.Core;
using cloud.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace cloud.MVVM.ViewModel
{
    class LoginViewModel : ObservableObject
    {

        private MainViewModel _mainViewModel;

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _alertlogin;

        public string AlertLogin
        {
            get { 
                return _alertlogin;
            }
            set {
                _alertlogin = value;
                OnPropertyChanged(nameof(AlertLogin));
            }
        }

        public RelayCommand LoginCommand { get; set; }


        public LoginViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            LoginCommand = new RelayCommand(Login);
        }

        private void Login(object parameter)
        {

            using CloudFileContext context = new CloudFileContext();

            var user = context.Users.FirstOrDefault( u => u.Username == Username  );

            if ( user != null )
            {
                if ( user.Password == Password )
                {
                    Password = null;
                    Username = null;
                    _mainViewModel.Userid = user.Id;
                    _mainViewModel.Isloged = true;
                    _mainViewModel.ShowLoginView.Execute(null); 
                }
                else
                {
                    AlertLogin = "Podaj poprawne hasło";
                }
            }
            else
            {
                AlertLogin = "Podaj poprawny login";
            }
        }
    }

}
