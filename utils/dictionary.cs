namespace Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /*
        This is me adding functionality to an existing type. 
        I want to be abble to ask a dictionary directly about it containing an item. 
    */

    public static class DictionaryExtensions
    {

        public static KeyValuePair<TKey, TValue>? GetKeyValuePairByValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
        {
            foreach (var entry in dictionary)
            {
                if (EqualityComparer<TValue>.Default.Equals(entry.Value, value))
                {
                    return entry;
                }
            }

            return null;
        }

    }
}