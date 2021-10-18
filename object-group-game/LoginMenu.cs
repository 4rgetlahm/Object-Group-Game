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

        private void LoadGame(Player player)
        {
            LocalData.Player = player;
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Hide();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            Tuple<int, Player> loginResponse = Authenticator.GetAuthenticator().Login(usernameInput.Text, passwordInput.Text);
            if(loginResponse.Item1 == 1)
            {
                LoadGame(loginResponse.Item2);
            }
            else if(loginResponse.Item1 < 0)
            {
                switch(loginResponse.Item1){
                    case -1:
                        MessageBox.Show("Player doesn't exist!");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Incorrect login!");
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            Tuple<int, Player> registerResponse = Authenticator.GetAuthenticator().Register(usernameInput.Text, passwordInput.Text);
            if (registerResponse.Item1 == 1)
            {
                LoadGame(registerResponse.Item2);
            }
            else if (registerResponse.Item1 < 0)
            {
                switch (registerResponse.Item1)
                {
                    case -1:
                        MessageBox.Show("Username already exists!");
                        break;
                    case -2:
                        MessageBox.Show("Incorrect username!");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Registration failed");
            }
        }
    }
}
