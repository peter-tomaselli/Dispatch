using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dispatch.Lib
{
    public interface ICallable : ICallable<object> { }

    public interface ICallable<R>
    {
        R InvokeWithArgs(params object[] args);
    }

    public class Callable<R> : ICallable
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

        object ICallable<object>.InvokeWithArgs(params object[] args)
        {
            return this.InvokeWithArgs(args);
        }
    }

    public class Callable<T1, R> : ICallable
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

        object ICallable<object>.InvokeWithArgs(params object[] args)
        {
            return this.InvokeWithArgs(args);
        }
    }

    public class Callable<T1, T2, R> : ICallable
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

        object ICallable<object>.InvokeWithArgs(params object[] args)
        {
            return this.InvokeWithArgs(args);
        }
    }
}
