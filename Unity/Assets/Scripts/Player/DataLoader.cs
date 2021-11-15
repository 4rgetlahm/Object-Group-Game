using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

        [SerializeField]
        private Button adminButton;

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

            goldLabel.text = "Gold: " + LocalPlayer.Instance.Gold;

            dexterityLabel.text = "Dexterity: " + LocalPlayer.Instance.Dexterity;
            strengthLabel.text = "Strength: " + LocalPlayer.Instance.Strength;
            intelligenceLabel.text = "Intelligence: " + LocalPlayer.Instance.Intelligence;
            Debug.Log(LocalPlayer.Instance.Username);
            if (LocalPlayer.Instance.ItemNameList == null)
            {
                LocalPlayer.Instance.ItemNameList.Add("No items!");
            }
            itemListLabel.text = String.Join("\n", LocalPlayer.Instance.ItemNameList.ToArray());

            adminButton.gameObject.SetActive(LocalPlayer.Instance.PlayerRole == PlayerRole.Administrator ? true : false);
        }
    }
}
