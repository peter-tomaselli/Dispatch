using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dispatch.Lib
{
    static class CallableSupport0
    {
        static int Arity<R>(this Func<R> func)
        {
            return 0;
        }

        internal static R Do<R>(this Func<R> func, params object[] args)
        {
            if (args.Length != func.Arity())
            {
                throw new InvalidOperationException("args are bad");
            }

            return func();
        }
    }

    static class CallableSupport1
    {
        static int Arity<T1, R>(this Func<T1, R> func)
        {
            return 1;
        }

        static Func<R> Curry<T1, R>(this Func<T1, R> func, T1 arg)
        {
            return () => func(arg);
        }

        internal static Func<R> Curry<T1, R>(this Func<T1, R> func, object arg)
        {
            if (arg is T1)
            {
                return func.Curry((T1)arg);
            }

            throw new InvalidOperationException("bad arg");
        }

        internal static R Do<T1, R>(this Func<T1, R> func, params object[] args)
        {
            if (args.Length != func.Arity())
            {
                throw new InvalidOperationException("args are bad");
            }

            return func.Curry(args[0])();
        }
    }

    static class CallableSupport2
    {
        static int Arity<T1, T2, R>(this Func<T1, T2, R> func)
        {
            return 2;
        }

        static Func<T2, R> Curry<T1, T2, R>(this Func<T1, T2, R> func, T1 arg)
        {
            return t2 => func(arg, t2);
        }

        internal static Func<T2, R> Curry<T1, T2, R>(this Func<T1, T2, R> func, object arg)
        {
            if (arg is T1)
            {
                return func.Curry((T1)arg);
            }

            throw new InvalidOperationException("bad arg");
        }

        internal static R Do<T1, T2, R>(this Func<T1, T2, R> func, params object[] args)
        {
            if (args.Length != func.Arity())
            {
                throw new InvalidOperationException("args are bad");
            }

            return func.Curry(args[0]).Curry(args[1])();
        }
    }
}
