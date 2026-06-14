using cloud.Core;
using cloud.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace cloud.MVVM.ViewModel
{

    class RegisterViewModel : ObservableObject
    {
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

        private string _ppassword;
        public string PPassword
        {
            get { return _ppassword; }
            set
            {
                _ppassword = value;
                OnPropertyChanged(nameof(PPassword));
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

        private bool _check;
        public bool Check
        {
            get
            {
                return _check;
            }
            set
            {
                _check = value;
                OnPropertyChanged(nameof(Check));
            }
        }

        private string _alertregister;
        public string AlertRegister
        {
            get
            {
                return _alertregister;
            }
            set
            {
                _alertregister = value;
                OnPropertyChanged(nameof(AlertRegister));
            }
        }





        public RelayCommand RegisterCommand { get; set; }

        private MainViewModel _mainViewModel;

        public RegisterViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            RegisterCommand = new RelayCommand(Register);
        }

        private void Register(object parameter)
        {
            if (Name == null || Surname == null || Email == null || PPassword == null || Password == null || Username == null)
            {
                AlertRegister = "Uzupełnij wszystkie pola";
                return;
            }

            if (Password != PPassword)
            {
                AlertRegister = "Hasła muszą być takie same";
                return;
            }

            

            if (!IsValidEmail(Email))
            {
                AlertRegister = "Podaj poprawnie e-mail";
                return;
            }

            //string passwordHash = BCrypt.Net.BCrypt.HashPassword(Password);

            if (Check == true)
            {
                using CloudFileContext context = new CloudFileContext();

                User user = new User()
                {
                    Name = this.Name,
                    Surname = this.Surname,
                    Username = this.Username,
                    Password = this.Password,
                    Email = this.Email

                };
                try
                {
                    
                    context.Add(user);
                    context.SaveChanges();

                    _mainViewModel.ShowLoginView.Execute(null);



                }
                catch (Exception ex)
                {
                    AlertRegister = "Wystapil bład: " + ex + ", sprobuj ponownie";
                }

            }
            else
            {
                AlertRegister = "Potwierdź regulamin";
                return;
            }
        }


        private static bool IsValidEmail(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }

        
    }



}

