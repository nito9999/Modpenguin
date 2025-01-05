using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Modtropica_server.poptropica
{
    internal class island_data
    {
        public class Island_name
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string IslandFullName { get; set; }
        }
        public static List<Island_name> Island_Names = new List<Island_name>
        {
            new Island_name
            {
                Id = "38",
                Name = "Carrot_as3",
                IslandFullName = "24 Carrot Island"
            },
            new Island_name
            {
                Id = "67",
                Name = "Arab1_as3",
                IslandFullName = "Arabian Nights - Episode 1"
            },
            new Island_name
            {
                Id = "74",
                Name = "Arab2_as3",
                IslandFullName = "Arabian Nights - Episode 2"
            },
            new Island_name
            {
                Id = "84",
                Name = "Arab3_as3",
                IslandFullName = "Arabian Nights - Episode 3"
            },
            new Island_name
            {
                Id = "10",
                Name = "Astro",
                IslandFullName = "Astro-Knights Island"
            },
            new Island_name
            {
                Id = "37",
                Name = "Backlot",
                IslandFullName = "Back Lot Island"
            },
            new Island_name
            {
                Id = "9",
                Name = "BigNate",
                IslandFullName = "Big Nate Island"
            },
            new Island_name
            {
                Id = "34",
                Name = "Charlie",
                IslandFullName = "Charlie and the Chocolate Factory"
            },
            new Island_name
            {
                Id = "12",
                Name = "Counter",
                IslandFullName = "Counterfeit Island"
            },
            new Island_name
            {
                Id = "18",
                Name = "Cryptid",
                IslandFullName = "Cryptids Island"
            },
            new Island_name
            {
                Id = "2",
                Name = "Early",
                IslandFullName = "Early Poptropica"
            },
            new Island_name
            {
                Id = "90",
                Name = "Prison_as3",
                IslandFullName = "Escape From Pelican Rock Island"
            },
            new Island_name
            {
                Id = "79",
                Name = "Ghd_as3",
                IslandFullName = "Galactic Hot Dogs Island"
            },
            new Island_name
            {
                Id = "24",
                Name = "GameShow",
                IslandFullName = "Game Show Island"
            },
            new Island_name
            {
                Id = "26",
                Name = "Ghost",
                IslandFullName = "Ghost Story Island"
            },
            new Island_name
            {
                Id = "17",
                Name = "Peanuts",
                IslandFullName = "Great Pumpkin Island"
            },
            new Island_name
            {
                Id = "25",
                Name = "LegendarySwords",
                IslandFullName = "Legendary Swords Island"
            },
            new Island_name
            {
                Id = "32",
                Name = "Moon",
                IslandFullName = "Lunar Colony"
            },
            new Island_name
            {
                Id = "96",
                Name = "Map_as3",
                IslandFullName = "Map"
            },
            new Island_name
                {
                Id = "51",
                Name = "DeepDive1_as3",
                IslandFullName = "Mission Atlantis - Episode 1"
            },
            new Island_name
            {
                Id = "57",
                Name = "DeepDive2_as3",
                IslandFullName = "Mission Atlantis - Episode 2"
            },
            new Island_name
            {
                Id = "61",
                Name = "DeepDive3_as3",
                IslandFullName = "Mission Atlantis - Episode 3"
            },
            new Island_name
            {
                Id = "42",
                Name = "Mocktropica_as3",
                IslandFullName = "Mocktropica Island"
            },
            new Island_name
            {
                Id = "92",
                Name = "Ftue_as3",
                IslandFullName = "Monkey Wrench Island"
            },
            new Island_name
            {
                Id = "45",
                Name = "Carnival_as3",
                IslandFullName = "Monster Carnival"
            },
            new Island_name
            {
                Id = "82",
                Name = "Viking_as3",
                IslandFullName = "Mystery of the Map Island"
            },
            new Island_name
            {
                Id = "23",
                Name = "Train",
                IslandFullName = "Mystery Train"
            },
            new Island_name
            {
                Id = "50",
                Name = "Myth_as3",
                IslandFullName = "Mythology Island"
            },
            new Island_name
            {
                Id = "8",
                Name = "Nabooti",
                IslandFullName = "Nabooti Island"
            },
            new Island_name
            {
                Id = "36",
                Name = "NightWatch",
                IslandFullName = "Night Watch Island"
            },
            new Island_name
            {
                Id = "64",
                Name = "Con1_as3",
                IslandFullName = "Poptropicon - Episode 1"
            },
            new Island_name
            {
                Id = "72",
                Name = "Con2_as3",
                IslandFullName = "Poptropicon - Episode 2"
            },
            new Island_name
            {
                Id = "76",
                Name = "Con3_as3",
                IslandFullName = "Poptropicon - Episode 3"
            },
            new Island_name
            {
                Id = "41",
                Name = "Poptropolis_as3",
                IslandFullName = "Poptropolis Games"
            },
            new Island_name
            {
                Id = "13",
                Name = "Reality",
                IslandFullName = "Reality TV Island"
            },
            new Island_name
            {
                Id = "21",
                Name = "Japan",
                IslandFullName = "Red Dragon Island"
            },
            new Island_name
            {
                Id = "27",
                Name = "Shipwreck",
                IslandFullName = "S.O.S. Island"
            },
            new Island_name
            {
                Id = "3",
                Name = "Shark",
                IslandFullName = "Shark Tooth Island"
            },
            new Island_name
            {
                Id = "78",
                Name = "Shrink_as3",
                IslandFullName = "Shrink Ray Island"
            },
            new Island_name
            {
                Id = "15",
                Name = "Trade",
                IslandFullName = "Skullduggery Island"
            },
            new Island_name
            {
                Id = "7",
                Name = "Spy",
                IslandFullName = "Spy Island"
            },
            new Island_name
            {
                Id = "16",
                Name = "Steam",
                IslandFullName = "Steamworks Island"
            },
            new Island_name
            {
                Id = "6",
                Name = "Super",
                IslandFullName = "Super Power Island"
            },
            new Island_name
            {
                Id = "33",
                Name = "Villain",
                IslandFullName = "Super Villain Island"
            },
            new Island_name
            {
                Id = "47",
                Name = "Survival1_as3",
                IslandFullName = "Survival - Episode 1"
            },
            new Island_name
            {
                Id = "53",
                Name = "Survival2_as3",
                IslandFullName = "Survival - Episode 2"
            },
            new Island_name
            {
                Id = "55",
                Name = "Survival3_as3",
                IslandFullName = "Survival - Episode 3"
            },
            new Island_name
            {
                Id = "59",
                Name = "Survival4_as3",
                IslandFullName = "Survival - Episode 4"
            },
            new Island_name
            {
                Id = "69",
                Name = "Survival5_as3",
                IslandFullName = "Survival - Episode 5"
            },
            new Island_name
            {
                Id = "44",
                Name = "Time_as3",
                IslandFullName = "Time Tangled Island"
            },
            new Island_name
            {
                Id = "86",
                Name = "Timmy_as3",
                IslandFullName = "Timmy Failure Island"
            },
            new Island_name
            {
                Id = "29",
                Name = "Woodland",
                IslandFullName = "Twisted Thicket"
            },
            new Island_name
            {
                Id = "28",
                Name = "Vampire",
                IslandFullName = "Vampire's Curse Island"
            },
            new Island_name
            {
                Id = "39",
                Name = "VirusHunter_as3",
                IslandFullName = "Virus Hunter Island"
            },
            new Island_name
            {
                Id = "19",
                Name = "West",
                IslandFullName = "Wild West Island"
            },
            new Island_name
            {
                Id = "31",
                Name = "Boardwalk",
                IslandFullName = "Wimpy Boardwalk"
            },
            new Island_name
            {
                Id = "20",
                Name = "Wimpy",
                IslandFullName = "Wimpy Wonderland"
            },
            new Island_name
            {
                Id = "35",
                Name = "Zombie",
                IslandFullName = "Zomberry Island"
            }
        };
    }
}
