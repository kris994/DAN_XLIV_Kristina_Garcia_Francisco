using DAN_XLIV_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace DAN_XLIV_Kristina_Garcia_Francisco
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Main Window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(this);
        }
    }
}
