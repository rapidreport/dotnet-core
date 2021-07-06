using System;
using System.IO;
using System.Data;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.pdf;

namespace test
{
    class Test_5_10_BackSlash
    {
        public override string ToString()
        {
            return "5.10 バックスラッシュ";
        }

        public void Run()
        {
            var name = "test_5_10_backslash";

            Report report = new Report(jp.co.systembase.json.Json.Read(Path.Combine("rrpt", name + ".rrpt")));

            report.Fill(new ReportDataSource(getDataTable()));
            ReportPages pages = report.GetPages();
            using (FileStream fs = new FileStream(Path.Combine("out", name + ".pdf"), FileMode.Create))
            {
                PdfRendererSetting setting = new PdfRendererSetting();
                setting.ReplaceBackslashToYen = true;
                PdfRenderer renderer = new PdfRenderer(fs, setting);
                pages.Render(renderer);
            }
        }

        private DataTable getDataTable()
        {
            var ret = new DataTable();
            ret.Columns.Add("v", typeof(String));
            ret.Rows.Add("1234567890\\234567890");
            ret.Rows.Add("1234567\\90\\234567890");
            ret.Rows.Add("1234567\\90\\23456\\890");
            ret.Rows.Add("\\1\\2\\3\\4\\5\\6\\7\\8\\9\\0111");
            ret.Rows.Add("123456789012345678901234567890");
            return ret;
        }
    }
}
