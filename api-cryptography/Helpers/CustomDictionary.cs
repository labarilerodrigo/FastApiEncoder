using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.Serialization;
//using System.Collections.Generic;

namespace FastApiEncoder.Helpers
{
    public class CustomDictionary<TKey, TValue>
    {
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.Dictionary`2 class
        //     that is empty, has the default initial capacity, and uses the default equality
        //     comparer for the key type.
        public CustomDictionary() { }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.Dictionary`2 class
        //     that contains elements copied from the specified System.Collections.Generic.IDictionary`2
        //     and uses the default equality comparer for the key type.
        //
        // Parameters:
        //   dictionary:
        //     The System.Collections.Generic.IDictionary`2 whose elements are copied to the
        //     new System.Collections.Generic.Dictionary`2.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     dictionary is null.
        //
        //   T:System.ArgumentException:
        //     dictionary contains one or more duplicate keys.
        public CustomDictionary(DictionaryExtensions.KeyValuePair<TKey, TValue> dictionary) {
            Dictionary = dictionary;
        }

        ////
        //// Parameters:
        ////   collection:
        //public Dictionary(IEnumerable<DictionaryExtensions.KeyValuePair<TKey, TValue>> collection) { }

        public DictionaryExtensions.KeyValuePair<TKey, TValue> Dictionary { get; set; }

    }
}
