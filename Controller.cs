using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drugstores
{
    internal class Controller
    {
        readonly List<string> menuLines = new List<string>() { "Товарные наименования", "Аптеки", "Склады", "Партии", "Товары в аптеке" };
        readonly string escapeStr = "Завершение работы";

        List<BaseTable> tables = new List<BaseTable>() { new Goods(), new Drugstores(), new Warehouses(), new Parties() };

        public void Run()
        {
            try
            {
                while (true)
                {
                    int choice = ViewHelpers.Menu(menuLines, escapeStr);
                    Console.WriteLine();
                    switch (choice)
                    {
                        case 0: return;
                        case 1:
                        case 2:
                        case 3:
                        case 4: tables[choice - 1].Show(); break;
                        case 5: new GoodsDrugstoreView().Show(); break;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
