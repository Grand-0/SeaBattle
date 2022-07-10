using System.Collections.Generic;

namespace API.Resources
{
    public class UserStore<T>
    {
        private readonly Dictionary<string, Dictionary<string, T>> _store;

        public UserStore()
        {
            _store = new Dictionary<string, Dictionary<string, T>>();
        }

        public int CountUser
        {
            get
            {
                return _store.Count;
            }
        }

        public void AddRecord(string userLogin, string key, T value)
        {
            lock (_store)
            {
                Dictionary<string, T> values;
                if (!_store.TryGetValue(key, out values))
                {
                    values = new Dictionary<string, T>();
                    _store.Add(userLogin, values);
                }

                lock (values)
                {
                    values.Add(key, value);
                }
            }
        }

        public void AddRecord(string userLogin)
        {
            lock (_store) 
            {
                if (_store.ContainsKey(userLogin))
                {
                    return;
                }

                _store.Add(userLogin, new Dictionary<string, T>());
            }
        }

        public void RemoveRecord(string userLogin)
        {
            lock (_store)
            {
                if (!_store.ContainsKey(userLogin))
                {
                    return;
                }

                _store.Remove(userLogin);
            }
        }

        public void RemoveUserRecord(string userLogin, string key)
        {
            lock (_store)
            {
                Dictionary<string, T> values;
                if (!_store.TryGetValue(userLogin, out values))
                {
                    return;
                }

                lock( values)
                {
                    if (!values.ContainsKey(key))
                    {
                        return;
                    }
                    values.Remove(key);
                }
            }
        }

        public void ChangeValueInRecord(string userLogin, string key, T newValue)
        {
            lock (_store)
            {
                Dictionary<string, T> values;
                if (!_store.TryGetValue(userLogin, out values))
                {
                    values = new Dictionary<string, T>();
                }

                lock (values)
                {
                    if (!values.ContainsKey(key))
                    {
                        values.Add(key, newValue);
                        _store.Add(userLogin, values);
                        return;
                    }

                    values[key] = newValue;
                }
            }
        }

        public Dictionary<string, T> GetUserRecords(string userLogin)
        {
            lock (_store)
            {
                Dictionary<string, T> records;
                if (!_store.TryGetValue(userLogin, out records)) 
                {
                    return new Dictionary<string, T>();
                }

                return records;
            }
        }

        public List<string> GetOnlineUsers()
        {
            List<string> users = new List<string>();
            foreach(var key in _store.Keys)
            {
                users.Add(key);
            }

            return users;
        }
    }
}
