using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;

namespace TothBence_torpedo
{
    /// <summary>
    /// Interaction logic for HS.xaml
    /// </summary>
    public partial class HS : Window
    {
        StreamReader scoresFile = null;

        List<Score> scoresList /*= scoresFile*/;

        public HS()
        {
            /*
            using (scoresFile = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("scores.xml")))
            {

            }
            */
            InitializeComponent();
        }

        private void AllScore_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Top10Score_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
