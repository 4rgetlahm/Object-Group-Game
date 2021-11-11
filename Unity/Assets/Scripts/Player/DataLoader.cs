using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.Player
{
    public class DataLoader : MonoBehaviour
    {

        [SerializeField]
        private TMP_Text usernameLabel;

        [SerializeField]
        private TMP_Text characterNameLabel;

        [SerializeField]
        private TMP_Text healthLabel;
        [SerializeField]
        private TMP_Text manaLabel;
        [SerializeField]
        private TMP_Text expLabel;

        [SerializeField]
        private TMP_Text goldLabel;

        [SerializeField]
        private TMP_Text dexterityLabel;
        [SerializeField]
        private TMP_Text strengthLabel;
        [SerializeField]
        private TMP_Text intelligenceLabel;

        [SerializeField]
        private TMP_Text itemListLabel;

        public DataLoader()
        {
            LocalPlayer.Instance.LocalPlayerUpdateEvent += this.updateLabels;
        }

        /*private void Update()
        {
            LocalPlayer.Instance.Health += 10;
        }*/

        void updateLabels(EventArgs eventArgs)
        {
            usernameLabel.text = LocalPlayer.Instance.Username;
            characterNameLabel.text = LocalPlayer.Instance.CharacterName;

            healthLabel.text = "Health: " + LocalPlayer.Instance.Health;
            manaLabel.text = "Mana: " + LocalPlayer.Instance.Mana;
            expLabel.text = "Experience: " + LocalPlayer.Instance.Experience;

            goldLabel.text = "Gold: " + LocalPlayer.Instance.Gold;

            dexterityLabel.text = "Dexterity: " + LocalPlayer.Instance.Dexterity;
            strengthLabel.text = "Strength: " + LocalPlayer.Instance.Strength;
            intelligenceLabel.text = "Intelligence: " + LocalPlayer.Instance.Intelligence;
            itemListLabel.text = String.Join("\n", LocalPlayer.Instance.ItemNameList.ToArray());
        }
    }
}
