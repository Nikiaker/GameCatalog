using Mackowiak.GameCatalog.BL;
using Mackowiak.GameCatalog.DAO;
using Mackowiak.GameCatalog.DAO.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Mackowiak.GameCatalog.UI.View;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace Mackowiak.GameCatalog.UI.ViewModels
{
    public class ManageDevelopersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DeveloperService developerService = new DeveloperService();

        public ICommand AddDeveloperCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand EditDeveloperCommand { get; set; }
        public ICommand RemoveDeveloperCommand { get; set; }

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

        public ManageDevelopersViewModel()
        {
            // Pobranie produktów z bazy danych
            LoadDevelopers();

            InitializeCommands();
        }

        private void LoadDevelopers()
        {
            // Pobranie produktów z bazy danych
            this.developerService = new DeveloperService();
            var developers = developerService.GetAllDevelopers();
            this.Developers = new ObservableCollection<Developer>(developers);
            OnPropertyChanged(nameof(Developers));
            OnPropertyChanged(nameof(FilteredDevelopers));
        }

        private void InitializeCommands()
        {
            AddDeveloperCommand = new RelayCommand(AddDeveloper);
            AddDeveloperCommand = new RelayCommand(AddDeveloper);
            SearchCommand = new RelayCommand(Search);
            EditDeveloperCommand = new RelayCommand(EditDeveloper);
            RemoveDeveloperCommand = new RelayCommand(RemoveDeveloper);
        }

        private void AddDeveloper()
        {
            // Otwórz okno dodawania produktu
            var window = new AddDeveloperWindow();
            window.ShowDialog();
            LoadDevelopers();
        }

        private void Search()
        {
            // Wyszukiwanie gry
            OnPropertyChanged(nameof(FilteredDevelopers));
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
                developerService.RemoveDeveloper(this.SelectedDeveloper.Id);
                LoadDevelopers();
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}