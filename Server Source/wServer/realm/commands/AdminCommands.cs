#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wServer.networking;
using wServer.networking.svrPackets;
using wServer.realm.entities;
using wServer.realm.entities.player;
using wServer.realm.setpieces;
using wServer.realm.worlds;
using db;

#endregion

namespace wServer.realm.commands
{
    internal class ListCommands : Command
    {
        public ListCommands()
            : base("commands", 5)
        {
        }
        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            Dictionary<string, Command> cmds = new Dictionary<string, Command>();
            Type t = typeof(Command);
            foreach (Type i in t.Assembly.GetTypes())
                if (t.IsAssignableFrom(i) && i != t)
                {
                    Command instance = (Command)Activator.CreateInstance(i);
                    cmds.Add(instance.CommandName, instance);
                }
            StringBuilder sb = new StringBuilder("");
            Command[] copy = cmds.Values.ToArray();
            for (int i = 0; i < copy.Length; i++)
            {
                if (i != 0) sb.Append(", ");
                sb.Append(copy[i].CommandName);
            }

            player.SendInfo(sb.ToString());
            return true;
        }
    }

    internal class IPBanCommand : Command
    {
        public IPBanCommand()
            : base("ipban", 8)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (string.IsNullOrEmpty(args[0]))
            {
                player.SendHelp("Usage: /ipban <playername>");
                return false;
            }
            try
            {
                foreach (KeyValuePair<int, Player> i in player.Owner.Players)
                {
                    if (i.Value.Name.ToLower() == args[0].ToLower().Trim())
                    {
                        string name = i.Value.Name;
                        string longIP = i.Value.Client.Socket.RemoteEndPoint.ToString();
                        string[] usrIP = longIP.Split(':');
                        i.Value.Manager.Database.DoActionAsync(db => db.IpBan(usrIP[0]));
                        i.Value.Client.Disconnect();
                        player.SendInfo("IP Banned " + i.Value.Name + " with the IP: " + usrIP[0]);
                    }

                    return true;

                }




            }
            catch
            {
                player.SendError("Cannot IP Ban!");
                return false;
            }
            return true;
        }
    }
    
    internal class UnIpBanCommand : Command
    {
        public UnIpBanCommand() :
            base("unipban", 8)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (string.IsNullOrEmpty(args[0]))
            {
                player.SendInfo("Usage: /unipban <IP>");
                player.SendInfo("IP's can only be accessed through the database.");
                return false;
            }
            player.Manager.Database.DoActionAsync(db =>
            {
                var cmd = db.CreateQuery();
                cmd.CommandText = "UPDATE ips SET banned=0 WHERE ip=@ip";
                cmd.Parameters.AddWithValue("@number", args[0]);
                player.SendInfo("Successfully unbanned IP " + args[0]);
                if (cmd.ExecuteNonQuery() == 0)
                {
                }
            });
            return true;
        }
    }


   /* internal class PetSkin : Command
    {
        public PetSkin() :
            base("petskin", permLevel: 10)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            var p = player.Manager.FindPlayer(args[0]);
            player.Manager.Database.DoActionAsync(db =>
            {
                var cmd = db.CreateQuery();
                cmd.CommandText = "UPDATE pets SET skin=@skin WHERE accId=@accId;";
                cmd.Parameters.AddWithValue("@accId", player.AccountId);
                cmd.Parameters.AddWithValue("@skin", args[0]);
                player.SendInfo("done");
                player.UpdateCount++;
                cmd.ExecuteNonQuery();
            });
            return true;
        }
    }*/

    internal class BanCommand : Command
    {
        public BanCommand() :
            base("ban", permLevel: 5)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            var p = player.Manager.FindPlayer(args[0]);
            var reason = args[1];
            player.Manager.Database.DoActionAsync(db =>
            {
                var cmd = db.CreateQuery();
                cmd.CommandText = "UPDATE accounts SET banned=1 WHERE id=@accId;";
                cmd.Parameters.AddWithValue("@accId", p.AccountId);
                cmd.ExecuteNonQuery();
                p.Client.Disconnect();
            });
            return true;
        }
    }

    internal class UnBanCommand : Command
    {
        public UnBanCommand() :
            base("unban", permLevel: 7)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            var p = player.Manager.FindPlayer(args[0]);
            player.Manager.Database.DoActionAsync(db =>
            {
                var cmd = db.CreateQuery();
                cmd.CommandText = "UPDATE accounts SET banned=0 WHERE id=@accId;";
                cmd.Parameters.AddWithValue("@accId", p.AccountId);
                cmd.ExecuteNonQuery();
            });

            return true;
        }
    }
    /*
    internal class PetMaxCommand : Command
    {
        public PetMaxCommand()
            : base("petmax", 8)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (player.Pet == null)
            {
                player.SendInfo("You dont have a pet following you.");
                return false;
            }
            player.Pet.Feed(new PetFoodNomNomNom());
            player.Pet.UpdateCount++;
            return true;
        }

        private class PetFoodNomNomNom : IFeedable
        {
            public int FeedPower { get; set; }

            public PetFoodNomNomNom()
            {
                FeedPower = Int32.MaxValue;
            }
        }
    }*/

    internal class RankC2ommand : Command
    {
        public RankC2ommand()
            : base("rank", 8)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (string.IsNullOrEmpty(args[0]))
            {
                player.SendInfo("Usage: /rank <playername> <number>");
                player.SendInfo("Founder: 10 - Do in Database!");
                player.SendInfo("Community Manager: 9");
                player.SendInfo("Developer: 8");
                player.SendInfo("Grand Moderator: 7");
                player.SendInfo("Moderator: 5");
                player.SendInfo("VIP: 3");
                player.SendInfo("Donator: 1");
                return false;
            }
            int c = int.Parse(args[1]);
            if (c > 9)
            {
                player.SendHelp("You cannot rank someone to 10. This must be done in the database.");
                return false;
            }
            if (c > 9 && player.Client.Account.Rank == 9)
            {
                player.SendHelp("At least you tried...");
                return false;
            }
            var p = player.Manager.FindPlayer(args[0]);
            player.Manager.Database.DoActionAsync(db =>
            {
                var cmd = db.CreateQuery();
                cmd.CommandText = "UPDATE accounts SET rank=@wutface WHERE id=@accId;";
                cmd.Parameters.AddWithValue("@accId", p.AccountId);
                cmd.Parameters.AddWithValue("@wutface", args[1]);
                player.SendInfo("Successfully ranked " + p.Name);
                p.SendInfo("You have been ranked by " + player.Name);
                cmd.ExecuteNonQuery();
                db.Dispose();
            });
            return true;
        }
    }

    internal class GiftCommand : Command
    {
        public GiftCommand()
            : base("gift", 10)
        {
        }
        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (args.Length == 1)
            {
                player.SendHelp("Usage: /gift <Playername> <Itemname>");
                return false;
            }
            string name = string.Join(" ", args.Skip(1).ToArray()).Trim();
            var plr = player.Manager.FindPlayer(args[0]);
            ushort objType;
            Dictionary<string, ushort> icdatas = new Dictionary<string, ushort>(player.Manager.GameData.IdToObjectType,
                StringComparer.OrdinalIgnoreCase);
            if (!icdatas.TryGetValue(name, out objType))
            {
                player.SendError("Item not found, perhaps a spelling error?");
                return false;
            }
            if (!player.Manager.GameData.Items[objType].Secret || player.Client.Account.Rank >= 10)
            {
                for (int i = 0; i < plr.Inventory.Length; i++)
                    if (plr.Inventory[i] == null)
                    {
                        plr.Inventory[i] = player.Manager.GameData.Items[objType];
                        plr.UpdateCount++;
                        plr.SaveToCharacter();
                        player.SendInfo("Success sending " + name + " to " + plr.Name);
                        plr.SendInfo("You got a " + name + " from " + player.Name);
                        break;
                    }
            }
            else
            {
                player.SendError("Item failed sending to " + plr.Name + ", make sure you spelt the command right, and their name!");
                return false;
            }
            return true;
        }
    }

    internal class SendCommand : Command
    {
        public SendCommand()
            : base("send", 10)
        {
        }
        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (args.Length < 3)
            {
                player.SendError("Usage: /send <player name> <ammount> <currency type>");
                return false;
            }
            var db = new Database();
            int ammount;
            bool parsed = int.TryParse(args[1], out ammount);

            if (!parsed)
            {
                player.SendError("Usage: /send <player name> <ammount> <currency type>");
                return false;
            }

            foreach (var w in player.Manager.Worlds)
            {
                World world = w.Value;
                foreach (var p in world.Players)
                    if (p.Value.Name.ToLower() == args[0].ToLower())
                    {
                        switch (args[2].ToLower())
                        {
                            case "gold":
                                db.UpdateCredit(p.Value.Client.Account, ammount);
                                db.Dispose();
                                break;
                            case "fame":
                                db.UpdateFame(p.Value.Client.Account, ammount);
                                db.Dispose();
                                break;
                            case "tokens":
                                db.UpdateFortuneToken(p.Value.Client.Account, ammount);
                                db.Dispose();
                                break;
                            default:
                                player.SendError("Invalid currency type!");
                                return false;
                        }
                        p.Value.SendInfo(player.Name + " has sent you " + ammount + " " + args[2].ToLower() + "!");
                        player.SendInfo("Sent " + p.Value.Name + " " + ammount + " " + args[2].ToLower() + "!");
                        return true;
                    }
            }
            player.SendError("Player not found!");
            return false;
        }
    }

    internal class GlandCommand : Command
    {
        public GlandCommand()
            : base("glands", 0)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (player.Owner is Nexus || player.Owner is PetYard || player.Owner is ClothBazaar || player.Owner is Vault || player.Owner is GuildHall || player.Owner is SkinShop)
            {
                player.SendInfo("You cannot use this command here");
                return false;
            }
            string[] random = "950|960|970|980|990|1000|1010|1020|1030|1040|1050|1060|1070|1080|1090|1100|1100|1110|1120|1130|1140|1050".Split('|');
            int tplocation = new Random().Next(random.Length);
            string topdank = random[tplocation];
            int x, y;
            try
            {
                x = int.Parse(topdank);
                y = int.Parse(topdank);
            }
            catch
            {
                player.SendError("Invalid coordinates!");
                return false;
            }
            player.Move(x + 0.5f, y + 0.5f);
            if (player.Pet != null)
                player.Pet.Move(x + 0.5f, y + 0.5f);
            player.UpdateCount++;
            player.Owner.BroadcastPacket(new GotoPacket
            {
                ObjectId = player.Id,
                Position = new Position
                {
                    X = player.X,
                    Y = player.Y
                }
            }, null);
            player.ApplyConditionEffect(new ConditionEffect()
            {
                Effect = ConditionEffectIndex.Invulnerable,
                DurationMS = 2500,
            });
            player.ApplyConditionEffect(new ConditionEffect()
            {
                Effect = ConditionEffectIndex.Invisible,
                DurationMS = 2500,
            });
            return true;
        }
    }

    internal class MaxCommand : Command
    {
        public MaxCommand()
            : base("max", 4)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            try
            {
                player.Stats[0] = player.ObjectDesc.MaxHitPoints;
                player.Stats[1] = player.ObjectDesc.MaxMagicPoints;
                player.Stats[2] = player.ObjectDesc.MaxAttack;
                player.Stats[3] = player.ObjectDesc.MaxDefense;
                player.Stats[4] = player.ObjectDesc.MaxSpeed;
                player.Stats[5] = player.ObjectDesc.MaxHpRegen;
                player.Stats[6] = player.ObjectDesc.MaxMpRegen;
                player.Stats[7] = player.ObjectDesc.MaxDexterity;
                player.SaveToCharacter();
                player.Client.Save();
                player.UpdateCount++;
                player.SendInfo("Success");
            }
            catch
            {
                player.SendError("Error while maxing stats");
                return false;
            }
            return true;
        }
    }


    internal class Mute : Command
    {
        public Mute()
            : base("mute", 5)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (args.Length == 0)
            {
                player.SendHelp("Usage: /mute <playername>");
                return false;
            }
            try
            {
                foreach (KeyValuePair<int, Player> i in player.Owner.Players)
                {
                    if (i.Value.Name.ToLower() == args[0].ToLower().Trim())
                    {
                        i.Value.Muted = true;
                        i.Value.Manager.Database.DoActionAsync(db => db.MuteAccount(i.Value.AccountId));
                        player.SendInfo("Player Muted.");
                    }
                }
            }
            catch
            {
                player.SendError("Cannot mute!");
                return false;
            }
            return true;
        }
    }

    internal class MaxPlayer : Command
    {
        public MaxPlayer()
            : base("maxplayer", 7)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (string.IsNullOrEmpty(args[0]))
            {
                player.SendHelp("Usage: /maxplayer <player>");
                return false;
            }
            var plr = player.Manager.FindPlayer(args[0]);
            try
            {
                plr.Stats[0] = plr.ObjectDesc.MaxHitPoints;
                plr.Stats[1] = plr.ObjectDesc.MaxMagicPoints;
                plr.Stats[2] = plr.ObjectDesc.MaxAttack;
                plr.Stats[3] = plr.ObjectDesc.MaxDefense;
                plr.Stats[4] = plr.ObjectDesc.MaxSpeed;
                plr.Stats[5] = plr.ObjectDesc.MaxHpRegen;
                plr.Stats[6] = plr.ObjectDesc.MaxMpRegen;
                plr.Stats[7] = plr.ObjectDesc.MaxDexterity;
                plr.SaveToCharacter();
                plr.Client.Save();
                plr.UpdateCount++;
                plr.SendInfo("You have been maxed by: " + player.Name);
            }
            catch
            {
                player.SendError("Error while maxing players stats!");
                return false;
            }
            return true;
        }
    }

    internal class UnMute : Command
    {
        public UnMute()
            : base("unmute", 5)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (args.Length == 0)
            {
                player.SendHelp("Usage: /unmute <playername>");
                return false;
            }
            try
            {
                foreach (KeyValuePair<int, Player> i in player.Owner.Players)
                {
                    if (i.Value.Name.ToLower() == args[0].ToLower().Trim())
                    {
                        i.Value.Muted = true;
                        i.Value.Manager.Database.DoActionAsync(db => db.UnmuteAccount(i.Value.AccountId));
                        player.SendInfo("Player Unmuted.");
                    }
                }
            }
            catch
            {
                player.SendError("Cannot unmute!");
                return false;
            }
            return true;
        }
    }

    internal class Kick : Command
    {
        public Kick()
            : base("kick", 5)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (args.Length == 0)
            {
                player.SendHelp("Usage: /kick <playername>");
                return false;
            }
            try
            {
                foreach (KeyValuePair<int, Player> i in player.Owner.Players)
                {
                    if (i.Value.Name.ToLower() == args[0].ToLower().Trim())
                    {
                        player.SendInfo("Player Disconnected");
                        i.Value.Client.Disconnect();
                    }
                }
            }
            catch
            {
                player.SendError("Cannot kick!");
                return false;
            }
            return true;
        }
    }

    internal class Announcement : Command
    {
        public Announcement()
            : base("announce", 7)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (args.Length == 0)
            {
                player.SendHelp("Usage: /announce <saytext>");
                return false;
            }
            string saytext = string.Join(" ", args);

            foreach (Client i in player.Manager.Clients.Values)
            {
                i.SendPacket(new TextPacket
                {
                    BubbleTime = 0,
                    Stars = -1,
                    Name = "@ANNOUNCEMENT",
                    Text = " " + saytext
                });
            }
            return true;
        }
    }

    internal class LeftToMax : Command
    {
        public LeftToMax()
            : base("lefttomax", 0)
        {
        }
        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            int Hp = player.ObjectDesc.MaxHitPoints - player.Stats[0];
            int Mp = player.ObjectDesc.MaxMagicPoints - player.Stats[1];
            int Atk = player.ObjectDesc.MaxAttack - player.Stats[2];
            int Def = player.ObjectDesc.MaxDefense - player.Stats[3];
            int Spd = player.ObjectDesc.MaxSpeed - player.Stats[4];
            int Vit = player.ObjectDesc.MaxHpRegen - player.Stats[5];
            int Wis = player.ObjectDesc.MaxMpRegen - player.Stats[6];
            int Dex = player.ObjectDesc.MaxDexterity - player.Stats[7];
            player.SendInfo(Hp / 5 + " Till maxed Health");
            player.SendInfo(Mp / 5 + " Till maxed Mana");
            player.SendInfo(Atk + " Till maxed Attack");
            player.SendInfo(Def + " Till maxed Defense");
            player.SendInfo(Spd + " Till maxed Speed");
            player.SendInfo(Vit + " Till maxed Vitality");
            player.SendInfo(Wis + " Till maxed Wisdom");
            player.SendInfo(Dex + " Till maxed Dexterity");
            return true;
        }
    }

    internal class FametoAccFame : Command
    {
        public FametoAccFame()
            : base("basefametoaccfame", 0)
        {
        }
        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (player.Client.Account.Rank < 5)
            {
                player.SendHelp("Command under development.");
                return false;
            }
            int a1 = Convert.ToInt32(args[0]);
            if (string.IsNullOrEmpty(args[0]))
            {
                player.SendHelp("Usage: /basefametoaccfame <Amount>");
                return false;
            }
            if (a1 > player.Fame)
            {
                player.SendHelp("You do not have this much fame. (Current basefame: " + player.Fame + ")");
                return false;
            }
            if (player.Fame <= 0)
            {
                player.SendInfo("You dont have any fame");
                return false;
            }
            player.Manager.Database.DoActionAsync(db =>
            {
                player.SendInfo("Successfully transferred Basefame into Accountfame (" + a1 + " basefame made into account fame)");
                player.CurrentFame = player.Fame = db.UpdateFame(player.Client.Account, +a1);
                player.SaveToCharacter();
                player.Fame = player.Fame = db.UpdateFame(player.Client.Account, -a1);
                player.Fame -= a1;
                player.UpdateCount++;
            });
            return true;
        }
    }

    internal class basefame : Command
    {
        public basefame()
            : base("basefame", 10)
        {
        }
        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (string.IsNullOrEmpty(args[0]))
            {
                player.SendHelp("Usage: /basefame <Amount>");
                return false;
            }
            player.Manager.Database.DoActionAsync(db =>
            {
                int a1 = Convert.ToInt32(args[0]);
                player.Fame = a1;
                player.UpdateCount++;
            });
            return true;
        }
    }


    internal class level : Command
    {
        public level()
            : base("level", 5)
        {
        }
        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            player.Level = 20;
            player.UpdateCount++;
            return true;
        }
    }
    internal class onlineCommand : Command
    {
        public onlineCommand()
            : base("online", 0)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            StringBuilder sb = new StringBuilder("Players Online: ");
            int ConPlayers = 0;
            foreach (KeyValuePair<int, World> w in player.Manager.Worlds)
            {
                World world = w.Value;
                if (w.Key != 0)
                {
                    Player[] copy = world.Players.Values.ToArray();
                    if (copy.Length != 0)
                        for (int i = 0; i < copy.Length; i++)
                            ConPlayers += 1;
                }
            }
            player.SendInfo(sb + ConPlayers.ToString());
            return true;
        }
    }


    internal class SellCommand : Command //Made by Mike
    {
        public SellCommand() :
            base("sell", 0)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (string.IsNullOrEmpty(args[0]))
            {
                player.SendInfo("Usage: /sell <inv slot>");
                return false;
            }
            string xd = args[0].ToLower();
            switch (xd)
            {
                case "1":
                    if (player.Inventory[4] == null)
                    {
                        player.SendInfo("You cannot sell me nothing, sir.");
                        return false;
                    }
                    if (player.Inventory[4].SellPrice < 0)
                    {
                        player.SendInfo("This item doesnt have a selling price.");
                        return false;
                    }
                    player.Manager.Database.DoActionAsync(db =>
                    {
                        if (player.Inventory[4].SellPrice > 0)
                        {
                            player.SendInfo("sold item for: " + player.Inventory[4].SellPrice);
                            player.CurrentFame = player.Client.Account.Stats.Fame = db.UpdateFame(player.Client.Account, +player.Inventory[4].SellPrice);
                            player.Inventory[4] = null;
                            player.SaveToCharacter();
                            player.UpdateCount++;
                            db.Dispose();
                        }
                    });
                    break;
                case "2":
                    if (player.Inventory[5] == null)
                    {
                        player.SendInfo("You cannot sell me nothing, sir.");
                        return false;
                    }
                    if (player.Inventory[5].SellPrice < 0)
                    {
                        player.SendInfo("This item doesnt have a selling price.");
                        return false;
                    }
                    player.Manager.Database.DoActionAsync(db =>
                    {
                        if (player.Inventory[5].SellPrice > 0)
                        {
                            player.SendInfo("sold item for: " + player.Inventory[5].SellPrice);
                            player.CurrentFame = player.Client.Account.Stats.Fame = db.UpdateFame(player.Client.Account, +player.Inventory[5].SellPrice);
                            player.Inventory[5] = null;
                            player.SaveToCharacter();
                            player.UpdateCount++;
                            db.Dispose();
                        }
                    });
                    break;
                case "3":
                    if (player.Inventory[6] == null)
                    {
                        player.SendInfo("You cannot sell me nothing, sir.");
                        return false;
                    }
                    if (player.Inventory[6].SellPrice < 0)
                    {
                        player.SendInfo("This item doesnt have a selling price.");
                        return false;
                    }
                    player.Manager.Database.DoActionAsync(db =>
                    {
                        if (player.Inventory[6].SellPrice > 0)
                        {
                            player.SendInfo("sold item for: " + player.Inventory[6].SellPrice);
                            player.CurrentFame = player.Client.Account.Stats.Fame = db.UpdateFame(player.Client.Account, +player.Inventory[6].SellPrice);
                            player.Inventory[6] = null;
                            player.SaveToCharacter();
                            player.UpdateCount++;
                            db.Dispose();
                        }
                    });
                    break;
                case "4":
                    if (player.Inventory[7] == null)
                    {
                        player.SendInfo("You cannot sell me nothing, sir.");
                        return false;
                    }
                    if (player.Inventory[7].SellPrice < 0)
                    {
                        player.SendInfo("This item doesnt have a selling price.");
                        return false;
                    }
                    player.Manager.Database.DoActionAsync(db =>
                    {
                        if (player.Inventory[7].SellPrice > 0)
                        {
                            player.SendInfo("sold item for: " + player.Inventory[7].SellPrice);
                            player.CurrentFame = player.Client.Account.Stats.Fame = db.UpdateFame(player.Client.Account, +player.Inventory[7].SellPrice);
                            player.Inventory[7] = null;
                            player.SaveToCharacter();
                            player.UpdateCount++;
                            db.Dispose();
                        }
                    });
                    break;
                case "5":
                    if (player.Inventory[8] == null)
                    {
                        player.SendInfo("You cannot sell me nothing, sir.");
                        return false;
                    }
                    if (player.Inventory[8].SellPrice < 0)
                    {
                        player.SendInfo("This item doesnt have a selling price.");
                        return false;
                    }
                    player.Manager.Database.DoActionAsync(db =>
                    {
                        if (player.Inventory[8].SellPrice > 0)
                        {
                            player.SendInfo("sold item for: " + player.Inventory[8].SellPrice);
                            player.CurrentFame = player.Client.Account.Stats.Fame = db.UpdateFame(player.Client.Account, +player.Inventory[8].SellPrice);
                            player.Inventory[8] = null;
                            player.SaveToCharacter();
                            player.UpdateCount++;
                            db.Dispose();
                        }
                    });
                    break;
                case "6":
                    if (player.Inventory[9] == null)
                    {
                        player.SendInfo("You cannot sell me nothing, sir.");
                        return false;
                    }
                    if (player.Inventory[9].SellPrice < 0)
                    {
                        player.SendInfo("This item doesnt have a selling price.");
                        return false;
                    }
                    player.Manager.Database.DoActionAsync(db =>
                    {
                        if (player.Inventory[9].SellPrice > 0)
                        {
                            player.SendInfo("sold item for: " + player.Inventory[9].SellPrice);
                            player.CurrentFame = player.Client.Account.Stats.Fame = db.UpdateFame(player.Client.Account, +player.Inventory[9].SellPrice);
                            player.Inventory[9] = null;
                            player.SaveToCharacter();
                            player.UpdateCount++;
                            db.Dispose();
                        }
                    });
                    break;
                case "7":
                    if (player.Inventory[10] == null)
                    {
                        player.SendInfo("You cannot sell me nothing, sir.");
                        return false;
                    }
                    if (player.Inventory[10].SellPrice < 0)
                    {
                        player.SendInfo("This item doesnt have a selling price.");
                        return false;
                    }
                    player.Manager.Database.DoActionAsync(db =>
                    {
                        if (player.Inventory[10].SellPrice > 0)
                        {
                            player.SendInfo("sold item for: " + player.Inventory[10].SellPrice);
                            player.CurrentFame = player.Client.Account.Stats.Fame = db.UpdateFame(player.Client.Account, +player.Inventory[10].SellPrice);
                            player.Inventory[10] = null;
                            player.SaveToCharacter();
                            player.UpdateCount++;
                            db.Dispose();
                        }
                    });
                    break;
                case "8":
                    if (player.Inventory[11] == null)
                    {
                        player.SendInfo("You cannot sell me nothing, sir.");
                        return false;
                    }
                    if (player.Inventory[11].SellPrice < 0)
                    {
                        player.SendInfo("This item doesnt have a selling price.");
                        return false;
                    }
                    player.Manager.Database.DoActionAsync(db =>
                    {
                        if (player.Inventory[11].SellPrice > 0)
                        {
                            player.SendInfo("sold item for: " + player.Inventory[11].SellPrice);
                            player.CurrentFame = player.Client.Account.Stats.Fame = db.UpdateFame(player.Client.Account, +player.Inventory[11].SellPrice);
                            player.Inventory[11] = null;
                            player.SaveToCharacter();
                            player.UpdateCount++;
                            db.Dispose();
                            
                        }
                    });
                    break;
                default:
                    player.SendInfo("Usage: /sell <inv slot>");
                    break;
            }
            return true;
        }
    }

    internal class KickAllCommand : Command
    {
        public KickAllCommand() : base("kickall", 8)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            int clients = 0;
            foreach (var i in player.Manager.Clients.Values.Where(i => i.Account.Name != player.Name))
            {
                i.Disconnect();
                clients++;
            }
            player.SendInfo($"Success, kicked {clients} clients");
            return true;
        }
    }
    internal class Take : Command
    {
        public Take()
            : base("take", 7)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (args.Length < 2)
            {
                player.SendHelp("Usage: /take <playername> <Itemname>");
                return false;
            }
            Player p = player.Manager.FindPlayer(args[0]);
            string name = string.Join(" ", args.ToArray(), 1, (args.Length - 1)).Trim();
            ushort objType;
            Dictionary<string, ushort> icdatas = new Dictionary<string, ushort>(player.Manager.GameData.IdToObjectType,
                StringComparer.OrdinalIgnoreCase);
            if (!icdatas.TryGetValue(name, out objType))
            {
                player.SendError("Unknown type!");
                return false;
            }
            if (!player.Manager.GameData.Items[objType].Secret || player.Client.Account.Rank >= 3)
            {
                for (int i = 0; i < p.Inventory.Length; i++)
                    if (p.Inventory[i] == player.Manager.GameData.Items[objType])
                    {
                        p.Inventory[i] = null;
                        p.UpdateCount++;
                        p.SaveToCharacter();
                        player.SendInfo("You took " + p.Name + " " + name + " away from them!");
                        break;
                    }
            }
            else
            {
                player.SendError("Item not found Or Item cannot be taken!");
                return false;
            }
            return true;
        }
    }
    internal class SetpieceCommand : Command
    {
        public SetpieceCommand()
            : base("setpiece", 8)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            ISetPiece piece = (ISetPiece)Activator.CreateInstance(Type.GetType(
                "wServer.realm.setpieces." + args[0], true, true));
            piece.RenderSetPiece(player.Owner, new IntPoint((int)player.X + 1, (int)player.Y + 1));
            return true;
        }
    }
    internal class TqCommand : Command
    {
        public TqCommand()
            : base("tq", 3)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (player.Quest == null)
            {
                player.SendError("Player does not have a quest!");
                return false;
            }
            player.Move(player.Quest.X + 5f, player.Quest.Y + 5f);
            if (player.Pet != null)
                player.Pet.Move(player.Quest.X + 5f, player.Quest.Y + 5f);
            player.UpdateCount++;
            player.Owner.BroadcastPacket(new GotoPacket
            {
                ObjectId = player.Id,
                Position = new Position
                {
                    X = player.Quest.X,
                    Y = player.Quest.Y
                }
            }, null);
            player.SendInfo("Success!");
            return true;

        }
    }
    internal class IPCommand : Command
    {
        public IPCommand()
            : base("ip", 8)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            var plr = player.Manager.FindPlayer(args[0]);
            var sb = new StringBuilder(plr.Name + "'s IP: ");
            sb.AppendFormat("{0}",
                plr.Client.Socket.RemoteEndPoint);
            player.SendInfo(sb.ToString());
            return true;
        }
    }
    internal class KillPlayerCommand : Command
    {
        public KillPlayerCommand()
            : base("killplayer", 9)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            foreach (Client i in player.Manager.Clients.Values)
            {
                if (i.Account.Name.EqualsIgnoreCase(args[0]))
                {
                    i.Player.HP = 0;
                    i.Player.Death("Karma");
                    player.SendInfo("Player killed!");
                    return true;
                }
            }
            player.SendError(string.Format("Player '{0}' could not be found!", args));
            return false;
        }
    }
    internal class TossEffCommand : Command
    {
        public TossEffCommand()
            : base("tosseff", 8)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (args.Length != 2)
            {
                player.SendHelp("Usage: /tosseff <PlayerName> <Effectname or Effectnumber>");
                return false;
            }
            try
            {
                foreach (KeyValuePair<string, Client> i in player.Manager.Clients.Where(i => i.Value.Player.Name.EqualsIgnoreCase(args[0])))
                {
                    i.Value.Player.ApplyConditionEffect(new ConditionEffect
                    {
                        Effect =
                            (ConditionEffectIndex)Enum.Parse(typeof(ConditionEffectIndex), args[1].Trim(), true),
                        DurationMS = -1
                    });
                    player.SendInfo("Success!");
                }
            }
            catch
            {
                player.SendError("Invalid effect or player name! ");
                return false;
            }
            return true;
        }
    }

    internal class RemoveTossEffCommand : Command
    {
        public RemoveTossEffCommand()
            : base("remtosseff", 8)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (args.Length != 2)
            {
                player.SendHelp("Usage: /tosseff <PlayerName> <Effectname or Effectnumber>");
                return false;
            }
            try
            {
                foreach (KeyValuePair<string, Client> i in player.Manager.Clients.Where(i => i.Value.Player.Name.EqualsIgnoreCase(args[0])))
                {
                    i.Value.Player.ApplyConditionEffect(new ConditionEffect
                    {
                        Effect =
                            (ConditionEffectIndex)Enum.Parse(typeof(ConditionEffectIndex), args[1].Trim(), true),
                        DurationMS = 0
                    });
                    player.SendInfo("Success!");
                }
            }
            catch
            {
                player.SendError("Invalid effect or player name! ");
                return false;
            }
            return true;
        }
    }
    internal class ServerQuitCommand : Command
    {
        public ServerQuitCommand()
            : base("squit", 10)
        {
        }
        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            player.Client.SendPacket(new TextPacket
            {
                BubbleTime = 0,
                Stars = -1,
                Name = "@INFO",
                Text = "Server is turning off in 1 minute. Leave the server to prevent account in use!"
            });
            player.Owner.Timers.Add(new WorldTimer(30000, (world, t) =>
            {
                player.Client.SendPacket(new TextPacket
                {
                    BubbleTime = 0,
                    Stars = -1,
                    Name = "@INFO",
                    Text = "Server is turning off in 30 seconds. Leave the server to prevent account in use!"
                });
            }));

            player.Owner.Timers.Add(new WorldTimer(45000, (world, t) =>
            {
                player.Client.SendPacket(new TextPacket
                {
                    BubbleTime = 0,
                    Stars = -1,
                    Name = "@INFO",
                    Text = "Server is turning off in 15 seconds. Leave the server to prevent account in use!"
                });
            }));
            player.Owner.Timers.Add(new WorldTimer(55000, (world, t) =>
            {
                player.Client.SendPacket(new TextPacket
                {
                    BubbleTime = 0,
                    Stars = -1,
                    Name = "@INFO",
                    Text = "Server is turning off in 5 seconds. Leave the server to prevent account in use!"
                });
            }));
            player.Owner.Timers.Add(new WorldTimer(60000, (world, t) =>
            {
                Environment.Exit(0);
            }));
            return true;
        }
    }

    internal class KillAll : Command
    {
        public KillAll() : base("killAll", permLevel: 7) { }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            var iterations = 0;
            var lastKilled = -1;
            var killed = 0;

            var mobName = args.Aggregate((s, a) => string.Concat(s, " ", a));
            while (killed != lastKilled)
            {
                lastKilled = killed;
                foreach (var i in player.Owner.Enemies.Values.Where(e =>
                    e.ObjectDesc?.ObjectId != null && e.ObjectDesc.ObjectId.ContainsIgnoreCase(mobName)))
                {
                    i.Death(time);
                    killed++;
                }
                if (++iterations >= 5)
                    break;
            }

            player.SendInfo($"{killed} enemy killed!");
            return true;
        }
    }


    internal class SetCommand : Command
    {
        public SetCommand()
            : base("setStat", 10)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (args.Length == 2)
            {
                try
                {
                    string stat = args[0].ToLower();
                    int amount = int.Parse(args[1]);
                    switch (stat)
                    {
                        case "health":
                        case "hp":
                            player.Stats[0] = amount;
                            break;
                        case "mana":
                        case "mp":
                            player.Stats[1] = amount;
                            break;
                        case "atk":
                        case "attack":
                            player.Stats[2] = amount;
                            break;
                        case "def":
                        case "defence":
                            player.Stats[3] = amount;
                            break;
                        case "spd":
                        case "speed":
                            player.Stats[4] = amount;
                            break;
                        case "vit":
                        case "vitality":
                            player.Stats[5] = amount;
                            break;
                        case "wis":
                        case "wisdom":
                            player.Stats[6] = amount;
                            break;
                        case "dex":
                        case "dexterity":
                            player.Stats[7] = amount;
                            break;
                        default:
                            player.SendError("Invalid Stat");
                            player.SendHelp("Stats: Health, Mana, Attack, Defence, Speed, Vitality, Wisdom, Dexterity");
                            player.SendHelp("Shortcuts: Hp, Mp, Atk, Def, Spd, Vit, Wis, Dex");
                            return false;
                    }
                    player.SaveToCharacter();
                    player.Client.Save();
                    player.UpdateCount++;
                    player.SendInfo("Success");
                }
                catch
                {
                    player.SendError("Error while setting stat");
                    return false;
                }
                return true;
            }
            else if (args.Length == 3)
            {
                foreach (Client i in player.Manager.Clients.Values)
                {
                    if (i.Account.Name.EqualsIgnoreCase(args[0]))
                    {
                        try
                        {
                            string stat = args[1].ToLower();
                            int amount = int.Parse(args[2]);
                            switch (stat)
                            {
                                case "health":
                                case "hp":
                                    i.Player.Stats[0] = amount;
                                    break;
                                case "mana":
                                case "mp":
                                    i.Player.Stats[1] = amount;
                                    break;
                                case "atk":
                                case "attack":
                                    i.Player.Stats[2] = amount;
                                    break;
                                case "def":
                                case "defence":
                                    i.Player.Stats[3] = amount;
                                    break;
                                case "spd":
                                case "speed":
                                    i.Player.Stats[4] = amount;
                                    break;
                                case "vit":
                                case "vitality":
                                    i.Player.Stats[5] = amount;
                                    break;
                                case "wis":
                                case "wisdom":
                                    i.Player.Stats[6] = amount;
                                    break;
                                case "dex":
                                case "dexterity":
                                    i.Player.Stats[7] = amount;
                                    break;
                                default:
                                    player.SendError("Invalid Stat");
                                    player.SendHelp("Stats: Health, Mana, Attack, Defence, Speed, Vitality, Wisdom, Dexterity");
                                    player.SendHelp("Shortcuts: Hp, Mp, Atk, Def, Spd, Vit, Wis, Dex");
                                    return false;
                            }
                            i.Player.SaveToCharacter();
                            i.Player.Client.Save();
                            i.Player.UpdateCount++;
                            player.SendInfo("Success");
                        }
                        catch
                        {
                            player.SendError("Error while setting stat");
                            return false;
                        }
                        return true;
                    }
                }
                player.SendError(string.Format("Player '{0}' could not be found!", args));
                return false;
            }
            else
            {
                player.SendHelp("Usage: /setStat <Stat> <Amount>");
                player.SendHelp("or");
                player.SendHelp("Usage: /setStat <Player> <Stat> <Amount>");
                player.SendHelp("Shortcuts: Hp, Mp, Atk, Def, Spd, Vit, Wis, Dex");
                return false;
            }
        }
    }

    /* internal class BuyCommand : Command
     {
         public BuyCommand()
             : base("buy", 1)
         {
         }

         protected override bool Process(Player player, RealmTime time, string[] args)
         {
             if (args.Length == 0)
             {
                 player.SendHelp("Usage: /buy <Itemname>");
                 return false;
             }
             string name = string.Join(" ", args.ToArray()).Trim();
             ushort objType;
             //creates a new case insensitive dictionary based on the XmlDatas
             Dictionary<string, ushort> icdatas = new Dictionary<string, ushort>(player.Manager.GameData.IdToObjectType, StringComparer.OrdinalIgnoreCase);
             if (!icdatas.TryGetValue(name, out objType))
             {
                 player.SendError("Unknown type!");
                 return false;
             }
             if (player.Manager.GameData.Items[objType].Secret)
             {
                 player.SendHelp("You cant buy admin items.");
                 return false;
             }
             if (player.Client.Account.Credits < 2500)
             {
                 player.SendHelp("You do not have enough gold to buy the specific item.");
                 return false;
             }
             if (!player.Manager.GameData.Items[objType].Secret || player.Client.Account.Rank >= 4)
             {
                 player.Manager.Database.DoActionAsync(db =>
                 {
                     for (int i = 4; i < player.Inventory.Length; i++)
                         if (player.Inventory[i] == null)
                         {
                             player.Inventory[i] = player.Manager.GameData.Items[objType];
                             player.Credits -= 2500;
                             db.UpdateCredit(player.Client.Account, -2500);
                             player.SaveToCharacter();
                             player.SendInfo("Successfully bought: " + name);
                             player.UpdateCount++;
                             break;
                         }
                 });
             }
             else
             {
                 player.SendError("Item cannot be bought!");
                 return false;
             }
             return true;
         }
     } */

    internal class GiveCommand : Command
    {
        public GiveCommand()
            : base("give", 7)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (args.Length == 0)
            {
                player.SendHelp("Usage: /give <Itemname>");
                return false;
            }
            string name = string.Join(" ", args.ToArray()).Trim();
            ushort objType;
            //creates a new case insensitive dictionary based on the XmlDatas
            Dictionary<string, ushort> icdatas = new Dictionary<string, ushort>(player.Manager.GameData.IdToObjectType, StringComparer.OrdinalIgnoreCase);
            if (!icdatas.TryGetValue(name, out objType))
            {
                player.SendError("Unknown type!");
                return false;
            }
            if (!player.Manager.GameData.Items[objType].Secret || player.Client.Account.Rank >= 7)
            {
                for (int i = 4; i < player.Inventory.Length; i++)
                    if (player.Inventory[i] == null)
                    {
                        player.Inventory[i] = player.Manager.GameData.Items[objType];
                        player.UpdateCount++;
                        player.SaveToCharacter();
                        player.SendInfo("Success!");
                        log.Info(player.Name + " has just given himself a " + name);
                        break;
                    }
            }
            else
            {
                player.SendError("Item cannot be given!");
                return false;
            }
            return true;
        }
    }

    internal class SpawnCommand : Command
    {
        public SpawnCommand()
            : base("spawn", 4)
        {
        }


        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            int num;
            if (args.Length > 0 && int.TryParse(args[0], out num)) //multi
            {
                string name = string.Join(" ", args.Skip(1).ToArray());
                World w = player.Manager.GetWorld(player.Owner.Id); //can't use Owner here, as it goes out of scope
                string announce = "Spawning " + num + " of " + name + " in 5 seconds . . .";
                ushort objType;
                //creates a new case insensitive dictionary based on the XmlDatas
                Dictionary<string, ushort> icdatas = new Dictionary<string, ushort>(player.Manager.GameData.IdToObjectType, StringComparer.OrdinalIgnoreCase);
                if (name.ToLower() == "vault chest" && player.Client.Account.Rank < 9)
                {
                    player.SendInfo("You are not allowed to spawn this entity.");
                    return false;
                }
                if (!icdatas.TryGetValue(name, out objType) ||
                !player.Manager.GameData.ObjectDescs.ContainsKey(objType))
                {
                    player.SendInfo("Unknown entity!");
                    return false;
                }
                int c = int.Parse(args[0]);
                if (c > 50)
                {
                    player.SendInfo("Cant spawn that many enemies.");
                    return false;
                }
                w.BroadcastPacket(new NotificationPacket
                {
                    Color = new ARGB(0x00ff00),
                    ObjectId = player.Client.Player.Id,
                    Text = "{\"key\":\"blank\",\"tokens\":{\"data\":\"" + announce + "\"}}",
                }, null);
                player.Owner.Timers.Add(new WorldTimer(5000, (world, t) =>
                {
                    for (int i = 0; i < num; i++)
                    {
                        Entity entity = Entity.Resolve(player.Manager, objType);
                        if (player.Owner is Nexus)
                        {
                            entity.Move(69, 18);
                            player.Owner.EnterWorld(entity);
                        }
                        else
                        {
                            entity.Move(player.X, player.Y);
                            player.Owner.EnterWorld(entity);
                        }
                    }
                }));
                player.SendInfo("Success!");
            }
            else
            {
                string name = string.Join(" ", args);
                ushort objType;
                //creates a new case insensitive dictionary based on the XmlDatas
                Dictionary<string, ushort> icdatas = new Dictionary<string, ushort>(player.Manager.GameData.IdToObjectType, StringComparer.OrdinalIgnoreCase);
                if (name.ToLower() == "vault chest")
                {
                    player.SendInfo("You are not allowed to spawn this entity.");
                    return false;
                }
                if (!icdatas.TryGetValue(name, out objType) || !player.Manager.GameData.ObjectDescs.ContainsKey(objType))
                {
                    player.SendHelp("Usage: /spawn <entityname>");
                    return false;
                }
                World w = player.Manager.GetWorld(player.Owner.Id); //can't use Owner here, as it goes out of scope
                string announce = "Spawning " + name + " in 5 seconds . . .";
                w.BroadcastPacket(new NotificationPacket
                {
                    Color = new ARGB(0x00ff00),
                    ObjectId = player.Client.Player.Id,
                    Text = "{\"key\":\"blank\",\"tokens\":{\"data\":\"" + announce + "\"}}",
                }, null);
                player.Owner.Timers.Add(new WorldTimer(5000, (world, t) =>
                {
                    Entity entity = Entity.Resolve(player.Manager, objType);
                    if (player.Owner is Nexus)
                    {
                        entity.Move(69, 18);
                        player.Owner.EnterWorld(entity);
                    }
                    else
                    {
                        entity.Move(player.X, player.Y);
                        player.Owner.EnterWorld(entity);
                    }
                }));
            }
            return true;
        }
    }

    internal class AddEffCommand : Command
    {
        public AddEffCommand()
            : base("addeff", 5)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (args.Length == 0)
            {
                player.SendHelp("Usage: /addeff <Effectname or Effectnumber>");
                return false;
            }
            try
            {
                player.ApplyConditionEffect(new ConditionEffect
                {
                    Effect = (ConditionEffectIndex)Enum.Parse(typeof(ConditionEffectIndex), args[0].Trim(), true),
                    DurationMS = -1
                });
                {
                    player.SendInfo("Success!");
                }
            }
            catch
            {
                player.SendError("Invalid effect!");
                return false;
            }
            return true;
        }
    }

    internal class RemoveEffCommand : Command
    {
        public RemoveEffCommand()
            : base("remeff", 5)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (args.Length == 0)
            {
                player.SendHelp("Usage: /remeff <Effectname or Effectnumber>");
                return false;
            }
            try
            {
                player.ApplyConditionEffect(new ConditionEffect
                {
                    Effect = (ConditionEffectIndex)Enum.Parse(typeof(ConditionEffectIndex), args[0].Trim(), true),
                    DurationMS = 0
                });
                player.SendInfo("Success!");
            }
            catch
            {
                player.SendError("Invalid effect!");
                return false;
            }
            return true;
        }
    }
    /*
    internal class SpawnDonorCommand : Command
    {
        public SpawnDonorCommand()
            : base("goldspawn", 1)
        {
        }


        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            int num;
            var enemies = new List<string>();
            // add some:

            enemies.Add("grand sphinx");
            enemies.Add("oryx the mad god 2");
            enemies.Add("hermit god");
            enemies.Add("Oryx the mad god 1");
            enemies.Add("lord of the lost lands");

            

            // does our list contain our test?
            bool found = enemies.Contains(args[0]);

            //string[] donorSpawns = { "grand sphinx", "cube god", "ghost ship", "ent ancient", "oryx the mad god 2", "skull shrine", "oryx the mad god 1" };
            if (args.Length > 0 && int.TryParse(args[0], out num)) //multi
            {
                string name = string.Join(" ", args.Skip(1).ToArray());
                World w = player.Manager.GetWorld(player.Owner.Id); //can't use Owner here, as it goes out of scope
                
                string announce = player.Client.Account.Name + "is spawning " + num + " of " + name + " in 5 seconds . . .";
                ushort objType;
                //creates a new case insensitive dictionary based on the XmlDatas
                Dictionary<string, ushort> icdatas = new Dictionary<string, ushort>(player.Manager.GameData.IdToObjectType, StringComparer.OrdinalIgnoreCase);

                if (!icdatas.TryGetValue(name, out objType) ||
                !player.Manager.GameData.ObjectDescs.ContainsKey(objType))
                {
                    player.SendInfo("You can only spawn one at a time!");

                    return false;
                }
                int c = int.Parse(args[0]);
                if (c > 1)
                {
                    player.SendInfo("Cant spawn that many enemies.");
                    return false;
                }
                if (!(player.Owner is Nexus))
                {
                    return false;
                } 
                
                   
                    w.BroadcastPacket(new NotificationPacket
                {
                    Color = new ARGB(0x00ff00),
                    ObjectId = player.Client.Player.Id,
                    Text = "{\"key\":\"blank\",\"tokens\":{\"data\":\"" + announce + "\"}}",
                }, null);
                player.Owner.Timers.Add(new WorldTimer(5000, (world, t) =>
                {
                    for (int i = 0; i < num; i++)
                    {
                        Entity entity = Entity.Resolve(player.Manager, objType);
                        if (player.Owner is Nexus)
                        {
                            entity.Move(69, 18);
                            player.Owner.EnterWorld(entity);
                        }
                        else
                        {
                            entity.Move(player.X, player.Y);
                            player.Owner.EnterWorld(entity);
                        }
                    }
                }));
                player.SendInfo("Success!");
            }
            else
            {
                string name = string.Join(" ", args);
                ushort objType;
                //creates a new case insensitive dictionary based on the XmlDatas

                string[] icdatas = { "oryx the mad god 1", "oryx the mad god 2", "grand sphinx", "hermit god" };
                if (player.Client.Account.Credits < 250)
                {
                    player.SendInfo("Please have 250 gold");
                    return false;
                }

                if (found == false)
                {
                    player.SendInfo("You are not allowed to spawn this entity. You can do common events, but not LotLL. 1 is the max!");
                    return false;
                }
                //if (!(donorSpawns.contains))
               
                World w = player.Manager.GetWorld(player.Owner.Id); //can't use Owner here, as it goes out of scope
                string announce = "Spawning " + name + " in 5 seconds . . .";
                w.BroadcastPacket(new NotificationPacket
                {
                    Color = new ARGB(0x00ff00),
                    ObjectId = player.Client.Player.Id,
                    Text = "{\"key\":\"blank\",\"tokens\":{\"data\":\"" + announce + "\"}}",
                }, null);
                player.Owner.Timers.Add(new WorldTimer(5000, (world, t) =>
                {
                    
                    if (player.Owner is Nexus)
                    {
                        entity.Move(69, 18);
                        player.Owner.EnterWorld(entity);
                    }
                    else
                    {
                        entity.Move(player.X, player.Y);
                        player.Owner.EnterWorld(entity);
                    }
                    player.Manager.Database.DoActionAsync(db =>
                    {
                        
                                
                                player.Credits -= 250;
                                db.UpdateCredit(player.Client.Account, -250);
                                player.SaveToCharacter();
                                player.SendInfo("Successfully spawned: " + name);
                                player.UpdateCount++;
                                db.Dispose();
                                
                            
                    });
                }));
            }
                        return true;
        }
    }
    */
    internal class posCmd : Command
    {
        public posCmd()
            : base("p", 0)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            player.SendInfo("X: " + (int)player.X + " - Y: " + (int)player.Y);
            return true;
        }
    }
}