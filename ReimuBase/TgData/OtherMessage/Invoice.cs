using System;
using System.Collections.Generic;
using System.Text;

namespace ReimuAPI.ReimuBase.TgData.OtherMessage
{
    public class Invoice
    {
        public string title { get; set; }
        public string description { get; set; }
        public string start_parameter { get; set; }
        public string currency { get; set; }
        public string total_amount { get; set; }
    }
}
