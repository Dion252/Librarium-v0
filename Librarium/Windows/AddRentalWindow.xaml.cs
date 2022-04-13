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
using Librarium.Classes;
using Librarium.DataBase;
using Librarium.Windows;

namespace Librarium.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddRentalWindow.xaml
    /// </summary>
    public partial class AddRentalWindow : Window
    {
        public AddRentalWindow()
        {
            InitializeComponent();
            bookBox.ItemsSource = AppData.Context.Book.ToList();
            bookBox.DisplayMemberPath = "Title";
            bookBox.SelectedIndex = 0;

            readerBox.ItemsSource = AppData.Context.Reader.ToList();
            readerBox.DisplayMemberPath = "LastName";
            readerBox.SelectedIndex = 0;

            workerBox.ItemsSource = AppData.Context.Employee.ToList();
            workerBox.DisplayMemberPath = "LastName";
            workerBox.SelectedIndex = 0;
        }

        private void addBookRental_Click(object sender, RoutedEventArgs e)
        {
            DataBase.BookRental bookRental = new DataBase.BookRental();
            bookRental.IDBook = bookBox.SelectedIndex + 1;
            bookRental.IDReader = readerBox.SelectedIndex + 1;
            bookRental.IDEmployee = workerBox.SelectedIndex + 1;
            bookRental.StartDate = date1.DisplayDate;
            //bookRental.EndDate = date2.DisplayDate;

            AppData.Context.BookRental.Add(bookRental);
            AppData.Context.SaveChanges();
            MessageBox.Show("Запись добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
