using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace learnWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void singlePlayerBtn_Click(object sender, RoutedEventArgs e)
        {
            //this.Hide();
            Window4 win4 = new Window4();
            //win2.ShowDialog();
            win4.Show();
            this.Close();
            
        }

        private void multiPlayerBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
