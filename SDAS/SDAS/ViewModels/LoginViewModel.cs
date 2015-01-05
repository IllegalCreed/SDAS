using SDAS_DataAdapter;
using System;
using System.Collections.Generic;
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
            AccessDataAdapter ADA = new AccessDataAdapter("..\\..\\..\\DB\\SDAS.accdb");
            if (ADA.Login(UserName, PassWord))
            {
                ParentVM.IsLoginPage = false;
                ParentVM.Pagesource = "SellerHomePage.xaml";
            }
        }
    }
}
