using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameLibrary;
using GameLibrary.Database;
using Server.Authentication;

namespace Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ItemsController : ControllerBase
	{
		[HttpGet]
		public IEnumerable<Item> GetItems()
		{
			Request.Query.TryGetValue("sessionid", out var vs);
			string sessionid = vs.FirstOrDefault();

			if (sessionid != null)
			{
				Session session = SessionManager.Instance.GetRealSession(Convert.FromBase64String(sessionid));
				if(session != null)
				{
					try
					{
						using (var context = new DataContext())
						{
							return context.Item.ToList();
						}
					}
					catch(Exception e)
					{
						Console.WriteLine("Caught exception while trying to fetch items: " + e.Message);
					}
				}
			}
			return null;
		}

		[HttpGet("{id}")]
		public Item GetItemByID(int id)
		{
			Request.Query.TryGetValue("sessionid", out var vs);
			string sessionid = vs.FirstOrDefault();

			if (sessionid != null)
			{
				Session session = SessionManager.Instance.GetRealSession(Convert.FromBase64String(sessionid));
				if (session != null)
				{
					try
					{
						using (var context = new DataContext())
						{
							return context.Item.Find(id);
						}
					}
					catch (Exception e)
					{
						Console.WriteLine($"Caught exception while trying to fetch item (id: {id}): " + e.Message);
					}
				}
			}
			return null;
		}

		[HttpDelete("remove/{id}")]
		public void DeleteItemByID(int id)
		{
			Request.Query.TryGetValue("sessionid", out var vs);
			string sessionid = vs.FirstOrDefault();

			if (sessionid != null)
			{
				Session session = SessionManager.Instance.GetRealSession(Convert.FromBase64String(sessionid));
				if (session != null)
				{
					try
					{
						using (var context = new DataContext())
						{
							context.Item.Remove(context.Item.Find(id));
							context.SaveChanges();
						}
					}
					catch (Exception e)
					{
						Console.WriteLine($"Caught exception while trying to remove item (id: {id}): " + e.Message);
					}
				}
			}
		}

		[HttpPut("add")]
		public void AddItem([FromBody] Item item)
		{
			Request.Query.TryGetValue("sessionid", out var vs);
			string sessionid = vs.FirstOrDefault();

			if (sessionid != null)
			{
				Session session = SessionManager.Instance.GetRealSession(Convert.FromBase64String(sessionid));
				if (session != null)
				{
					try
					{
						using (var context = new DataContext())
						{
							context.Item.Add(item);
							context.SaveChanges();
						}
					}
					catch (Exception e)
					{
						Console.WriteLine("Caught exception while trying to add item: " + e.Message);
					}
				}
			}
		}
	}
}
