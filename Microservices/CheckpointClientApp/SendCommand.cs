using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;
using System.Net;
using System.Net.Http;

namespace CheckpointClientApp
{
    class SendCommand : ICommand
    {
        protected ViewModel _vm;
        private HttpClient client;
        
        public SendCommand(ViewModel vm)
        {
            _vm = vm;
            client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object arg)
        {
            return true;
        }

        public void Execute(object arg)
        {
           
            Task.Run(() => ExecuteAsync());
        }

        public async void ExecuteAsync()
        {
       
             client.BaseAddress = new Uri("https://localhost:5001");
             var stringContent = new StringContent($"{{ \"checkpointId\": \"{_vm.model._checkpointId}\", \"driverId\": \"{_vm.model._driverId}\" }}", 
                                                                                                      System.Text.Encoding.UTF8, "application/json");
             var response = await client.PostAsync("/CheckpointConnection/send", stringContent);
             string str = await response.Content.ReadAsStringAsync();
             //MessageBox.Show(response.StatusCode.ToString());
             //MessageBox.Show(str);
            
        }
    }

   public class ViewModel : INotifyPropertyChanged
    {
        public Model model;

        public ViewModel ()
        {
            this.model = new Model() ;
        }
        public string CheckpointId
        {
            get { return model._checkpointId.ToString(); }
            set
            {  
                if (Int32.TryParse(value, out model._checkpointId)) OnPropertyChanged("CheckpointId");
                else MessageBox.Show("Номер пропускного пункта имеет недопустимое значение! ");
            }
        }
        public string DriverId
        {
            get { return model._driverId.ToString(); }
            set
            {
                if (Int32.TryParse(value, out model._driverId)) OnPropertyChanged("DriverId");
                else MessageBox.Show("Идентификатор водителя имеет недопустимое значение! ");
            }
        }
        private ICommand _sendCommand;
        
        public ICommand SendCommand
        {
            get
            {
                if (_sendCommand == null) _sendCommand = new SendCommand(this);
                return _sendCommand;
            }
           
        } 
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Model
    {
        public int _checkpointId;
        public int _driverId;
       
    }

  
}
