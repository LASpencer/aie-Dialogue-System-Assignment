﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public class SerializedDictionary<TKey, TValue>
    {
        [SerializeField]
        private List<TKey> keys;
        [SerializeField]
        private List<TValue> values;

        public SerializedDictionary()
        {
            keys = new List<TKey>();
            values = new List<TValue>();
        }

        public Dictionary<TKey, TValue> ToDictionary()
        {
            Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();
            int dictCount = Math.Min(keys.Count, values.Count);
            for (int i = 0; i < dictCount; ++i)
            {
                dict.Add(keys[i], values[i]);
            }
            return dict;
        }

        public void CopyDictionary(Dictionary<TKey,TValue> dictionary)
        {
            keys.Clear();
            values.Clear();

            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }
    }

    [Serializable]
    public class StringActorDict : SerializedDictionary<string, DialogueActor> { };
}
