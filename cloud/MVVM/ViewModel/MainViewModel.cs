using cloud.Core;
using cloud.MVVM.Model;
using System.Windows;

namespace cloud.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand MoveWindowCommand { get; set; }
        public RelayCommand ShutdownWindowCommand { get; set; }
        public RelayCommand MaximizeWindowCommand { get; set; }
        public RelayCommand MinimizeWindowCommand { get; set; }
        public RelayCommand ShowFileView { get; set; }
        public RelayCommand ShowSettingsView { get; set; }
        public RelayCommand ShowLoginView { get; set; }
        public RelayCommand ShowRegisterView { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoginButtonChecked;
        public bool IsLoginButtonChecked
        {
            get { return _isLoginButtonChecked; }
            set
            {
                _isLoginButtonChecked = value;
                OnPropertyChanged();
            }
        }


        private bool _isloged;
        public bool Isloged
        {
            get
            {
                return _isloged;
            }
            set
            {
                _isloged = value;
                OnPropertyChanged();
            }
        }

        private int _userid;
        public int Userid
        {
            get
            {
                return _userid;
            }
            set
            {
                _userid = value;
                OnPropertyChanged();
            }
        }

        private string _nameoflog = "Zaloguj się";

        public string NameofLog
        {
            get 
            { 
                return _nameoflog;
            }
            set 
            { 
                _nameoflog = value; 
                OnPropertyChanged(nameof(NameofLog));
            }
        }


        //public void Islogin( bool islogin)
        //{
        //    Isloged = islogin;
        //}

        public SettingsViewModel SettingsVM { get; set; }
        public LoginViewModel LoginVM { get; set; }
        public FileViewModel FileVM { get; set; }
        public RegisterViewModel RegisterVM { get; set; }
        public UserViewModel UserVM { get; set; }
        public FileUserViewModel FileUserVM { get; set; }

        public MainViewModel()
        {

            FileVM = new FileViewModel();
            SettingsVM = new SettingsViewModel();
            LoginVM = new LoginViewModel(this);
            RegisterVM = new RegisterViewModel(this);
            CurrentView = FileVM;

            Application.Current.MainWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            MoveWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.DragMove(); });
            ShutdownWindowCommand = new RelayCommand(o => { Application.Current.Shutdown(); });
            MaximizeWindowCommand = new RelayCommand(o => 
            { 
                if(Application.Current.MainWindow.WindowState == WindowState.Maximized)
                {
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                }
                else
                {
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
                }
            });
            MinimizeWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.WindowState = WindowState.Minimized; });


            ShowFileView = new RelayCommand(o => 
            {
                if (!Isloged)
                {
                    CurrentView = FileVM;
                }
                else
                {
                    FileUserVM = new FileUserViewModel(Userid);
                    CurrentView = FileUserVM;
                }
            });
            ShowSettingsView = new RelayCommand(o => { CurrentView = SettingsVM; });
            ShowLoginView = new RelayCommand(o =>
            {
                if ( !Isloged )
                {
                    NameofLog = "Zaloguj się";
                    CurrentView = LoginVM;
                }
                else
                {
                    NameofLog = "Konto";
                    UserVM = new UserViewModel( Userid,this ); 
                    CurrentView = UserVM;
                    //MessageBox.Show(Userid.ToString());
                }
            });
            ShowRegisterView = new RelayCommand(o => { CurrentView =  RegisterVM; });

        }
    }
}
