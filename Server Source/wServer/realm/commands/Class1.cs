using db;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using wServer.networking;
using wServer.networking.cliPackets;
using wServer.networking.svrPackets;
using wServer.realm.entities;
using wServer.realm.entities.player;
using wServer.realm.setpieces;
using wServer.realm.worlds;


namespace wServer.realm.commands
{
    

    internal class Goto : Command
    {
        public Goto()
            : base("goto", 7)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (args.Length == 0 || args.Length == 1)
            {
                player.SendHelp("Usage: /goto <X coordinate> <Y coordinate>");
            }
            else
            {
                int x, y;
                try
                {
                    x = int.Parse(args[0]);
                    y = int.Parse(args[1]);
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
            }
            return true;
        }
    }
    internal class TPpos : Command
    {
        public TPpos()
            : base("tppos", 7)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (args.Length == 0 || args.Length == 1)
            {
                player.SendHelp("Usage: /tppos <X coordinate> <Y coordinate>");
            }
            else
            {
                int x, y;
                try
                {
                    x = int.Parse(args[0]);
                    y = int.Parse(args[1]);
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
            }
            return true;
        }
    }

    internal class VisitCommand : Command
    {
        public VisitCommand()
            : base("visit", 6)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (args.Length < 1)
            {
                player.SendHelp("Usage: /visit <playername>");
                return false;
            }
            else if (String.Equals(player.Name.ToLower(), args[0].ToLower()))
            {
                player.SendInfo("You are You.");
                return false;
            }
            foreach (KeyValuePair<string, Client> i in player.Manager.Clients)
            {
                if (i.Value.Player.Owner is PetYard)
                {
                    player.SendInfo("Player is in his Pet Yard you cannot visit him.");
                    return false;
                }
                else if (i.Value.Player.Name.EqualsIgnoreCase(args[0]))
                {
                    if (player.Owner == i.Value.Player.Owner)
                    {
                        player.SendInfo("You are already in the same world as this Player.");
                        return false;
                    }
                    else
                    {
                        player.Client.Reconnect(new ReconnectPacket
                        {
                            GameId = i.Value.Player.Owner.Id,
                            Host = "",
                            IsFromArena = false,
                            Key = i.Value.Player.Owner.PortalKey,
                            KeyTime = -1,
                            Name = i.Value.Player.Owner.Name,
                            Port = -1
                        });
                    }
                    return true;
                }
            }
            player.SendError("Player could not be found!");
            return false;
        }
    }
    internal class Summon : Command
    {
        public Summon()
            : base("summon", 7)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (player.Owner is Vault || player.Owner is PetYard)
            {
                player.SendInfo("You can't summon in this world.");
                return false;
            }
            foreach (KeyValuePair<string, Client> i in player.Manager.Clients)
            {
                if (i.Value.Player.Name.EqualsIgnoreCase(args[0]))
                {
                    Packet pkt;
                    if (i.Value.Player.Owner == player.Owner)
                    {
                        i.Value.Player.Move(player.X, player.Y);
                        pkt = new GotoPacket
                        {
                            ObjectId = i.Value.Player.Id,
                            Position = new Position(player.X, player.Y)
                        };
                        i.Value.Player.UpdateCount++;
                        player.SendInfo("Player summoned!");

                    }
                    else
                    {
                        pkt = new ReconnectPacket
                        {
                            GameId = player.Owner.Id,
                            Host = "",
                            IsFromArena = false,
                            Key = player.Owner.PortalKey,
                            KeyTime = -1,
                            Name = player.Owner.Name,
                            Port = -1
                        };
                        player.SendInfo("Player will connect to you now!");
                    }

                    i.Value.SendPacket(pkt);

                    return true;
                }
            }
            player.SendError(string.Format("Player '{0}' could not be found!", args));
            return false;
        }
    }
    internal class Bring : Command
    {
        public Bring()
            : base("bring", 7)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (player.Owner is Vault || player.Owner is PetYard)
            {
                player.SendInfo("You can't bring in this world.");
                return false;
            }
            foreach (KeyValuePair<string, Client> i in player.Manager.Clients)
            {
                if (i.Value.Player.Name.EqualsIgnoreCase(args[0]))
                {
                    Packet pkt;
                    if (i.Value.Player.Owner == player.Owner)
                    {
                        i.Value.Player.Move(player.X, player.Y);
                        pkt = new GotoPacket
                        {
                            ObjectId = i.Value.Player.Id,
                            Position = new Position(player.X, player.Y)
                        };
                        i.Value.Player.UpdateCount++;
                        player.SendInfo("Player summoned!");

                    }
                    else
                    {
                        pkt = new ReconnectPacket
                        {
                            GameId = player.Owner.Id,
                            Host = "",
                            IsFromArena = false,
                            Key = player.Owner.PortalKey,
                            KeyTime = -1,
                            Name = player.Owner.Name,
                            Port = -1
                        };
                        player.SendInfo("Player will connect to you now!");
                    }

                    i.Value.SendPacket(pkt);

                    return true;
                }
            }
            player.SendError(string.Format("Player '{0}' could not be found!", args));
            return false;
        }
    }
    internal class SummonAll : Command
    {
        public SummonAll()
            : base("summonall", 8)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            foreach (var i in player.Manager.Clients.Values)
            {

                if (i.Player.Owner == player.Owner)
                {
                    i.Player.Move(player.X, player.Y);
                    i.SendPacket(new GotoPacket
                    {
                        ObjectId = i.Player.Id,
                        Position = new Position(player.X, player.Y)
                    });
                    i.Player.UpdateCount++;
                    player.SendInfo("Players summoned!");
                }
                else
                {
                    i.SendPacket(new ReconnectPacket
                    {
                        GameId = player.Owner.Id,
                        Host = "",
                        IsFromArena = false,
                        Key = player.Owner.PortalKey,
                        KeyTime = -1,
                        Name = player.Owner.Name,
                        Port = -1
                    });
                    player.SendInfo("Players will connect to you now!");
                    return true;
                }
            }
            player.SendError("Players info could not be found!");
            return false;
        }
    }

}
