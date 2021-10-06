using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game
{
	public class World
	{
		// Returns all locations from the database
		public static List<Location> GetLocations()
		{
			using(var context = new Database.WorldContext()) 
			{
				List<Location> locationList = new List<Location>();

				var locations = context.Location;

				foreach (var location in locations)
				{
					var item = GetItem(location.ItemID);

					locationList.Add(new Location(location, item));
				}

				return locationList;
			}
		}

		// Returns item with provided ID
		public static Item GetItem(int id)
		{
			using(var context = new Database.WorldContext())
			{
				var item = context.Item.Where(i => i.ID == id).FirstOrDefault();	

				List<Effect> effects = new List<Effect>();

				var itemEffects = context.ItemEffects.Where(e => e.ItemID == id);

				foreach (var itemEffect in itemEffects) {
					Effect effect = context.Effect.Where(e => e.ID == itemEffect.EffectID).FirstOrDefault();

					effect.Value = itemEffect.Value;

					effects.Add(effect);
				}

				return new Item(item, effects);
			}
		}

		// Returns all items from the database
		public static List<Item> GetItems() 
		{
			using(var context = new Database.WorldContext())
			{
				var items = context.Item;

				List<Item> itemList = new List<Item>();

				foreach (var item in items)
				{
					itemList.Add(GetItem(item.ID));	
				}

				return itemList;
			}
		}
	}
}
