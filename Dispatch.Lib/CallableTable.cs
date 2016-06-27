using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dispatch.Lib
{
    /// <summary>
    /// this interface represents an object that acts like a 'dispatch table' for a number of ICallables that share the same return type (they do not have to share the same number or type of arguments). Each callable is addressable via the combination of a 'domain' (think namespace) and a name
    /// </summary>
    /// <typeparam name="R"></typeparam>
    public interface ICallableTable<R>
    {
        IEnumerable<KeyValuePair<string, string>> TableInfo { get; }

        ICallable<R> GetCallable(string domain, string name);

        void Register(string name, ICallable<R> callable, params string[] domains);
    }

    public class CallableTable<R> : ICallableTable<R>
    {
        private IDictionary<string, IDictionary<string, ICallable<R>>> _recs = new Dictionary<string, IDictionary<string, ICallable<R>>>();

        public IEnumerable<KeyValuePair<string, string>> TableInfo
        {
            get
            {
                foreach (KeyValuePair<string, IDictionary<string, ICallable<R>>> domain in _recs)
                {
                    foreach (KeyValuePair<string, ICallable<R>> rec in domain.Value)
                    {
                        yield return new KeyValuePair<string, string>(domain.Key, rec.Key);
                    }
                }
            }
        }

        public ICallable<R> GetCallable(string domain, string name)
        {
            if (_recs.ContainsKey(domain) && _recs[domain].ContainsKey(name))
            {
                return _recs[domain][name];
            }

            throw new InvalidOperationException("could not find callable for params");
        }

        public void Register(string name, ICallable<R> callable, params string[] domains)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name");
            }

            if (callable == null)
            {
                throw new ArgumentNullException("callable");
            }

            if (domains == null)
            {
                throw new ArgumentNullException("domains");
            }

            foreach (string domain in domains)
            {
                if (_recs.ContainsKey(domain))
                {
                    _recs[domain][name] = callable;
                }
                else
                {
                    _recs[domain] = new Dictionary<string, ICallable<R>>
                    {
                        { name, callable }
                    };
                }
            }
        }
    }
}
