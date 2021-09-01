using System;

namespace Domain.Service.Entities
{
    public abstract class Entity : ICloneable
    {
        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}
