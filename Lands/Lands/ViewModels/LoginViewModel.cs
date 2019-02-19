namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Services;
    using Domain.Models;
    using Views;
    using Xamarin.Forms;
    using Acr.UserDialogs;
    using System;

    public class LoginViewModel : BaseViewModel
    {
        #region Services
        private readonly ApiService apiService;
        #endregion

        #region Attributes
        private string agente;
        private string password;
        private string servidor;
        private bool isRunning;
        private bool isRemembered;
        private bool isEnabled;
        #endregion

        #region Properties
        public string Agente { get { return agente; } set { SetValue(ref agente, value); } }
        public string Password { get { return password; } set { SetValue(ref password, value); } }
        public string Servidor { get { return servidor; } set { SetValue(ref servidor, value); } }
        public bool IsRunning { get { return isRunning; } set { SetValue(ref isRunning, value); } }
        public bool IsRemembered { get { return isRemembered; } set { SetValue(ref isRemembered, value); } }
        public bool IsEnabled { get { return isEnabled; } set { SetValue(ref isEnabled, value); } }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();

            this.IsRemembered = true;
            this.IsEnabled = true;

            this.Agente = "age1";
            this.Password = "123";
            this.Servidor = "107.161.187.210:1220";
        }
        #endregion

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Agente))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Ingresa tu clave de Agente",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Ingresa tu Password",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Servidor))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Ingresa tu Servidor",
                    "Aceptar");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Aceptar");
                return;
            }


            Agente agente = new Agente();
            agente.ClaveAge = this.Agente;
            agente.Password = this.Password;

            var resp = await this.apiService.GetTransaction<Agente>(
                "http://" + this.Servidor,
                "/Api",
                "/Agente",
                agente);

            if (!resp.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    resp.Message,
                    "Aceptar");
                return;
            }

            agente = (Agente)resp.Result;

            if (!agente.Status)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    agente.Message,
                    "Aceptar");
                return;
            }

            agente.Password = this.Password;
            agente.Servidor = this.Servidor;
            if (this.IsRemembered)
            {
                this.Guarda(agente);
            }

            App.AgenteLog = (Agente)agente;

            MainViewModel.GetInstance().Sucursales = new Encuestas.SucursalesViewModel();
            Application.Current.MainPage = new MasterPage();

            this.IsRunning = false;
            this.IsEnabled = true;
            this.Agente = string.Empty;
            this.Password = string.Empty;
            this.Servidor = string.Empty;

        }

        private void Guarda(Agente agente)
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(App.RUTA_DB))
                {
                    conn.CreateTable<Agente>();
                    conn.Query<Agente>("delete from Agente");
                    conn.Insert(agente);
                }
                UserDialogs.Instance.Toast("Agente Guardado", TimeSpan.FromMilliseconds(2000));
            }
            catch (Exception)
            {

                UserDialogs.Instance.Toast("Error al guardar Agente", TimeSpan.FromMilliseconds(2000));
            }
        }

    }
}
