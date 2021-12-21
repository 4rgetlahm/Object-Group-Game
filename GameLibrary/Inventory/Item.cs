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
		public int LevelRequirement { get; set; }
		public ItemType ItemType { get; set; }
		public ItemModel ItemModel { get; set; }
		public List<Effect> Effects { get; set; }
		[JsonIgnore]
		public List<Character> Characters { get; set; }

		public Item(string Name, ItemType ItemType, ItemModel itemModel, int Strength = 0, int Dexterity = 0, int Intelligence = 0, List<Effect> Effects = null)
		{
			this.Name = Name;
			this.ItemType = ItemType;
			this.ItemModel = itemModel;
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
