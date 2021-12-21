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

		public EffectType EffectType { get; set; }
		[JsonIgnore]
		public List<Item> Items { get; set; }

		public Effect(string name, EffectType effectType)
		{
			this.Name = name;
			this.EffectType = effectType;
		}

		protected Effect()
        {

        }
	}
}
