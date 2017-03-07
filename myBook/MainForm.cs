
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;

namespace myBook
{
	/// <summary>
	/// Description of MainForm
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}
		private delegate void AsynUpdateUI(int step);
        private System.Timers.Timer timer1 = new System.Timers.Timer();
        //private static string preURL = "http://qt.gtimg.cn/q=";

		void MainFormLoad(object sender, EventArgs e)
		{
            //listView1.LargeImageList = imageList1;
            //DirectoryInfo TheFolder=new DirectoryInfo(@"f:\\mybooks");
            ////遍历文件夹
            //int i = 0;
            //foreach(DirectoryInfo NextFolder in TheFolder.GetDirectories())
            //{
            //    ListViewItem lvi = new ListViewItem();
            //    lvi.Text = NextFolder.Name;
            //    lvi.ImageIndex = i ;
            //    listView1.Items.Add(lvi);
            //    i++;
            //}
            //listView1.Columns[0].Width = -2;

		}


		void ToolStripButton3Click(object sender, EventArgs e)
		{
			ToolStripMenuItem tsmi = new ToolStripMenuItem("测试按钮"); 

            //创建菜单 文本图片并添加 
            ToolStripMenuItem tsmi2 = new ToolStripMenuItem("测试按钮");
            
            var menu = (ToolStripMenuItem)menuStrip1.Items[0];
			menu.DropDownItems.Add(tsmi);
            //绑定菜单点击事件 
            tsmi2.Click += DemoClick; 
            //添加菜单 
            tsmi.DropDownItems.Add(tsmi2); 

		}
		//自定义点击事件需要执行作 
  		private void DemoClick(object sender, EventArgs e) 
        { 
            ToolStripMenuItem but = sender as ToolStripMenuItem; 
            MessageBox.Show(but.Text); 
        }
  		


        private void toolStripButton5_Click( object sender, EventArgs e )
        {
			int tskCount = 10000;

            this.toolStripProgressBar1.Maximum = tskCount;
            this.toolStripProgressBar1.Value = 0;

            DoWork dw = new DoWork();
            dw.UpdateUIDeledate += UpdataUIStatus;
            dw.TaskCallBack += Accomplish;

            Thread th = new Thread(new ParameterizedThreadStart(dw.Write));
            th.IsBackground = true;
            th.Start(tskCount);
        }
		

        private void UpdataUIStatus( int _step )
        {
            if (InvokeRequired)
            {
                this.Invoke(new AsynUpdateUI(delegate( int s ) {
                    this.toolStripProgressBar1.Value += s;
                    this.toolStripStatusLabel1.Text = toolStripProgressBar1.Value.ToString() + "/" + toolStripProgressBar1.Maximum.ToString();
                }),_step);
            }
            else
            {
                this.toolStripProgressBar1.Value += _step;
                this.toolStripStatusLabel1.Text = toolStripProgressBar1.Value.ToString() + "/" + toolStripProgressBar1.Maximum.ToString();
            }
        }
        //完成任务时需要调用
        private void Accomplish( )
        {
            //还可以进行其他的一些完任务完成之后的逻辑处理
            MessageBox.Show("任务完成");
        }
		
        private void toolStripButton1_Click( object sender, EventArgs e )
        {
            
        }

        private void toolStripButton6_Click( object sender, EventArgs e )
        {
            if (toolStripButton6.Text == "启动监测")
            {
                toolStripButton6.Text = "停止监测";
                toolStripStatusLabel2.Text = "已启动";
                stkCTL4.Start();
                stkCTL5.Start();
                stkCTL6.Start();
            }
            else
            {
                toolStripButton6.Text = "启动监测";
                toolStripStatusLabel2.Text = "已停止";
                stkCTL4.Stop();
                stkCTL5.Stop();
                stkCTL6.Stop();
            }
        }
        private  void TimeEvent( object source, ElapsedEventArgs e )
        {

        }
        //public static  void InvokeIfRequired<T>( this T c, Action<T> action ) where T : Control
        //{
        //    if (c.InvokeRequired)
        //    {
        //        c.Invoke(new Action(() => action(c)));
        //    }
        //    else
        //    {
        //        action(c);
        //    }
        //}
        //textBox1.InvokeIfRequired(c => { c.Text = "it works!"; });
        private void toolStripComboBox1_SelectedIndexChanged( object sender, EventArgs e )
        {
            if (toolStripComboBox1.Text == "早报")
            {
                webBrowser1.Navigate("http://t.gw.com.cn/dzhzb");
            }
            if (toolStripComboBox1.Text == "晚报")
            {
                webBrowser1.Navigate("http://t.gw.com.cn/dzhwb");
            }
        }

        private void toolStripButton2_Click( object sender, EventArgs e )
        {
            //Action<string> action = ( x ) => { toolStripStatusLabel3.Text = x.ToString(); };
            //toolStripStatusLabel3.Invoke(action, DateTime.Now.ToString("HH:mm:ss"));
            this.Invoke(new Action(() =>
            {
                //statusStrip.Items["toolStripStatusLabel_DateTime"].Text = DateTime.Now.ToLongTimeString();
            }));
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            HttpCodeLib.XJHTTP xj = new HttpCodeLib.XJHTTP();
            var html1 = xj.GetHtml("http://nufm3.dfcfw.com/EM_Finance2014NumericApplication/JS.aspx?type=CT&cmd=SZ&sty=UDFN&st=z&sr=&p=&ps=&token=44c9d251add88e27b65ed86506f6e5da").Html;
            var str = xj.GetStringMid(html1,"\"","\"").Split(new char[]{','});
            MessageBox.Show(str[0]);
        }

	}
}
