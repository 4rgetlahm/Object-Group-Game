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
		public string DisplayName { get; set; }
		public int Strength { get; set; }
		public int Dexterity { get; set; }
		public int Intelligence { get; set; }
		public int Constitution { get; set; }
		public int Endurance { get; set; }
		[JsonIgnore]
		public List<Character> Characters { get; set; }

		public Item(string DisplayName, int Strength = 0, int Dexterity = 0, int Intelligence = 0, int Constitution = 0, int Endurance = 0)
		{
			this.ItemID = ItemID;
			this.DisplayName = DisplayName;
			this.Strength = Strength;
			this.Dexterity = Dexterity;
			this.Intelligence = Intelligence;
			this.Constitution = Constitution;
			this.Endurance = Endurance;
		}

		public Item (int ItemID, string DisplayName, int Strength, int Dexterity, int Intelligence, int Constitution, int Endurance, List<Character> characters)
		{
			this.ItemID = ItemID;
			this.DisplayName = DisplayName;
			this.Strength = Strength;
			this.Dexterity = Dexterity;
			this.Intelligence = Intelligence;
			this.Constitution = Constitution;
			this.Endurance = Endurance;
			this.Characters = characters;
		}

		protected Item ()
		{

		}

		public int GetStats()
        {
			return Strength + Intelligence + Dexterity + Constitution + Endurance;
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
