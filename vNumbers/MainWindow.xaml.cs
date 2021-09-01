using System.Threading.Tasks;
using System.Windows;

namespace vNumbers
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            IncomingController controller = new IncomingController();
            Task.Run(() => controller.DoWork());
            InitializeComponent();
        }
    }
}
