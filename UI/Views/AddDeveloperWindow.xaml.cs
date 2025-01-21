using Mackowiak.GameCatalog.DAO.Models;
using Mackowiak.GameCatalog.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
