using System.Collections.Generic;

namespace System.Runtime.Serialization
{
    internal class HybridObjectCache
    {
        private Dictionary<string, object> _objectDictionary;
        private Dictionary<string, object> _referencedObjectDictionary;

        internal HybridObjectCache()
        {
        }

        internal void Add(string id, object obj)
        {
            if (_objectDictionary == null)
            {
                _objectDictionary = new Dictionary<string, object>();
            }

            if (_objectDictionary.TryGetValue(id, out object existingObject))
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.MultipleIdDefinition, id)));
            }

            _objectDictionary.Add(id, obj);
        }

        internal void Remove(string id)
        {
            if (_objectDictionary != null)
            {
                _objectDictionary.Remove(id);
            }
        }

        internal object GetObject(string id)
        {
            if (_referencedObjectDictionary == null)
            {
                _referencedObjectDictionary = new Dictionary<string, object>
                {
                    { id, null }
                };
            }
            else if (!_referencedObjectDictionary.ContainsKey(id))
            {
                _referencedObjectDictionary.Add(id, null);
            }

            if (_objectDictionary != null)
            {
                _objectDictionary.TryGetValue(id, out object obj);
                return obj;
            }

            return null;
        }

        internal bool IsObjectReferenced(string id)
        {
            if (_referencedObjectDictionary != null)
            {
                return _referencedObjectDictionary.ContainsKey(id);
            }
            return false;
        }

    }
}
