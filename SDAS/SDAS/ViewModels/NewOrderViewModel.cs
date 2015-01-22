using SDAS_Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SDAS.ViewModels
{
    public class NewOrderViewModel : ViewModelBase
    {
        private SellerViewModel ParentVM;

        public NewOrderViewModel(SellerViewModel VM)
        {
            ParentVM = VM;

            VisitWays = Global.GetInstance().ADA.GetVisitWays();
            EducationalBackgrounds = Global.GetInstance().ADA.GetEducationalBackgrounds();
            Channels = Global.GetInstance().ADA.GetChannels();
            Provinces = Global.GetInstance().ADA.GetProvinces();
        }

        public ICommand CreatedOrderCommand
        {
            get
            {
                return new BaseCommand(OnCreatedOrder);
            }
        }

        public void OnCreatedOrder()
        {
            Customer customer = new Customer();
            customer.Name = this.Name;
            customer.Sex = this.Sex;
            customer.Age = this.Age;
            customer.IDNumber = this.IDNumber;
            customer.PhoneNumber = this.PhoneNumber;

            if(!string.IsNullOrEmpty(this.ResidencePrefectureCode))
            {
                customer.Residence.Code = this.ResidencePrefectureCode;
            }
            else
            {
                customer.Residence.Code = this.ResidenceProvinceCode;
            }

            if (!string.IsNullOrEmpty(this.WorkPlacePrefectureCode))
            {
                customer.WorkPlace.Code = this.WorkPlacePrefectureCode;
            }
            else
            {
                customer.WorkPlace.Code = this.WorkPlaceProvinceCode;
            }

            customer.FamilyNumber = this.FamilyNumber;
            customer.Channel = this.Channel;
            customer.EducationalBackground = this.EducationalBackground;
            customer.VisitWay = this.VisitWay;

            customer.ID = Global.GetInstance().ADA.AddCustomer(customer);
            int orderID = Global.GetInstance().ADA.AddOrder(customer.ID, Global.GetInstance().CurrentUser.ID);
            ParentVM.ParentVM.Pagesource = "OrderPage.xaml";
        }

        private List<string> mEducationalBackgrounds;
        public List<string> EducationalBackgrounds
        {
            get
            {
                return mEducationalBackgrounds;
            }
            set
            {
                if (mEducationalBackgrounds != value)
                {
                    mEducationalBackgrounds = value;
                    RaisePropertyChanged(() => EducationalBackgrounds);
                }
            }
        }

        private List<string> mVisitWays;
        public List<string> VisitWays
        {
            get
            {
                return mVisitWays;
            }
            set
            {
                if (mVisitWays != value)
                {
                    mVisitWays = value;
                    RaisePropertyChanged(() => VisitWays);
                }
            }
        }

        private List<string> mChannels;
        public List<string> Channels
        {
            get
            {
                return mChannels;
            }
            set
            {
                if (mChannels != value)
                {
                    mChannels = value;
                    RaisePropertyChanged(() => Channels);
                }
            }
        }

        private List<Administrative> mProvinces;
        public List<Administrative> Provinces
        {
            get
            {
                return mProvinces;
            }
            set
            {
                if (mProvinces != value)
                {
                    mProvinces = value;
                    RaisePropertyChanged(() => Provinces);
                }
            }
        }

        private List<Administrative> mResidencePrefectures;
        public List<Administrative> ResidencePrefectures
        {
            get
            {
                return mResidencePrefectures;
            }
            set
            {
                if (mResidencePrefectures != value)
                {
                    mResidencePrefectures = value;
                    RaisePropertyChanged(() => ResidencePrefectures);
                }
            }
        }

        private List<Administrative> mWorkPlacePrefectures;
        public List<Administrative> WorkPlacePrefectures
        {
            get
            {
                return mWorkPlacePrefectures;
            }
            set
            {
                if (mWorkPlacePrefectures != value)
                {
                    mWorkPlacePrefectures = value;
                    RaisePropertyChanged(() => WorkPlacePrefectures);
                }
            }
        }

        private string mResidenceProvinceCode;
        public string ResidenceProvinceCode
        {
            get
            {
                return mResidenceProvinceCode;
            }
            set
            {
                mResidenceProvinceCode = value;
                ResidencePrefectures = Global.GetInstance().ADA.GetSubordinateAdministrativeByCode(mResidenceProvinceCode);
            }
        }

        private string mWorkPlaceProvinceCode;
        public string WorkPlaceProvinceCode
        {
            get
            {
                return mWorkPlaceProvinceCode;
            }
            set
            {
                mWorkPlaceProvinceCode = value;
                WorkPlacePrefectures = Global.GetInstance().ADA.GetSubordinateAdministrativeByCode(mWorkPlaceProvinceCode);
            }
        }

        public string ResidencePrefectureCode
        {
            get;
            set;
        }

        public string WorkPlacePrefectureCode
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string IDNumber
        {
            get;
            set;
        }

        public string PhoneNumber
        {
            get;
            set;
        }

        public string Sex
        {
            get;
            set;
        }

        public int Age
        {
            get;
            set;
        }

        public string EducationalBackground
        {
            get;
            set;
        }

        public int FamilyNumber
        {
            get;
            set;
        }

        public string VisitWay
        {
            get;
            set;
        }

        public string Channel
        {
            get;
            set;
        }
    }
}
