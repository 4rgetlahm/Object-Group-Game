using System;
using System.Collections.Generic;
using System.Text;

namespace GameLibrary.Inventory
{
    public class Gold : Item
    {
        public int Amount { get; set; }
        public Gold(int amount) : base("Gold", ItemType.GOLD)
        {
            this.Amount = amount;
        }
    }
}
