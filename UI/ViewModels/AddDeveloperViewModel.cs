using CommunityToolkit.Mvvm.Input;
using Mackowiak.GameCatalog.BL;
using Mackowiak.GameCatalog.DAO.Models;
using System.ComponentModel;
using System.Windows;

namespace Mackowiak.GameCatalog.UI.ViewModels
{
    public partial class AddDeveloperViewModel : INotifyPropertyChanged
    {
        private bool _isEdit = false;
        private DeveloperService _developerService = new DeveloperService();

        public event PropertyChangedEventHandler PropertyChanged;

        public Developer Developer { get; set; }

        public RelayCommand<Window> SaveDeveloperCommand { get; set; }

        public AddDeveloperViewModel()
        {
            this.Developer = new Developer();

            this.SaveDeveloperCommand = new RelayCommand<Window>(this.SaveDeveloper);
        }

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public AddDeveloperViewModel(Developer developer)
            : this()
        {
            this.Developer = developer;
            this.OnPropertyChanged(nameof(this.Developer));
            this._isEdit = true;
        }

        private void SaveDeveloper(Window window)
        {
            if (!this._isEdit)
            {
                _developerService.AddDeveloper(this.Developer);
            }
            else
            {
                _developerService.UpdateDeveloper(this.Developer);
            }

            window.Close();
        }
    }
}
