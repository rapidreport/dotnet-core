using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.gdi;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xlsx;
using NPOI.XSSF.UserModel;
using System;
using System.IO;

namespace test
{
    internal class Test_5_14_Color
    {
        public override string ToString()
        {
            return "5_14 色指定";
        }

        public void Run()
        {
            var name = "test_5_14_color";

            Report report = new Report(Json.Read(Path.Combine("rrpt", name + ".rrpt")));

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

            {
                var printer = new Printer(pages);
                var preview = new FmPrintPreview(printer);
                preview.ShowDialog();
            }
        }
    }
}
