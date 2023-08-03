using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using CrystalEditor.Core.NPCs;
using CrystalEditor.Core.Parsing;
using CrystalEditor.Core.World;
using Newtonsoft.Json.Linq;

namespace CrystalEditor.Core
{
    public enum SaveResult
    {
        FailedNoSuchFile = -3,
        FailedCannotAccessFile = -2,
        FailedGenericError = -1,
        Success = 0
    }

    public sealed class Save : SquirrelDeserializer
    {
        public const int SAVE_SIZE = 0x48000;

        public string FileLocation;
        private readonly SquirrelSerializer _serializer = new SquirrelSerializer();

        public bool Valid { get; } = true;

        [Description("How much Gil the kingdom has.")]
        public uint Gil
        {
            get => Data[0]["game"]["currentTP"].ToObject<uint>();
            set => Data[0]["game"]["currentTP"] = value;
        }

        [Description("How much Elementite the kingdom has.")]
        public uint Elementite
        {
            get => Data[0]["game"]["currentBP"].ToObject<uint>();
            set => Data[0]["game"]["currentBP"] = value;
        }

        [Description("The name of the kingdom.")]
        public string CountryName
        {
            get => Data[0]["game"]["countryName"].ToObject<string>();
            set => Data[0]["game"]["countryName"] = value;
        }

        [Description("The player's name.")]
        public string PlayerName
        {
            get => Data[0]["game"]["playerName"].ToObject<string>();
            set => Data[0]["game"]["playerName"] = value;
        }

        public readonly List<Medal> Medals = new List<Medal>();
        public readonly Flags Flags;
        public readonly List<Villager> Villagers = new List<Villager>();
        public readonly List<Hero> Heros = new List<Hero>();
        public readonly List<Dungeon> Dungeons = new List<Dungeon>();
        public readonly List<WanderingMonster> WanderingMonsters = new List<WanderingMonster>();
        public readonly City City;

        public Save(string fileLocation) : base(fileLocation)
        {
            FileLocation = fileLocation;
            LoadHeroNames();
            LoadMedals();
            LoadVillagers();
            LoadDungeons();
            LoadHeros();
            LoadWanderingMonsters();
            Flags = new Flags(Data[0]["game"]["flags"] as JArray);
            City = new City(Data[0]["world"]["city"] as JObject);
        }

        private void LoadHeroNames()
        {
            Villager.HeroNames = Data[0]["world"]["actorNames"].ToObject<string[]>();
        }

        private void LoadMedals()
        {
            foreach (var medalInfo in (Data[0]["game"]["medals"] as JObject))
            {
                if (medalInfo.Key == "towerMedals") continue; // TODO: Handle these
                Medals.Add(new Medal(int.Parse(medalInfo.Key), medalInfo.Value as JArray));
            }
        }

        private void LoadVillagers()
        {
            foreach (var villagerInfo in (Data[0]["world"]["actors"] as JArray))
            {
                Villagers.Add(new Villager(villagerInfo as JArray));
            }
        }

        private void LoadDungeons()
        {
            foreach (var dungeon in (Data[0]["world"]["dungeons"] as JObject))
            {
                int idx = int.Parse(dungeon.Key);

                if (idx == 0) continue; /* we skip the castle dungeon for now because it's slightly different in structure */
                Dungeons.Add(new Dungeon(dungeon.Value as JArray, idx));
            }
        }

        private void LoadHeros()
        {
            foreach (var heroInfo in (Data[0]["world"]["heroes"] as JArray))
            {
                if (heroInfo[8].ToObject<bool>())
                {
                    Heros.Add(new TravellerHero(heroInfo as JArray));
                }
                else
                {
                    Heros.Add(new Hero(heroInfo as JArray));
                }
            }
        }

        private void LoadWanderingMonsters()
        {
            foreach (var wandererInfo in Data[0]["world"]["wandering"] as JObject)
            {
                int idx = int.Parse(wandererInfo.Key);
                WanderingMonsters.Add(new WanderingMonster(wandererInfo.Value as JArray, idx));
            }
        }

        private void SaveHeroNames()
        {
            Data[0]["world"]["actorNames"] = new JArray(Villager.HeroNames);
        }

        public SaveResult Write()
        {
            if (!File.Exists(FileLocation)) return SaveResult.FailedNoSuchFile;

            try
            {
                SaveHeroNames();

                _serializer.Serialize(Data, File.Create(FileLocation), SAVE_SIZE);
                return SaveResult.Success;
            }
            catch
            {
                return SaveResult.FailedCannotAccessFile;
            }

            // This should never appear.
            return SaveResult.FailedGenericError;
        }
    }
}
