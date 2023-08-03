using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace CrystalEditor.Core
{
    public enum MedalType
    {
        MEDAL_VIT = 0,
        MEDAL_STR = 1,
        MEDAL_END = 2,
        MEDAL_DEX = 3,
        MEDAL_QUI = 4,
        MEDAL_INT = 5,
        MEDAL_FAI = 6,
        MEDAL_STR_01 = 7,
        MEDAL_END_01 = 8,
        MEDAL_DEX_01 = 9,
        MEDAL_QUI_01 = 10,
        MEDAL_INT_01 = 11,
        MEDAL_FAI_01 = 12,
        MEDAL_NONE = 13,
        MEDAL_SWORD = 14,
        MEDAL_AXE = 15,
        MEDAL_HAMMER = 16,
        MEDAL_DAGGER = 17,
        MEDAL_FIRE = 18,
        MEDAL_ICE = 19,
        MEDAL_THUNDER = 20,
        MEDAL_DEBUFF = 21,
        MEDAL_HEAL = 22,
        MEDAL_BUFF = 23,
        MEDAL_DEFENSE = 24,
        MEDAL_HOLY = 25,
        MEDAL_P1_NORMAL = 26,
        MEDAL_P1_MONEY_WEAPON = 27,
        MEDAL_P1_MONEY_ARMOR = 28,
        MEDAL_P1_SAVE_MONEY = 29,
        MEDAL_P1_WELLPREPARED = 30,
        MEDAL_P1_LIKE_PARK = 31,
        MEDAL_P1_LIKE_BAR = 32,
        MEDAL_P1_LIKE_ARENA = 33,
        MEDAL_P1_LIKE_CASINO = 34,
        MEDAL_P2_NORMAL = 35,
        MEDAL_P2_GIVESUPEASILY = 36,
        MEDAL_P2_TENACIOUS = 37,
        MEDAL_P2_FAST = 38,
        MEDAL_P2_SLOW = 39,
        MEDAL_P2_LIKE_WEAK = 40,
        MEDAL_P2_LIKE_STRONG = 41,
        MEDAL_P2_NO_LUNCH = 42,
        /* DLC inifity tower medals */
        MEDAL_MUGEN_01 = 43,
        MEDAL_MUGEN_02 = 44,
        MEDAL_MUGEN_03 = 45,
        MEDAL_MUGEN_04 = 46,
        MEDAL_MUGEN_05 = 47,
        MEDAL_MUGEN_06 = 48,
        MEDAL_MUGEN_07 = 49,
        MEDAL_MUGEN_08 = 50,
        MEDAL_MUGEN_09 = 51,
        MEDAL_MUGEN_10 = 52,
        MEDAL_MUGEN_11 = 53,
        MEDAL_MUGEN_12 = 54,
        MEDAL_MUGEN_13 = 55,
        MEDAL_MUGEN_14 = 56,
        MEDAL_MUGEN_15 = 57,
        MEDAL_MUGEN_16 = 58,
        MEDAL_MUGEN_17 = 59,
        MEDAL_MUGEN_18 = 60,
        MEDAL_MUGEN_19 = 61,
        MEDAL_MUGEN_20 = 62,
        MEDAL_MUGEN_21 = 63,
        MEDAL_MUGEN_22 = 64,
        MEDAL_MUGEN_23 = 65,
        MEDAL_MUGEN_50 = 66,
        MEDAL_MUGEN_51 = 67,
        MEDAL_MUGEN_52 = 68,
        MEDAL_MUGEN_53 = 69,
        MEDAL_MUGEN_54 = 70,
        MEDAL_MUGEN_55 = 71,
        MEDAL_MUGEN_56 = 72,
        MEDAL_MUGEN_57 = 73,
        MEDAL_MUGEN_58 = 74,
        MEDAL_MUGEN_59 = 75,
        MEDAL_MUGEN_60 = 76,
        MEDAL_MUGEN_61 = 77,
        MEDAL_MUGEN_62 = 78,
        MEDAL_MUGEN_63 = 79,
        MEDAL_MUGEN_64 = 80,
        MEDAL_MUGEN_65 = 81,
        MEDAL_MUGEN_66 = 82,
        MEDAL_MUGEN_67 = 83,
        MEDAL_MUGEN_90 = 84,
        MEDAL_MUGEN_91 = 85,
        MEDAL_MUGEN_92 = 86,
        MEDAL_MUGEN_93 = 87,
        MEDAL_MAX = 88
    }

    public sealed class Medal
    {
        private static readonly string[] _medalNames =
        {
            "Vitality",
            "Strength",
            "Toughness",
            "Dexterity",
            "Agility",
            "Intellect",
            "Willpower", // where is Vitality+??
            "Strength+",
            "Toughness+",
            "Dexterity+",
            "Agility+",
            "Intellect+",
            "Willpower+",
            "No Specialty",
            "Dueler",
            "Executioner",
            "Bludgeoner",
            "Impaler",
            "Fire",
            "Frost",
            "Lightning",
            "Enfeebling",
            "Restoration",
            "Enhancing",
            "Protection",
            "Holy",
            "Adventurer",
            "Weapon Buff",
            "Armor Buff",
            "Bargain Hunter",
            "Item Buff",
            "Treehugger",
            "Tavern Lurker",
            "Training Freak",
            "Game Guru",
            "Balance",
            "Safety",
            "Tenacity",
            "Preemptive",
            "Last Stand",
            "Mop-up",
            "Vanguard",
            "Forced March",

            /* DLC medals */
            "Sentinel",
            "Duelist",
            "Acrobat",
            "Infiltrator",
            "Guardian",
            "Artisan",
            "Craftsman",
            "Wanderer",
            "Traveler",
            "Gladiator",
            "Hermit",
            "Assassin",
            "Gladiator+",
            "Hermit+",
            "Assassin+",
            "Hercules",
            "Ulysses",
            "Hercules+",
            "Ulysses+",
            "Achilles",
            "Achilles+",
            "Olympus",
            "Olympus+",
            "Flamebrand",
            "Frostbrand",
            "Electrobrand",
            "Fire Ward",
            "Frost Ward",
            "Lightning Ward",
            "Knowledge",
            "Wisdom",
            "V Talisman",
            "S Talisman",
            "T Talisman",
            "D Talisman",
            "A Talisman",
            "I Talisman",
            "W Talisman",
            "Hi-Potion",
            "Remedy",
            "Phoenix",
            "Loner",
            "Partygoer",
            "Frog",
            "Twilight",
            "MEDAL_MAX"
        };

        private readonly JArray _medalData; // Reference

        public readonly MedalType Type;

        [ReadOnly(true)]
        public string Name
        {
            get => (int)Type >= _medalNames.Length ? ((int)Type).ToString() : _medalNames[(int)Type];
        }

        public bool IsRevealed
        {
            get => _medalData[0].ToObject<bool>();
            set => _medalData[0] = value;
        }
        public uint Count
        {
            get => _medalData[1].ToObject<uint>();
            set => _medalData[1] = value;
        }

        public Medal(int idx, JArray data)
        {
            Type = (MedalType)idx;
            _medalData = data;
        }
    }
}
