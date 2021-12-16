using GameLibrary;
using GameLibrary.Inventory;
using Server.Authentication;

namespace Server.Services
{
    public interface IEquipmentService
    {
        void EquipItem(byte[] sessionID, Item item);
        void EquipItem(Player player, Item item);
        void EquipItem(Session session, Item item);

        void UnequipItem(Session session, ItemType itemType);
        void UnequipItem(byte[] sessionID, ItemType itemType);
        void UnequipItem(Player player, ItemType itemType);
    }
}