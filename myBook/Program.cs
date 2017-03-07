/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2017-2-4
 * Time: 9:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace myBook
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            //var main_form = new MainForm();
            //main_form.Show();
            //Application.Run();
			Application.Run(new MainForm());
		}
		
	}
}
