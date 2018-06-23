#region

using wServer.networking;

#endregion

namespace wServer.realm.worlds
{
    public class HauntedCemeteryGates : World
    {
        public HauntedCemeteryGates()
        {
            Name = "Haunted Cemetery Gates";
            ClientWorldName = "Haunted Cemetery Gates";
            Background = 0;
            Difficulty = 5;
            AllowTeleport = true;
        }

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.HauntedCemeteryGates.jm", MapType.Json);
        }

        public override World GetInstance(Client psr)
        {
            return Manager.AddWorld(new HauntedCemeteryGates());
        }
    }
}