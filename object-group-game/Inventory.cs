using System;
using System.Collections.Generic;
using System.Text;

namespace object_group_game {
	class Inventory {
		private List<Item> _items;
		public List<Item> Items {
			get {
				return _items.clone();
			}

			private set {
				_items = value;
			}
		}

		Inventory() {
			Items = new List<Item>();
		}

		Inventory(List<Item> items) {
			Items = items;
		}
	}
}
