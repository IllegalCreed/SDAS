using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SDAS.ViewModels
{
    public class SellerViewModel
    {
        public MainViewModel ParentVM;

        public SellerViewModel(MainViewModel VM)
        {
            ParentVM = VM;
            FVM = new FilterViewModel(this);
            NOVM = new NewOrderViewModel(this);
            OVM = new OrderViewModel(this);
        }

        public FilterViewModel FVM
        {
            get;
            set;
        }

        public NewOrderViewModel NOVM
        {
            get;
            set;
        }

        public OrderViewModel OVM
        {
            get;
            set;
        }

        public ICommand CreateOrderCommand
        {
            get
            {
                return new BaseCommand(OnCreateOrder);
            }
        }

        public void OnCreateOrder()
        {
            ParentVM.Pagesource = "NewOrderPage.xaml";
        }
    }
}
