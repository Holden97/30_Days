using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class Prop : ICost, ILockable
    {
        public PropData propData;

        public Prop(PropData propData)
        {
            this.propData = propData;
        }

        public int Cost => propData.cost;

        public bool IsLocked { get; set; }
    }

    public interface ICost
    {
        public int Cost { get; }
    }

    public interface ILockable
    {
        public bool IsLocked { get; set; }
    }
}
