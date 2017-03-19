using System;
using System.Data;
using GTANetworkServer;
using GTANetworkShared;
using MySql.Data.MySqlClient;
using Insight.Database.Providers.MySql;
using Insight.Database;

using BCr = BCrypt.Net;

namespace FrostyCnR 
{
    public class FrostyGM : Script
    {
        private static MySqlConnectionStringBuilder _database;
        private static IUserRepository _userRepository;

        public string[] VehicleModels = new string[]{
        "Dinghy",
        "Dinghy2",
        "Dinghy3",
        "Dinghy4",
        "Jetmax",
        "Marquis",
        "Seashark",
        "Seashark2",
        "Seashark3",
        "Speeder",
        "Speeder2",
        "Squalo",
        "Submersible",
        "Submersible2",
        "Suntrap",
        "Toro",
        "Toro2",
        "Tropic",
        "Tropic2",
        "Tug",

        "Benson",
        "Biff",
        "Hauler",
        "Mule",
        "Mule2",
        "Mule3",
        "Packer",
        "Phantom",
        "Phantom2",
        "Pounder",
        "Stockade",
        "Stockade3",

        "Blista",
        "Blista2",
        "Blista3",
        "Brioso",
        "Dilettante",
        "Dilettante2",
        "Issi2",
        "Panto",
        "Prairie",
        "Rhapsody",

        "CogCabrio",
        "Examplar",
        "F620",
        "Felon",
        "Felon2",
        "Jackal",
        "Oracle",
        "Oracle2",
        "Sentinel",
        "Sentinel2",
        "Windsor",
        "Windsor2",
        "Zion",
        "Zion2",

        "Bmx",
        "Cruiser",
        "Fixter",
        "Scorcher",
        "TriBike",
        "TriBike2",
        "TriBike3",

        "Ambulance",
        "FBI",
        "FBI2",
        "FireTruck",
        "PBus",
        "Police",
        "Police2",
        "Police3",
        "Police4",
        "PoliceOld1",
        "PoliceOld2",
        "PoliceT",
        "Policeb",
        "Polmav",
        "Pranger",
        "Predator",
        "Riot",
        "Sheriff",
        "Sheriff2",

        "Annihilator",
        "Buzzard",
        "Buzzard2",
        "Cargobob",
        "Cargobob2",
        "Cargobob3",
        "Cargobob4",
        "Frogger",
        "Frogger2",
        "Maverick",
        "Savage",
        "Skylift",
        "Supervolito",
        "Supervolito2",
        "Swift",
        "Swift2",
        "Valkyrie",
        "Valkyrie2",
        "Volatus",

        "BUlldozer",
        "Cutter",
        "Dump",
        "Flatbed",
        "Guardian",
        "Handler",
        "Mixer",
        "Mixer2",
        "Rubble",
        "TipTruck",
        "TipTruck2",

        "Barracks",
        "Barracks2",
        "Barracks3",
        "Crusader",
        "Rhino",

        "Akuma",
        "Avarus",
        "Bagger",
        "Bati",
        "Bati2",
        "BF400",
        "Blazer4",
        "CarbonRS",
        "Chimera",
        "Cliffhanger",
        "Daemon",
        "Daemon2",
        "Defiler",
        "Double",
        "Enduro",
        "Esskey",
        "Faggio",
        "Faggio2",
        "Faggio3",
        "Fcr", //?
        "Fcr", //?
        "Gargoyle",
        "Hakuchou",
        "Hexer",
        "Innovation",
        "Lectro",
        "Manchez",
        "Nemesis",
        "Nightblade",
        "PCJ",
        "Ratbike",
        "Ruffian",
        "Sanchez",
        "Sanchez2",
        "Sanctus",
        "Shotaro",
        "Sovereign",
        "Thrust",
        "Vader",
        "Vindicator",
        "Vortex",
        "Wolfsbane",
        "Zombiea",
        "Zombieb",

        "Blade",
        "Buccaneer",
        "Buccaneer2",
        "Chino",
        "Chino2",
        "Dominator",
        "Dominator2",
        "Dukes",
        "Dukes2",
        "Faction",
        "Faction2",
        "Faction3",
        "Gauntlet",
        "Gauntlet2",
        "Hotknife",
        "Lurcher",
        "Moonbeam",
        "Moonbeam2",
        "Nightshade",
        "Phoenix",
        "Picador",
        "RatLoader",
        "RatLoader2",
        "Ruiner",
        "Ruiner2",
        "SabreGT",
        "SabreGT2",
        "Sadler2",
        "SlamVan",
        "SlamVan2",
        "SlamVan3",
        "Stalion",
        "Stalion2",
        "Tampa",
        "Vigero",
        "Virgo",
        "Virgo2",
        "Virgo3",
        "Voodoo",
        "Voodoo2",

        "BfInjection",
        "Bifta",
        "Blazer",
        "Blazer2",
        "Blazer3",
        "Blazer5",
        "Bodhi2",
        "Brawler",
        "DLoader",
        "Dune",
        "Dune2",
        "Dune5",
        "Dune4",
        "Insurgent",
        "Insurgent2",
        "Kalahari",
        "Lguard",
        "Marshall",
        "Mesa",
        "Mesa2",
        "Mesa3",
        "Monster",
        "RancherXL",
        "RancherXL2",
        "Rebel",
        "Rebel2",
        "Sandking",
        "Sandking2",
        "Technical",
        "Technical2",
        "TrophyTruck",
        "TrophyTruck2",

        "Besra",
        "Blimp",
        "Blimp2",
        "CargoPlane",
        "Cuban800",
        "Dodo",
        "Duster",
        "Hydra",
        "Jet",
        "Lazer",
        "Luxor",
        "Luxor2",
        "Mammatus",
        "Milijet",
        "Nimbus",
        "Shamal",
        "Stunt",
        "TItan",
        "Velum",
        "Velum2",
        "Vestra",

        "BJXL",
        "Baller",
        "Baller2",
        "Baller3",
        "Baller4",
        "Baller5",
        "Baller6",
        "Cavalcade",
        "Cavalcade2",
        "Contender",
        "Dubsta",
        "Dubsta2",
        "Dubsta3",
        "FQ0",
        "Granger",
        "Gresley",
        "Habanero",
        "Huntley",
        "Landstalker",
        "Patriot",
        "Radi",
        "Rocoto",
        "Seminole",
        "Serrano",
        "XLS",
        "XLS2",

        "Asea",
        "Asea2",
        "Asterope",
        "Cog55",
        "Cog552",
        "Cognoscenti",
        "Emperor",
        "Emperor2",
        "Emperor3",
        "Fugitive",
        "Glendale",
        "Ingot",
        "Intruder",
        "Limo",
        "Premier",
        "Primo",
        "Primo2",
        "Regina",
        "Romero",
        "Stanier",
        "Stratum",
        "Stretch",
        "Surge",
        "Tailgater",
        "Warrener",
        "Washington",

        "Airbus",
        "Brickade",
        "Bus",
        "Coach",
        "Rallytruck",
        "RentalBus",
        "Taxi",
        "Tourbus",
        "Trash",
        "Trash2",

        "Alpha",
        "Banshee",
        "Banshee2",
        "BestiaGTS",
        "Buffalo",
        "Buffalo2",
        "Buffalo3",
        "Carbonizzare",
        "Comet2",
        "Comet3",
        "Coquette",
        "Elegy",
        "Elegy2",
        "Feltzer2",
        "Feltzer3",
        "Furoregt",
        "Fusilade",
        "Futo",
        "Jester",
        "Jester2",
        "Khamelion",
        "Kuruma",
        "Kuruma2",
        "Lynx",
        "Massacro",
        "Massacro",
        "Ninef",
        "Ninef2",
        "Omnis",
        "Penumbra",
        "RapidGT",
        "RapidGT2",
        "Raptor",
        "Schafter2",
        "Schafter3",
        "Schafter4",
        "Schafter5",
        "Schafter6",
        "Schwarzer",
        "Seven70",
        "Specter",
        "Specter2",
        "Sultan",
        "Surano",
        "Tampa2",
        "Tropos",
        "Verlierer2",

        "BType",
        "BType2",
        "BType3",
        "Casco",
        "Coquette2",
        "Coquette3",
        "JB700",
        "Mamba",
        "Manana",
        "Monroe",
        "Peyote",
        "Pigalle",
        "Stinger",
        "StingerGT",
        "Tornado",
        "Tornado2",
        "Tornado3",
        "Tornado4",
        "Tornado5",
        "Tornado6",
        "ZType",

        "Adder",
        "Bullet",
        "Cheetah",
        "EntityXF",
        "FMJ",
        "Infernus",
        "LE7B",
        "Nero",
        "Nero2",
        "Osiris",
        "Penetrator",
        "Pfister811",
        "Prototipo",
        "Reaper",
        "Sheava",
        "SultanRS",
        "Superd",
        "T20",
        "Tempesta",
        "Turismor",
        "Tyrus",
        "Vacca",
        "Voltic",
        "Voltic2",
        "Zentorno",
        "Italigtb",
        "Italigtb2",

        "ArmyTanker",
        "ArmyTrailer",
        "ArmyTrailer2",
        "BaleTrailer",
        "BoatTrailer",
        "CableCar",
        "DockTrailer",
        "GrainTrailer",
        "PropTrailer",
        "RakeTrailer",
        "TR2",
        "TR3",
        "TR4",
        "TRFlat",
        "TVTrailer",
        "Tanker",
        "Tanker2",
        "TrailerLogs",
        "TrailerSmall",
        "Trailers",
        "Trailers2",
        "Trailers3",

        "Freight",
        "FreightCar",
        "FreightCont1",
        "FreightCont2",
        "FreightGrain",
        "FreightTrailer",
        "TankerCar",

        "Airtug",
        "Caddy",
        "Caddy2",
        "Docktug",
        "Forklift",
        "Mower",
        "Ripley",
        "Sadler",
        "Scrap",
        "TowTruck",
        "TowTruck2",
        "Tractor",
        "Tractor2",
        "Tractor3",
        "UtilityTruck",
        "UtilityTruck2",
        "UtilityTruck3",
        //Vans
        "Bison",
        "Bison2",
        "Bison3",
        "BobcatXL",
        "Boxville",
        "Boxville2",
        "Boxville3",
        "Boxville4",
        "Boxville5",
        "Burrito",
        "Burrito2",
        "Burrito3",
        "Burrito4",
        "Burrito5",
        "Camper",
        "GBurrito",
           "Journey",
        "Minivan",
        "Minivan2",
        "Paradise",
        "Pony",
        "Pony2",
        "Rumpo",
        "Rumpo2",
        "Rumpo3",
        "Speedo",
        "Speedo2",
        "Surfer",
        "Surfer2",
        "Taco",
        "Youga",
        "Youga2"
    };

