using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace object_group_game
{
	[Table("Items")]
	public class Item
	{
		[Key]
		public int ItemID { get; set; }
		public string DisplayName { get; set; }
		public int Strength { get; set; }
		public int Dexterity { get; set; }
		public int Intelligence { get; set; }
		public virtual List<Effect> Effects { get; set; }
		public virtual List<Character> Characters { get; set; }

		public Item(string DisplayName, int Strength = 0, int Dexterity = 0, int Intelligence = 0, List<Effect> Effects = null)
		{
			this.ItemID = ItemID;
			this.DisplayName = DisplayName;
			this.Strength = Strength;
			this.Dexterity = Dexterity;
			this.Intelligence = Intelligence;
			this.Effects = Effects == null ? new List<Effect>() : Effects;
		}

		public Item (int ItemID, string DisplayName, int Strength, int Dexterity, int Intelligence, List<Effect> Effects, List<Character> characters)
		{
			this.ItemID = ItemID;
			this.DisplayName = DisplayName;
			this.Strength = Strength;
			this.Dexterity = Dexterity;
			this.Intelligence = Intelligence;
			this.Effects = Effects;
			this.Characters = characters;
		}

		protected Item ()
		{

		}
	}
}
