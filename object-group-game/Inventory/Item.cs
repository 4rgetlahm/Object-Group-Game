using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace object_group_game
{
	public class Item
	{
		public int ID { get; set; }
		public string DisplayName { get; set; }
		public List<Effect> Effects { get; set; }

		public Item (Database.Item item, List<Effect> effects)
		{
			this.ID = item.ID;
			this.DisplayName = item.DisplayName;
			this.Effects = effects;
		}

		public Item ()
		{

		}
	}
}
