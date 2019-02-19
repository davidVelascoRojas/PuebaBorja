namespace Lands.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Views;
    using Xamarin.Forms;
    using Domain.Models;

    public class MenuItemViewModel
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }

        #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }
        #endregion

        #region Methods
        private void Navigate()
        {
            App.Master.IsPresented = false;

            if (this.PageName == "LoginPage")
            {
                using (var conn = new SQLite.SQLiteConnection(App.RUTA_DB))
                {
                    conn.CreateTable<Agente>();
                    conn.Query<Agente>("delete from Agente");
                }
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else if(this.PageName == "")
            {

            }
        }
        #endregion
    }
}
