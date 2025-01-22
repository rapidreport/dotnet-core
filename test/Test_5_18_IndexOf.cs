using System;
using System.IO;
using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.pdf;

namespace test
{
    class Test_5_18_IndexOf
    {
        public override string ToString()
        {
            return "5.18 indexof";
        }

        public void Run()
        {
            var name = "test_5_18_indexof";

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
