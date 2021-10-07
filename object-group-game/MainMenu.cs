using GMap.NET;
using GMap.NET.MapProviders;
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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            healthBar.ForeColor = Color.Red;
            experienceBar.ForeColor = Color.Yellow;
            manaBar.ForeColor = Color.Blue;

            updateTimer.Start();

            updateBars();

            // config map
            liveMap.MapProvider = GMapProviders.GoogleMap;
            liveMap.ShowCenter = false;
            liveMap.Position = new PointLatLng(54.6961334816182, 25.2985095977783);
            liveMap.MinZoom = 0;
            liveMap.MaxZoom = 24;
            liveMap.Zoom = 9;

        }

        public void updateBars()
        {
            characterNameLabel.Text = LocalData.Player.Character.Name;
            healthBar.Value = (int)LocalData.Player.Character.Health;
            manaBar.Value = (int)LocalData.Player.Character.Mana;

            strengthBar.Value = LocalData.Player.Character.Attributes.Strength;
            dexterityBar.Value = LocalData.Player.Character.Attributes.Dexterity;
            intelligenceBar.Value = LocalData.Player.Character.Attributes.Intelligence;

            goldLabel.Text = "Gold: " + LocalData.Player.Character.Gold;
        }

        

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void health_SystemColorsChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            updateBars();
        }

        private void liveMap_Load(object sender, EventArgs e)
        {

        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
