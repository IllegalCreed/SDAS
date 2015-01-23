using SDAS.Views;
using SDAS_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAS.ViewModels
{
    public class OrderViewModel:ViewModelBase
    {
        private SellerViewModel ParentVM;

        public OrderViewModel(SellerViewModel VM)
        {
            ParentVM = VM;

            List<string> states = Global.GetInstance().ADA.GetStates();
            foreach (string item in states)
            {
                ProgressNode node = new ProgressNode()
                {
                    Name = item
                };
                States.Add(node);
            }
        }

        private Order mCurrentOrder;
        public Order CurrentOrder
        {
            get
            {
                return mCurrentOrder;
            }
            set
            {
                if (value != mCurrentOrder)
                {
                    mCurrentOrder = value;
                    RaisePropertyChanged(() => CurrentOrder);
                }
            }
        }

        private ProgressNodeCollection mStates = new ProgressNodeCollection();
        public ProgressNodeCollection States
        {
            get
            {
                return mStates;
            }
            set
            {
                if (value != mStates)
                {
                    mStates = value;
                    RaisePropertyChanged(() => States);
                }
            }
        }

        private int mCurrentState = 1;
        public int CurrentState
        {
            get
            {
                return mCurrentState;
            }
            set
            {
                if (value != mCurrentState)
                {
                    mCurrentState = value;
                    RaisePropertyChanged(() => CurrentState);
                }
            }
        }
    }
}
