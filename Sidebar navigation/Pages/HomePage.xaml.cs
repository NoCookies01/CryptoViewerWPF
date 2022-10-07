using Sidebar_navigation.Data;
using Sidebar_navigation.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Sidebar_navigation.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page, IDataPage
    {

        public Action<string> RedirectToCurrency { get; set; }

        private CurResponce _carResponse;
        public CurResponce CurResponce
        {
            get
            {
                return _carResponse;
            }
            set
            {
                _carResponse = value;
                if (CurResponce == null)
                {
                    return;
                }

                DataLoading = Task.Run(() =>
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        FillCurrencies(CurResponce.assets);
                    });

                });
            }
        }

        private MainThread _mainThread = new MainThread();
        private Helpers.Timeout _searchInputTimeout = new Helpers.Timeout();
        public Task DataLoading { get; set; }

        private SortOrder _sortOrder = SortOrder.Unknown;

        public HomePage()
        {
            InitializeComponent();

        }

        public void FillCurrencies(List<Assets> assets)
        {
            CurrencyList.Items.Clear();
            foreach (var item in assets)
            {
                var name = new ListViewItem() { Content = item.name };
                var price = new ListViewItem() { Content = item.price };    

                //name.MouseDoubleClick += SelectCurrency;
                name.MouseEnter += Name_GotFocus;
                name.MouseLeave += Name_MouseLeave;
                name.MouseDoubleClick += OnSelectCurrency;

                CurrencyList.Items.Add(name);
                CurrencyList.Items.Add(price);

                name.FontSize = 15;
                name.HorizontalAlignment = HorizontalAlignment.Left;

                price.FontSize = 15;
                price.HorizontalAlignment = HorizontalAlignment.Left;

            }

        }

        private void OnSelectCurrency(object sender, MouseButtonEventArgs e)
        {
            var obj = sender as ListViewItem;

            RedirectToCurrency(obj.Content.ToString());
        }

        private void Name_MouseLeave(object sender, MouseEventArgs e)
        {
            myPopup.IsOpen = false;
        }
        
        private void Name_GotFocus(object sender, RoutedEventArgs e)
        {
            var obj = sender as ListViewItem;

            myPopup.PlacementTarget = obj;

            var data = CurResponce.assets.Find(a => a.name == obj.Content);
            var dataText = "\nName: " + data.name;
            dataText += "\nChange in 24 hours: " + data.change_24h;
            dataText += "\nChange in 7 days: " + data.change_7d;
            dataText += "\nDescription: " + data.description;

            myPopupText.Text = dataText;

            myPopup.IsOpen = true;
        }

        private void popup_exitbutton_Click(object sender, RoutedEventArgs e)
        {
            myPopup.IsOpen = false;
        }

        private void FilterCurrencies()
        {
            if (CurResponce != null)
            {
                var newList = CurResponce.assets
                    .Where(asset =>
                        asset.name
                        .ToLower()
                        .Contains(searchbox.Text.ToLower()));

                switch (_sortOrder)
                {
                    case SortOrder.Name:
                        newList = newList.OrderBy(asset => asset.name);
                        break;
                    case SortOrder.Price:
                        newList = newList.OrderByDescending(asset => asset.price);
                        break;
                    default:
                        break;
                }


                FillCurrencies(newList.ToList());
            }
        }

        private void searchbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            searchbox.Text = null;
        }

        private void searchbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _searchInputTimeout.Run(() =>
                _mainThread.RunInMainThread(() =>
                {
                    FilterCurrencies();
                }), 200);
        }

        private void price_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(messageBoxText: "Is Selected");
        }

        private void SortByName(object sender, MouseButtonEventArgs e)
        {
            _sortOrder = SortOrder.Name;
            FilterCurrencies();
        }


        private void SortByPrice(object sender, MouseButtonEventArgs e)
        {
            _sortOrder = SortOrder.Price;
            FilterCurrencies();
        }
    }

    public enum SortOrder
    {
        Unknown,
        Name,
        Price
    }

}
