using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dispatch.Lib
{
    /// <summary>
    /// this interface represents an object that can be invoked with an arbitrary number of arguments to return a value of type `R`
    /// </summary>
    /// <typeparam name="R"></typeparam>
    public interface ICallable<R>
    {
        R InvokeWithArgs(params object[] args);
    }

    /// <summary>
    /// an implementation of ICallable that wraps a function of no arguments
    /// </summary>
    /// <typeparam name="R"></typeparam>
    public class Callable<R> : ICallable<R>
    {
        private readonly Func<R> _func;

        public Callable(Func<R> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            _func = func;
        }

        public R InvokeWithArgs(params object[] args)
        {
            return _func.Do(args);
        }
    }

    /// <summary>
    /// an implementation of ICallable that wraps a function of one argument
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="R"></typeparam>
    public class Callable<T1, R> : ICallable<R>
    {
        private readonly Func<T1, R> _func;

        public Callable(Func<T1, R> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            _func = func;
        }

        public R InvokeWithArgs(params object[] args)
        {
            return _func.Do(args);
        }
    }

    public class Callable<T1, T2, R> : ICallable<R>
    {
        private readonly Func<T1, T2, R> _func;

        public Callable(Func<T1, T2, R> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            _func = func;
        }

        public R InvokeWithArgs(params object[] args)
        {
            return _func.Do(args);
        }
    }
}
