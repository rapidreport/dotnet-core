using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace test
{
    static class MainModule
    {
        public static List<Object> Tests = new List<Object>();

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Tests.Add(new Test_0_1());
            Tests.Add(new Test_0_2());
            Tests.Add(new Test_0_3());
            Tests.Add(new Test_0_4());
            Tests.Add(new Test_0_5_PDF());
            Tests.Add(new Test_0_6());

            Application.Run(new FmTest());
        }
    }
}
