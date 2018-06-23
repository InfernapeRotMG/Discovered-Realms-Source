﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wServer.networking.svrPackets;
using wServer.realm;
using wServer.realm.entities;
using wServer.realm.entities.player;

namespace wServer.logic.behaviors.PetBehaviors
{
    internal class PetHealHP : PetBehavior
    {
        //<Parameters>
        //   <MaxHeal min = "10" max="90" curve="exp_incr"/>
        //   <Cooldown min = "10" max="1" curve="dim_returns"/>
        //</Parameters>

        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            state = 1000;
            base.OnStateEntry(host, time, ref state);
        }

        protected override bool PlayerOwnerRequired => true;

        protected override void TickCore(Pet pet, RealmTime time, ref object state)
        {
            var cool = (int)state;
            if (cool <= 0)
            {
                Player player = pet.GetEntity(pet.PlayerOwner.Id) as Player;
                if (player == null) return;

                int maxHp = player.Stats[0] + player.Boost[0];
                int h = GetHP(pet, ref cool);
                if (h == -1) return;
                int newHp = Math.Min(maxHp, player.HP + h);
                if (newHp != player.HP)
                {
                    if (player.HasConditionEffect(ConditionEffectIndex.Sick))
                    {
                        player.Owner.BroadcastPacket(new ShowEffectPacket
                        {
                            EffectType = EffectType.Trail,
                            TargetId = pet.Id,
                            PosA = new Position { X = player.X, Y = player.Y },
                            Color = new ARGB(0xffffffff)
                        }, null);
                        player.Owner.BroadcastPacket(new NotificationPacket
                        {
                            ObjectId = player.Id,
                            Text = "{\"key\":\"blank\",\"tokens\":{\"data\":\"No Effect\"}}",
                            Color = new ARGB(0xFF0000)
                        }, null);
                        state = cool;
                        return;
                    }
                    int n = newHp - player.HP;
                    player.HP = newHp;
                    player.UpdateCount++;
                    player.Owner.BroadcastPacket(new ShowEffectPacket
                    {
                        EffectType = EffectType.Potion,
                        TargetId = player.Id,
                        Color = new ARGB(0xffffffff)
                    }, null);
                    player.Owner.BroadcastPacket(new ShowEffectPacket
                    {
                        EffectType = EffectType.Trail,
                        TargetId = pet.Id,
                        PosA = new Position { X = player.X, Y = player.Y },
                        Color = new ARGB(0xffffffff)
                    }, null);
                    player.Owner.BroadcastPacket(new NotificationPacket
                    {
                        ObjectId = player.Id,
                        Text = "{\"key\":\"blank\",\"tokens\":{\"data\":\"+" + n + "\"}}",
                        Color = new ARGB(0xff00ff00)
                    }, null);
                }
            }
            else
                cool -= time.thisTickTimes;

            state = cool;
        }

