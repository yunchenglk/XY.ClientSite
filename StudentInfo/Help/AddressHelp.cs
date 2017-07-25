using StudentInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentInfo.Help
{
    public class AddressHelp
    {
        public AddressInfo getInfo(string str)
        {
            AddressInfo i = new AddressInfo();
            i.province = getPRO(str);
            if (!string.IsNullOrEmpty(i.province))
                str = str.Replace(i.province, "");

            i.city = getCITY(str);
            if (!string.IsNullOrEmpty(i.city))
                str = str.Replace(i.city, "");

            i.area = getAREA(str);
            if (!string.IsNullOrEmpty(i.area))
                str = str.Replace(i.area, "");

            i.country = getCOUNTRY(str);
            if (!string.IsNullOrEmpty(i.country))
                str = str.Replace(i.country, "");

            i.village = getVILLAGE(str);
            if (!string.IsNullOrEmpty(i.village))
                str = str.Replace(i.village, "");
            i.other = str;
            return i;
        }


        public string getPRO(string str)
        {
            if (str.Contains("省"))
            {
                var index = str.IndexOf("省");
                if (index < 4)
                    return str.Substring(0, index + 1);
            }
            return "";
        }
        public string getCITY(string str)
        {
            if (str.Contains("市"))
            {
                var index = str.IndexOf("市");
                if (index < 4)
                    return str.Substring(0, index + 1);
            }
            return "";
        }

        public string getAREA(string str)
        {
            if (str.Contains("区"))
            {
                var index = str.IndexOf("区");
                if (index < 4)
                    return str.Substring(0, index + 1);
            }
            if (str.Contains("旗"))
            {
                var index = str.IndexOf("旗");
                if (index < 4)
                    return str.Substring(0, index + 1);
            }
            if (str.Contains("县"))
            {
                var index = str.IndexOf("县");
                if (index < 4)
                    return str.Substring(0, index + 1);
            }
            return "";
        }

        public string getCOUNTRY(string str)
        {

            if (str.Contains("乡"))
            {
                var index = str.IndexOf("乡");
                if (index < 4)
                    return str.Substring(0, index + 1);
            }
            if (str.Contains("镇"))
            {
                var index = str.IndexOf("镇");
                if (index < 4)
                    return str.Substring(0, index + 1);
            }
            return "";
        }
        public string getVILLAGE(string str)
        {
            if (str.Contains("村"))
            {
                var index = str.IndexOf("村");
                if (index < 4)
                    return str.Substring(0, index + 1);
            }
            return "";
        }
    }
}