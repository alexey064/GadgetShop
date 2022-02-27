using DesktopClient.Model;
using DesktopClient.other;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DesktopClient.ViewModel
{
    class LogInViewModel : INotifyPropertyChanged
    {
        private LogInModel _loginModel;
        private string _ErrorMessage;
        private ICommand _clod;

        public string ErrorMessage 
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        public LogInModel logindata {
            get { return _loginModel; }                
            set {
                _loginModel = value;
                OnPropertyChanged("logindata");
            }}
        private ICommand _loginCommand;
        public ICommand loginCommand 
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand(param => { LogIn(param); });
                }
                return _loginCommand;
            }
        }   
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public async Task LogIn(object param) 
        {
            LogInModel model = (LogInModel)param;
            bool result = await NetworkManager.getInstance().LogIn(model);
            if (result)
            {
                foreach (Window item in Application.Current.Windows)
                {
                    if (item.DataContext == this) item.Close();
                }
            }
            else {
                ErrorMessage = "не правильно введен логин или пароль";
            }
        }
    }
}