using GameLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    [Table("Players")]
    public class Player : IEquatable<Player>
    {
        public delegate void PlayerUpdateEventHandler(PlayerEventArgs args);
        public event PlayerUpdateEventHandler PlayerUpdateEvent;

        [Key]
        public int PlayerID { get; set; }
        public string Name { get; private set; }

        [DefaultValue(PlayerRole.Default)]
        public PlayerRole PlayerRole { get; private set; }
        public virtual Character Character { get; set; }

        protected Player()
        {

        }

        public Player(string name)
        {
            this.Name = name;
        }

        public Player(int PlayerID, string name)
        {
            this.PlayerID = PlayerID;
            this.Name = name;
        }

        public void registerEvents()
        {
            this.Character.CharacterUpdateEvent += this.OnCharacterUpdate;
        }

        public bool Equals(Player other)
        {
            // First two lines are just optimizations
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return PlayerID.Equals(other.PlayerID);
        }

        public void OnCharacterUpdate(CharacterEventArgs args)
        {
            OnPlayerUpdate(new PlayerEventArgs(this));
        }

        public void OnPlayerUpdate(PlayerEventArgs args)
        {
            if (PlayerUpdateEvent != null)
            {
                PlayerUpdateEvent(args);
            }
        }
    }
}
