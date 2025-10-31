using System;
using System.Collections.Generic;

namespace _Project.Scripts.Systems.HashSetUtil {
    public class ObservableHashSet<T> {
        private readonly HashSet<T> _set = new HashSet<T>();
        public event Action onUpdate;

        public bool Add(T item) {
            var added = _set.Add(item);
            if(added) onUpdate?.Invoke();
            return added;
        }
        
        public bool Remove(T item)
        {
            var removed = _set.Remove(item);
            if (removed) onUpdate?.Invoke();
            return removed;
        }

        public bool Contains(T item) {
            var contained = _set.Contains(item);
            return contained;
        }
        
        public void Clear() {
            if (_set.Count <= 0) return;
            
            _set.Clear();
            onUpdate?.Invoke();
        }
        
        public int Count => _set.Count;
        public IEnumerable<T> Items => _set;
    }
}