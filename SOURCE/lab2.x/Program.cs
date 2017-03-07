using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2.x
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        ///         
        static Program()
        {
            loadDLL.LoadResourceDll.RegistDLL();
        }
    [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Form1());
        }
    }
}
