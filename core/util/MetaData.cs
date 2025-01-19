using System;
using System.Collections.Generic;

namespace TerraX69.core.global
{
    internal class MetaData
    {
        private readonly Dictionary<string, object> metadata;

        public MetaData()
        {
            metadata = new Dictionary<string, object>();
        }

        public bool HasMeta(string key)
        {
            return metadata.ContainsKey(key);
        }

        public void SetMetadata(string key, object value)
        {
            metadata[key] = value;
        }

        public object GetMetadata(string key)
        {
            return metadata.ContainsKey(key) ? metadata[key] : null;
        }

        public void RemoveMetadata(string key)
        {
            metadata.Remove(key);
        }
    }
}