        Random hotwireChance = new Random();

        public FrostyGM()
        {
            API.onResourceStart += myResourceStart;
            API.onPlayerConnected += OnPlayerConnected;
        }

        private void myResourceStart()
        {
            API.consoleOutput("[------------------------------------------]");
            API.consoleOutput("[            Frosty Cops n Robbers         ]");
            API.consoleOutput("[                Version 0.0.1             ]");
            API.consoleOutput("[           Created by VirtualFrost        ]");
            API.consoleOutput("[------------------------------------------]");

            MySqlInsightDbProvider.RegisterProvider();
            _database = new MySqlConnectionStringBuilder("server=localhost;user=root;database=frosty-cnr;port=3306;password=;");
            _userRepository = _database.Connection().As<IUserRepository>();

        }

        public interface IUserRepository
        {
            UserAccount RegisterAccount(UserAccount userAccount);
            UserAccount GetAccount(string name);
        }

        public class UserAccount
        {
            public string Username { get; set; }
            public string Hash { get; set; }
        }

        public void OnPlayerConnected(Client player)
        {
            API.sendChatMessageToPlayer(player, "Welcome to Frosty Cops and Robbers!");
        }

        [Command("login", GreedyArg = true)]
        public void CMD_userLogin(Client player, string password)
        {
            UserAccount account = _userRepository.GetAccount(player.name);

            bool isPasswordCorrect = BCr.BCrypt.Verify(password, account.Hash);

            if (isPasswordCorrect)
            {
                API.sendChatMessageToPlayer(player, "You're now logged in!");
            } else
            {
                API.sendChatMessageToPlayer(player, "Wrong Password!");
            }
        }

