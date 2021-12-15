using GameLibrary;
using Server.Authentication;

namespace Server.Services
{
    public interface IEquipmentService
    {
        void EquipItem(byte[] sessionID, Item item);
        void EquipItem(Character character, Item item);
        void EquipItem(Session session, Item item);
    }
}