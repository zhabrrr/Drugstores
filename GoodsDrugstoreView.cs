using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drugstores
{
    internal class GoodsDrugstoreView
    {
        public void Show()
        {
            int? drugstoreId = ViewHelpers.InputId(DbHelpers.Exists, "Drugstores", "Id аптеки: ");
            if (drugstoreId == null)
                return;

            GoodsDrugstoreDao dao = new GoodsDrugstoreDao();
            List<GoodsDrugstore> data = dao.ReadData(drugstoreId.Value);
            ShowData(data);
            Console.WriteLine("Нажмите любую клавишу для возврата в гланое меню");
            Console.ReadKey();
        }

        private void ShowData(List<GoodsDrugstore> data)
        {
            if (data.Count == 0)
                Console.WriteLine("Товаров не найдено");
            else
            {
                Console.WriteLine($"Id    {"Товар",-20}  Количество");
                foreach (GoodsDrugstore item in data)
                    Console.WriteLine($"{item.GoodsId,-4}  {item.GoodsName,-20}  {item.Amount}");
            }
        }
    }
}
