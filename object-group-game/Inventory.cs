using System;
using System.Collections.Generic;
using System.Text;

namespace object_group_game
{
	class Inventory 
	{
		public List<Item> Items { get; private set; }

		Inventory()
		{
			Items = new List<Item>();
		}

		Inventory(ICollection<Item> items)
		{
			Items = items.ToList();
		}

		public List<Item> GetItems()
		{
			return Items.clone();
		}
	}
}
