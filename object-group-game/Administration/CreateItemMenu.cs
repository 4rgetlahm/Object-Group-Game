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
    public partial class CreateItemMenu : Form
    {

        public CreateItemMenu()
        {
            InitializeComponent();
        }

        public CreateItemMenu(Player player)
        {
            InitializeComponent();
            if(!player.PlayerRole.HasFlag(PlayerRole.Administrator))
            {
                throw new Exception("Player is not administrator!");
            }

            effectInput.Items.Add("");
            foreach(Effect effect in EffectList.GetInstance().Effects)
            {
                effectInput.Items.Add(effect.DisplayName);
            }
        }

        private void CreateItemMenu_Load(object sender, EventArgs e)
        {
            
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            Item newItem = new Item(nameInput.Text, (int) strInput.Value, (int) dexInput.Value, (int) intInput.Value);
            using (var context = new DataContext())
            {
                if (effectInput.SelectedItem.ToString() != "")
                {
                    Effect effect = context.Effect.First(e => e.DisplayName == effectInput.SelectedItem.ToString());
                    newItem.Effects.Add(effect);
                }
                context.Add(newItem);
                context.SaveChanges();
            }
            this.Close();
        }
    }
}
