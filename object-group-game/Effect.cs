using System;
using System.Collections.Generic;
using System.Text;

namespace object_group_game {
	class Effect {
		private string status;
		private string displayName;
		private int value;

		Effect(string status, string displayName, int value) {
			this.status = status;
			this.displayName = displayName;
			this.value = value;
		}

		string GetEffectKey() {
			return status;
		}

		string GetDisplayName() {
			return displayName;
		}

		int GetValue() {
			return value;
		}
	}
}
