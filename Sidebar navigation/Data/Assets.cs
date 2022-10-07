using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sidebar_navigation.Data
{
    public class Assets
    {
        public Quote quote { get; set; }
        public string asset_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string website { get; set; }
        public string ethereum_contract_address { get; set; }
        public float price { get; set; }
        public float volume_24h { get; set; }
        public float change_1h { get; set; }
        public float change_24h { get; set; }
        public float change_7d { get; set; }
        public float total_supply { get; set; }
        public float circulating_supply { get; set; }
        public float max_supply { get; set; }
        public float market_cap { get; set; }
        public float fully_diluted_market_cap { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
