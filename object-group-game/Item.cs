using System;
using System.Collections.Generic;
using System.Text;

namespace object_group_game {
	class Item {
		public string Name { get; private set; }
		public List<Effect> Effects { get; private set; }

		Item(string name, List<Effect> effects) {
			Name = name;
			Effects = effects;
		}
	}
}
