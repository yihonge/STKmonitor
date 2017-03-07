using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace myBook
{
    public class StkTools
    {
        
        private static string GetHtmlSource( string url )
        {
            string result = "";
            try
            {
                HttpCodeLib.XJHTTP xj = new HttpCodeLib.XJHTTP();
                result = xj.GetHtml(url).Html;
            }
            catch
            {
                result = "";
            }
            return result;
        }
        public static void addListItemByThread( ListView lv, ListViewItem lvi )
        {
            if (lv.InvokeRequired)
            {
            	
                lv.Invoke(new MethodInvoker(() => addListItemByThread(lv, lvi)));
            }
            else
            {
                lv.Items.Add(lvi);
                lv.EnsureVisible(lv.Items.Count - 1);
            }
        }
        public static void appStkBid( ListView lv, string[] strs )
        {
            addListItemByThread(lv, new ListViewItem(new string[] { strs[0], strs[1] }));
            addListItemByThread(lv, new ListViewItem(new string[] { strs[2], strs[3] }));
            addListItemByThread(lv, new ListViewItem(new string[] { strs[4], strs[5] }));
            addListItemByThread(lv, new ListViewItem(new string[] { strs[6], strs[7] }));
            addListItemByThread(lv, new ListViewItem(new string[] { strs[8], strs[9] }));
            addListItemByThread(lv, new ListViewItem(new string[] { "-----", "-----" }));
            addListItemByThread(lv, new ListViewItem(new string[] { strs[10], strs[11] }));
            addListItemByThread(lv, new ListViewItem(new string[] { strs[12], strs[13] }));
            addListItemByThread(lv, new ListViewItem(new string[] { strs[14], strs[15] }));
            addListItemByThread(lv, new ListViewItem(new string[] { strs[16], strs[17] }));
            addListItemByThread(lv, new ListViewItem(new string[] { strs[18], strs[19] }));
        }
        public static string reString( string _txt )
        {
            string restring = "";
            if (_txt != "")
            {
                var t = _txt.Split(new char[] { '/' });
                for (int i = 0; i < t.Length - 2; i++)
                {
                    restring += (t[i] + "/");
                }
            }
            return restring;
        }
        public static string getSotckStatus()
        {
            String s = "";
            try
            {
                int hour = DateTime.Now.Hour;
                int minutes = DateTime.Now.Minute;
                if (hour >= 0 && hour < 10)
                {
                    if (hour >= 9)
                    {
                        if (minutes >= 30)// 9:30-10:00
                            s = "交易中";
                        else
                            // 9:00-9:30
                            s = "未开盘";
                    }
                    else
                    {// 0:00-9:00
                        s = "未开盘";
                    }
                }
                else if (hour > 9 && hour < 12)
                {
                    if (hour > 10)
                    {
                        if (hour >= 11)
                        {
                            if (minutes >= 30)// 11:30-12:00
                                s = "休市中";
                            else
                                // 11:00-11:30
                                s = "交易中";
                        }
                        else
                        {// 10:00-11:00
                            s = "交易中";
                        }
                    }
                    else
                    {
                        if (minutes >= 30)// 9:30-10:00
                            s = "交易中";
                        else
                            // 9:00-9:30
                            s = "未开盘";
                    }
                }
                else if (hour >= 11 && hour < 13)
                {
                    if (hour < 12)
                    {
                        if (minutes >= 30)// 11:30-12:00
                            s = "休市中";
                        else
                            // 11:00-11:30
                            s = "交易中";
                    }
                    else
                    {
                        s = "休市中";
                    }
                }
                else if (hour > 12 && hour < 15)
                {
                    if (hour >= 13)
                    {// 13:00-15:00
                        s = "交易中";
                    }
                    else
                    {// 12:00-13:00
                        s = "休市中";
                    }
                }
                else if (hour >= 15 && hour <= 24)
                {// 15:00-24:00
                    s = "已结束";
                }
            }
            catch (Exception e)
            {
                return s;
            }
            return s;
        }

        public static List<Stock> getStockInfo( string _url )
        {
            List<Stock> stkLst = new List<Stock>();
            string str = GetHtmlSource(_url).Replace("\n","");
            if (str != "" || str != null || !str.Contains("404") || !str.Contains("超时"))
            {
                var strLst = str.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (strLst.Length > 0)
                {
                    foreach (var item in strLst)
                    {
                        Stock stk = new Stock();
                        Details dels = new Details();
                        string[] bid = new string[20];
                        var rtn = item.Split(new char[] { '~' }, StringSplitOptions.RemoveEmptyEntries);
                        if (rtn.Length == 49)
                        {
                            stk.Symbol = rtn[2];
                            stk.Name = rtn[1];
                            stk.LastTradePrice = rtn[4];
                            stk.OpenPrice = rtn[5];
                            stk.NowPrice = rtn[3];
                            stk.Highest= rtn[46];
                            stk.Lowest= rtn[47];
                            stk.DayHighPrice= rtn[33];
                            stk.DayLowPrice= rtn[34];
                            stk.cjl= rtn[6];
                            stk.TurnOver= rtn[38];
                            stk.TradeDetails= rtn[29];
                            stk.zde= rtn[31];
                            stk.zdf= rtn[32];

                            bid[10] = rtn[9];
                            bid[11] = rtn[10];
                            bid[12] = rtn[11];
                            bid[13] = rtn[12];
                            bid[14] = rtn[13];
                            bid[15] = rtn[14];
                            bid[16] = rtn[15];
                            bid[17] = rtn[16];
                            bid[18] = rtn[17];
                            bid[19] = rtn[18];

                            bid[8] = rtn[19];
                            bid[9] = rtn[20];
                            bid[6] = rtn[21];
                            bid[7] = rtn[22];
                            bid[4] = rtn[23];
                            bid[5] = rtn[24];
                            bid[2] = rtn[25];
                            bid[3] = rtn[26];
                            bid[0] = rtn[27];
                            bid[1] = rtn[28];

                            stk.bids = bid;
                            stkLst.Add(stk);
                            
                        }
                        else if (rtn.Length == 45)
                        {
                            stk.Symbol = rtn[0];
                            stk.Name = rtn[1];
                            stkLst.Add(stk);
                        }
                        else { stkLst = null; }
                    }
                }

            }
            return stkLst;
        }
        private static string[] dealData(string str)
        {
            if (str != null || str != "")
            {
                string[] info = str.Split(new char[] { '|' });
                info.OrderBy(x => x).Distinct();
                return info;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Extension method for faster string to decimal conversion. 
        /// </summary>
        /// <param name="str">String to be converted to positive decimal value</param>
        /// <remarks>Method makes some assuptions - always numbers, no "signs" +,- etc.</remarks>
        /// <returns>Decimal value of the string</returns>
        public static decimal ToDecimal(string str)
        {
            long value = 0;
            var decimalPlaces = 0;
            bool hasDecimals = false;

            for (var i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                if (ch == '.')
                {
                    hasDecimals = true;
                    decimalPlaces = 0;
                }
                else
                {
                    value = value * 10 + (ch - '0');
                    decimalPlaces++;
                }
            }

            var lo = (int)value;
            var mid = (int)(value >> 32);
            return new decimal(lo, mid, 0, false, (byte)(hasDecimals ? decimalPlaces : 0));
        }
        
        /// <summary>
        /// Extension method for faster string to Int32 conversion. 
        /// </summary>
        /// <param name="str">String to be converted to positive Int32 value</param>
        /// <remarks>Method makes some assuptions - always numbers, no "signs" +,- etc.</remarks>
        /// <returns>Int32 value of the string</returns>
        public static int ToInt32(string str)
        {
            int value = 0;
            for (var i = 0; i < str.Length; i++)
            {
                value = value * 10 + (str[i] - '0');
            }
            return value;
        }
        
        /// <summary>
        /// Breaks the specified string into csv components, all commas are considered separators
        /// </summary>
        /// <param name="str">The string to be broken into csv</param>
        /// <param name="size">The expected size of the output list</param>
        /// <returns>A list of the csv pieces</returns>
        public static List<string> ToCsv(string str, int size = 4)
        {
            int last = 0;
            var csv = new List<string>(size);
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ',')
                {
                    if (last != 0) last = last + 1;
                    csv.Add(str.Substring(last, i - last));
                    last = i;
                }
            }
            if (last != 0) last = last + 1;
            csv.Add(str.Substring(last));
            return csv;
        }
        
        /// <summary>
        /// Breaks the specified string into csv components, works correctly with commas in data fields 
        /// </summary>
        /// <param name="str">The string to be broken into csv</param>
        /// <param name="size">The expected size of the output list</param>
        /// <returns>A list of the csv pieces</returns>
        public static List<string> ToCsvData(string str, int size = 4)
        {
            var csv = new List<string>(size);

            int last = -1;
            bool textDataField = false;

            for (var i = 0; i < str.Length; i++)
            {
                switch (str[i])
                {
                    case '"':
                        textDataField = !textDataField;
                        break;
                    case ',':
                        if (!textDataField)
                        {
                            csv.Add(str.Substring(last + 1, (i - last)).Trim(' ', ','));
                            last = i;
                        }
                        break;
                    default:
                        break;
                }
            }

            if (last != str.Length - 1)
            {
                csv.Add(str.Substring(last + 1).Trim());
            }

            return csv;
        }

    }
}
