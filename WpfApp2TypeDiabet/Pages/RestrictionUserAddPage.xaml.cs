using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfApp2TypeDiabet.Services;

namespace WpfApp2TypeDiabet.Pages
{
    /// <summary>
    /// Логика взаимодействия для RestrictionUserAddPage.xaml
    /// </summary>
    public partial class RestrictionUserAddPage : Page
    {
        string _prevText = string.Empty;
        ObservableCollection<string> goodsList = new ObservableCollection<string>();
        ObservableCollection<string> newGoodsList = new ObservableCollection<string>();
        public RestrictionUserAddPage()
        {
            InitializeComponent();
            if(goodsList.Any())
            {
                goodsList.Clear();
            }
            goodsList = (ObservableCollection<string>)ToFindGoodComboBox.ItemsSource;
        }

        private void ToFindGoodComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _prevText = ToFindGoodComboBox.Text;
            if(newGoodsList.Any())
            {
                newGoodsList.Clear();
            }
            if (!string.IsNullOrEmpty(_prevText) || !string.IsNullOrWhiteSpace(_prevText))
            {
                foreach (string good in goodsList)
                {
                    if (good.Contains(_prevText))
                    {
                        newGoodsList.Add(good);
                    }
                }
                ToFindGoodComboBox.ItemsSource = newGoodsList;
            }
            else
            {
                ToFindGoodComboBox.ItemsSource = goodsList;
            }
        }
    }
}
