namespace Lands.ViewModels.Encuestas
{
    using Lands.Services;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Domain.Models;
    using ItemViewModel;
    using Xamarin.Forms;
    using System.Collections.Generic;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public class SucursalesViewModel : BaseViewModel
    {
        #region Services
        private readonly ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<SucursalItemViewModel> sucursales;
        private bool isRefreshing;
        #endregion

        #region Properties
        public ObservableCollection<SucursalItemViewModel> Sucursales
        {
            get { return this.sucursales; }
            set { SetValue(ref this.sucursales, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Constructors
        public SucursalesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadSucursales();
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadSucursales);
            }
        }
        #endregion

        #region Methods
        private async void LoadSucursales()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Aceptar");
                //await Application.Current.MainPage.Navigation.PopAsync();
                await App.Navigator.PopAsync();
                this.IsRefreshing = false;
                return;
            }

            Catalogos catalogos = new Catalogos();

            var response = await this.apiService.GetTransaction<Catalogos>(
                "http://" + App.AgenteLog.Servidor,
                "/api",
                "/Sucursales",
                catalogos);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                await App.Navigator.PopAsync();
                this.IsRefreshing = false;
                return;
            }

            catalogos = (Catalogos)response.Result;

            if (!catalogos.Status)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    catalogos.Message,
                    "Aceptar");
                this.IsRefreshing = false;
                return;
            }

            MainViewModel.GetInstance().ListSucursales = (List<Sucursal>)catalogos.ListSucursal;
            this.Sucursales = new ObservableCollection<SucursalItemViewModel>(ToSucursalItemViewModel());
            this.IsRefreshing = false;
        }

        private IEnumerable<SucursalItemViewModel> ToSucursalItemViewModel()
        {
            return MainViewModel.GetInstance().ListSucursales.Select(S => new SucursalItemViewModel
            {
                ClaveSuc = S.ClaveSuc,
                Descripcion = S.Descripcion
            });
        }

        #endregion


    }
}
