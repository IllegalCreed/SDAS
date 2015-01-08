using SDAS_DataAdapter;
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
    public class LoginViewModel : ViewModelBase
    {
        private MainViewModel ParentVM;

        public LoginViewModel(MainViewModel VM)
        {
            ParentVM = VM;
        }

        public string UserName
        {
            get;
            set;
        }

        public string PassWord
        {
            get;
            set;
        }


        public ICommand LoginCommand
        {
            get
            {
                return new BaseCommand(OnLogin);
            }
        }

        public void OnLogin()
        {
            Global.GetInstance().CurrentUser = Global.GetInstance().ADA.Login(UserName, PassWord);
            if (Global.GetInstance().CurrentUser != null)
            {
                ParentVM.IsLoginPage = false;
                ParentVM.Pagesource = "SellerHomePage.xaml";

                ParentVM.SVM.Orders = new ObservableCollection<Order>(Global.GetInstance().ADA.GetAllOrderByUserID(Global.GetInstance().CurrentUser.ID));
            }
        }
    }
}
