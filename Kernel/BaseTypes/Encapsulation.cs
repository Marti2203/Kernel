using System;

namespace Kernel.BaseTypes
{
    public sealed class Encapsulation : Object
    {
        internal Guid Identificator => id;
        readonly Object content;
        readonly Guid id;

        public Encapsulation(Object content)
            : this(content, Guid.NewGuid()) { }
        internal Encapsulation(Object content, Guid id)
        {
            this.content = content;
            this.id = id;
        }
        public Object Open(Guid key) => Open(key, "Wrong key.");
        internal Object Open(Guid key, string errorMessage) => id == key ? content : throw new ArgumentException(errorMessage);
        public override bool Equals(Object other) => ReferenceEquals(this, other);
        public override string ToString() => $"Capsule {id}<{content}>";
    }
}
