using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Drugstores
{
    internal class Parties : BaseTable
    {
        new protected class Item : BaseTable.Item
        {
            public int GoodsId;
            public int WarehouseId;
            public int Count;

            public Item() : base()
            {
            }

            public Item(SqlDataReader reader) : base(reader)
            {
                GoodsId = reader.GetInt32(1);
                WarehouseId = reader.GetInt32(2);
                Count = reader.GetInt32(3);
            }

            override public List<SqlParameter> CreateSqlParameters()
            {
                return new List<SqlParameter>() { new SqlParameter("@goodsId", GoodsId),
                                                  new SqlParameter("@warehouseId", WarehouseId),
                                                  new SqlParameter("@count", Count) };
            }
        }

        override protected string SqlExprSel { get { return "SELECT Id, GoodsId, WarehouseId, Count FROM Parties"; } }
        override protected string SqlExprIns { get { return "INSERT INTO Parties (GoodsId, WarehouseId, Count) VALUES (@goodsId, @warehouseId, @count)"; } }
        override protected string SqlExprDel { get { return "DELETE FROM Parties WHERE (Id = @id)"; } }

        override protected void ShowItems()
        {
            if (items.Count == 0)
                Console.WriteLine("Партии в базе данных отсутствуют");
            else
            {
                Console.WriteLine("Id    Товар  Склад  Количество");
                foreach (Item item in items)
                    Console.WriteLine($"{item.Id,-4}  {item.GoodsId,-5}  {item.WarehouseId,-5}  {item.Count}");
            }
        }

        override protected Item InputItem()
        {
            Item newItem = new Item();

            int? inputNum = ViewHelpers.InputId(DbHelpers.Exists, "Goods", "Id товара: ");
            if (inputNum == null)
                return null;
            newItem.GoodsId = inputNum.Value;

            inputNum = ViewHelpers.InputId(DbHelpers.Exists, "Warehouses", "Id склада: ");
            if (inputNum == null)
                return null;
            newItem.WarehouseId = inputNum.Value;

            while (true)
            {
                Console.WriteLine();
                Console.Write("Количество: ");
                inputNum = ViewHelpers.InputInt();
                if (inputNum == null)
                    return null;
                if (inputNum.Value > 0)
                {
                    newItem.Count = inputNum.Value;
                    break;
                }
                Console.WriteLine($"Недопустимое значение. Повторите ввод. Для отмены нажмите Esc.");
            }
            return newItem;
        }

        override protected Item CreateItem(SqlDataReader reader)
        {
            return new Item(reader);
        }
    }
}
