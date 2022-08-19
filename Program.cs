using System.Data.SqlClient;

namespace Drugstores
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Controller controller = new Controller();
            controller.Run();
        }
    }
}