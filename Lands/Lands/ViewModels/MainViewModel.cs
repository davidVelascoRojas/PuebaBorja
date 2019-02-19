namespace Lands.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Domain.Models;
    using Encuestas;
    public class MainViewModel
    {
        #region Prpperties
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }
        public List<Departamento> ListDepartamentos { get; set; }
        public List<Pregunta> ListEncuestas { get; set; }
        public List<Sucursal> ListSucursales { get; set; }
        #endregion

        #region ViewModels
        public LoginViewModel Login { get; set; }
        public DepartamentosViewModel Departamentos { get; set; }
        public EncuentasViewModel Encuestas { get; set; }
        public SucursalesViewModel Sucursales { get; set; }
        #endregion

        #region Constructos
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            LoadMenu();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion

        #region Methods
        private void LoadMenu()
        {
            this.Menus = new ObservableCollection<MenuItemViewModel>();
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "salir",
                Title = "Salir",
                PageName = "LoginPage"
            });
        }
        #endregion
    }
}

