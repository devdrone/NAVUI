using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NAVUi
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Func<XElement, string> e = new Func<XElement, string>(ElementData);
            Application.Run(new Form1(e));
        }

        static string ElementData(XElement element)
        {
            var result = string.Empty;
            if (element != null)
            {
                result = element.Value;
            }

            return result;
        }
    }
}
