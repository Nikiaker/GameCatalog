using Mackowiak.GameCatalog.DAO.Models;
using Mackowiak.GameCatalog.UI.ViewModels;
using System.Windows;

namespace Mackowiak.GameCatalog.UI.View
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddDeveloperWindow : Window
    {
        public AddDeveloperWindow()
        {
            InitializeComponent();
            DataContext = new AddDeveloperViewModel();
        }

        public AddDeveloperWindow(Developer developer)
        {
            InitializeComponent();
            DataContext = new AddDeveloperViewModel(developer);
        }
    }
}
