using Dispatch.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    class Program
    {
        private static ICallableTable<string> Table
        {
            get
            {
                ICallableTable<string> table = new CallableTable<string>();
                table.Register("hello", new Callable<string>(GetHello), "foo");
                table.Register("person", new Callable<string, string>(GetHelloForPerson), "foo");
                return table;
            }
        }

        private static ICallableTable<int> IntTable
        {
            get
            {
                ICallableTable<int> table = new CallableTable<int>();
                table.Register("add", new Callable<int, int, int>(AddTwoNumbers), "foo", "bar", "math");
                table.Register("three", new Callable<int>(GetThree), "bar");
                return table;
            }
        }

        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

            ICallable<string> doHello = Table.GetCallable("foo", "hello");
            ICallable<string> doHelloPerson = Table.GetCallable("foo", "person");
            ICallable<int> doThree = IntTable.GetCallable("bar", "three");

            Console.WriteLine(doHello.InvokeWithArgs().ToString());
            Console.WriteLine(doThree.InvokeWithArgs().ToString());

            Console.WriteLine(doHelloPerson.InvokeWithArgs("Bob").ToString());

            ICallable<int> addition = IntTable.GetCallable("math", "add");

            Console.WriteLine(addition.InvokeWithArgs(3, 3));

            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.WriteLine();

            sw.Reset();
            sw.Start();

            Console.WriteLine(Table.TableInfo.ToList());
            Console.WriteLine(IntTable.TableInfo.ToList());

            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds);

            Console.ReadKey();
        }

        private static int AddTwoNumbers(int lhs, int rhs)
        {
            return lhs + rhs;
        }

        private static string GetHello()
        {
            return "Hello world!";
        }

        private static string GetHelloForPerson(string person)
        {
            return string.Format("Hello, {0}", person);
        }

        private static int GetThree()
        {
            return 3;
        }
    }
}
