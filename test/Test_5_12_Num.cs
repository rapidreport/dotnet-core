using System;
using System.IO;
using System.Data;
using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.pdf;

namespace test
{
    class Test_5_12_Num
    {
        public override string ToString()
        {
            return "5.12 numオペレータ";
        }

        public void Run()
        {
            var name = "test_5_12_num";

            Report report = new Report(Json.Read(Path.Combine("rrpt", name + ".rrpt")));
            report.GlobalScope.Add("time", DateTime.Now);
            report.GlobalScope.Add("lang", "core");

            report.Fill(new ReportDataSource(_GetDataTable()));
            ReportPages pages = report.GetPages();
            using (FileStream fs = new FileStream(Path.Combine("out", name + ".pdf"), FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                pages.Render(renderer);
            }
        }

        private DataTable _GetDataTable()
        {
            var ret = new DataTable();
            ret.Columns.Add("v", typeof(String));
            ret.Rows.Add("1");
            ret.Rows.Add("2");
            ret.Rows.Add("3");
            return ret;
        }
    }
}
