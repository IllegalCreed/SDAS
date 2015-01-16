using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAS_Model
{
    public class Customer : ViewModelBase
    {
        public int ID;

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

        private string mSex;
        public string Sex
        {
            get
            {
                return mSex;
            }
            set
            {
                if (mSex != value)
                {
                    mSex = value;
                    RaisePropertyChanged(() => Sex);
                }
            }
        }

        private int mAge;
        public int Age
        {
            get
            {
                return mAge;
            }
            set
            {
                if (mAge != value)
                {
                    mAge = value;
                    RaisePropertyChanged(() => Age);
                }
            }
        }

        private string mIDNumber;
        public string IDNumber
        {
            get
            {
                return mIDNumber;
            }
            set
            {
                if (mIDNumber != value)
                {
                    mIDNumber = value;
                    RaisePropertyChanged(() => IDNumber);
                }
            }
        }

        private string mPhoneNumber;
        public string PhoneNumber
        {
            get
            {
                return mPhoneNumber;
            }
            set
            {
                if (mPhoneNumber != value)
                {
                    mPhoneNumber = value;
                    RaisePropertyChanged(() => PhoneNumber);
                }
            }
        }

        public string Residence;
        public string WorkPlace;
        public int FamilyNumber;
        public string Channel;
        public string EducationalBackground;
        public string VisitWay;
    }
}
