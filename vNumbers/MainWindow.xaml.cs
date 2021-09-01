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
            Task.Run(() => new IncomingController().DoWork());
            InitializeComponent();
        }
    }
}
