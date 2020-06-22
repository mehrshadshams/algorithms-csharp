using System;
using System.Collections.Generic;

namespace AIMA.Agent
{
    public class DynamicObject : ICloneable, IEquatable<DynamicObject>
    {
        private readonly Dictionary<string, object> attributes = new Dictionary<string, object>();

        public DynamicObject()
        {
        }
        
        private DynamicObject(Dictionary<string, object> attributes)
        {
            this.attributes = attributes;
        }
        
        public object this[string key]
        {
            get { return attributes[key]; }
            set { attributes[key] = value; }
        }

        public object Clone()
        {
            var attributeClone = new Dictionary<string, object>(attributes);
            return new DynamicObject(attributeClone);
        }

        public bool Equals(DynamicObject other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(attributes, other.attributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DynamicObject) obj);
        }

        public override int GetHashCode()
        {
            return (attributes != null ? attributes.GetHashCode() : 0);
        }
    }
}