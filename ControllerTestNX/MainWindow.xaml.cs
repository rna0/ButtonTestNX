using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;
using Button = WpfApp1.Models.Button;

namespace ControllerTestNX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataAccess = new ItemsControl();
            AddBButton();
            AddBButton();
            AddBButton();
            AddBButton();
            AddBButton();
        }

        private void AddBButton()
        {
            DataAccess.Items.Add(new ButtonModel { Button = Button.B });
        }
    }
}