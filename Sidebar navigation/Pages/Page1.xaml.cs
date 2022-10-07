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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class PageConverter : Page
    {
        public CurResponce CurResponce { get; set; }

        public Task DataLoading { get; set; }

        public PageConverter()
        {
            InitializeComponent();

            DataLoading = Task.Run(() =>
            {
                Connect cont = new Connect();


                CurResponce = cont.HttpGet("https://cryptingup.com/api/assets");
                Application.Current.Dispatcher.Invoke((Action)delegate {
                    foreach (var item in CurResponce.assets)
                    {
                        fromcom.Items.Add(new ListViewItem() { Content = item.name });
                        tocom.Items.Add(new ListViewItem() { Content = item.name });
                    }
                });
                
            });
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
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }




            //cur1*amount/cur2

        }
    }
}
