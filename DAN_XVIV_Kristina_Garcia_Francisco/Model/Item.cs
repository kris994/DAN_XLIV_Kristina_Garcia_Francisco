using DAN_XVIV_Kristina_Garcia_Francisco.ViewModel;

namespace DAN_XVIV_Kristina_Garcia_Francisco.Model
{
    public partial class tblItem : BaseViewModel
    {
        Service service = new Service();

        /// <summary>
        /// Item amount
        /// </summary>
        private int amount;
        public int Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }
    }
}
