using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using GameLibrary;

namespace Assets.Scripts.Player
{
    public class DataLoader : MonoBehaviour
    {

        [SerializeField]
        private List<TMP_Text> usernameLabel;

        [SerializeField]
        private List<TMP_Text> characterNameLabel;

        [SerializeField]
        private List<TMP_Text> healthLabel;
        [SerializeField]
        private List<TMP_Text> manaLabel;
        [SerializeField]
        private List<TMP_Text> expLabel;

        [SerializeField]
        private List<TMP_Text> goldLabel;

        [SerializeField]
        private List<TMP_Text> dexterityLabel;
        [SerializeField]
        private List<TMP_Text> strengthLabel;
        [SerializeField]
        private List<TMP_Text> intelligenceLabel;

        [SerializeField]
        private GameObject expeditionDataObject;
        [SerializeField]
        private List<TMP_Text> expeditionLocationLabels;
        [SerializeField]
        private List<TMP_Text> expeditionTitleLabels;

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
            usernameLabel.ForEach(t => t.text = LocalPlayer.Instance.Player.Name);
            characterNameLabel.ForEach(t => t.text = LocalPlayer.Instance.Player.Character.Name);

            goldLabel.ForEach(t => t.text = "Gold: " + LocalPlayer.Instance.Player.Character.Gold);

            dexterityLabel.ForEach(t => t.text = "Dexterity: " + LocalPlayer.Instance.Player.Character.Equipment.GetDexterity());
            strengthLabel.ForEach(t => t.text = "Strength: " + LocalPlayer.Instance.Player.Character.Equipment.GetStrength());
            intelligenceLabel.ForEach(t => t.text = "Intelligence: " + LocalPlayer.Instance.Player.Character.Equipment.GetIntelligence());

            adminButton.gameObject.SetActive(LocalPlayer.Instance.Player.PlayerRole.HasFlag(PlayerRole.Administrator) ? true : false);
            Debug.Log(LocalPlayer.Instance.Player.Character.Expedition);
            if(LocalPlayer.Instance.Player.Character.Expedition != null)
            {
                expeditionDataObject.SetActive(true);
                expeditionTitleLabels.ForEach(t => t.text = LocalPlayer.Instance.Player.Character.Expedition.Mission.Title);
                //expeditionLocationLabels.ForEach(t => t.text = LocalPlayer.Instance.Player.Character.Expedition.Mission.Title);
            }
            else
            {
                expeditionDataObject.SetActive(false);
            }
        }
    }
}
