using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer;
using jp.co.systembase.report.renderer.gdi;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.pdf.imageloader;
using jp.co.systembase.report.renderer.xlsx;
using jp.co.systembase.report.renderer.xlsx.imageloader;
using jp.co.systembase.NPOI.XSSF.UserModel;
using System;
using System.Drawing;
using System.IO;
using jp.co.systembase.report.renderer.gdi.imageloader;

namespace test
{
    class Test_0_4
    {
        public override string ToString()
        {
            return "0.4 画像";
        }

        private ImageMap _GetImageMap()
        {
            var ret = new ImageMap();
            ret.Add("bmp", Image.FromFile("./img/logo.bmp"));
            ret.Add("gif", Image.FromFile("./img/logo.gif"));
            ret.Add("jpg", Image.FromFile("./img/logo.jpg"));
            ret.Add("png", Image.FromFile("./img/logo.png"));
            return ret;
        }

        public void Run()
        {
            var name = "test_0_4";

            Report report = new Report(Json.Read(Path.Combine("rrpt", name + ".rrpt")));
            report.GlobalScope.Add("time", DateTime.Now);
            report.GlobalScope.Add("lang", "core");

            report.Fill(DummyDataSource.GetInstance());
            ReportPages pages = report.GetPages();
            using (FileStream fs = new FileStream(Path.Combine("out", name + ".pdf"), FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                renderer.ImageLoaderMap.Add("image", new PdfImageLoader(_GetImageMap()));
                pages.Render(renderer);
            }

            using (FileStream fs = new FileStream(Path.Combine("out", name + ".xlsx"), FileMode.Create))
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                XlsxRenderer renderer = new XlsxRenderer(workbook);
                renderer.NewSheet("sheet_name");
                renderer.ImageLoaderMap.Add("image", new XlsxImageLoader(_GetImageMap()));
                pages.Render(renderer);
                workbook.Write(fs);
            }

            {
                var printer = new Printer(pages);
                printer.ImageLoaderMap.Add("image", new GdiImageLoader(_GetImageMap()));
                var preview = new FmPrintPreview(printer);
                preview.ShowDialog();
            }
        }

    }
}
