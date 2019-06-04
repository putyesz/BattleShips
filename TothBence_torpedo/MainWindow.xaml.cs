using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace TothBence_torpedo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XmlTextReader xml = new XmlTextReader("scores.xml");

        public MainWindow()
        {
            using (xml)
            {

            }
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(!(sender is Button))
            {
                throw new ArgumentException("Sender must be button type!");
            }

            Button senderButton = (Button)sender;
            switch (senderButton.Tag.ToString())
            {
                case "P2IA":
                    P2AI p2AI = new P2AI();
                    this.Close();
                    p2AI.Show();
                    break;

                case "P2P":
                    P2P p2p = new P2P();
                    this.Close();
                    p2p.Show();
                    break;

                case "HS":
                    HS hs = new HS();
                    hs.Show();
                    break;

                default:
                    throw new Exception("Something went wrong!");
            }
        }
    }
}
