#region

using System;
using System.Linq;
using wServer.networking.svrPackets;
using wServer.realm;
using wServer.realm.entities;
using wServer.realm.entities.player;

#endregion

namespace wServer.logic.behaviors
{
    public class BringPlayer : CycleBehavior
    {

        private readonly int newX;
        private readonly int newY;
        private readonly int range;
        protected Cooldown coolDown;
        protected readonly int coolDownOffset;

        public BringPlayer(int newX, int newY, int range, Cooldown coolDown = new Cooldown(), int coolDownOffset = 0)
        {
            this.newX = newX;
            this.newY = newY;
            this.range = range;
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
                foreach (Player o in host.Owner.PlayersCollision.HitTest(host.X, host.Y, range).OfType<Player>())
                {
                    o.Move(newX + 0.5f, newY + 0.5f);
                    o.UpdateCount++;
                    o.Owner.BroadcastPacket(new GotoPacket
                    {
                        ObjectId = o.Id,
                        Position = new Position
                        {
                            X = o.X,
                            Y = o.Y
                        }
                    }, null);
                }
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


