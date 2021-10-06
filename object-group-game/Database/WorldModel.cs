namespace object_group_game.Database
{
	public class Item
	{
		public int ID { get; set; }
		public string DisplayName { get; set; }
	}

	public class Location
	{
		public int ID { get; set; }
		public string DisplayName { get; set; }
		public LocationType Type { get; set; }
		public double Latitude { get; set; }
		public double Longtitude { get; set; }
		public int Radius { get; set; }
		public int ItemID { get; set; }
	}

	public class ItemEffects
	{
		public int ItemID { get; set; }
		public int EffectID { get; set; }
		public int Value { get; set; }
	}
}
