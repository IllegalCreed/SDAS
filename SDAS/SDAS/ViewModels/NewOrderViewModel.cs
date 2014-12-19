using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SDAS.ViewModels
{
    public class NewOrderViewModel : ViewModelBase
    {
        private SellerViewModel ParentVM;

        public NewOrderViewModel(SellerViewModel VM)
        {
            ParentVM = VM;
        }

        public ICommand CreatedOrderCommand
        {
            get
            {
                return new BaseCommand(OnCreatedOrder);
            }
        }

        public void OnCreatedOrder()
        {
            ParentVM.ParentVM.Pagesource = "OrderPage.xaml";
        }
    }
}
