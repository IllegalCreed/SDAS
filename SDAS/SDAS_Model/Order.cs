using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAS_Model
{
    public class Order : ViewModelBase
    {
        public Order()
        {
            Customer = new Customer();
            Offer = new Offer();
            Verify = new Verify();
            Contract = new Contract();
            PropertyRights = new PropertyRights();
            Saler = new User();
            VisitLogs = new List<VisitLog>();
        }

        private int mID;
        public int ID
        {
            get
            {
                return mID;
            }
            set
            {
                if (mID != value)
                {
                    mID = value;
                    RaisePropertyChanged(() => ID);
                }
            }
        }

        private Customer mCustomer;
        public Customer Customer
        {
            get
            {
                return mCustomer;
            }
            set
            {
                if (mCustomer != value)
                {
                    mCustomer = value;
                    RaisePropertyChanged(() => Customer);
                }
            }
        }

        public string State;
        public Offer Offer;
        public Verify Verify;
        public Contract Contract;
        public DateTime LoanDate;
        public DateTime CheckInDate;
        public PropertyRights PropertyRights;
        public User Saler;
        public List<VisitLog> VisitLogs;
    }
}
