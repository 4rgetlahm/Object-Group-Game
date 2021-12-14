using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameLibrary
{
    public class DropItem
    {
        [Key]
        public int DropItemID { get; set; }
        public Item Item { get; set; }
        public double DropRate { get; set; }

        public DropItem(Item item, double dropRate)
        {
            this.Item = item;
            this.DropRate = dropRate;
        }

        protected DropItem()
        {

        }
    }
}