        [Command("register", GreedyArg = true)]
        public void CMD_userRegister(Client player, string password)
        {
            var hash = BCr.BCrypt.HashPassword(password, BCr.BCrypt.GenerateSalt(12));

            UserAccount account = new UserAccount
            {
                Username = player.name,
                Hash = hash

            };

            _userRepository.RegisterAccount(account);
            API.sendChatMessageToPlayer(player, "You have registered!");
        }

        //Admin Commands
        [Command("getpos", GreedyArg = true)]
        public void getPosition(Client player, string name)
        {
            Vector3 PlayerPos = API.getEntityPosition(player);
            API.sendChatMessageToPlayer(player, "X: " + PlayerPos.X + " Y: " + PlayerPos.Y + " Z: " + PlayerPos.Z);
            API.consoleOutput("Name: " + name + " X: " + PlayerPos.X + " Y: " + PlayerPos.Y + " Z: " + PlayerPos.Z);
        }

        [Command("goto", GreedyArg = true)]
        public void goToPosition(Client player, string location)
        {
            string pos = location;
            switch (pos)
            {
                case "lombank1":
                    API.setEntityPosition(player, new Vector3(-1581.081, -558.571, 34.95325));
                    break;
                case "lombank2":
                    API.setEntityPosition(player, new Vector3(-693.5095, -582.5724, 31.55194));
                    break;
                case "lombank3":
                    API.setEntityPosition(player, new Vector3(6.959941, -933.3794, 29.90501));
                    break;
                case "lombank4":
                    API.setEntityPosition(player, new Vector3(-863.7408, -192.3661, 37.83302));
                    break;
                case "mazebank1":
                    API.setEntityPosition(player, new Vector3(-1370.474, -503.0782, 33.15739));
                    break;
                case "mazebank2":
                    API.setEntityPosition(player, new Vector3(-66.95165, -802.4657, 44.22729));
                    break;
                case "mazebank3":
                    API.setEntityPosition(player, new Vector3(-1317.919, -832.2131, 16.9697));
                    break;
                case "kayton1":
                    API.setEntityPosition(player, new Vector3(-817.6145, -622.4203, 29.22164));
                    break;
                case "kayton2":
                    API.setEntityPosition(player, new Vector3(-731.3457, -811.931, 23.67677));
                    break;
                case "flecca1":
                    API.setEntityPosition(player, new Vector3(-2966.404, 482.9045, 15.69272));
                    break;
                case "flecca2":
                    API.setEntityPosition(player, new Vector3(1175.276, 2702.673, 38.1727));
                    break;
                case "flecca3":
                    API.setEntityPosition(player, new Vector3(-1214.419, -327.6115, 37.67231));
                    break;
                case "flecca4":
                    API.setEntityPosition(player, new Vector3(150.9875, -1037.244, 29.3392));
                    break;
                case "flecca5":
                    API.setEntityPosition(player, new Vector3(315.5083, -275.3137, 53.9239));
                    break;
            }
        }
        //House Commands

