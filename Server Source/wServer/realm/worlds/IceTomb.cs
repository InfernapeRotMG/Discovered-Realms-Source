namespace wServer.realm.worlds
{
    public class IceTomb : World
    {
        public IceTomb()
        {
            Name = "Ice Tomb";
            ClientWorldName = "Ice Tomb";
            Background = 0;
            Difficulty = 0;
            ShowDisplays = true;
            AllowTeleport = false;
        }

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.IceTomb.jm", MapType.Json);
        }
    }
}
