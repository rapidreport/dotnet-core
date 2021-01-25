using System;
using System.IO;
using System.Data;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.pdf;

namespace test
{
    class Test_5_9_WeekdayOperator
    {
        public override string ToString()
        {
            return "5.9 オペレータ曜日";
        }

        public void Run()
        {
            var name = "test_5_9_weekdayoperator";

            Report report = new Report(jp.co.systembase.json.Json.Read(Path.Combine("rrpt", name + ".rrpt")));

            report.Fill(new ReportDataSource(getDataTable()));
            ReportPages pages = report.GetPages();
            using (FileStream fs = new FileStream(Path.Combine("out", name + ".pdf"), FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                pages.Render(renderer);
            }
        }

        private DataTable getDataTable()
        {
            var ret = new DataTable();
            ret.Columns.Add("v", typeof(DateTime));
            ret.Columns.Add("answer", typeof(int));
            var day = DateTime.Today;
            for(var i = 0;i < 6; i++)
            {
                ret.Rows.Add(day, day.DayOfWeek);
                day = day.AddDays(1);
            }
            return ret;
        }
    }
}
