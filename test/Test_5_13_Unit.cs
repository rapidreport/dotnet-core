using iTextSharp.text.pdf;
using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.gdi;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xlsx;
using Newtonsoft.Json;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Drawing.Printing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace test
{
    class Test_5_13_Unit
    {
        public override string ToString()
        {
            return "5_13 集計単位";
        }

        public void Run()
        {
            var name = "test_5_13_unit";

            Report report = new Report(Json.Read(Path.Combine("rrpt", name + ".rrpt")));
            report.GlobalScope.Add("time", DateTime.Now);
            report.GlobalScope.Add("lang", "core");

            report.Fill(DummyDataSource.GetInstance());
            ReportPages pages = report.GetPages();
            {
                var preview = new FmPrintPreview(new Printer(pages));
                preview.ShowDialog();
            }
        }
    }
}
