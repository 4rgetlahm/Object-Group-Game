using System;
using System.Collections.Generic;
using System.Text;

namespace object_group_game {
	class Inventory {
		private List<Item> Items { get; set; }

		Inventory() {
			Items = new List<Item>();
		}

		Inventory(List<Item> items) {
			Items = items;
		}
		
		public List<Item> GetItems() {
			return Items.clone();
		}
	}
}
