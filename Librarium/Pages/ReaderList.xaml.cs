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
    /// Логика взаимодействия для ReaderList.xaml
    /// </summary>
    public partial class ReaderList : Page
    {
        List<Reader> readerList = new List<Reader>();
        List<string> listSort = new List<string>() { "По умолчанию", "По фамилии", "По имени", "По адресу" };

        public ReaderList()
        {
            InitializeComponent();
            comboBox.ItemsSource = listSort;
            comboBox.SelectedItem = 0;
            Filtration();
        }

        private void Filtration()
        {
            readerList = AppData.Context.Reader.ToList();
            readerList = readerList.
                            Where(i => i.LastName.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            i.FirstName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();

            switch (comboBox.SelectedIndex)
            {
                case 0:
                    readerList = readerList.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    readerList = readerList.OrderBy(i => i.LastName).ToList();
                    break;
                case 2:
                    readerList = readerList.OrderBy(i => i.FirstName).ToList();
                    break;
                case 3:
                    readerList = readerList.OrderBy(i => i.Address).ToList();
                    break;
                default:
                    readerList = readerList.OrderBy(i => i.ID).ToList();
                    break;
            }

            listRead.ItemsSource = readerList;
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
            AddReaderWindow addReaderWindow = new AddReaderWindow();
            addReaderWindow.ShowDialog();
            Filtration();
        }


        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            
                var item = listRead.SelectedItem as DataBase.Reader;
                var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultClick == MessageBoxResult.Yes)
                {
                        AppData.Context.Reader.Remove(item);
                        AppData.Context.SaveChanges();
                        MessageBox.Show("Пользователь успешно удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    Filtration();
                }
            
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            var item = listRead.SelectedItem as DataBase.Reader;
            ChangeReaderWindow changereaderwindow = new ChangeReaderWindow(item);
            changereaderwindow.ShowDialog();
            Filtration();
        }
        
    }
}
