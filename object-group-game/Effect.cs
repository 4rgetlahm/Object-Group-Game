using System;
using System.Collections.Generic;
using System.Text;

namespace object_group_game {
	class Effect {
		private string Status { get; set; }
		private string DisplayName { get; set; }
		private int Value { get; set; }

		Effect(string status, string displayName, int value) {
			Status = status;
			DisplayName = displayName;
			Value = value;
		}

		public string GetStatus() {
			return Status;
		}

		public string GetDisplayName() {
			return DisplayName;
		}

		public int GetValue() {
			return Value;
		}
	}
}
