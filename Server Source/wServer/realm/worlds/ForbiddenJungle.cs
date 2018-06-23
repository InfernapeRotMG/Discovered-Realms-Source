namespace wServer.realm.worlds
{
    public class ForbiddenJungle : World
    {
        public ForbiddenJungle()
        {
            Name = "Forbidden Jungle";
            ClientWorldName = "Forbidden Jungle";
            Background = 0;
            Difficulty = 0;
            ShowDisplays = true;
            AllowTeleport = true;
        }

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.OrgaJungle.jm", MapType.Json);
        }
    }
}
