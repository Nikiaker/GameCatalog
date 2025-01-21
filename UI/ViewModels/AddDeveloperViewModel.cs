using CommunityToolkit.Mvvm.Input;
using Core.DictionaryProvider;
using Mackowiak.GameCatalog.BL;
using Mackowiak.GameCatalog.Core;
using Mackowiak.GameCatalog.DAO;
using Mackowiak.GameCatalog.DAO.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Mackowiak.GameCatalog.UI.ViewModels
{
    public partial class AddDeveloperViewModel : INotifyPropertyChanged
    {
        private bool isEdit = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public Developer Developer { get; set; }
        private DeveloperService developerService = new DeveloperService();

        public RelayCommand<Window> SaveDeveloperCommand { get; set; }

        public AddDeveloperViewModel()
        {
            Developer = new Developer();

            SaveDeveloperCommand = new RelayCommand<Window>(SaveDeveloper);
        }

        public AddDeveloperViewModel(Developer developer)
            : this()
        {
            this.Developer = developer;
            this.OnPropertyChanged(nameof(Developer));
            this.isEdit = true;
        }

        private void SaveDeveloper(Window window)
        {
            if (!this.isEdit)
            {
                developerService.AddDeveloper(Developer);
            }
            else
            {
                developerService.UpdateDeveloper(Developer);
            }

            window.Close();
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
