using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace myBook
{
    public class DoWork
    {
        public delegate void UpdateUI( int _step );//声明一个更新主线程的委托
        public UpdateUI UpdateUIDeledate;//实例化

        public delegate void AccomplishTask( );//声明一个完成任务后通知主线程的委托
        public AccomplishTask TaskCallBack;

        public void Write( object lineCount )
        {
            StreamWriter writeIO = new StreamWriter("text.txt", false, Encoding.GetEncoding("gb2312"));
            string head = "编号，省，市";
            writeIO.Write(head);
            for (int i = 0; i < (int)lineCount; i++)
            {
                writeIO.WriteLine(i.ToString() + "，湖南，衡阳");
                //写入数据后，调用更新主线程的委托
                UpdateUIDeledate(1);
            }
            TaskCallBack();
            writeIO.Close();
        }

    }
}
