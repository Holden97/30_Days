﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    [Serializable]
    public class JsonWrapper<T>
    {
        public T[] items;
    }
}
