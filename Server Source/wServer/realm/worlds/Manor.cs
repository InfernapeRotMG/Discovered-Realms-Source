namespace wServer.realm.worlds
{
    public class ManoroftheImmortals : World
    {
        public ManoroftheImmortals()
        {
            Name = "Manor of the Immortals";
            ClientWorldName = "Manor of the Immortals";
            Background = 0;
            Difficulty = 0;
            ShowDisplays = true;
            AllowTeleport = true;
        }

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.OrgaManor.jm", MapType.Json);
        }
    }
}
