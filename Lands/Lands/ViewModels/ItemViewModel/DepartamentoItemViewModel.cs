namespace Lands.ViewModels.ItemViewModel
{
    using Domain.Models;
    using GalaSoft.MvvmLight.Command;
    using Encuestas;
    using Views.Encuentas;
    using System.Windows.Input;

    public class DepartamentoItemViewModel : Departamento
    {
        #region Commands
        public ICommand SelectDepartamentoCommand
        {
            get
            {
                return new RelayCommand(SelectDepartamento);
            }
        }
        #endregion

        private async void SelectDepartamento()
        {
            MainViewModel.GetInstance().Encuestas = new EncuentasViewModel(this);
            await App.Navigator.PushAsync(new EncuentasPage());
        }
    }
}
