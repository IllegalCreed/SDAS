using SDAS.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SDAS.Views
{
    /// <summary>
    /// NewOrderPage.xaml 的交互逻辑
    /// </summary>
    public partial class NewOrderPage : Page
    {
        public NewOrderPage()
        {
            InitializeComponent();
        }

        public ICommand GoBackCommand
        {
            get
            {
                return new BaseCommand(OnGoBack);
            }
        }

        public void OnGoBack()
        {
            this.NavigationService.GoBack();
        }
    }
}
