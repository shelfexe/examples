using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profile_directory.ViewModel;
using System.Windows.Input;
using System.Windows;
namespace Profile_directory.Commands
{
    abstract class Command : ICommand
    {
        protected ProfileVM _pvm;
        public Command(ProfileVM pvm)
        {
            _pvm = pvm;
        }
        public event EventHandler CanExecuteChanged;
        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);
    }

    class AddDirectionToUser : Command
    {
        public AddDirectionToUser(ProfileVM pvm) : base(pvm) { }
        public override bool CanExecute(object parameter)
        {

            return true;
        }
        public override void Execute(object parameter)
        {

            if (_pvm.SelectedDirection != null)
            {
                if (!_pvm.chosenDirections.Contains(_pvm.SelectedDirection, new comparerDir()))  // Если это направление еще не было выбрано, то копируем целиком
                    _pvm.chosenDirections.Add(_pvm.SelectedDirection.FullCopy());                // Иначе находим направление и переносим в все все профили 
                else                                                                             
                {
                    _pvm.chosenDirections[_pvm.chosenDirections.IndexOf(_pvm.SelectedDirection)] = _pvm.SelectedDirection.FullCopy();
                }
            }
            else if (_pvm.SelectedProfile != null)                                              // Если направление выбранного профиля уже есть и еще не имеет этого профиля, то добавляем
            {                                                                                   // Иначе, если выбранное направление не содержится, то копируем направление с пустными профилями и добавляем в них выбранный
                if (_pvm.chosenDirections.Contains(_pvm.SelectedProfile.Directions, new comparerDir()) && !_pvm.chosenDirections[_pvm.chosenDirections.IndexOf(_pvm.SelectedProfile.Directions)].Profiles.Contains(_pvm.SelectedProfile, new comparerProf()))
                    _pvm.chosenDirections[_pvm.chosenDirections.IndexOf(_pvm.SelectedProfile.Directions)].Profiles.Add(_pvm.SelectedProfile);
                else if (!_pvm.chosenDirections.Contains(_pvm.SelectedProfile.Directions, new comparerDir()))
                {
                    var dir = _pvm.SelectedProfile.Directions.Copy();
                    dir.Profiles.Add(_pvm.SelectedProfile);
                    _pvm.chosenDirections.Add(dir);
                }
            }
        }
    }

    class UpCommand : Command
    {
        public UpCommand(ProfileVM pvm) : base(pvm) { }
        public override bool CanExecute(object parameter)
        {

            return true;
        }
        public override void Execute(object parameter)
        {
            if (_pvm.ChosenSelectedDirection != null)
            {
                int index = _pvm.chosenDirections.IndexOf(_pvm.ChosenSelectedDirection);
                if (index > 0)
                {
                    var dir = _pvm.chosenDirections[index];
                    _pvm.chosenDirections[index] = _pvm.chosenDirections[index - 1];
                    _pvm.chosenDirections[index - 1] = dir;
                }
            }
            else if (_pvm.ChosenSelectedProfile != null)
            {
                int dirIndex = _pvm.chosenDirections.IndexOf(_pvm.ChosenSelectedProfile?.Directions);
                int profIndex = _pvm.chosenDirections[dirIndex].Profiles.IndexOf(_pvm.ChosenSelectedProfile);
                if (profIndex > 0)
                {
                    var prof = _pvm.chosenDirections[dirIndex].Profiles[profIndex];
                    _pvm.chosenDirections[dirIndex].Profiles[profIndex] = _pvm.chosenDirections[dirIndex].Profiles[profIndex - 1];
                    _pvm.chosenDirections[dirIndex].Profiles[profIndex - 1] = prof;
                }

            }
        }
    }

    class DownCommand : Command
    {
        public DownCommand(ProfileVM pvm) : base(pvm) { }
        public override bool CanExecute(object parameter)
        {

            return true;
        }
        public override void Execute(object parameter)
        {
            if (_pvm.ChosenSelectedDirection != null)
            {
                int index = _pvm.chosenDirections.IndexOf(_pvm.ChosenSelectedDirection);
                if (index < _pvm.chosenDirections.Count - 1)
                {
                    var dir = _pvm.chosenDirections[index];
                    _pvm.chosenDirections[index] = _pvm.chosenDirections[index + 1];
                    _pvm.chosenDirections[index + 1] = dir;
                }
            }
            else if (_pvm.ChosenSelectedProfile != null)
            {
                int dirIndex = _pvm.chosenDirections.IndexOf(_pvm.ChosenSelectedProfile?.Directions);
                int profIndex = _pvm.chosenDirections[dirIndex].Profiles.IndexOf(_pvm.ChosenSelectedProfile);
                if (profIndex < _pvm.chosenDirections[dirIndex].Profiles.Count - 1)
                {
                    var prof = _pvm.chosenDirections[dirIndex].Profiles[profIndex];
                    _pvm.chosenDirections[dirIndex].Profiles[profIndex] = _pvm.chosenDirections[dirIndex].Profiles[profIndex + 1];
                    _pvm.chosenDirections[dirIndex].Profiles[profIndex + 1] = prof;
                }

            }
        }
    }

    class ClearCommand : Command
    {
        public ClearCommand(ProfileVM pvm) : base(pvm) { }
        public override bool CanExecute(object parameter)
        {

            return true;
        }
        public override void Execute(object parameter)
        {
            _pvm.chosenDirections.Clear();
        }
    }

    class UploadCommand : Command
    {
        public UploadCommand(ProfileVM pvm) : base(pvm) { }
        public override bool CanExecute(object parameter)
        {

            return true;
        }
        public override void Execute(object parameter)
        {
            ExecuteAsync();
        }

        private async void ExecuteAsync()
        {
             if (_pvm.SelectedUser != null)
            {
                for (int i = 0; i<_pvm.chosenDirections.Count; i++)
                {
                    for (int j = 0; j<_pvm.chosenDirections[i].Profiles.Count; j++)
                    {
                        _pvm.context.chosenProfiles.Add(new ChosenProfiles
                        {
                            Priority = j + 1,
                            ProfileId = _pvm.chosenDirections[i].Profiles[j].ProfileId,
                            UserId = _pvm.SelectedUser.UserId
                        });
                    }
                        _pvm.context.chosenDirections.Add(new ChosenDirections
                    {
                        DirectionId = _pvm.chosenDirections[i].DirectionId,
                        Priority = i + 1,
                        UserId = _pvm.SelectedUser.UserId
                    });
                }
                try {
                        await _pvm.context.SaveChangesAsync();
                    }
                catch { MessageBox.Show("Ошибка при сохранении!"); }
            }
        }
    }
    public class comparerDir : IEqualityComparer<Directions>
    {
        public bool Equals(Directions x, Directions y)
        {
            return (x.Name == y.Name);
        }
        public int GetHashCode(Directions obj)
        {
            return obj.ToString().GetHashCode();
        }
    }

    public class comparerProf : IEqualityComparer<Profiles>
    {
        public bool Equals(Profiles x, Profiles y)
        {
            return (x.Name == y.Name);
        }
        public int GetHashCode(Profiles obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
