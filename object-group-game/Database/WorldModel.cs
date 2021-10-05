namespace object_group_game.Database
{
	public class Location
	{
		public int ID { get; set; }
		public string DisplayName { get; set; }
		public string Type { get; set; }
		public double Latitude { get; set; }
		public double Longtitude { get; set; }
		public int Radius { get; set; }
		public virtual Item Item { get; set; }
	}

	public class Item
	{
		public int ID { get; set; }
		public string DisplayName { get; set; }
	}

	public class Effect {
		public int ID { get; set; }
		public string DisplayName { get; set; }
		public string StatusName { get; set; }
	}

	public class ItemEffects {
		public int ItemID { get; set; }
		public int EffectID { get; set; }
		public int Value { get; set; }
	}
}
