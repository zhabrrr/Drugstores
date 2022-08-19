using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drugstores
{
    internal class ViewHelpers
    {
        static public int Menu(List<string> lines, string escapeLine)
        {
            while (true)
            {
                Console.WriteLine();
              
                for (int i = 0; i < lines.Count; ++i)
                {
                    string s = $"{i + 1} - {lines[i]}";
                    Console.WriteLine(s);
                }
                Console.WriteLine("Esc – " + escapeLine);
                Console.Write("> ");

                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();
                if (key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("1");
                    return 0;
                }
                int result = ((short)key.KeyChar) - '0';
                if (result > 0 && result <= lines.Count)
                    return result;

                Console.WriteLine("Некорректный выбор");
            }
        }

        //returns null if Esc was pressed
        static public string InputString()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("1");
                    return null;
                }
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return sb.ToString();
                }
                sb.Append(key.KeyChar);
            }
        }

        static public int? InputInt()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("1");
                    return null;
                }
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    int result;
                    if (int.TryParse(sb.ToString(), out result))
                        return result;
                }
                else
                if (char.IsDigit(key.KeyChar))
                    sb.Append(key.KeyChar);
            }
        }

        static public int? InputId(Func<int, bool> checkExistance, string prompt)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine(prompt);
                int? input = InputInt();
                if (input == null || input.Value == 0)
                    return null;

                if (checkExistance(input.Value))
                    return input;

                Console.WriteLine("Несуществующее значение. Повторите ввод. Для отмены нажмите Esc.");
            }
        }

        static public int? InputId(Func<string, int, bool> checkExistance, string dbName, string prompt)
        {
            while(true)
            {
                Console.WriteLine();
                Console.Write(prompt);
                int? inputNum = InputInt();
                if (inputNum == null)
                    return null;
                if (DbHelpers.Exists(dbName, inputNum.Value))
                    return inputNum;
                Console.WriteLine($"Несуществующее значение. Повторите ввод. Для отмены нажмите Esc.");
            }
        }
    }
}
