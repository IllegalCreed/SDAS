using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAS.ViewModels
{
    public class FilterViewModel : ViewModelBase
    {
        private SellerViewModel ParentVM;

        public FilterViewModel(SellerViewModel VM)
        {
            ParentVM = VM;
        }

        private bool mIsDetailFilter = false;
        /// <summary>
        /// 是否为详细筛选
        /// </summary>
        public bool IsDetailFilter
        {
            get
            {
                return mIsDetailFilter;
            }
            set
            {
                if (value != mIsDetailFilter)
                {
                    mIsDetailFilter = value;
                    RaisePropertyChanged(() => IsDetailFilter);
                }
            }
        }
    }
}
