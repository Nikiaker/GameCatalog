using Mackowiak.GameCatalog.BL;
using Mackowiak.GameCatalog.DAO.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Mackowiak.GameCatalog.UI.View;
using System.ComponentModel;

namespace Mackowiak.GameCatalog.UI.ViewModels
{
    public class ManageDevelopersViewModel : INotifyPropertyChanged
    {
        private DeveloperService _developerService = new DeveloperService();

        public event PropertyChangedEventHandler PropertyChanged;
        
        public string Filter { get; set; }
        public ObservableCollection<Developer> Developers { get; set; }
        public ObservableCollection<Developer> FilteredDevelopers
        {
            get
            {
                if (!string.IsNullOrEmpty(Filter))
                {
                    return new ObservableCollection<Developer>(
                        Developers.Where(g => g.Name.ToLower().Contains(Filter.ToLower())));
                }

                return Developers;
            }
        }
        public Developer SelectedDeveloper { get; set; }

        public ICommand AddDeveloperCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand EditDeveloperCommand { get; set; }
        public ICommand RemoveDeveloperCommand { get; set; }

        public ManageDevelopersViewModel()
        {
            this.LoadDevelopers();
            this.InitializeCommands();
        }

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void LoadDevelopers()
        {
            // Pobranie produktów z bazy danych
            this._developerService = new DeveloperService();
            var developers = _developerService.GetAllDevelopers();
            this.Developers = new ObservableCollection<Developer>(developers);
            this.OnPropertyChanged(nameof(this.Developers));
            this.OnPropertyChanged(nameof(this.FilteredDevelopers));
        }

        private void InitializeCommands()
        {
            this.AddDeveloperCommand = new RelayCommand(this.AddDeveloper);
            this.AddDeveloperCommand = new RelayCommand(this.AddDeveloper);
            this.SearchCommand = new RelayCommand(this.Search);
            this.EditDeveloperCommand = new RelayCommand(this.EditDeveloper);
            this.RemoveDeveloperCommand = new RelayCommand(this.RemoveDeveloper);
        }

        private void AddDeveloper()
        {
            var window = new AddDeveloperWindow();
            window.ShowDialog();
            this.LoadDevelopers();
        }

        private void Search()
        {
            // Wyszukiwanie gry
            this.OnPropertyChanged(nameof(this.FilteredDevelopers));
        }

        private void EditDeveloper()
        {
            if (this.SelectedDeveloper != null)
            {
                var window = new AddDeveloperWindow(this.SelectedDeveloper);
                window.ShowDialog();
            }
        }

        private void RemoveDeveloper()
        {
            if (this.SelectedDeveloper != null)
            {
                this._developerService.RemoveDeveloper(this.SelectedDeveloper.Id);
                this.LoadDevelopers();
            }
        }
    }
}