namespace wServer.realm.worlds
{
    public class OryxThree : World
    {
        public OryxThree()
        {
            Name = "Oryx Three";
            ClientWorldName = "dungeons.Oryx_Three";
            Dungeon = true;
            Background = 0;
            AllowTeleport = true;
        }

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.oryxthree.jm", MapType.Json);
        }
    }
}

