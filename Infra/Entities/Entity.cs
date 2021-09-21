using System;

namespace Infra
{
    public abstract class Entity : ICloneable
    {
        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}
