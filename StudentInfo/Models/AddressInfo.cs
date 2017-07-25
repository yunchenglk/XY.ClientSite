using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentInfo.Models
{
    public class AddressInfo
    {
        public string province { get; set; }
        //市
        public string city { get; set; }
        //县（区、旗、县级市）
        public string area { get; set; }
        //乡（镇）
        public string country { get; set; }
        //村
        public string village { get; set; }
        //其他
        public string other { get; set; }
        public override string ToString()
        {
            return "省：" + province + "\n市：" + city + "\n区县：" + area + "\n乡镇：" + country + "\n村：" + village + "\n其他：" + other;
        }
    }
}