        private int GetHP(Pet host, ref int cooldown)
        {
            for (var i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        if (host.FirstPetLevel.Ability == Ability.Heal)
                        {
                            return CalculateHeal(host.FirstPetLevel.Level, ref cooldown);
                        }
                        break;
                    case 1:
                        if (host.SecondPetLevel.Ability == Ability.Heal)
                        {
                            return CalculateHeal(host.SecondPetLevel.Level, ref cooldown);
                        }
                        break;
                    case 2:
                        if (host.ThirdPetLevel.Ability == Ability.Heal)
                        {
                            return CalculateHeal(host.ThirdPetLevel.Level, ref cooldown);
                        }
                        break;
                }
            }
            return -1;
        }

        private int CalculateHeal(int level, ref int cooldown)
        {
            if (Enumerable.Range(1, 1).Contains(level))
            {
                cooldown = 10000;
                return 10;
            }
            if (Enumerable.Range(2, 1).Contains(level))
            {
                cooldown = 9350;
                return 10;
            }
            if (Enumerable.Range(3, 1).Contains(level))
            {
                cooldown = 8690;
                return 10;
            }
            if (Enumerable.Range(4, 1).Contains(level))
            {
                cooldown = 8040;
                return 10;
            }
            if (Enumerable.Range(5, 1).Contains(level))
            {
                cooldown = 7390;
                return 10;
            }
            if (Enumerable.Range(6, 1).Contains(level))
            {
                cooldown = 7130;
                return 10;
            }
            if (Enumerable.Range(7, 1).Contains(level))
            {
                cooldown = 6880;
                return 10;
            }
            if (Enumerable.Range(8, 1).Contains(level))
            {
                cooldown = 6620;
                return 10;
            }
            if (Enumerable.Range(9, 1).Contains(level))
            {
                cooldown = 6370;
                return 10;
            }
            if (Enumerable.Range(10, 1).Contains(level))
            {
                cooldown = 6110;
                return 10;
            }
            if (Enumerable.Range(11, 1).Contains(level))
            {
                cooldown = 5940;
                return 10;
            }
            if (Enumerable.Range(12, 1).Contains(level))
            {
                cooldown = 5760;
                return 10;
            }
            if (Enumerable.Range(13, 1).Contains(level))
            {
                cooldown = 5590;
                return 10;
            }
            if (Enumerable.Range(14, 1).Contains(level))
            {
                cooldown = 5420;
                return 10;
            }
            if (Enumerable.Range(15, 1).Contains(level))
            {
                cooldown = 5250;
                return 11;
            }
            if (Enumerable.Range(16, 1).Contains(level))
            {
                cooldown = 5130;
                return 11;
            }
            if (Enumerable.Range(17, 1).Contains(level))
            {
                cooldown = 5010;
                return 11;
            }
            if (Enumerable.Range(18, 1).Contains(level))
            {
                cooldown = 4900;
                return 11;
            }
            if (Enumerable.Range(19, 1).Contains(level))
            {
                cooldown = 4780;
                return 11;
            }
            if (Enumerable.Range(20, 1).Contains(level))
            {
                cooldown = 4670;
                return 11;
            }
            if (Enumerable.Range(21, 1).Contains(level))
            {
                cooldown = 4580;
                return 12;
            }
            if (Enumerable.Range(22, 1).Contains(level))
            {
                cooldown = 4490;
                return 12;
            }
            if (Enumerable.Range(23, 1).Contains(level))
            {
                cooldown = 4400;
                return 12;
            }
            if (Enumerable.Range(24, 1).Contains(level))
            {
                cooldown = 4310;
                return 12;
            }
            if (Enumerable.Range(25, 1).Contains(level))
            {
                cooldown = 4220;
                return 12;
            }
            if (Enumerable.Range(26, 1).Contains(level))
            {
                cooldown = 4140;
                return 13;
            }
            if (Enumerable.Range(27, 1).Contains(level))
            {
                cooldown = 4060;
                return 13;
            }
            if (Enumerable.Range(28, 1).Contains(level))
            {
                cooldown = 3980;
                return 13;
            }
            if (Enumerable.Range(29, 1).Contains(level))
            {
                cooldown = 3900;
                return 14;
            }
            if (Enumerable.Range(30, 1).Contains(level))
            {
                cooldown = 3820;
                return 14;
            }
            if (Enumerable.Range(31, 1).Contains(level))
            {
                cooldown = 3750;
                return 14;
            }
            if (Enumerable.Range(32, 1).Contains(level))
            {
                cooldown = 3690;
                return 14;
            }
            if (Enumerable.Range(33, 1).Contains(level))
            {
                cooldown = 3620;
                return 15;
            }
            if (Enumerable.Range(34, 1).Contains(level))
            {
                cooldown = 3550;
                return 15;
            }
            if (Enumerable.Range(35, 1).Contains(level))
            {
                cooldown = 3480;
                return 16;
            }
            if (Enumerable.Range(36, 1).Contains(level))
            {
                cooldown = 3430;
                return 16;
            }
            if (Enumerable.Range(37, 1).Contains(level))
            {
                cooldown = 3390;
                return 16;
            }
            if (Enumerable.Range(38, 1).Contains(level))
            {
                cooldown = 3340;
                return 17;
            }
            if (Enumerable.Range(39, 1).Contains(level))
            {
                cooldown = 3290;
                return 17;
            }
            if (Enumerable.Range(40, 1).Contains(level))
            {
                cooldown = 3240;
                return 18;
            }
            if (Enumerable.Range(41, 1).Contains(level))
            {
                cooldown = 3190;
                return 18;
            }
            if (Enumerable.Range(42, 1).Contains(level))
            {
                cooldown = 3130;
                return 19;
            }
            if (Enumerable.Range(43, 1).Contains(level))
            {
                cooldown = 3080;
                return 19;
            }
            if (Enumerable.Range(44, 1).Contains(level))
            {
                cooldown = 3030;
                return 20;
            }
            if (Enumerable.Range(45, 1).Contains(level))
            {
                cooldown = 2970;
                return 20;
            }
            if (Enumerable.Range(46, 1).Contains(level))
            {
                cooldown = 2910;
                return 21;
            }
            if (Enumerable.Range(47, 1).Contains(level))
            {
                cooldown = 2850;
                return 21;
            }
            if (Enumerable.Range(48, 1).Contains(level))
            {
                cooldown = 2780;
                return 22;
            }
            if (Enumerable.Range(49, 1).Contains(level))
            {
                cooldown = 2720;
                return 22;
            }
            if (Enumerable.Range(50, 1).Contains(level))
            {
                cooldown = 2660;
                return 23;
            }
            if (Enumerable.Range(51, 1).Contains(level))
            {
                cooldown = 2610;
                return 18;
            }
            if (Enumerable.Range(52, 1).Contains(level))
            {
                cooldown = 2560;
                return 19;
            }
            if (Enumerable.Range(53, 1).Contains(level))
            {
                cooldown = 2510;
                return 19;
            }
            if (Enumerable.Range(54, 1).Contains(level))
            {
                cooldown = 2460;
                return 20;
            }
            if (Enumerable.Range(55, 1).Contains(level))
            {
                cooldown = 2410;
                return 20;
            }
            if (Enumerable.Range(56, 1).Contains(level))
            {
                cooldown = 2380;
                return 21;
            }
            if (Enumerable.Range(57, 1).Contains(level))
            {
                cooldown = 2340;
                return 21;
            }
            if (Enumerable.Range(58, 1).Contains(level))
            {
                cooldown = 2300;
                return 22;
            }
            if (Enumerable.Range(59, 1).Contains(level))
            {
                cooldown = 2260;
                return 22;
            }
            if (Enumerable.Range(60, 1).Contains(level))
            {
                cooldown = 2220;
                return 30;
            }
            if (Enumerable.Range(61, 1).Contains(level))
            {
                cooldown = 2190;
                return 31;
            }
            if (Enumerable.Range(62, 1).Contains(level))
            {
                cooldown = 2150;
                return 32;
            }
            if (Enumerable.Range(63, 1).Contains(level))
            {
                cooldown = 2120;
                return 33;
            }
            if (Enumerable.Range(64, 1).Contains(level))
            {
                cooldown = 2080;
                return 34;
            }
            if (Enumerable.Range(65, 1).Contains(level))
            {
                cooldown = 2050;
                return 35;
            }
            if (Enumerable.Range(66, 1).Contains(level))
            {
                cooldown = 2030;
                return 36;
            }
            if (Enumerable.Range(67, 1).Contains(level))
            {
                cooldown = 2010;
                return 37;
            }
            if (Enumerable.Range(68, 1).Contains(level))
            {
                cooldown = 1990;
                return 38;
            }
            if (Enumerable.Range(69, 1).Contains(level))
            {
                cooldown = 1980;
                return 39;
            }
            if (Enumerable.Range(70, 1).Contains(level))
            {
                cooldown = 1960;
                return 40;
            }
            if (Enumerable.Range(71, 1).Contains(level))
            {
                cooldown = 1930;
                return 41;
            }
            if (Enumerable.Range(72, 1).Contains(level))
            {
                cooldown = 1910;
                return 42;
            }
            if (Enumerable.Range(73, 1).Contains(level))
            {
                cooldown = 1880;
                return 43;
            }
            if (Enumerable.Range(74, 1).Contains(level))
            {
                cooldown = 1850;
                return 45;
            }
            if (Enumerable.Range(75, 1).Contains(level))
            {
                cooldown = 1830;
                return 46;
            }
            if (Enumerable.Range(76, 1).Contains(level))
            {
                cooldown = 1790;
                return 47;
            }
            if (Enumerable.Range(77, 1).Contains(level))
            {
                cooldown = 1740;
                return 48;
            }
            if (Enumerable.Range(78, 1).Contains(level))
            {
                cooldown = 1700;
                return 50;
            }
            if (Enumerable.Range(79, 1).Contains(level))
            {
                cooldown = 1650;
                return 51;
            }
            if (Enumerable.Range(80, 1).Contains(level))
            {
                cooldown = 1610;
                return 53;
            }
            if (Enumerable.Range(81, 1).Contains(level))
            {
                cooldown = 1570;
                return 54;
            }
            if (Enumerable.Range(82, 1).Contains(level))
            {
                cooldown = 1530;
                return 55;
            }
            if (Enumerable.Range(83, 1).Contains(level))
            {
                cooldown = 1500;
                return 57;
            }
            if (Enumerable.Range(84, 1).Contains(level))
            {
                cooldown = 1460;
                return 59;
            }
            if (Enumerable.Range(85, 1).Contains(level))
            {
                cooldown = 1420;
                return 60;
            }
            if (Enumerable.Range(86, 1).Contains(level))
            {
                cooldown = 1420;
                return 62;
            }
            if (Enumerable.Range(87, 1).Contains(level))
            {
                cooldown = 1420;
                return 64;
            }
            if (Enumerable.Range(88, 1).Contains(level))
            {
                cooldown = 1420;
                return 65;
            }
            if (Enumerable.Range(89, 1).Contains(level))
            {
                cooldown = 1420;
                return 67;
            }
            if (Enumerable.Range(90, 1).Contains(level))
            {
                cooldown = 1420;
                return 69;
            }
            if (Enumerable.Range(91, 1).Contains(level))
            {
                cooldown = 1570;
                return 71;
            }
            if (Enumerable.Range(92, 1).Contains(level))
            {
                cooldown = 1530;
                return 73;
            }
            if (Enumerable.Range(93, 1).Contains(level))
            {
                cooldown = 1500;
                return 75;
            }
            if (Enumerable.Range(94, 1).Contains(level))
            {
                cooldown = 1460;
                return 77;
            }
            if (Enumerable.Range(95, 1).Contains(level))
            {
                cooldown = 1420;
                return 79;
            }
            if (Enumerable.Range(96, 1).Contains(level))
            {
                cooldown = 1420;
                return 81;
            }
            if (Enumerable.Range(97, 1).Contains(level))
            {
                cooldown = 1420;
                return 83;
            }
            if (Enumerable.Range(98, 1).Contains(level))
            {
                cooldown = 1420;
                return 85;
            }
            if (Enumerable.Range(99, 1).Contains(level))
            {
                cooldown = 1420;
                return 88;
            }
            if (!Enumerable.Range(100, 1).Contains(level)) throw new Exception("Invalid PetLevel");
            cooldown = 1000;
            return 90;
            //switch (level)
            //{
            //    case 1:
            //        cooldown = 10000;
            //        return 10;
            //    case 2:
            //        cooldown = 9200;
            //        return 11;
            //    default:
            //        throw new Exception("Invalid PetLevel");
            //}
        }
    }
}