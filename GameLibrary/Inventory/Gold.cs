using System;
using System.Collections.Generic;
using System.Text;

namespace GameLibrary.Inventory
{
    public class Gold : Item
    {
        public double Amount { get; set; }
        public Gold(double amount) : base("Gold", ItemType.GOLD, ItemModel.NONE)
        {
            this.Amount = amount;
        }
        protected Gold()
        {

        }
    }
}
