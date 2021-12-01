using GameLibrary.Inventory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace GameLibrary
{
	[Table("Items")]
	public class Item : IComparable<Item>
	{
		[Key]
		public int ItemID { get; set; }
		public string Name { get; set; }
		public int Strength { get; set; }
		public int Dexterity { get; set; }
		public int Intelligence { get; set; }
		public ItemType itemType { get; set; }
		public List<Effect> Effects { get; set; }
		[JsonIgnore]
		public List<Character> Characters { get; set; }

		public Item(string Name, ItemType ItemType, int Strength = 0, int Dexterity = 0, int Intelligence = 0, List<Effect> Effects = null)
		{
			this.Name = Name;
			this.itemType = ItemType;
			this.Strength = Strength;
			this.Dexterity = Dexterity;
			this.Intelligence = Intelligence;
			this.Effects = Effects == null ? new List<Effect>() : Effects;
		}

		protected Item ()
		{

		}

		public int GetStats()
        {
			return this.Strength + this.Intelligence + this.Dexterity;
        }

		public int CompareTo(Item obj)
		{
			if (obj == null) return 1;

			int thisStats = obj.GetStats();
			int compareStats = obj.GetStats();
			if (thisStats == compareStats)
			{
				return 0;
			}
			else if (thisStats > compareStats)
			{
				return 1;
			}
			else
			{
				return -1;
			}
		}
    }
}
