using Mackowiak.GameCatalog.DAO.Models;
using Mackowiak.GameCatalog.UI.ViewModels;
using System.Windows;

namespace Mackowiak.GameCatalog.UI.View
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddGameWindow : Window
    {
        public AddGameWindow()
        {
            InitializeComponent();
            DataContext = new AddGameViewModel();
        }

        public AddGameWindow(Game game)
        {
            InitializeComponent();
            DataContext = new AddGameViewModel(game);
        }
    }
}
