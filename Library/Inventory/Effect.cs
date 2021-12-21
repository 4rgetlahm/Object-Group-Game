using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameLibrary.Inventory
{
	[Table("Effects")]
	public class Effect
	{
		[Key]
		public int ID { get; set; }
		public string Name { get; set; }

		public EffectType effectType { get; set; }
		[JsonIgnore]
		public List<Item> Items { get; set; }

		public Effect(string displayName)
		{
			this.Name = displayName;
		}

		protected Effect()
        {

        }
	}
}
