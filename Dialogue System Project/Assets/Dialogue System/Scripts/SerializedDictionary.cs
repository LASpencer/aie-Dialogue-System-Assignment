using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public class SerializedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<TKey> keys;
        [SerializeField]
        private List<TValue> values;

        private Dictionary<TKey, TValue> dict;

        public SerializedDictionary()
        {
            dict = new Dictionary<TKey, TValue>();
            keys = new List<TKey>();
            values = new List<TValue>();
        }

        public void OnAfterDeserialize()
        {
            if(dict == null)
            {
                dict = new Dictionary<TKey, TValue>();
            } else
            {
                dict.Clear();
            }

            int dictCount = Math.Min(keys.Count, values.Count);
            for(int i = 0; i < dictCount; ++i)
            {
                dict.Add(keys[i], values[i]);
            }
        }

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();

            foreach(KeyValuePair<TKey,TValue> pair in dict)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        // Implementing dictionary
        public TValue this[TKey key] { get { return dict[key]; } set { dict[key] = value; } }

        public ICollection<TKey> Keys { get { return dict.Keys; } }

        public ICollection<TValue> Values { get { return dict.Values; } }

        public int Count { get { return dict.Count; } }

        public bool IsReadOnly { get { return ((IDictionary<TKey, TValue>)dict).IsReadOnly; } }

        public void Add(TKey key, TValue value)
        {
            dict.Add(key, value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            dict.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            dict.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return dict.Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return dict.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((IDictionary<TKey,TValue>)dict).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return dict.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            return dict.Remove(key);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>)dict).Remove(item);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return dict.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dict.GetEnumerator();
        }
    }

    public class StringActorDict : SerializedDictionary<string, DialogueActor> { };
}
