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
            Authenticator authenticator = Authenticator.GetAuthenticator();
            Tuple<int, Player> loginResponse = authenticator.Login(usernameInput.Text, passwordInput.Text);

            if(loginResponse.Item1 == 1)
            {
                LocalData.Player = loginResponse.Item2;
                MainMenu mainMenu = new MainMenu();
                this.Hide();
                mainMenu.Show();
                return;
            }

            MessageBox.Show("Error occured! Error code " + loginResponse.Item1);

        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            Authenticator.GetAuthenticator().Register(usernameInput.Text, passwordInput.Text);
        }
    }
}
