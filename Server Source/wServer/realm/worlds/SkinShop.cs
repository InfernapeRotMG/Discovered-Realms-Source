namespace wServer.realm.worlds
{
    public class SkinShop : World
    {
        public SkinShop()
        {
            Id = SKIN_SHOP;
            Name = "Skin Shop";
            ClientWorldName = "Skin Shop";
            Background = 0;
            Difficulty = 0;
            ShowDisplays = true;
            AllowTeleport = true;
            SetMusic("Nexus");
        }

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.OrgaSkinShop.jm", MapType.Json);
        }
    }
}
