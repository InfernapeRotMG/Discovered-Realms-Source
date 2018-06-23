#region

using wServer.networking;

#endregion

namespace wServer.realm.worlds
{
    public class HauntedCemeteryFinalBattle : World
    {
        public HauntedCemeteryFinalBattle()
        {
            Name = "Haunted Cemetery Final Battle";
            ClientWorldName = "Haunted Cemetery Final Battle";
            Background = 0;
            Difficulty = 5;
            AllowTeleport = true;
        }

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.HauntedCemeteryFinalBattle.jm", MapType.Json);
        }

        public override World GetInstance(Client psr)
        {
            return Manager.AddWorld(new HauntedCemeteryFinalBattle());
        }
    }
}