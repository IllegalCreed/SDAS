using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAS_Model
{
    public class Administrative:ViewModelBase
    {
        private string mCode;
        public string Code
        {
            get
            {
                return mCode;
            }
            set
            {
                if (mCode != value)
                {
                    mCode = value;
                    RaisePropertyChanged(() => Code);
                }
            }
        }

        private string mName;
        public string Name
        {
            get
            {
                return mName;
            }
            set
            {
                if (mName != value)
                {
                    mName = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }
    }
}
