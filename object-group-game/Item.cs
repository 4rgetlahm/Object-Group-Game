using System;
using System.Collections.Generic;
using System.Text;

namespace object_group_game {
	class Item {
		private string Name { get; set; }
		private List<Effect> Effects { get; set; }

		Item(string name, List<Effect> effects) {
			Name = name;
			Effects = effects;
		}

		public string GetName() {
			return Name;
		}

		public List<Effect> GetEffects() {
			return Effects.clone();
		}
	}
}
