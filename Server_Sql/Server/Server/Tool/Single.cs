using System.Collections;
using System.Collections.Generic;


    public class Single<T> where T : new()
    {
        private static T _instance;
        private static readonly object _lock = new object();
        public static T Instance {
            get {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new T();
                    }
                }
                return _instance;
            }
        }
    }

