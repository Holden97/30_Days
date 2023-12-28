using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    [Serializable]
    public class CommodityData
    {
        public string name;
        public Sprite avatar;
        public int cost;
        public string abstractDescription;
        [TextArea]
        public string detailsDescription;

        public virtual string AbstractDescription => abstractDescription;
        public virtual string DetailsDescription => detailsDescription;
    }
}
