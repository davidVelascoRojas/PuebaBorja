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

    public class DepartamentosViewModel : BaseViewModel
    {
        #region Services
        private readonly ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<DepartamentoItemViewModel> departamentos;
        private Sucursal sucursal;
        private bool isRefreshing;
        #endregion

        #region Properties
        public ObservableCollection<DepartamentoItemViewModel> Departamentos
        {
            get { return this.departamentos; }
            set { SetValue(ref this.departamentos, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Constructors
        public DepartamentosViewModel(Sucursal sucursal)
        {
            this.sucursal = sucursal;
            this.apiService = new ApiService();
            this.LoadDepartamentos();
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadDepartamentos);
            }
        }
        #endregion

        #region Methods
        private async void LoadDepartamentos()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Aceptar");
                await App.Navigator.PopAsync();
                this.IsRefreshing = false;
                return;
            }

            Catalogos catalogos = new Catalogos();
            catalogos.Departamento.ClaveSuc = this.sucursal.ClaveSuc;
            var response = await this.apiService.GetTransaction<Catalogos>(
                "http://" + App.AgenteLog.Servidor,
                "/api",
                "/Departamentos",
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

            MainViewModel.GetInstance().ListDepartamentos = (List<Departamento>)catalogos.ListDepartamento;
            this.Departamentos = new ObservableCollection<DepartamentoItemViewModel>(ToDepartamentoItemViewModel());
            this.IsRefreshing = false;
        }

        private IEnumerable<DepartamentoItemViewModel> ToDepartamentoItemViewModel()
        {
            return MainViewModel.GetInstance().ListDepartamentos.Select(S => new DepartamentoItemViewModel
            {
                ClaveDep = S.ClaveDep,
                ClaveSuc = S.ClaveSuc,
                Descripcion = S.Descripcion
            });
        }

        #endregion

    }
}
