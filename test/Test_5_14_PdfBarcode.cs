using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.pdf;
using System;
using System.IO;

namespace test
{
    internal class Test_5_14_PdfBarcode
    {
        public override string ToString()
        {
            return "5_14 PDFバーコード";
        }

        public void Run()
        {
            var name = "test_5_14_pdfbarcode";

            Report report = new Report(Json.Read(Path.Combine("rrpt", name + ".rrpt")));

            report.Fill(DummyDataSource.GetInstance());
            ReportPages pages = report.GetPages();
            using (FileStream fs = new FileStream(Path.Combine("out", name + ".pdf"), FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                pages.Render(renderer);
            }
        }
    }
}
