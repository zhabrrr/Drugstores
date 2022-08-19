using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Drugstores
{
    internal class GoodsDrugstoreDao
    {
        private string sqlExprReadAll = @"select g.Id, g.Name, isnull(SUM(p.Count),0)
                                          from Goods as g
                                            left join(select p.GoodsId, p.Count
                                                      from Parties as p
                                                      join Warehouses as w on w.Id = p.WarehouseId and w.DrugstoreId = @DrugstoreId
                                                     ) as p on g.Id = p.GoodsId
                                          group by g.Id, g.Name
                                          order by g.id";

        //private string sqlExprRead = @"select g.Id, g.Name, isnull(SUM(p.Count),0)
        //                               from Goods as g
        //                                  join Parties as p on g.Id = p.GoodsId
        //                                  join Warehouses as w on w.Id = p.WarehouseId and w.DrugstoreId = @DrugstoreId
        //                               group by g.Id, g.Name
        //                               order by g.id";

        public List<GoodsDrugstore> ReadData(int drugstoreId)
        {
            List<GoodsDrugstore> data = new List<GoodsDrugstore>();
            using (SqlConnection connection = new SqlConnection(DbHelpers.СonnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExprReadAll, connection);
                command.Parameters.Add(new SqlParameter("@DrugstoreId", drugstoreId));
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(CreateDataItem(reader));
                }
                reader.Close();
            }
            return data;
        }

        private GoodsDrugstore CreateDataItem(SqlDataReader reader)
        {
            GoodsDrugstore item = new GoodsDrugstore();
            item.GoodsId = reader.GetInt32(0);
            item.GoodsName = reader.GetString(1);
            item.Amount = reader.GetInt32(2);
            return item;
        }
    }
}