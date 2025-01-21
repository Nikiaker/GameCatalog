using System.Windows;

namespace Mackowiak.GameCatalog.UI
{
    public partial class ManageDevelopersWindow : Window
    {
        public ManageDevelopersWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.ManageDevelopersViewModel();
        }
    }
}