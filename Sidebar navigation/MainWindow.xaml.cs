using Sidebar_navigation.Data;
using Sidebar_navigation.Pages;
using Sidebar_navigation.Thread;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Sidebar_navigation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDataPage
    {
        public CurResponce CurResponce { get; set; }
        public Task DataLoading { get; set; }
        private MainThread _mainThread = new MainThread();
        public MainWindow()
        {
            InitializeComponent();

            toppanframe.Content = new HomePage()
            {
                RedirectToCurrency = (string name) => RedirectToCurrencyByName(name)
            };


            DataLoading = Task.Run(() =>
            {
                Connect cont = new Connect();

                CurResponce = cont.HttpGet("https://cryptingup.com/api/assets");
                CurResponce.assets = CurResponce.assets.Where(asset => asset.name != null && asset.name.Length > 0).ToList();
                _mainThread.RunInMainThread(() => { 
                    (toppanframe.Content as IDataPage).CurResponce = CurResponce;
                });

            });

        }

        private void sidebar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var selected = sidebar.SelectedItem as NavButton;

           // navframe.Navigate(selected.NavLink);
        }

        private void exitbutton_Click(object sender, RoutedEventArgs e)
        {
            Close();    

        }

        private void converterbutton_Click(object sender, RoutedEventArgs e)
        {
            toppanframe.Content = new PageConvertor()
            {
                CurResponce = CurResponce
            };
        }

        private void homebutton_Click(object sender, RoutedEventArgs e)
        {
            toppanframe.Content = new HomePage()
            {
                CurResponce = CurResponce,
                RedirectToCurrency = (string name) => RedirectToCurrencyByName(name)
            };
        }

        private void RedirectToCurrencyByName(string name)
        {
            toppanframe.Content = new CurrencyPage()
            {
                Currency = CurResponce.assets.Find(currency => currency.name.Equals(name))
            };
        }

        private void RedirectToChartByPrice(float price)
        {
            toppanframe.Content = new CurrencyPage()
            {
                Currency = CurResponce.assets.Find(currency => currency.price.Equals(price))
            };
        }

        private void infobutton_Click(object sender, RoutedEventArgs e)
        {
            toppanframe.Content = new InfoPage();
        }
    }
}
