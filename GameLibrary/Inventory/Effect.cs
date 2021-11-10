using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameLibrary
{
	[Table("Effects")]
	[Serializable]
	public class Effect
	{
		[Key]
		public int ID { get; set; }
		public string StatusName { get; set; }
		public string DisplayName { get; set; }
		public int Value { get; set; }
		[JsonIgnore]
		public List<Item> Items { get; set; }

		public Effect(string statusName, string displayName, int value)
		{
			this.StatusName = statusName;
			this.DisplayName = displayName;
			this.Value = value;
		}

		protected Effect()
        {

        }
	}
}
