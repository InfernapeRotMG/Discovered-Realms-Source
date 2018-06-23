#region


using System;
using System.Linq;
using wServer.networking.svrPackets;
using wServer.realm;
using wServer.realm.entities;

#endregion

namespace wServer.logic.behaviors
{
    public class SpecificHeal : Behavior
    {
        //State storage: cooldown timer

        private readonly string group;
        private readonly int amount;
        private readonly double range;
        private Cooldown coolDown;

        public SpecificHeal(double range, int amount, string group, Cooldown coolDown = new Cooldown())
        {
            this.range = (float)range;
            this.amount = amount;
            this.group = group;
            this.coolDown = coolDown.Normalize();
        }

        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            state = 0;
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
            int cool = (int)state;

            if (cool <= 0)
            {
                if (group == "Self")
                {
                    Enemy entity = host as Enemy;
                    int newHp = entity.HP + amount;
                    if (newHp <= entity.ObjectDesc.MaxHP)
                    {
                        entity.HP = entity.HP + amount;
                        entity.UpdateCount++;
                        entity.Owner.BroadcastPacket(new ShowEffectPacket
                        {
                            EffectType = EffectType.Potion,
                            TargetId = entity.Id,
                            Color = new ARGB(0xffffffff)
                        }, null);
                        entity.Owner.BroadcastPacket(new NotificationPacket
                        {
                            ObjectId = entity.Id,
                            Text = "{\"key\":\"blank\",\"tokens\":{\"data\":\"+" + amount + "\"}}",
                            Color = new ARGB(0xff00ff00)
                        }, null);
                    }
                    else
                    {
                        int defecitToMaxHp = Convert.ToInt32(entity.ObjectDesc.MaxHP) - entity.HP;
                        if (defecitToMaxHp <= 0)
                        {
                        }
                        else
                        {
                            entity.HP = entity.HP + defecitToMaxHp;
                            entity.UpdateCount++;
                            entity.Owner.BroadcastPacket(new ShowEffectPacket
                            {
                                EffectType = EffectType.Potion,
                                TargetId = entity.Id,
                                Color = new ARGB(0xffffffff)
                            }, null);
                            entity.Owner.BroadcastPacket(new NotificationPacket
                            {
                                ObjectId = entity.Id,
                                Text = "{\"key\":\"blank\",\"tokens\":{\"data\":\"+" + amount + "\"}}",
                                Color = new ARGB(0xff00ff00)
                            }, null);
                        }
                    }
                }
                else
                {
                    foreach (Enemy entity in host.GetNearestEntitiesByGroup(range, group).OfType<Enemy>())
                    {
                        int newHp = entity.HP + amount;
                        if (newHp <= entity.ObjectDesc.MaxHP)
                        {
                            entity.HP = entity.HP + amount;
                            entity.UpdateCount++;
                            entity.Owner.BroadcastPacket(new ShowEffectPacket
                            {
                                EffectType = EffectType.Potion,
                                TargetId = entity.Id,
                                Color = new ARGB(0xffffffff)
                            }, null);
                            entity.Owner.BroadcastPacket(new NotificationPacket
                            {
                                ObjectId = entity.Id,
                                Text = "{\"key\":\"blank\",\"tokens\":{\"data\":\"+" + amount + "\"}}",
                                Color = new ARGB(0xff00ff00)
                            }, null);
                        }
                        else
                        {
                            int defecitToMaxHp = Convert.ToInt32(entity.ObjectDesc.MaxHP) - entity.HP;
                            if (defecitToMaxHp <= 0)
                            {
                            }
                            else
                            {
                                entity.HP = entity.HP + defecitToMaxHp;
                                entity.UpdateCount++;
                                entity.Owner.BroadcastPacket(new ShowEffectPacket
                                {
                                    EffectType = EffectType.Potion,
                                    TargetId = entity.Id,
                                    Color = new ARGB(0xffffffff)
                                }, null);
                                entity.Owner.BroadcastPacket(new NotificationPacket
                                {
                                    ObjectId = entity.Id,
                                    Text = "{\"key\":\"blank\",\"tokens\":{\"data\":\"+" + amount + "\"}}",
                                    Color = new ARGB(0xff00ff00)
                                }, null);
                            }
                        }
                    }
                }
                cool = coolDown.Next(Random);
            }
            else
                cool -= time.thisTickTimes;

            state = cool;
        }
    }
}