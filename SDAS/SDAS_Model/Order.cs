using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAS_Model
{
    public class Order
    {
        public int ID;
        public Customer Customer;
        public string State;
        public Offer Offer;
        public Verify Verify;
        public DateTime LoanDate;
        public DateTime CheckInDate;
        public PropertyRights PropertyRights;
        public Saler Saler;
        public List<VisitLog> VisitLogs;
    }
}
