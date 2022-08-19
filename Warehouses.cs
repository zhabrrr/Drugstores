using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Drugstores
{
    internal class Warehouses : BaseTable
    {
        new protected class Item : BaseTable.Item
        {
            public int DrugstoreId;
            public string Name;

            public Item() : base()
            {
            }

            public Item(SqlDataReader reader) : base(reader)
            {
                DrugstoreId = reader.GetInt32(1);
                Name = reader.GetString(2);
            }

            override public List<SqlParameter> CreateSqlParameters()
            {
                return new List<SqlParameter>() { new SqlParameter("@drugstoreId", DrugstoreId),
                                                  new SqlParameter("@name", Name) };
            }
        }

        override protected string SqlExprSel { get { return "SELECT Id, DrugstoreId, Name FROM Warehouses"; } }
        override protected string SqlExprIns { get { return "INSERT INTO Warehouses (DrugstoreId, Name) VALUES (@drugstoreId, @name)"; } }
        override protected string SqlExprDel { get { return "DELETE FROM Warehouses WHERE (Id = @id)"; } }

        override protected void ShowItems()
        {
            if (items.Count == 0)
                Console.WriteLine("Склады в базе данных отсутствуют");
            else
            {
                Console.WriteLine("Id    Аптека  Название склада");
                foreach (Item item in items)
                    Console.WriteLine($"{item.Id,-4}  {item.DrugstoreId,-6}  {item.Name}");
            }
        }

        override protected Item InputItem()
        {
            Item newItem = new Item();
            do
            {
                Console.WriteLine();
                Console.Write("Название склада: ");
                string input = ViewHelpers.InputString();
                if (input == null)
                    return null;
                newItem.Name = input;
            } while (string.IsNullOrWhiteSpace(newItem.Name));

            int? inputNum = ViewHelpers.InputId(DbHelpers.Exists, "Drugstores", "Id аптеки: ");
            if (inputNum == null)
                return null;
            newItem.DrugstoreId = inputNum.Value;

            return newItem;
        }

        override protected Item CreateItem(SqlDataReader reader)
        {
            return new Item(reader);
        }
    }
}
