namespace wServer.realm.worlds
{
    public class ToxicSewers : World
    {
        public ToxicSewers()
        {
            Name = "Toxic Sewers";
            ClientWorldName = "Toxic Sewers";
            Background = 0;
            Difficulty = 0;
            ShowDisplays = true;
            AllowTeleport = true;
        }

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.OrgaSewers.jm", MapType.Json);
        }
    }
}
