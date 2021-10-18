using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
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

            if(LocalData.Player.PlayerRole.HasFlag(PlayerRole.Administrator))
            {
                gameMasterToolStripMenuItem.Visible = true;
            }
            else
            {
                gameMasterToolStripMenuItem.Visible = false;
            }

        }

        public void updateBars()
        {
            characterNameLabel.Text = LocalData.Player.Character.Name;
            healthBar.Value = (int)LocalData.Player.Character.Health;
            manaBar.Value = (int)LocalData.Player.Character.Mana;

            //strengthBar.Value = LocalData.Player.Character.GetStrength();
            //dexterityBar.Value = LocalData.Player.Character.GetDexterity();
            //intelligenceBar.Value = LocalData.Player.Character.GetIntelligence();

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
            var loginForm = Application.OpenForms.Cast<Form>().Where(form => form.Name == "LoginMenu").FirstOrDefault();
            if(loginForm != null)
            {
                LocalData.Clear();
                loginForm.Show();
                this.Close();
            }
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            updateBars();
        }


        private void liveMap_Load(object sender, EventArgs e)
        {
            GMapOverlay markers = new GMapOverlay("markers");

            foreach(Location location in LocationList.GetInstance().Locations)
            {
                GMapMarker marker = new GMarkerGoogle(new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longtitude), GMarkerGoogleType.blue);
                markers.Markers.Add(marker);
            }
            liveMap.Overlays.Add(markers);
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void createNewItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItemMenu createItemMenu = new CreateItemMenu(LocalData.Player);
            createItemMenu.ShowDialog();
        }

        private void createNewLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
