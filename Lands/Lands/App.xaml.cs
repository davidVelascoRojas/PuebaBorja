namespace Lands
{
    using System;
    using Xamarin.Forms;
    using Views;
    using Domain.Models;
    using System.Collections.Generic;
    using ViewModels;
    using ViewModels.Encuestas;


    public partial class App : Application
	{
        #region Properties
        public static NavigationPage Navigator { get; internal set; }
        public static string RUTA_DB;
        public static MasterPage Master { get; internal set; }
        public static Agente AgenteLog { get; internal set; }
        #endregion

        #region Contructors
        public App(string ruta_bd)
        {
            InitializeComponent();
            RUTA_DB = ruta_bd;
            try
            {
                List<Agente> agente = new List<Agente>();
                using (var conn = new SQLite.SQLiteConnection(App.RUTA_DB))
                {
                    conn.CreateTable<Agente>();
                    agente = conn.Table<Agente>().ToList();
                    Agente agenteLog = agente[0];
                    App.AgenteLog = new Agente
                    {
                        ClaveAge = agenteLog.ClaveAge,
                        Nombre = agenteLog.Nombre,
                        Password = agenteLog.Password,
                        Servidor = agenteLog.Servidor
                    };

                    MainViewModel.GetInstance().Sucursales = new SucursalesViewModel();
                    Application.Current.MainPage = new MasterPage();
                }
            }
            catch (Exception e)
            {
                MainViewModel.GetInstance().Login = new LoginViewModel();
                MainPage = new NavigationPage(new LoginPage());
            }
        }
        #endregion

        #region Methods
        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        } 
        #endregion
    }
}
