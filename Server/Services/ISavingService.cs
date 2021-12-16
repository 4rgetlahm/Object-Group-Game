using GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public interface ISavingService
    {
        void Save(Player player);
        //void SaveEquipment(Player player);
    }
}
