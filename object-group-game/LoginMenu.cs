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
    using object_group_game.Extensions;
    public partial class LoginMenu : Form
    {
        public LoginMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void LoadGame(Player player)
        {
            Session.Player = player;
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Hide();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            Tuple<int, Player> loginResponse = Authenticator.GetAuthenticator().Login(usernameInput.Text, passwordInput.Text);

            switch (loginResponse.GetStatus())
            {
                case 1:
                    LoadGame(loginResponse.GetPlayer());
                    break;
                case -1:
                    MessageBox.Show("Player doesn't exist!");
                    break;
                case 0:
                    MessageBox.Show("Incorrect login!");
                    break;
                default:
                    MessageBox.Show("Login failed!");
                    break;
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            Tuple<int, Player> registerResponse = Authenticator.GetAuthenticator().Register(usernameInput.Text, passwordInput.Text);

            switch (registerResponse.GetStatus())
            {
                case 1:
                    LoadGame(registerResponse.GetPlayer());
                    break;
                case -1:
                    MessageBox.Show("Username already exists!");
                    break;
                case -2:
                    MessageBox.Show("Incorrect username!");
                    break;
                default:
                    MessageBox.Show("Registration failed!");
                    break;
            }
        }
    }
}
