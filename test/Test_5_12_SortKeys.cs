using System;
using System.IO;
using System.Data;
using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.pdf;

namespace test
{
    class Test_5_12_SortKeys
    {
        public override string ToString()
        {
            return "5.12 ソートキーの昇順/降順";
        }

        public void Run()
        {
            var name = "test_5_12_sortkeys";

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
            var l = new object[] {null, 1, 2 };
            ret.Columns.Add("a", typeof(Object));
            ret.Columns.Add("b", typeof(Object));
            ret.Columns.Add("c", typeof(Object));
            ret.Columns.Add("d", typeof(Object));
            for (var a = 0; a < 3; a++)
            {
                for (var b = 0; b < 3; b++)
                {
                    for (var c = 0; c < 3; c++)
                    {
                        for (var d = 0; d < 3; d++)
                        {
                            ret.Rows.Add(l[a], l[b], l[c], l[d]);
                        }
                    }
                }
            }
            return ret;
        }
    }
}
