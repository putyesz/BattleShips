using System.Windows;
using System.Windows.Input;

namespace TothBence_torpedo
{
    /// <summary>
    /// Interaction logic for P2AI.xaml
    /// </summary>
    public partial class P2AI : Window
    {
        public P2AI()
        {
            InitializeComponent();
            P1.Focus();
        }

        private void Start_Button_Clicked(object sender, RoutedEventArgs e)
        {
            if (P1.Text == "")
            {
                MessageBox.Show("Missing name!");
                return;
            }
            else if (P1.Text == "AI")
            {
                MessageBox.Show("Cannot use 'AI' as name!");
                return;
            }
            else
            {
                ShipSetup shipSetup = new ShipSetup(P1.Text, "AI");
                shipSetup.Show();
                this.Close();
            }
        }

        private void Back_Button_Clicked(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void Enter_Key(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Start_Button_Clicked(sender, e);
            }
        }
    }
}
