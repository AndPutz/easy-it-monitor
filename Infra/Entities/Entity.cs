using System;

namespace Infra.Entities
{
    public abstract class Entity : ICloneable
    {
        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}
