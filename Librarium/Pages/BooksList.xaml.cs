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
    /// Логика взаимодействия для BooksList.xaml
    /// </summary>
    public partial class BooksList : Page
    {
        List<Book> bookList = new List<Book>();

        List<string> listSort = new List<string>() { "По умолчанию", "По названию", "По фамилии автора", "По имени автора", "По издательству" };

        public BooksList()
        {
            InitializeComponent();
            //ComboBox.SelectedItem = listSort;
            //ComboBox.SelectedIndex = 0;
            ComboBox.ItemsSource = listSort;
            ComboBox.SelectedItem = 0;
            Filtration();
        }

        private void Filtration()
        {
            bookList = AppData.Context.Book.ToList();
            bookList = bookList.
                            Where(i => i.Title.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            i.PublishHouse.NamePublishHouse.ToLower().Contains(txtSearch.Text.ToLower())).ToList();

            switch (ComboBox.SelectedIndex)
            {
                case 0:
                    bookList = bookList.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    bookList = bookList.OrderBy(i => i.Title).ToList();
                    break;
                case 2:
                    bookList = bookList.OrderBy(i => i.Author.LastName).ToList();
                    break;
                case 3:
                    bookList = bookList.OrderBy(i => i.Author.FirstName).ToList();
                    break;
                case 4:
                    bookList = bookList.OrderBy(i => i.PublishHouse.NamePublishHouse).ToList();
                    break;
                default:
                    bookList = bookList.OrderBy(i => i.ID).ToList();
                    break;
            }

            listBook.ItemsSource = bookList;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtration();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtration();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow addBookWindow = new AddBookWindow();
            addBookWindow.ShowDialog();

            Filtration();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = listBook.SelectedItem as DataBase.Book;
                var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);


                if (resultClick == MessageBoxResult.Yes)
                {
                    AppData.Context.Book.Remove(item);

                    AppData.Context.SaveChanges();

                    MessageBox.Show("Книга успешно удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    Filtration();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            var item = listBook.SelectedItem as DataBase.Book;
            ChangeBookWindow changebookwindow = new ChangeBookWindow(item);
            changebookwindow.ShowDialog();
            Filtration();
        }
    }
}
