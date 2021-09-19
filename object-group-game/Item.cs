using System;
using System.Collections.Generic;
using System.Text;

namespace object_group_game {
	class Item {
		public string Name { get; set; }
		public List<Effect> Effects { get; set; }

		Item(string name, List<Effect> effects) {
			Name.set(name);
			Effects.set(effects);
		}
	}
}
