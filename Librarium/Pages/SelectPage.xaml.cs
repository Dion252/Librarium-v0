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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Librarium.Pages
{
    /// <summary>
    /// Логика взаимодействия для SelectPage.xaml
    /// </summary>
    public partial class SelectPage : Page
    {
        public SelectPage()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            SecondFrame.Content = new BooksList();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            SecondFrame.Content = new ReaderList();
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            SecondFrame.Content = new RentalList();
        }
    }
}
