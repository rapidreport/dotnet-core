using System;
using System.Data;
using System.IO;
using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.pdf;

namespace test
{
    class Test_5_7_IsNumeric
    {
        public override string ToString()
        {
            return "5.7 IsNumeric";
        }

        public void Run()
        {
            var name = "test_5_7_isnumeric";

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
            ret.Columns.Add("vInteger", typeof(int));
            ret.Columns.Add("vUInteger", typeof(uint));
            ret.Columns.Add("vLong", typeof(long));
            ret.Columns.Add("vULong", typeof(ulong));
            ret.Columns.Add("vSingle", typeof(Single));
            ret.Columns.Add("vDouble", typeof(Double));
            ret.Columns.Add("vDecimal", typeof(Decimal));
            ret.Columns.Add("vSByte", typeof(SByte));
            ret.Columns.Add("vByte", typeof(Byte));
            ret.Columns.Add("vShort", typeof(short));
            ret.Columns.Add("vUShort", typeof(ushort));
            ret.Rows.Add(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            ret.Rows.Add(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            return ret;
        }
    }
}
