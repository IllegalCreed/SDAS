using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAS.ViewModels
{
    public class OrderViewModel
    {
        private SellerViewModel ParentVM;

        public OrderViewModel(SellerViewModel VM)
        {
            ParentVM = VM;
        }
    }
}
