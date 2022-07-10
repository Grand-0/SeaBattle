using System.Collections.Generic;
using System.Linq;

namespace API.Resources
{
    public class Store<T>
    {
        private readonly Dictionary<T, HashSet<string>> _store = new Dictionary<T, HashSet<string>>();

        public int Count
        {
            get
            {
                return _store.Count;
            }
        }

        public void Add(T key, string value)
        {
            lock (_store)
            {
                HashSet<string> values;
                if (!_store.TryGetValue(key, out values))
                {
                    values = new HashSet<string>();
                    _store.Add(key, values);
                }

                lock (values)
                {
                    values.Add(value);
                }
            }
        }

        public IEnumerable<string> GetValues(T key)
        {
            HashSet<string> values;
            if (_store.TryGetValue(key, out values))
            {
                return values;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string value)
        {
            lock (_store)
            {
                HashSet<string> values;
                if (!_store.TryGetValue(key, out values))
                {
                    return;
                }

                lock (values)
                {
                    values.Remove(value);

                    if (values.Count == 0)
                    {
                        _store.Remove(key);
                    }
                }
            }
        }
    }
}
