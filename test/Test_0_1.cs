using iTextSharp.text.pdf;
using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.gdi;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xlsx;
using Newtonsoft.Json;
using jp.co.systembase.NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Drawing.Printing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace test
{
    class Test_0_1
    {
        public override string ToString()
        {
            return "0.1 基本機能";
        }

        public void Run()
        {
            var name = "test_0_1";

            Report report = new Report(Json.Read(Path.Combine("rrpt", name + ".rrpt")));
            report.GlobalScope.Add("time", DateTime.Now);
            report.GlobalScope.Add("lang", "core");

            report.Fill(DummyDataSource.GetInstance());
            ReportPages pages = report.GetPages();
            using (FileStream fs = new FileStream(Path.Combine("out", name + ".pdf"), FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                renderer.Setting.FontMap.Add("meiryo", BaseFont.CreateFont(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "meiryo.ttc,0"), 
                    BaseFont.IDENTITY_H, BaseFont.EMBEDDED));
                pages.Render(renderer);
            }

            using (FileStream fs = new FileStream(Path.Combine("out", name + ".xlsx"), FileMode.Create))
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                XlsxRenderer renderer = new XlsxRenderer(workbook);
                renderer.Setting.FontMap.Add("meiryo", "メイリオ");
                renderer.NewSheet("sheet_name");
                pages.Render(renderer);
                workbook.Write(fs);
            }

            {
                var printer = new Printer(pages);
                printer.Setting.FontMap.Add("meiryo", "メイリオ");
                var preview = new FmPrintPreview(printer);
                preview.ShowDialog();
            }
        }
    }
}
