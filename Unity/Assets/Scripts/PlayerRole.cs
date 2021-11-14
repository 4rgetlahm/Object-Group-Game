using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[Flags]
public enum PlayerRole
{
    Default = 1,
    Subscriber = 2,
    Moderator = 4,
    Administrator = 8,
    PlayerModerator = Default | Subscriber | Moderator,
    Manager = Default | Subscriber | Moderator | Administrator
}

