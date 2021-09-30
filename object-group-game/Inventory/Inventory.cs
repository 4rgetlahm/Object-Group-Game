using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace object_group_game
{
	class Inventory 
	{
		public List<Item> Items { get; private set; }

		public Inventory()
		{
			Items = new List<Item>();
		}

		public Inventory(ICollection<Item> items)
		{
			Items = items.ToList();
		}
	}
}
