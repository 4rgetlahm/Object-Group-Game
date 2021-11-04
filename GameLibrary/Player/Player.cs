using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game
{
    [Table("Players")]
    public class Player
    {
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
    }
}
