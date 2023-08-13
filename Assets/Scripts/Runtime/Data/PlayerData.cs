using System;
using UnityEngine;

namespace Runtime.Data
{
    [Serializable]
    public class PlayerData
    {
        public long TotalPlayTime = 0;
        public long LastSaveTime = 0;
    }
}