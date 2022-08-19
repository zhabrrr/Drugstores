using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Drugstores
{
    internal class Drugstores : BaseTable
    {
        new protected class Item : BaseTable.Item
        {
            public string Name;
            public string Address;
            public string Phone;

            public Item() : base()
            {
            }

            public Item(SqlDataReader reader) : base(reader)
            {
                Name = reader.GetString(1);
                Address = reader.GetString(2);
                Phone = reader.GetString(3);
            }
            override public List<SqlParameter> CreateSqlParameters()
            {
                return new List<SqlParameter>() { new SqlParameter("@name", Name),
                                                  new SqlParameter("@address", Address),
                                                  new SqlParameter("@phone", Phone) };
            }
        }

        override protected string SqlExprSel { get { return "SELECT Id, Name, Address, Phone FROM Drugstores"; } }
        override protected string SqlExprIns { get { return "INSERT INTO Drugstores (Name, Address, Phone) VALUES (@name, @address, @phone)"; } }
        override protected string SqlExprDel { get { return "DELETE FROM Drugstores WHERE Id = @id"; } }

        override protected void ShowItems()
        {
            if (items.Count == 0)
                Console.WriteLine("Аптеки в базе данных отсутствуют");
            else
            {
                Console.WriteLine($"Id    {"Название аптеки",-20}  {"Адрес",-30}  Телефон");
                foreach (Item item in items)
                    Console.WriteLine($"{item.Id,-3}  {item.Name,-20}   {item.Address,-30}   {item.Phone}");
            }
        }

        override protected Item InputItem()
        {
            Item newItem = new Item();
            do
            {
                Console.WriteLine();
                Console.Write("Название аптеки: ");
                string input = ViewHelpers.InputString();
                if (input == null)
                    return null;
                newItem.Name = input;
            } while (string.IsNullOrWhiteSpace(newItem.Name));

            Console.WriteLine();
            Console.Write("Адрес: ");
            newItem.Address = ViewHelpers.InputString();
            if (newItem.Address == null)
                return null;

            Console.WriteLine();
            Console.Write("Телефон: ");
            newItem.Phone = ViewHelpers.InputString();
            if (newItem.Phone == null)
                return null;

            return newItem;
        }

        override protected Item CreateItem(SqlDataReader reader)
        {
            return new Item(reader);
        }
    }
}
