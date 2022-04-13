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
using Librarium.Classes;
using Librarium.DataBase;
using Librarium.Windows;

namespace Librarium.Pages
{
    /// <summary>
    /// Логика взаимодействия для DeliverList.xaml
    /// </summary>
    public partial class RentalList : Page
    {
        List<BookRental> rentList = new List<BookRental>();

        List<string> listSort = new List<string>() { "По умолчанию", "По фамилии читателя", "По имени читателя", "По названию книги" };

        public RentalList()
        {
            InitializeComponent();
            comboBox.ItemsSource = listSort;
            comboBox.SelectedItem = 0;
            Filtration();
        }

        

        private void Filtration()
        {
            rentList = AppData.Context.BookRental.ToList();
            rentList = rentList.
                            Where(i => i.Reader.LastName.ToLower().Contains(txtSearch.Text.ToLower()) || i.Reader.FirstName.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            i.Book.Title.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    rentList = rentList.OrderBy(i => i.IDReader).ToList();
                    break;
                case 1:
                    rentList = rentList.OrderBy(i => i.Reader.LastName).ToList();
                    break;
                case 2:
                    rentList = rentList.OrderBy(i => i.Reader.FirstName).ToList();
                    break;
                case 3:
                    rentList = rentList.OrderBy(i => i.Book.Title).ToList();
                    break;
                default:
                    rentList = rentList.OrderBy(i => i.IDReader).ToList();
                    break;
            }
            bookRentalList.ItemsSource = rentList;

        }

        private void RenewalCredit()
        {


            var ii = bookRentalList.SelectedItem as DataBase.BookRental;
            ii.Credit = (decimal)RentalClass.Credit(ii.IDBook, ii.StartDate, ii.EndDate);

            AppData.Context.SaveChanges();


        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtration();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtration();
        }

        private void addRent_Click(object sender, RoutedEventArgs e)
        {
            AddRentalWindow addRent = new AddRentalWindow();
            addRent.ShowDialog();
            Filtration();
        }

        private void addBack_Click(object sender, RoutedEventArgs e)
        {

            var i = bookRentalList.SelectedItem as DataBase.BookRental;
            i.EndDate = DateTime.Now.Date;
            AppData.Context.SaveChanges();
            MessageBox.Show("Книга вернулась домой.");
            
            
            RenewalCredit();

            Filtration();
        }


    }

}
