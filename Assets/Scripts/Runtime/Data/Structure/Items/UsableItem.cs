using System;
using UnityEngine;

namespace Runtime.Data.Structure.Items
{
    /// <summary>
    /// Usable item data class. (Ex. Potion, Elixir, etc.)
    /// </summary>
    [Serializable]
    public class UsableItem : Item
    {
        /// <summary>
        /// Need to enter the code for the item's effect.
        /// Need character parameter for the effect.
        /// </summary>
        public virtual void Use()
        {
            
        }
    }
}