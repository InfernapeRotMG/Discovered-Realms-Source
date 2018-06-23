namespace wServer.realm.worlds
{
    public class TheHive : World
    {
        public TheHive()
        {
            Name = "The Hive";
            ClientWorldName = "The Hive";
            Background = 0;
            Difficulty = 0;
            ShowDisplays = true;
            AllowTeleport = true;
        }

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.OrgaTheHive.jm", MapType.Json);
        }
    }
}
