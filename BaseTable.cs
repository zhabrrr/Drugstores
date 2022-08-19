using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Drugstores
{
    internal abstract class BaseTable
    {
        abstract protected class Item
        {
            public int Id;

            public Item()
            {
            }

            public Item(SqlDataReader reader)
            {
                Id = reader.GetInt32(0);
            }

            abstract public List<SqlParameter> CreateSqlParameters();
        }

        protected readonly List<Item> items = new List<Item>();

        protected readonly List<string> menuLines = new List<string>() { "Добавить" };
        protected readonly List<string> menuLinesDel = new List<string>() { "Добавить", "Удалить" };
        protected readonly string escapeStr = "Вернуться в главное меню";

        abstract protected string SqlExprSel { get; }
        abstract protected string SqlExprIns { get; }
        abstract protected string SqlExprDel { get; }

        public void Show()
        {
            while (true)
            {
                ReadItems();
                ShowItems();
                int choice = ViewHelpers.Menu(items.Count > 0 ? menuLinesDel : menuLines, escapeStr);
                Console.WriteLine();
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Введите параметры создаваемого объекта или нажмите Esc для отмены");
                        Item newItem = InputItem();
                        if (newItem != null)
                            AddItem(newItem);
                        break;
                    case 2:
                        Console.WriteLine("Введите Id удаляемого объекта или нажмите Esc для отмены");
                        int? id = InputId();
                        if (id.HasValue)
                            DeleteItem(id.Value);
                        break;
                    case 0: return;
                }
            }
        }

        abstract protected void ShowItems();
        abstract protected Item CreateItem(SqlDataReader reader);
        abstract protected Item InputItem();

        protected bool HasItem(int id)
        {
            return items.Any(item => item.Id == id);
        }

        virtual protected int? InputId()
        {
            return ViewHelpers.InputId(HasItem, "Id: ");
        }

        private void ReadItems()
        {
            items.Clear();
            using (SqlConnection connection = new SqlConnection(DbHelpers.СonnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(SqlExprSel, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    items.Add(CreateItem(reader));
                }
                reader.Close();
            }
        }

        private void AddItem(Item item)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.СonnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(SqlExprIns, connection);
                foreach(SqlParameter param in item.CreateSqlParameters())
                    command.Parameters.Add(param);
                int number = command.ExecuteNonQuery();
            }
        }

        private void DeleteItem(int id)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.СonnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(SqlExprDel, connection);
                SqlParameter nameParam = new SqlParameter("@id", id);
                command.Parameters.Add(nameParam);
                int number = command.ExecuteNonQuery();
            }
        }
    }
}
