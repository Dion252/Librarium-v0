using Librarium.Classes;
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
using Librarium.DataBase;

namespace Librarium.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            var userAuth = AppData.Context.Employee.ToList().
               Where(i => i.Login == loginField.Text && i.Password == passwordField.Text).
               FirstOrDefault();
            if (userAuth != null)
            {
                NavigationService.Navigate(new SelectPage());
            }
            else
            {
                MessageBox.Show("Пользователя с такими данными не существует!");
            }
        }

     
    }
}
