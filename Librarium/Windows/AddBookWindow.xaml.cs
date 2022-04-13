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
using Librarium.Classes;

namespace Librarium.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        public AddBookWindow()
        {
            InitializeComponent();

            ComboBoxPublishHouse.ItemsSource = AppData.Context.PublishHouse.ToList();
            ComboBoxPublishHouse.DisplayMemberPath = "NamePublishHouse";
            ComboBoxPublishHouse.SelectedIndex = 0;

            ComboBoxLastNameAuthor.ItemsSource = AppData.Context.Author.ToList();
            ComboBoxLastNameAuthor.DisplayMemberPath = "LastName";
            ComboBoxLastNameAuthor.SelectedIndex = 0;

            ComboBoxFirstNameAuthor.ItemsSource = AppData.Context.Author.ToList();
            ComboBoxFirstNameAuthor.DisplayMemberPath = "FirstName";
            ComboBoxFirstNameAuthor.SelectedIndex = 0;

            

        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Ошибка", "Пустое поле в названии книги", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataBase.Book book = new DataBase.Book();
            book.Title = txtTitle.Text;
            book.IDAuthor = ComboBoxLastNameAuthor.SelectedIndex + 1;
            book.IDAuthor = ComboBoxFirstNameAuthor.SelectedIndex + 1;
            book.IDPublishHouse = ComboBoxPublishHouse.SelectedIndex + 1;


            AppData.Context.Book.Add(book);
            AppData.Context.SaveChanges();
            MessageBox.Show("Книга успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();

        }
    }
}
