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
using Librarium.DataBase;
using Librarium.Classes;

namespace Librarium.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangeBookWindow.xaml
    /// </summary>
    public partial class ChangeBookWindow : Window
    {
        DataBase.Book chbook = new Book();
        public ChangeBookWindow(DataBase.Book book)
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

            chbook = book;
            TitleField.Text = chbook.Title;
        }

        private void ChangeBook_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(TitleField.Text))
            {
                MessageBox.Show("Ошибка", "Пустое поле в названии книги", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (TitleField.Text.Length == 1 || TitleField.Text.Length > 50)
            {
                MessageBox.Show("Не допустимое название книги. Запрещённое количество символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            chbook.Title = TitleField.Text;
            chbook.IDAuthor = ComboBoxLastNameAuthor.SelectedIndex + 1;
            chbook.IDAuthor = ComboBoxFirstNameAuthor.SelectedIndex + 1;
            chbook.IDPublishHouse = ComboBoxPublishHouse.SelectedIndex + 1;
            AppData.Context.SaveChanges();
            MessageBox.Show("Книга успешно отредактирована!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();

        }
    }
}
