using System;
using System.IO;
using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xlsx;
using NPOI.XSSF.UserModel;

namespace test
{
    class Test_0_2
    {
        public override string ToString()
        {
            return "0.2 図形";
        }

        public void Run()
        {
            var name = "test_0_2";

            Report report = new Report(Json.Read(Path.Combine("rrpt", name + ".rrpt")));
            report.GlobalScope.Add("time", DateTime.Now);
            report.GlobalScope.Add("lang", "core");

            report.Fill(DummyDataSource.GetInstance());
            ReportPages pages = report.GetPages();
            using (FileStream fs = new FileStream(Path.Combine("out", name + ".pdf"), FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                pages.Render(renderer);
            }

            using (FileStream fs = new FileStream(Path.Combine("out", name + ".xlsx"), FileMode.Create))
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                XlsxRenderer renderer = new XlsxRenderer(workbook);
                renderer.NewSheet("sheet_name");
                pages.Render(renderer);
                workbook.Write(fs);
            }
        }
    }
}
