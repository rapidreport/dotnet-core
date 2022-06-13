using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.gdi;
using System;
using System.IO;

namespace test
{
    internal class Test_5_14_Date
    {
        public override string ToString()
        {
            return "5_14 Dateオペレータ";
        }

        public void Run()
        {
            var name = "test_5_14_date";

            Report report = new Report(Json.Read(Path.Combine("rrpt", name + ".rrpt")));

            report.Fill(DummyDataSource.GetInstance());
            ReportPages pages = report.GetPages();
            {
                var preview = new FmPrintPreview(new Printer(pages));
                preview.ShowDialog();
            }
        }
    }
}
