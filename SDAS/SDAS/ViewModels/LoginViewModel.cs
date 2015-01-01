using SDAS_DataAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SDAS.ViewModels
{
    public class LoginViewModel
    {
        private MainViewModel ParentVM;

        public LoginViewModel(MainViewModel VM)
        {
            ParentVM = VM;
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


            ParentVM.IsLoginPage = false;
            ParentVM.Pagesource = "SellerHomePage.xaml";
        }
    }
}
