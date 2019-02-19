namespace Lands.ViewModels.Encuestas
{
    using System;
    using Lands.Services;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Domain.Models;
    using ItemViewModel;
    using Xamarin.Forms;
    using System.Collections.Generic;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public class EncuentasViewModel : BaseViewModel
    {
        #region Services
        private readonly ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<EncuestaItemViewModel> encuestas;
        private bool isRefreshing;
        private Departamento departamento;
        #endregion

        #region Properties
        public ObservableCollection<EncuestaItemViewModel> Encuestas
        {
            get { return this.encuestas; }
            set { SetValue(ref this.encuestas, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Constructors
        public EncuentasViewModel(Departamento departamento)
        {
            this.departamento = departamento;
            this.apiService = new ApiService();
            this.LoadEncuestas();
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadEncuestas);
            }
        }
        public ICommand GuardarCommand
        {
            get
            {
                return new RelayCommand(Guardar);
            }
        }
        #endregion

        #region Methods
        private async void Guardar()
        {
            this.IsRefreshing = true;
            Encuesta encuesta = new Encuesta();
            encuesta.Encabezado.ClaveAge = App.AgenteLog.ClaveAge;
            encuesta.Encabezado.ClaveSuc = departamento.ClaveSuc;
            encuesta.Encabezado.Fecha = DateTime.Now;
            List<Partida> listPartidas = new List<Partida>();
            Partida partida;
            foreach (var part in Encuestas)
            {
                partida = new Partida();
                partida.ClaveDep = part.ClaveDep;
                partida.ClavePre = part.ClavePre;
                bool resp = part.Respuesta;
                if (resp) { partida.Respuesta = "S"; }
                else partida.Respuesta = "N";
                listPartidas.Add(partida);
            }
            encuesta.Partidas = listPartidas;



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

            var response = await this.apiService.GetTransaction<Encuesta>(
                "http://" + App.AgenteLog.Servidor,
                "/api",
                "/Encuesta",
                encuesta);

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

            encuesta = (Encuesta)response.Result;

            await Application.Current.MainPage.DisplayAlert(
                    "Aviso",
                    encuesta.Message,
                    "Aceptar");

            this.IsRefreshing = false;

            LoadEncuestas();
        }

        private async void LoadEncuestas()
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
            catalogos.Pregunta.ClaveSuc = this.departamento.ClaveSuc;
            catalogos.Pregunta.ClaveDep = this.departamento.ClaveDep;
            var response = await this.apiService.GetTransaction<Catalogos>(
                "http://" + App.AgenteLog.Servidor,
                "/api",
                "/Preguntas",
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

            MainViewModel.GetInstance().ListEncuestas = (List<Pregunta>)catalogos.ListPregunta;
            this.Encuestas = new ObservableCollection<EncuestaItemViewModel>(ToEncuestaItemViewModel());
            this.IsRefreshing = false;
        }

        private IEnumerable<EncuestaItemViewModel> ToEncuestaItemViewModel()
        {
            return MainViewModel.GetInstance().ListEncuestas.Select(S => new EncuestaItemViewModel
            {
                ClaveDep = S.ClaveDep,
                ClaveSuc = S.ClaveSuc,
                ClavePre = S.ClavePre,
                Descripcion = S.Descripcion
            });
        }

        #endregion
    }
}
