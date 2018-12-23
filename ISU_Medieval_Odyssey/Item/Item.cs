// Author: Joon Song, Steven Ung
// File Name: Item.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold Item object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey.Item
{
    public abstract class Item
    {
        /// <summary>
        /// The value of the item - the price at which it will be purchased at
        /// </summary>
        public short Value { get; protected set; }
    }
}
