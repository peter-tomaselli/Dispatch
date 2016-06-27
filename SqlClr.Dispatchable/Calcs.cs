using Dispatch.Lib;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace SqlClr.Dispatchable
{
    public static class Calcs
    {
        private static ICallableTable<SqlString> Table
        {
            get
            {
                ICallableTable<SqlString> table = new CallableTable<SqlString>();
                table.Register("hello", new Callable<SqlString>(GetEnglish), "english");
                table.Register("hello", new Callable<SqlString>(GetFrench), "french");
                return table;
            }
        }

        public static SqlString GetEnglish()
        {
            return "Hello";
        }

        public static SqlString GetFrench()
        {
            return "Bonjour";
        }

        [SqlProcedure]
        public static SqlString GetHello(SqlString domain)
        {
            return Table.GetCallable(domain.ToString(), "hello").InvokeWithArgs();
        }
    }
}
