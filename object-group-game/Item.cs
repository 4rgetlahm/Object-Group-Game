using System;
using System.Collections.Generic;
using System.Text;

namespace object_group_game {
	class Item {
		private string name;
		private List<Effect> effects;

		Item(string name, List<Effect> effects) {
			this.name = name;
			this.effects = effects;
		}

		string GetName() {
			return name;
		}

		List<Effect> GetEffects() {
			return effects.clone();
		}
	}
}
