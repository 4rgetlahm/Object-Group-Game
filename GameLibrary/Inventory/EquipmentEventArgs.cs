using System;
using System.Collections.Generic;
using System.Text;

namespace GameLibrary.Inventory
{
    public class EquipmentEventArgs
    {
        public Equipment Equipment { get; set; }
        public EquipmentEventArgs(Equipment equipment)
        {
            Equipment = equipment;
        }
    }
}
