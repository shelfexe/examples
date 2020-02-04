using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using Profile_directory.Commands;
namespace Profile_directory.ViewModel
{
    class ProfileVM : INotifyPropertyChanged
    {
        public ProfileVM()
        {
            context = new Models.ProfilesContext();
            if (!context.Database.Exists())
            {
                MessageBox.Show("Не удалось подключиться к базе данных!");
                Environment.Exit(0);
            }
            users = new ObservableCollection<Users>(context.users.ToList());
            profiles = new ObservableCollection<Profiles>(context.profiles.ToList());
            directions = new ObservableCollection<Directions>(context.directions.ToList());
            
            chosenDirections = new ObservableCollection<Directions>();
        }

        public Models.ProfilesContext context;
        public ObservableCollection<Users> users { get; }
        public ObservableCollection<Profiles> profiles { get; } 
        public ObservableCollection<Directions> directions { get; }
        public ObservableCollection<Directions> chosenDirections { get; set; }

        private Users _selectedUser;
        private Profiles _selectedProfile;
        private Directions _selectedDirection;
        private Profiles _chosenSelectedProfile;
        private Directions _chosenSelectedDirection;
        private ICommand _addDirectionToUser;
        private ICommand _upCommand;
        private ICommand _downCommand;
        private ICommand _clearCommand;
        private ICommand _uploadCommand;
        public ICommand AddDirectionToUser
        {
            get
            {
                if (_addDirectionToUser == null)
                {
                    _addDirectionToUser = new AddDirectionToUser(this);
                }
                return _addDirectionToUser;
            }
        }
        public ICommand UpCommand
        {
            get
            {
                if (_upCommand == null)
                {
                    _upCommand = new UpCommand(this);
                }
                return _upCommand;
            }
        }

        public ICommand DownCommand
        {
            get
            {
                if (_downCommand == null)
                {
                    _downCommand = new DownCommand(this);
                }
                return _downCommand;
            }
        }

        public ICommand ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                {
                    _clearCommand = new ClearCommand(this);
                }
                return _clearCommand;
            }
        }
        public ICommand UploadCommand
        {
            get
            {
                if (_uploadCommand == null)
                {
                    _uploadCommand = new UploadCommand(this);
                }
                return _uploadCommand;
            }
        }
        public Users SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }
        public Directions SelectedDirection
        {
            get { return _selectedDirection; }
            set
            {
                _selectedDirection = value;
                _selectedProfile = null;
                OnPropertyChanged("SelectedDirection");
            }
        }
        public Profiles SelectedProfile
        {
            get { return _selectedProfile; }
            set
            {
                _selectedProfile = value;
                _selectedDirection = null;
                OnPropertyChanged("SelectedProfile"); 
            }
        }

        public Directions ChosenSelectedDirection
        {
            get { return _chosenSelectedDirection; }
            set
            {
                _chosenSelectedDirection = value;
                _chosenSelectedProfile = null;
                OnPropertyChanged("ChosenSelectedDirection");
            }
        }

        public Profiles ChosenSelectedProfile
        {
            get { return _chosenSelectedProfile; }
            set
            {
                _chosenSelectedProfile = value;
                _chosenSelectedDirection = null;
                OnPropertyChanged("ChosenSelectedProfile");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
