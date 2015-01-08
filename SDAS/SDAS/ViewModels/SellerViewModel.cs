using SDAS_Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SDAS.ViewModels
{
    public class SellerViewModel:ViewModelBase
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

        private ObservableCollection<Order> mOrders;
        public ObservableCollection<Order> Orders
        {
            get
            {
                return mOrders;
            }
            set
            {
                if (mOrders != value)
                {
                    mOrders = value;
                    RaisePropertyChanged(() => Orders);
                }
            }
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
