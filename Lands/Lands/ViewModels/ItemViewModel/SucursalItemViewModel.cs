namespace Lands.ViewModels.ItemViewModel
{
    using Domain.Models;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Encuestas;
    using Views.Encuentas;

    public class SucursalItemViewModel : Sucursal
    {
        #region Commands
        public ICommand SelectSucursalCommand
        {
            get
            {
                return new RelayCommand(SelectSucursal);
            }
        }
        #endregion

        private async void SelectSucursal()
        {
            MainViewModel.GetInstance().Departamentos = new DepartamentosViewModel(this);
            await App.Navigator.PushAsync(new DepartamentosPage());
        }
    }
}
