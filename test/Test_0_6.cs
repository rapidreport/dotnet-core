using iTextSharp.text.pdf;
using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.pdf;
using System.IO;

namespace test
{
    class Test_0_6
    {
        public override string ToString()
        {
            return "0.6 外字";
        }

        public void Run()
        {
            var name = "test_0_6";

            Report report = new Report(Json.Read(Path.Combine("rrpt", name + ".rrpt")));
            report.Fill(DummyDataSource.GetInstance());
            ReportPages pages = report.GetPages();
            using (FileStream fs = new FileStream(Path.Combine("out", name + ".pdf"), FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                renderer.Setting.GaijiFont = BaseFont.CreateFont("font/eudc.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                renderer.Setting.GaijiFontMap.Add("gothic", BaseFont.CreateFont("font/msgothic.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED));
                pages.Render(renderer);
            }
        }
    }
}
