using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace CrystalEditor.Core.World
{
    public enum DungeonBossState
    {
        None,
        Fighting,
        Dead
    }

    public sealed class Dungeon
    {
        public static readonly string[] DungeonNames =
        {
            "Castle",
            "City West Gate",
            "City South Gate",
            "City East Gate",
            "City Entrance",
            "Pallum Dei Caverns",
            "Goblins' Den",
            "Kubito Grotto",
            "Terun Westway",
            "Dolsam Gulch",
            "Junun Dei Meadows",
            "Polpus Mistmoor",
            "Auris Swordpath",
            "Primone Darkwood",
            "Beltevra Forest",
            "Garrit Dryway",
            "Jungle Ruins of Si Khem",
            "Panclare Brightwood",
            "Kutith Basin",
            "Carcus Southway",
            "Janktra Plains",
            "Vena Eih Marsh",
            "Bronkith Crossroads",
            "Nefron Beach",
            "Ogre Camp",
            "Diaphi Riverway",
            "Ramva Riverbank",
            "Corrum Sih Highroad",
            "Shrine of Awakening",
            "Letyna Tundra",
            "Orc Keep",
            "Clannit Mirrorlake",
            "Musqu Mazewood",
            "Aukul Canyon",
            "Lost City of Nevul",
            "Basu Sah Whisperpath",
            "Denthe Bridge",
            "Temple of Ko Ruh",
            "Orth Bridge",
            "Quavitas Crag",
            "Fronze Cove",
            "Enthe Frostfall",
            "Olvita Barrows",
            "Erithrow Cliffs",
            "Axilla Cinderwood",
            "Derumi Desert",
            "Glisera Oasis",
            "Cortek Sandhollows",
            "Desert City of Clavis",
            "Barbair Abyss",
            "Langooth Peak",
            "Corone Waterkeep",
            "Rinfor Nightwood",
            "Renth Beach",
            "Tempora Estuary",
            "Eorta Deepway",
            "Isle of Giants",
            "Colossal City of Heparl",
            "Altenica Hills",
            "Tunika Mountains",
            "Pranta Rapids",
            "Simuth Hollows",
            "Fasieth Deepmaze",
            "Hillum Sih Longroad",
            "Forbidden Land of O'Kokuh",
            "Infinity Spire (0)",
            "Infinity Spire (1)",
            "Infinity Spire (2)"
        };

        private readonly JArray _data;
        private readonly int _idx;

        public Dungeon(JArray data, int idx)
        {
            _data = data;
            _idx = idx;
        }

        [ReadOnly(true)]
        public string Name
        {
            get => _idx >= DungeonNames.Length ? _idx.ToString() : DungeonNames[_idx];
        }

        public int EncountStepNum
        {
            get => _data[0].ToObject<int>();
            set => _data[0] = value;
        }

        public bool IsConnected
        {
            get => _data[1].ToObject<bool>();
            set => _data[1] = value;
        }

        public int CurrentExploration
        {
            get => _data[2].ToObject<int>();
            set => _data[2] = value;
        }

        public bool ExplorationReset
        {
            get => _data[3].ToObject<bool>();
            set => _data[3] = value;
        }

        public DungeonBossState BossState
        {
            get => _data[4].ToObject<DungeonBossState>();
            set => _data[4] = (int)value;
        }

        public int BossHealth
        {
            get
            {
                int? health = _data[5].ToObject<int?>();

                if (health.HasValue)
                {
                    return health.Value;
                }
                else
                {
                    return -1;
                }
            }

            set
            {
                if (value < 0)
                {
                    _data[5] = null;
                }
                else
                {
                    _data[5] = value;
                }
            }
        }

        public int JobAdjustment0
        {
            get => _data[6][0].ToObject<int>();
            set => _data[6][0] = value;
        }

        public int JobAdjustment1
        {
            get => _data[6][1].ToObject<int>();
            set => _data[6][1] = value;
        }

        public int JobAdjustment2
        {
            get => _data[6][2].ToObject<int>();
            set => _data[6][2] = value;
        }

        public int JobAdjustment3
        {
            get => _data[6][3].ToObject<int>();
            set => _data[6][3] = value;
        }

        public int JobAdjustment4
        {
            get => _data[6][4].ToObject<int>();
            set => _data[6][4] = value;
        }
    }
}
