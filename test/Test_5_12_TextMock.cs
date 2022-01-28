using System;
using System.IO;
using System.Data;
using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.pdf;

namespace test
{
    class Test_5_12_TextMock
    {
        public override string ToString()
        {
            return "5.12 Text要素のMock";
        }

        public void Run()
        {
            var name = "test_5_12_textmock";

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
