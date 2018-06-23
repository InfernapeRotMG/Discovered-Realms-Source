#region

using System;
using System.Globalization;
using System.IO;
using System.Threading;
using db;
using log4net;
using log4net.Config;
using wServer.networking;
using wServer.realm;
using System.Net.Mail;
using System.Net;

#endregion

namespace wServer
{
    internal static class Program
    {
        public static bool WhiteList { get; private set; }
        public static bool Verify { get; private set; }
        internal static SimpleSettings Settings;

        private static readonly ILog log = LogManager.GetLogger("Server");
        private static RealmManager manager;

        public static DateTime WhiteListTurnOff { get; private set; }

        private static void Main(string[] args)
        {
            Console.Title = "Fabiano Swagger of Doom - World Server";
            try
            {
                XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net_wServer.config"));

                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                Thread.CurrentThread.Name = "Entry";

                Settings = new SimpleSettings("wServer");
                new Database(
                    Settings.GetValue<string>("db_host", "127.0.0.1"),
                    Settings.GetValue<string>("db_database", "rotmgprod"),
                    Settings.GetValue<string>("db_user", "root"),
                    Settings.GetValue<string>("db_auth", ""));

                manager = new RealmManager(
                    Settings.GetValue<int>("maxClients", "100"),
                    Settings.GetValue<int>("tps", "20"));

                WhiteList = Settings.GetValue<bool>("whiteList", "false");
                Verify = Settings.GetValue<bool>("verifyEmail", "false");
                WhiteListTurnOff = Settings.GetValue<DateTime>("whitelistTurnOff");

                manager.Initialize();
                manager.Run();

                Server server = new Server(manager);
                PolicyServer policy = new PolicyServer();

                Console.CancelKeyPress += (sender, e) => e.Cancel = true;

                policy.Start();
                server.Start();
                new Thread(AutoRestart).Start();
                if (Settings.GetValue<bool>("broadcastNews", "false") && File.Exists("news.txt"))
                    new Thread(autoBroadcastNews).Start();
                log.Info("Server initialized.");

                uint key = 0;
                while ((key = (uint)Console.ReadKey(true).Key) != (uint)ConsoleKey.Escape)
                {
                    if (key == (2 | 80))
                        Settings.Reload();
                }

                log.Info("Terminating...");
                server.Stop();
                policy.Stop();
                manager.Stop();
                log.Info("Server terminated.");
            }
            catch (Exception e)
            {
                log.Fatal(e);

                foreach (var c in manager.Clients)
                {
                    c.Value.Disconnect();
                }
                Console.ReadLine();
            }
        }

        private static void AutoRestart()
        {
            log.Info("Auto-Restarter started...");
            Thread.Sleep(5000);
            ChatManager cm = new ChatManager(manager);
            Thread.Sleep(6900000);
            cm.IngameAnnounce("Server will be auto-restarted in 5 minutes, please exit the game to prevent account in use!");
            Thread.Sleep(60000);
            cm.Announce("Server will be auto-restarted in 4 minutes, please exit the game to prevent account in use!");
            Thread.Sleep(60000);
            cm.Announce("Server will be auto-restarted in 3 minutes, please exit the game to prevent account in use!");
            Thread.Sleep(60000);
            cm.Announce("Server will be auto-restarted in 2 minutes, please exit the game to prevent account in use!");
            Thread.Sleep(60000);
            cm.Announce("Server will be auto-restarted in 1 minute, please exit the game to prevent account in use!");
            Thread.Sleep(50000);
            cm.Announce("Server will be auto-restarted in 10 seconds, please exit the game to prevent account in use!");
            Thread.Sleep(10000);
            System.Diagnostics.Process.Start("wServer.exe");
            Thread.Sleep(1);
            closeserv();
        }
        private static void closeserv()
        {
            foreach (var i in manager.Clients.Values)
            {
                i.Disconnect();
            }
            Thread.Sleep(500);
            Environment.Exit(0);
        }


        public static void SendEmail(MailMessage message, bool enableSsl = true)
        {
            SmtpClient client = new SmtpClient
            {
                Host = Settings.GetValue<string>("smtpHost", "smtp.gmail.com"),
                Port = Settings.GetValue<int>("smtpPort", "587"),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials =
                    new NetworkCredential(Settings.GetValue<string>("serverEmail"),
                        Settings.GetValue<string>("serverEmailPassword"))
            };

            client.Send(message);
        }

        private static void autoBroadcastNews()
        {
                var news = File.ReadAllLines("news.txt");
                do
                {
                    ChatManager cm = new ChatManager(manager);
                    cm.News(news[new Random().Next(news.Length)]);
                    Thread.Sleep(300000); //5 min
                }
                while (true);
            }
    }
}