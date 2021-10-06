using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game.Database
{
	class User
	{
		// Checks whether a login with provided username exists
		public bool UserExists(string username)
		{
			using(var context = new Database.UserContext())
			{
				return context.Login.Where(l => l.Username.Equals(username)).FirstOrDefault() != null;
			}
		}

		// Checks whether there is a login with provided username and password
		public bool CorrectPassword(string username, string password)
		{
			using(var context = new Database.UserContext())
			{
				return context.Login.Where(l => l.Username.Equals(username) && l.Password.Equals(password)).FirstOrDefault() != null;
			}
		}

		// Creates a new login inside the database
		// Assumes the username is checked for availability
		public void NewLogin(string username, string password)
		{
			using(var context = new Database.UserContext())
			{
				context.Login.Add(new Login() {
					Username = username,
					Password = password
				});

				context.SaveChanges();
			}
		}
	}
}
