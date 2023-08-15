﻿using System;
using UnityEngine;

namespace Runtime.Data
{
    [Serializable]
    public class PlayerData
    {
        public long Level = 1;
        public long Anima = 0;
        public long TotalPlayTime = 0;
        public long LastSaveTime = 0;
    }
}