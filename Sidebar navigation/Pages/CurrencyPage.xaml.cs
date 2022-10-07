using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Sidebar_navigation.Data;
using Sidebar_navigation.Thread;

namespace Sidebar_navigation.Pages
{
    /// <summary>
    /// Interaction logic for CurrencyPage.xaml
    /// </summary>
    public partial class CurrencyPage : Page
    {

        private Assets _currency;
        public Assets Currency
        {
            get
            {
                return _currency;
            }
            set
            {
                _currency = value;

                FillData();
            }
        }
        private MainThread _mainThread = new MainThread();

        public CurrencyPage()
        {
            InitializeComponent();
        }

        public void FillData()
        {
            TitleName.Text = _currency.name;
            

            if (_currency.description == "")
            {
                TitleDescription.Text = "NoDescription";

                TitleDescription.FontSize = 20;
                TitleDescription.HorizontalAlignment = HorizontalAlignment.Center;
                TitleDescription.VerticalAlignment = VerticalAlignment.Center;  
            }
            else
            {
                TitleDescription.Text = _currency.description;
            }

            TitlePrice.Text = Convert.ToString(_currency.price) + " USD";

            TitleChange1h.Text = Convert.ToString(_currency.change_1h) +"%";

            TitleChange24h.Text = Convert.ToString(_currency.change_24h) + "%";


            TitleChange7d.Text = Convert.ToString(_currency.change_7d) + "%" ;


            TitleAssetId.Text = _currency.asset_id;
            TitleStatus.Text = _currency.status;

            TitleUpdatedAt.Text = Convert.ToString(_currency.updated_at);


            TitleMarketCup.Text = Convert.ToString(_currency.market_cap) + " USD";


            TitleVolume24h.Text = Convert.ToString(_currency.volume_24h) + " USD";


        }

    }
}
