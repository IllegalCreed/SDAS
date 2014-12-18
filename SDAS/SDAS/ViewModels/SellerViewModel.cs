using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAS.ViewModels
{
    public class SellerViewModel
    {
        private MainViewModel ParentVM;

        public SellerViewModel(MainViewModel VM)
        {
            ParentVM = VM;
            FVM = new FilterViewModel(this);
        }

        public FilterViewModel FVM
        {
            get;
            set;
        }
    }
}
