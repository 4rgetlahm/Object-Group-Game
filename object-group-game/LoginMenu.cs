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

            Tuple<int, Player> loginResponse = Authenticator.GetAuthenticator().Login(usernameInput.Text, passwordInput.Text);
            if(loginResponse.Item1 == 1)
            {
                LocalData.Player = loginResponse.Item2;
                MainMenu mainMenu = new MainMenu();
                mainMenu.Show();
                this.Hide();
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            Authenticator.GetAuthenticator().Register(usernameInput.Text, passwordInput.Text);
        }
    }
}
