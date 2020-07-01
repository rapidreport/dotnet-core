using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace test
{
    class Test_0_5_Data
    {
        public static DataTable GetDataTable()
        {
            var ret = new DataTable();
            ret.Columns.Add("bumonCd", typeof(Decimal));
            ret.Columns.Add("bumon", typeof(String));
            ret.Columns.Add("uriageDate", typeof(DateTime));
            ret.Columns.Add("denpyoNo", typeof(Decimal));
            ret.Columns.Add("shohinCd", typeof(String));
            ret.Columns.Add("shohin", typeof(String));
            ret.Columns.Add("tanka", typeof(Decimal));
            ret.Columns.Add("suryo", typeof(Decimal));
            for(var i = 1;i <= 100; i++)
            {
                for(var j = 1;j <= 50; j++)
                {
                    ret.Rows.Add(i, "部門" + i, DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", null), j, "PC00001", "ノートパソコン", 70000, 10);
                    ret.Rows.Add(i, "部門" + i, DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", null), j, "DP00002", "モニター", 25000, 10);
                    ret.Rows.Add(i, "部門" + i, DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", null), j, "PR00003", "プリンタ", 20000, 2);
                    ret.Rows.Add(i, "部門" + i, DateTime.ParseExact("2013/02/10", "yyyy/MM/dd", null), j, "PR00003", "プリンタ", 20000, 3);
                }
            }
            return ret;
        }
    }
}