        //Robbery Commands

        //Vehicle Commands
        [Command("v", GreedyArg = true)]
        public void SpawnCarCommand(Client player, string model)
        {
            int modelindex = -1;
            for (int i = 0; i < VehicleModels.Length; i++)
            {
                if (VehicleModels[i].IndexOf(model, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    modelindex = i;
                    break;
                }
            }
            if (modelindex == -1)
            {
                API.sendChatMessageToPlayer(player, "Vehicle model on name " + model + " not found!");
                return;
            }

            var rot = API.getEntityRotation(player.handle);
            var veh = API.createVehicle(API.vehicleNameToModel(VehicleModels[modelindex]), player.position, new Vector3(0, 0, rot.Z), 0, 0);

            API.setPlayerIntoVehicle(player, veh, -1);
            API.sendChatMessageToPlayer(player, "Vehicle model " + VehicleModels[modelindex] + " spawned!");
        }

        [Command("engine", GreedyArg = true)]
        public void carEngine(Client player)
        {
            var vehicle = API.getPlayerVehicle(player);
            if(API.getPlayerVehicleSeat(player) != -1 && API.isPlayerInAnyVehicle(player))
            {
                API.sendChatMessageToPlayer(player, "You need to be inside a vehicle and in the drivers seat to use this command.");
            } else
            {
                if(API.getVehicleEngineStatus(vehicle) == true)
                {
                    API.setVehicleEngineStatus(vehicle, false);
                    API.sendChatMessageToPlayer(player, "You have turned off the engine.");
                } else
                {
                    API.setVehicleEngineStatus(vehicle, true);
                    API.sendChatMessageToPlayer(player, "You have turned on the engine.");
                }
            }
        }

        [Command("seatbelt", GreedyArg = true)]
        public void carSeatbelt(Client player)
        {
            if (!API.isPlayerInAnyVehicle(player))
            {
                API.sendChatMessageToPlayer(player, "You need to be inside a vehicle to use this command.");
            }
            else
            {
                if(API.getPlayerSeatbelt(player) == true)
                {
                    API.setPlayerSeatbelt(player, false);
                    API.sendChatMessageToPlayer(player, "You have taken off your seatbelt");
                } else
                {
                    API.setPlayerSeatbelt(player, true);
                    API.sendChatMessageToPlayer(player, "You have put on your seatbelt");
                }
            }
        }

        [Command("cardoor", GreedyArg = true)]
        public void carDoors(Client player)
        {
            var vehicle = API.getPlayerVehicle(player);
            var seatID = API.getPlayerVehicleSeat(player)+1;

            if (!API.isPlayerInAnyVehicle(player))
            {
                API.sendChatMessageToPlayer(player, "You need to be inside a vehicle and in the drivers seat to use this command.");
            }
            else
            {
                if(API.getVehicleDoorState(vehicle, seatID) == false)
                {
                    API.setVehicleDoorState(vehicle, seatID, true);
                    API.sendChatMessageToPlayer(player, "You have opened your door.");
                } else
                {
                    API.setVehicleDoorState(vehicle, seatID, false);
                    API.sendChatMessageToPlayer(player, "You have closed your door.");
                }
            }
        }

        [Command("cartrunk", GreedyArg = true)]
        public void carTrunk(Client player)
        {
            var vehicle = API.getPlayerVehicle(player);

            if (!API.isPlayerInAnyVehicle(player))
            {
                API.sendChatMessageToPlayer(player, "You need to be inside a vehicle and in the drivers seat to use this command.");
            }
            else
            {
                if (API.getVehicleDoorState(vehicle, 5) == false)
                {
                    API.setVehicleDoorState(vehicle, 5, true);
                    API.sendChatMessageToPlayer(player, "You have opened the trunk.");
                }
                else
                {
                    API.setVehicleDoorState(vehicle, 5, false);
                    API.sendChatMessageToPlayer(player, "You have closed the trunk.");
                }
            }
        }

        [Command("carhood", GreedyArg = true)]
        public void carHood(Client player)
        {
            var vehicle = API.getPlayerVehicle(player);

            if (!API.isPlayerInAnyVehicle(player))
            {
                API.sendChatMessageToPlayer(player, "You need to be inside a vehicle and in the drivers seat to use this command.");
            }
            else
            {
                if (API.getVehicleDoorState(vehicle, 4) == false)
                {
                    API.setVehicleDoorState(vehicle, 4, true);
                    API.sendChatMessageToPlayer(player, "You have opened the hood.");
                }
                else
                {
                    API.setVehicleDoorState(vehicle, 4, false);
                    API.sendChatMessageToPlayer(player, "You have closed the hood.");
                }
            }
        }

        [Command("hotwire", GreedyArg = true)]
        public void hotwireVeh(Client player)
        {
            int hotwire;
            var vehicle = API.getPlayerVehicle(player);
            if (API.getVehicleEngineStatus(vehicle) == true)
            {
                API.sendChatMessageToPlayer(player, "The vehicle is already turned on.");
            } else
            {
                hotwire = hotwireChance.Next(1, 10);
                if (hotwire >= 5)
                {
                    API.setVehicleEngineStatus(vehicle, true);
                    API.sendChatMessageToPlayer(player, "You successfully hotwired the car.");
                    API.sendChatMessageToPlayer(player, hotwire.ToString());
                }
                else
                {
                    API.sendChatMessageToPlayer(player, "You failed to hotwire the car.");
                    API.sendChatMessageToPlayer(player, hotwire.ToString());
                }
            }
        }
    }
}
