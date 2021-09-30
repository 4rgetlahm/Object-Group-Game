using System;
using System.Collections.Generic;
using System.Text;

namespace object_group_game
{
	class Effect
	{
		public string Status { get; private set; }
		public string DisplayName { get; private set; }
		public int Value { get; private set; }

		Effect(string status, string displayName, int value)
		{
			Status = status;
			DisplayName = displayName;
			Value = value;
		}
	}
}
