using Sidebar_navigation.Data;
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

namespace Sidebar_navigation.Pages
{
    /// <summary>
    /// Interaction logic for PageConvertor.xaml
    /// </summary>
    public partial class PageConvertor : Page, IDataPage
    {
        private CurResponce _curResponce;
        public CurResponce CurResponce
        {
            get
            {
                return _curResponce;
            }
            set
            {
                _curResponce = value;
                if (CurResponce == null)
                {
                    return;
                }

                fromcom.Items.Clear();
                tocom.Items.Clear();
                foreach (var item in CurResponce.assets)
                {
                    fromcom.Items.Add(new ListViewItem() { Content = item.name });
                    tocom.Items.Add(new ListViewItem() { Content = item.name });
                }
            }
        }

        public Task DataLoading { get; set; }

        public PageConvertor()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                float amount = Convert.ToInt32(Text_box_ammount.Text);

                var from = fromcom.SelectionBoxItem.ToString();
                var to = tocom.SelectionBoxItem.ToString();
                var fromPrice = CurResponce.assets.Find(x => x.name == from).price;
                var toPrice = CurResponce.assets.Find(x => x.name == to).price;

                resultCur.Content = (fromPrice * amount / toPrice).ToString();
                resultCur.FontSize = 20;
                resultCur.FontWeight = FontWeights.DemiBold; 
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }




            //cur1*amount/cur2

        }

      
    }
}
