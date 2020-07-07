using DAN_XLIV_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace DAN_XLIV_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for Worker.xaml
    /// </summary>
    public partial class Worker : Window
    {
        /// <summary>
        /// Worker window
        /// </summary>
        public Worker()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(this);
        }
    }
}
