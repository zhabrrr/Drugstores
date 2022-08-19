using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace Drugstores
{
    internal class Goods : BaseTable
    {
        new protected class Item : BaseTable.Item
        {
            public string Name;

            public Item() : base()
            {
            }

            public Item(SqlDataReader reader) : base(reader)
            {
                Name = reader.GetString(1);
            }
            override public List<SqlParameter> CreateSqlParameters()
            {
                return new List<SqlParameter>() { new SqlParameter("@name", Name) };
            }
        }
        override protected string SqlExprSel { get { return "SELECT Id, Name FROM Goods"; } }
        override protected string SqlExprIns { get { return "INSERT INTO Goods (Name) VALUES (@name)"; } }
        override protected string SqlExprDel { get { return "DELETE FROM Goods WHERE (Id = @id)"; } }

        override protected void ShowItems()
        {
            if(items.Count == 0)
                Console.WriteLine("Товарные наименования в базе данных отсутствуют");
            else
            {
                Console.WriteLine("Id    Название товара");
                foreach (Item item in items)
                    Console.WriteLine($"{item.Id,-4}  {item.Name}");
            }
        }

        override protected Item InputItem()
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("Название товара: ");
                string input = ViewHelpers.InputString();
                if (input == null)
                    return null;
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return new Item() { Name = input };
                }
            }
        }

        override protected Item CreateItem(SqlDataReader reader)
        {
            return new Item(reader);
        }
    }
}
