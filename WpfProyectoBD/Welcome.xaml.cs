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

namespace WpfProyectoBD
{
    /// <summary>
    /// Lógica de interacción para Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void btnCERRAR_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnVISTAUSUARIO_Click(object sender, RoutedEventArgs e)
        {
            UserWindow usrWin = new UserWindow();
            usrWin.Show();
            this.Close();
        }
    }
}
