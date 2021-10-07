using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace object_group_game
{
	public class Item
	{
		[Key]
		public int ItemID { get; set; }
		public string DisplayName { get; set; }
		public int Strength { get; set; }
		public int Dexterity { get; set; }
		public int Intelligence { get; set; }
		public List<Effect> Effects { get; set; }

		public Item (int ItemID, string DisplayName, int Strength, int Dexterity, int Intelligence, List<Effect> Effects)
		{
			this.ItemID = ItemID;
			this.DisplayName = DisplayName;
			this.Strength = Strength;
			this.Dexterity = Dexterity;
			this.Intelligence = Intelligence;
			this.Effects = Effects;
		}

		public Item ()
		{

		}
	}
}
