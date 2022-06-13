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
            Tests.Add(new Test_5_7_YbCode());
            Tests.Add(new Test_5_7_IsNumeric());
            Tests.Add(new Test_5_7_CharSpacing_Ng());
            Tests.Add(new Test_5_9_WeekdayOperator());
            Tests.Add(new Test_5_10_BackSlash());
            Tests.Add(new Test_5_12_Num());
            Tests.Add(new Test_5_12_TextMock());
            Tests.Add(new Test_5_12_SortKeys());
            Tests.Add(new Test_5_13_Unit());
            Tests.Add(new Test_5_14_PdfBarcode());
            Tests.Add(new Test_5_14_Color());
            Tests.Add(new Test_5_14_Date());

            Application.Run(new FmTest());
        }
    }
}
