using System;
using System.Collections.Generic;
using System.Text;

namespace object_group_game {
	class Effect {
		public string Status { get; set; }
		public string DisplayName { get; set; }
		public int Value { get; set; }

		Effect(string status, string displayName, int value) {
			this.status = status;
			this.displayName = displayName;
			this.value = value;
		}
	}
}
