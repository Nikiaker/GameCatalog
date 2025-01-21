using CommunityToolkit.Mvvm.Input;
using Core.DictionaryProvider;
using Mackowiak.GameCatalog.BL;
using Mackowiak.GameCatalog.Core;
using Mackowiak.GameCatalog.DAO;
using Mackowiak.GameCatalog.DAO.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Mackowiak.GameCatalog.UI.ViewModels
{
    public partial class AddDeveloperViewModel
    {
        public Developer Developer { get; set; }
        private DeveloperService developerService = new DeveloperService();

        public RelayCommand<Window> SaveDeveloperCommand { get; set; }

        public AddDeveloperViewModel()
        {
            Developer = new Developer();

            SaveDeveloperCommand = new RelayCommand<Window>(SaveDeveloper);
        }

        private void SaveDeveloper(Window window)
        {
            developerService.AddDeveloper(Developer);
            window.Close();
        }
    }
}
