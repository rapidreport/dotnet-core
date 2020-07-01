using iTextSharp.text.pdf;
using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.pdf;
using System;
using System.IO;

namespace test
{
    class Test_0_3
    {
        public override string ToString()
        {
            return "0.3 バーコード";
        }

        public void Run()
        {
            var name = "test_0_3";

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
        }
    }
}
