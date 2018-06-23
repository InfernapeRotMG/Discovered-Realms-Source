#region

using System.Linq;
using wServer.networking.svrPackets;
using wServer.realm;
using wServer.realm.entities;
using wServer.realm.entities.player;

#endregion

namespace wServer.logic.behaviors
{
    public class Say : CycleBehavior
    {
        private readonly string text;
        protected Cooldown coolDown;
        protected readonly int coolDownOffset;

        public Say(string text, int coolDownOffset = 0, Cooldown coolDown = new Cooldown())
        {
            this.text = text;
            this.coolDownOffset = coolDownOffset;
            this.coolDown = coolDown.Normalize();
        }


        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            state = coolDownOffset;
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
            if (state == null) return;
            int cool = (int)state;
            Status = CycleStatus.NotStarted;
            if (cool <= 0)
            {
                TextPacket packet = new TextPacket
                {
                    Name = "#" + (host.ObjectDesc.DisplayId ?? host.ObjectDesc.ObjectId),
                    ObjectId = host.Id,
                    Stars = -1,
                    BubbleTime = 5,
                    Recipient = "",
                    Text = text,
                    CleanText = ""
                };
                if (text.Contains("{PLAYER}"))
                {
                    Entity player = host.GetNearestEntity(10, null);
                    if (player == null) return;
                    text.Replace("{PLAYER}", player.Name);
                }
                text.Replace("{HP}", (host as Enemy).HP.ToString());
                foreach (Player i in host.Owner.PlayersCollision.HitTest(host.X, host.Y, 999).OfType<Player>())
                    if (host.Dist(i) < 999)
                        i.Client.SendPacket(packet);
            }
            else
            {
                cool -= time.thisTickTimes;
                Status = CycleStatus.InProgress;
            }
            state = cool;
        }
    }
}