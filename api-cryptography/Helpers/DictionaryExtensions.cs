
using System;
using System.Collections;

namespace FastApiEncoder.Helpers
{
    public class DictionaryExtensions {

        //
        // Summary:
        //     Defines a key/value pair that can be set or retrieved.
        //
        // Type parameters:
        //   TKey:
        //     The type of the key.
        //
        //   TValue:
        //     The type of the value.
        public struct KeyValuePair<TKey, TValue>
        {
            //
            // Summary:
            //     Initializes a new instance of the DictionaryExtensions.KeyValuePair structure
            //     with the specified key and value.
            //
            // Parameters:
            //   key:
            //     The object defined in each key/value pair.
            //
            //   value:
            //     The definition associated with key.
            public KeyValuePair(TKey key, TValue value) {
                this.Key = key;
                this.Value = value;
            }

            //
            // Summary:
            //     Gets or sets the key in the key/value pair.
            //
            // Returns:
            //     A TKey that is the key of the System.Collections.Generic.KeyValuePair`2.
            public TKey Key { get; set; }
            //
            // Summary:
            //     Gets or sets the value in the key/value pair.
            //
            // Returns:
            //     A TValue that is the value of the System.Collections.Generic.KeyValuePair`2.
            public TValue Value { get; set; }

        }

    }
}
