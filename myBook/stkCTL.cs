using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace myBook
{
    public partial class stkCTL : UserControl
    {
        public stkCTL( )
        {
            InitializeComponent();
        }
        private static string preURL = "http://qt.gtimg.cn/q=";
        private System.Timers.Timer tm1 = new System.Timers.Timer();
        private void initListViewBid( )
        {
            listView1.Columns.Add("1", 60, HorizontalAlignment.Center);
            listView1.Columns.Add("2", 50, HorizontalAlignment.Center);
            listView1.Columns.Add("3", 50, HorizontalAlignment.Center);
            listView1.Columns.Add("4", 20, HorizontalAlignment.Center);

            listView2.Columns.Add("1", 60, HorizontalAlignment.Center);
            listView2.Columns.Add("2", 60, HorizontalAlignment.Center);

            listView1.HeaderStyle = ColumnHeaderStyle.None;
            listView2.HeaderStyle = ColumnHeaderStyle.None;
            
        }
        public static void appSumInfo( Label lb, string _txt )
        {
            if (lb.InvokeRequired)
            {

                lb.Invoke(new MethodInvoker(() => lb.Text = _txt));
            }
            else
            {
                lb.Text = _txt;
                
            }
        }
        public void Start()
        {
            tm1.Interval = 7 * 1000;
            tm1.Elapsed += new System.Timers.ElapsedEventHandler(tm1_Elapsed);

            tm1.Enabled = true;
        }
        public void Stop()
        {
            tm1.Enabled = false;
        }
        private void stkCTL_Load( object sender, EventArgs e )
        {
            initListViewBid();
            comboBox1.SelectedIndex = 0;
        }
        private List<string> items = new List<string>();
        private void appStkDetails( ListView lv, string str )
        {
            string[] info = str.Split(new char[] { '|' });
            var query = info.OrderBy(o => o).Distinct();
            foreach (string itm in query)
            {
                if (!items.Contains(StkTools.reString(itm)))
                {
                    items.Add(StkTools.reString(itm));
                    string[] rt = itm.Split(new char[] { '/' });
                    StkTools.addListItemByThread(lv, new ListViewItem(new string[] { rt[0], rt[1], rt[2], rt[3] }));
                }
            }
        }
        private void tm1_Elapsed( object sender, System.Timers.ElapsedEventArgs e )
        {
                
                string txt = Invoke(new Func<string>(() => { return comboBox1.Text; })).ToString();
                var rtn = StkTools.getStockInfo(preURL + txt);

                if (rtn != null)
                {
                    string info = "";
                    info = "["+rtn[0].Name + "][" + rtn[0].zde +"][" + rtn[0].zdf+"%]";
                    appSumInfo(label1,info);
                    listView2.Invoke(new MethodInvoker(() => listView2.Items.Clear()));
                    StkTools.appStkBid(listView2, rtn[0].bids);
                    appStkDetails(listView1,rtn[0].TradeDetails);
                }
                else
                {

                    //label21.Text = "未取得数据";
                    return;
                }
        }
    }
}
