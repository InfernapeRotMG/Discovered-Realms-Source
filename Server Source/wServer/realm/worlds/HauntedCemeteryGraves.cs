#region

using wServer.networking;

#endregion

namespace wServer.realm.worlds
{
    public class HauntedCemeteryGraves : World
    {
        public HauntedCemeteryGraves()
        {
            Name = "Haunted Cemetery Graves";
            ClientWorldName = "Haunted Cemetery Graves";
            Background = 0;
            Difficulty = 5;
            AllowTeleport = true;
        }

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.HauntedCemeteryGraves.jm", MapType.Json);
        }

        public override World GetInstance(Client psr)
        {
            return Manager.AddWorld(new HauntedCemeteryGraves());
        }
    }
}