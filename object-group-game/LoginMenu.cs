using object_group_game.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace object_group_game
{
    public partial class LoginMenu : Form
    {
        public LoginMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            //Login attempt with database
            //If success, open MainMenu

            using (var db = new DataContext())
            {
                var player = new Player(0, "test");
                player.Character = new Character(0, "testchar", 100.0, 100999.0, 50.0, 500.0);

                List <Effect> effects = new List<Effect>();
                effects.Add(new Effect("coolstatus", "cooler name", 5));
                effects.Add(new Effect("shiny", "shiny name", 0));

                List<Item> items = new List<Item>();

                Item item = new Item(0, "Thunderfury", 999, 100, 0, effects);

                List<Effect> newEffects = new List<Effect>(effects);

                effects.Add(new Effect("anoda", "one", 99));

                Item item2 = new Item(0, "Bonecrusher", 5, 106, 3, newEffects);

                items.Add(item);
                items.Add(item2);

                player.Character.Items = items;

                db.Add(player);


                db.SaveChanges();
            }
            
            /*List<Location> locations = World.GetLocations();
            foreach(Location location in locations)
            {
                MessageBox.Show(location.DisplayName);
            }*/

            /*Authenticator authenticator = Authenticator.GetAuthenticator();
            Tuple<int, Player> loginResponse = authenticator.Login(usernameInput.Text, passwordInput.Text);

            if(loginResponse.Item1 == 1)
            {
                LocalData.Player = loginResponse.Item2;
                MainMenu mainMenu = new MainMenu();
                this.Hide();
                mainMenu.Show();
                return;
            }

            MessageBox.Show("Error occured! Error code " + loginResponse.Item1);*/

        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            Authenticator.GetAuthenticator().Register(usernameInput.Text, passwordInput.Text);
        }
    }
}
