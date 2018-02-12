
namespace Exchange.ViewModels
{
    using Exchange.Models;
    using GalaSoft.MvvmLight.Command;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using System.ComponentModel;
    using System.Collections.Generic;
    using Exchange.Helpers;
    using Exchange.Services;
    using System;
    using System.Threading.Tasks;

    public class MainViewModel : INotifyPropertyChanged
    {
        #region Events        
         
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Serivces
        ApiService apiService;
        DialogService dialogSevice;
        DataService dataService;

        #endregion
        #region Attributes

        bool _isRunning;
        bool _isEnabled;
        string _result;
        Rate _sourceRate;
        string _status;
        Rate _targetRate;
        List<Rate> rates;

        ObservableCollection<Rate> _rates;
        #endregion


        #region Properties
        public string Amount
        {
            get;
            set;
        }
        public ObservableCollection<Rate> Rates
        {
            get
            {
                return _rates;
            }
            set
            {
                if (_rates != value)
                {
                    _rates = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Rates)));
                }
            }
        }
        public Rate SourceRate
        {
            get
            {
                return _sourceRate;
            }
            set
            {
                if (_sourceRate != value)
                {
                    _sourceRate = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(SourceRate)));
                }
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Status)));
                }
            }
        }
        public Rate TargetRate
        {
            get
            {
                return _targetRate;
            }
            set
            {
                if (_targetRate != value)
                {
                    _targetRate = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(TargetRate)));
                }
            }
        }
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }
        public string Result
        {
            get
            {
                return _result;
            }
            set
            {
                if (_result != value)
                {
                    _result = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Result)));
                }
            }
        }

        #endregion

        #region Constructors
        
        public MainViewModel()
        {
            //Siempre instanciamos los servicios
            //en el constructor que vayamos a usar
            apiService = new ApiService();
            dialogSevice = new DialogService();
            dataService = new DataService();
            LoadRates();
        }

        #endregion

        #region Methods

        async void LoadRates()
        {
            IsRunning = true;
            Result = Lenguages.Loading;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                LoadLocalData();
                /*IsRunning = false;
                Result = connection.Message;
                return;*/
            }
            else
            {
                await LoadDataFromAPI();
            }

            //enviamos a apiService la direccion y el controlador

            if (rates.Count == 0)
            {
                IsRunning = false;
                IsEnabled = false;
                Result = Lenguages.Erro_internet_db+
                    "Please try againa with internet connection";
                Status = Lenguages.Erro_internet_db;
                return;
            }

            Rates = new ObservableCollection<Rate>(rates);
            IsRunning = false;
            IsEnabled = true;
            Result = Lenguages.Ready;
            //Status = Lenguages.Rate_loaded_internet;

          /* try
            {
                //creamos objeto de Microsoft.net.http

                var cliente = new HttpClient();
                cliente.BaseAddress = new 
                    Uri("");
                var controller = "/api/Rates";
                //solicitamos informacion de la solicitud
                var response = await cliente.GetAsync(controller);
                //leemos el resultado
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    IsRunning = false;
                    Result = result;
                }
                //convertimos 
                var rates = JsonConvert.DeserializeObject<List<Rate>>(result);
                Rates = new ObservableCollection<Rate>(rates);

                IsEnabled = true;
                IsRunning = false;
                Result = Lenguages.Rate_loaded_internet;
            }
            catch (Exception ex)
            {
                //si falla conexion entra en este modo
                //y deja de mostrar el ActivityIndicator
                //muestra el mensaje

                IsRunning = false;
                IsEnabled = true;
                Result = ex.Message;
            }*/
        }

        private void LoadLocalData()
        {
            rates = dataService.Get<Rate>(false);
            Status = Lenguages.Rate_loaded_localdata;

        }

        async Task LoadDataFromAPI()
        {
            var url = "http://apiexchangerates.azurewebsites.net"; //Application.Current.Resources["URLAPI"].ToString(); 
            var response = await apiService.GetList<Rate>(
                url,
                "/api/Rates");

            //si la respuesta es negativa
            if (!response.IsSuccess)
            {
                LoadLocalData();
                return;
            }

            rates = (List<Rate>)response.Result;
            //si la respuesta es positiva
            //se crea una lista con el resultado,
            // response.Result  devuelve un objeto tipo lista, por lot anto,
            //hay que castear la respuesta con (List<Rate>)
            dataService.DeleteAll<Rate>();
            dataService.Save(rates);
            Status = Lenguages.Rate_loaded_internet;
        }

        #endregion

        #region Commands

        public ICommand SwitchCommand

        {
            get
            {
                return new RelayCommand(Switch);
            }
        }

        void Switch()
        {
            var aux = SourceRate;
            SourceRate = TargetRate;
            TargetRate = aux;
            Convert();
        }

        public ICommand ConvertCommand
         {
            
            get
            {
                 return new RelayCommand(Convert);
             }
          }


         async void Convert()
         {

            if (string.IsNullOrEmpty(Amount))
            {
                await dialogSevice.ShowMessage(
                    Lenguages.Error,
                    Lenguages.AmountValidation);
                return;
            }

            decimal amount = 0;
            if (!decimal.TryParse(Amount, out amount))
            {
                await dialogSevice.ShowMessage(
                    Lenguages.Error,
                    Lenguages.AmountNumericValidation);
                return;
            }

            if (SourceRate == null)
            {
                await dialogSevice.ShowMessage(
                    Lenguages.Error,
                    Lenguages.SourceRateValidation);
                return;
            }

            if (TargetRate == null)
            {
                await dialogSevice.ShowMessage(
                    Lenguages.Error,
                    Lenguages.TargetRateValidation);
                return;
            }

            var amountConverted = amount /
                                  (decimal)SourceRate.TaxRate * 
                                  (decimal)TargetRate.TaxRate;

            Result = string.Format(
                "{0} {1:C2} = {2} {3:C2}",
                SourceRate.Code,
                amount,
                TargetRate.Code,
                amountConverted);

        } 
        #endregion
    }
}
