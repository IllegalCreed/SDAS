using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAS.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            LVM = new LoginViewModel(this);
            SVM = new SellerViewModel(this);
        }


        private bool mIsLoginPage = true;
        /// <summary>
        /// 是否为登录页
        /// </summary>
        public bool IsLoginPage
        {
            get
            {
                return mIsLoginPage;
            }
            set
            {
                if (value != mIsLoginPage)
                {
                    mIsLoginPage = value;
                    RaisePropertyChanged(() => IsLoginPage);
                }
            }
        }

        private string mPageSource;
        public string Pagesource
        {
            get
            {
                return mPageSource;
            }
            set
            {
                if (value != mPageSource)
                {
                    mPageSource = value;
                    RaisePropertyChanged(() => Pagesource);
                }
            }
        }

        public LoginViewModel LVM
        {
            get;
            set;
        }

        public SellerViewModel SVM
        {
            get;
            set;
        }
    }
}
