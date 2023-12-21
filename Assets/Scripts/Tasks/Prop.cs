using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class Prop : ICost
    {
        public PropData propData;

        public Prop(PropData propData)
        {
            this.propData = propData;
        }

        public int Cost => propData.cost;
    }

    public interface ICost
    {
        public int Cost { get; }
    }
}
