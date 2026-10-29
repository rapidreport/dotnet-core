using iTextSharp.text.pdf;
using jp.co.systembase.json;
using jp.co.systembase.NPOI.XSSF.UserModel;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.xlsx;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace test
{
    class Test_0_5_XLSX
    {
        public override string ToString()
        {
            return "0.5 1000ページ(XLSX)";
        }

        public void Run()
        {
            var name = "test_0_5";

            var sw = new Stopwatch();
            sw.Start();

            Report report = new Report(Json.Read(Path.Combine("rrpt", name + ".rrpt")));
            report.Fill(new ReportDataSource(Test_0_5_Data.GetDataTable()));
            ReportPages pages = report.GetPages();
            using (FileStream fs = new FileStream(Path.Combine("out", name + ".xlsx"), FileMode.Create))
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                XlsxRenderer renderer = new XlsxRenderer(workbook);
                renderer.NewSheet(name);
                pages.Render(renderer);
                workbook.Write(fs);
            }

            sw.Stop();
            MessageBox.Show(sw.ElapsedMilliseconds.ToString());
        }
    }
}
