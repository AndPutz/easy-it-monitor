using System;

namespace Infra.Entities
{
    public class ParamEntity : Entity, IComparable
    {
        public string Name { get; set; }

        /// <summary>
        /// Cycle Time of each item to monitor
        /// If its zero (0) will use the KeepAlive timer set to Agent Monitor
        /// </summary>
        public int CycleTime { get; set; }

        public virtual int CompareTo(object obj)
        {
            return Name.CompareTo((obj as ParamEntity).Name);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj as ParamEntity == null)
                return false;
            if ((obj as ParamEntity).Name == "")
                return base.Equals(obj);

            return (obj as ParamEntity).Name == Name;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Name.GetHashCode();
        }
    }
}
