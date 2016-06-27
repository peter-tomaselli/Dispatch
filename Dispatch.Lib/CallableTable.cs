using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dispatch.Lib
{
    public interface ICallableTable
    {
        IEnumerable<KeyValuePair<string, string>> TableInfo { get; }

        ICallable GetCallable(string domain, string name);

        void Register(string name, ICallable callable, params string[] domains);
    }

    public class CallableTable : ICallableTable
    {
        private IDictionary<string, IDictionary<string, ICallable>> _recs = new Dictionary<string, IDictionary<string, ICallable>>();

        public IEnumerable<KeyValuePair<string, string>> TableInfo
        {
            get
            {
                foreach (KeyValuePair<string, IDictionary<string, ICallable>> domain in _recs)
                {
                    foreach (KeyValuePair<string, ICallable> rec in domain.Value)
                    {
                        yield return new KeyValuePair<string, string>(domain.Key, rec.Key);
                    }
                }
            }
        }

        public ICallable GetCallable(string domain, string name)
        {
            if (_recs.ContainsKey(domain) && _recs[domain].ContainsKey(name))
            {
                return _recs[domain][name];
            }

            throw new InvalidOperationException("could not find callable for params");
        }

        public void Register(string name, ICallable callable, params string[] domains)
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
                    _recs[domain] = new Dictionary<string, ICallable>
                    {
                        { name, callable }
                    };
                }
            }
        }
    }
}
