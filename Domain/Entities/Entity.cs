using System;

namespace Domain.Entities
{
    public abstract class Entity : ICloneable
    {
        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}
