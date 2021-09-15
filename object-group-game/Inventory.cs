using System;
using System.Collections.Generic;
using System.Text;

namespace object_group_game {
	class Inventory {
		private List<Item> items;

		List<Item> GetItems() {
			return items.clone();
		}
	}
}
