using System.Windows;

namespace TothBence_torpedo
{
    /// <summary>
    /// Interaction logic for P2P.xaml
    /// </summary>
    public partial class P2P : Window
    {
        public P2P()
        {
            InitializeComponent();
            P1.Focus();
        }

        private void Start_Button_Clicked(object sender, RoutedEventArgs e)
        {
            if (P1.Text == "" || P2.Text == "")
            {
                MessageBox.Show("Missing name!");
                return;
            }
            else if (P1.Text == "AI" || P2.Text == "AI")
            {
                MessageBox.Show("Cannot use 'AI' as name!");
                return;
            }
            else
            {
                ShipSetup shipSetup = new ShipSetup(P1.Text, P2.Text);
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

        private void P1_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                P2.Focus();
            }
        }

        private void P2_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Start_Button_Clicked(sender, e);
            }
        }
    }
}
