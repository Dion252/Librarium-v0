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
using Librarium.Windows;
using Librarium.DataBase;
using Librarium.Classes;
using System.IO;
using Microsoft.Win32;

namespace Librarium.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangeReaderWindow.xaml
    /// </summary>
    public partial class ChangeReaderWindow : Window
    {
        DataBase.Reader chreader = new DataBase.Reader();

        string pathPhoto = null;

        public ChangeReaderWindow(DataBase.Reader reader)
        {
            InitializeComponent();
            genderBox.ItemsSource = AppData.Context.Gender.ToList();
            genderBox.DisplayMemberPath = "NameGender";
            genderBox.SelectedIndex = reader.IDGender + 1;
            chreader = reader;
            lastnameField.Text = reader.LastName;
            nameField.Text = reader.FirstName;
            numberField.Text = reader.Phone;
            emailField.Text = reader.Email;
            addressField.Text = reader.Address;

            if (reader.Photo != null)
            {
                using (MemoryStream stream = new MemoryStream(reader.Photo))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    imgUser.Source = bitmapImage;
                }
            }

        }

        private void ChangeReader_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(lastnameField.Text))
            {
                MessageBox.Show("Ошибка", "Пустое поле фамилии", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(nameField.Text))
            {
                MessageBox.Show("Ошибка", "Пустое поле имени", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(numberField.Text))
            {
                MessageBox.Show("Ошибка", "Пустое поле номера телефона", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(emailField.Text))
            {
                MessageBox.Show("Ошибка", "Пустое поле email", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(addressField.Text))
            {
                MessageBox.Show("Ошибка", "Пустое поле адреса", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            chreader.LastName = lastnameField.Text;
            chreader.FirstName = nameField.Text;
            chreader.Phone = numberField.Text;
            chreader.Email = emailField.Text;
            chreader.Address = addressField.Text;
            chreader.IDGender = genderBox.SelectedIndex + 1;

            if (pathPhoto != null)
            {
                chreader.Photo = File.ReadAllBytes(pathPhoto);
            }

            AppData.Context.SaveChanges();
            MessageBox.Show("Книга успешно отредактирована!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();

        }

        private void DeterminatePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                imgUser.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                pathPhoto = openFileDialog.FileName;
            }
        }
    }
}
