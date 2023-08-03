using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CrystalEditor.Core
{
    public enum FlagType
    {
        GAME_FLAG_UNLOCK_BUKIYA1 = 0,
        GAME_FLAG_UNLOCK_ZAKKAYA1 = 1,
        GAME_FLAG_UNLOCK_BOUGUYA1 = 2,
        GAME_FLAG_UNLOCK_DOUGUYA1 = 3,
        GAME_FLAG_UNLOCK_MINKA2 = 4,
        GAME_FLAG_UNLOCK_PARKS1 = 5,
        GAME_FLAG_UNLOCK_MAGICSCHOOL1 = 6,
        GAME_FLAG_UNLOCK_MINKA3 = 7,
        GAME_FLAG_UNLOCK_MINKA4 = 8,
        GAME_FLAG_UNLOCK_MARKET1 = 9,
        GAME_FLAG_UNLOCK_BAR1 = 10,
        GAME_FLAG_UNLOCK_BAR2 = 11,
        GAME_FLAG_UNLOCK_PARKL1 = 12,
        GAME_FLAG_UNLOCK_SHUDOIN1 = 13,
        GAME_FLAG_UNLOCK_ARENA1 = 14,
        GAME_FLAG_UNLOCK_YADOYA1 = 15,
        GAME_FLAG_UNLOCK_GUILD1 = 16,
        GAME_FLAG_UNLOCK_CASINO1 = 17,
        GAME_FLAG_UNLOCK_BUKIYA2 = 18,
        GAME_FLAG_UNLOCK_BUKIYA3 = 19,
        GAME_FLAG_UNLOCK_BOUGUYA2 = 20,
        GAME_FLAG_UNLOCK_BOUGUYA3 = 21,
        GAME_FLAG_UNLOCK_DOUGUYA2 = 22,
        GAME_FLAG_UNLOCK_DOUGUYA3 = 23,
        GAME_FLAG_UNLOCK_MAGICSCHOOL2 = 24,
        GAME_FLAG_UNLOCK_SHUDOIN2 = 25,
        GAME_FLAG_UNLOCK_ARENA2 = 26,
        GAME_FLAG_UNLOCK_CASTLE2 = 27,
        GAME_FLAG_UNLOCK_CASTLE3 = 28,
        GAME_FLAG_UNLOCK_PARKL2 = 29,
        GAME_FLAG_UNLOCK_PARKL3 = 30,
        GAME_FLAG_UNLOCK_PARKS2 = 31,
        GAME_FLAG_UNLOCK_PARKS3 = 32,
        GAME_FLAG_UNLOCK_MINKAM = 33,
        GAME_FLAG_UNLOCK_MINKAL = 34,
        GAME_FLAG_UNLOCK_MINKAG = 35,
        GAME_FLAG_UNLOCK_LILTYM = 36,
        GAME_FLAG_UNLOCK_SELKIEM = 37,
        GAME_FLAG_UNLOCK_YUKEM = 38,
        GAME_FLAG_UNLOCK_BAR3 = 39,
        GAME_FLAG_UNLOCK_CASINO2 = 40,
        GAME_FLAG_UNLOCK_CASINO3 = 41,
        GAME_FLAG_UNLOCK_WORSHIPPING_A = 42,
        GAME_FLAG_UNLOCK_WORSHIPPING_B = 43,
        GAME_FLAG_UNLOCK_WORSHIPPING_C = 44,
        GAME_FLAG_UNLOCK_ZAKKAYA2 = 45,
        GAME_FLAG_UNLOCK_ZAKKAYA3 = 46,
        GAME_FLAG_UNLOCK_MINKA1 = 47,
        GAME_FLAG_UNLOCK_LIBRARY = 48,
        GAME_FLAG_UNLOCK_RESEARCH_SWORD1 = 49,
        GAME_FLAG_UNLOCK_RESEARCH_SWORD2 = 50,
        GAME_FLAG_UNLOCK_RESEARCH_SWORD3 = 51,
        GAME_FLAG_UNLOCK_RESEARCH_HAMMER1 = 52,
        GAME_FLAG_UNLOCK_RESEARCH_HAMMER2 = 53,
        GAME_FLAG_UNLOCK_RESEARCH_HAMMER3 = 54,
        GAME_FLAG_UNLOCK_RESEARCH_AXE1 = 55,
        GAME_FLAG_UNLOCK_RESEARCH_AXE2 = 56,
        GAME_FLAG_UNLOCK_RESEARCH_MAIL1 = 57,
        GAME_FLAG_UNLOCK_RESEARCH_MAIL2 = 58,
        GAME_FLAG_UNLOCK_RESEARCH_MAIL3 = 59,
        GAME_FLAG_UNLOCK_RESEARCH_HELM1 = 60,
        GAME_FLAG_UNLOCK_RESEARCH_HELM2 = 61,
        GAME_FLAG_UNLOCK_RESEARCH_HELM3 = 62,
        GAME_FLAG_UNLOCK_RESEARCH_GLOVE1 = 63,
        GAME_FLAG_UNLOCK_RESEARCH_GLOVE2 = 64,
        GAME_FLAG_UNLOCK_RESEARCH_POTION1 = 65,
        GAME_FLAG_UNLOCK_RESEARCH_POTION2 = 66,
        GAME_FLAG_UNLOCK_RESEARCH_POTION3 = 67,
        GAME_FLAG_UNLOCK_RESEARCH_ANTIDOTE1 = 68,
        GAME_FLAG_UNLOCK_RESEARCH_ANTIDOTE2 = 69,
        GAME_FLAG_UNLOCK_RESEARCH_ANTIDOTE3 = 70,
        GAME_FLAG_UNLOCK_RESEARCH_PHOENIX1 = 71,
        GAME_FLAG_UNLOCK_RESEARCH_PHOENIX2 = 72,
        GAME_FLAG_UNLOCK_RESEARCH_FIRE1 = 73,
        GAME_FLAG_UNLOCK_RESEARCH_FIRE2 = 74,
        GAME_FLAG_UNLOCK_RESEARCH_FIRE3 = 75,
        GAME_FLAG_UNLOCK_RESEARCH_ICE1 = 76,
        GAME_FLAG_UNLOCK_RESEARCH_ICE2 = 77,
        GAME_FLAG_UNLOCK_RESEARCH_ICE3 = 78,
        GAME_FLAG_UNLOCK_RESEARCH_HEAL1 = 79,
        GAME_FLAG_UNLOCK_RESEARCH_HEAL2 = 80,
        GAME_FLAG_UNLOCK_RESEARCH_HEAL3 = 81,
        GAME_FLAG_UNLOCK_RESEARCH_BUFF1 = 82,
        GAME_FLAG_UNLOCK_RESEARCH_BUFF2 = 83,
        GAME_FLAG_UNLOCK_RESEARCH_BUFF3 = 84,
        GAME_FLAG_UNLOCK_GUILD_HEROMAX8 = 85,
        GAME_FLAG_UNLOCK_GUILD_HEROMAX12 = 86,
        GAME_FLAG_UNLOCK_GUILD_HEROMAX16 = 87,
        GAME_FLAG_UNLOCK_GUILD_HEROPAY1 = 88,
        GAME_FLAG_UNLOCK_GUILD_HEROPAY2 = 89,
        GAME_FLAG_UNLOCK_GUILD_HEROPAY3 = 90,
        GAME_FLAG_UNLOCK_NOTHING = 91,
        GAME_FLAG_DISABLE_MORNING_REPORT = 92,
        GAME_FLAG_DISABLE_BUILD_DESTRUCTION = 93,
        GAME_FLAG_UNLOCK_CLOCK = 94,
        GAME_FLAG_UNLOCK_BP = 95,
        GAME_FLAG_UNLOCK_TP = 96,
        GAME_FLAG_UNLOCK_SP = 97,
        GAME_FLAG_UNLOCK_SP_BALL = 98,
        GAME_FLAG_GAMEFLAG_DUMMY_DUMMY = 99,
        GAME_FLAG_SAMPLE_PACKAGE = 100,
        GAME_FLAG_WORSHIPPING_PACKAGE = 101,
        GAME_FLAG_MINKAG_PACKAGE = 102,
        GAME_FLAG_LILTY_PACKAGE = 103,
        GAME_FLAG_SELKIE_PACKAGE = 104,
        GAME_FLAG_YUKE_PACKAGE = 105,
        GAME_FLAG_LIBRARY_PACKAGE = 106,
        GAME_FLAG_THIRDBUKIYA_PACKAGE = 107,
        GAME_FLAG_THIRDBOUGUYA_PACKAGE = 108,
        GAME_FLAG_THIRDDOUGUYA_PACKAGE = 109,
        GAME_FLAG_THIRDCASINO_PACKAGE = 110,
        GAME_FLAG_THIRDBAR_PACKAGE = 111,
        GAME_FLAG_THIRDPARKL_PACKAGE = 112,
        GAME_FLAG_MINKAMAX_PACKAGE = 113,
        GAME_FLAG_INFINITETOWER_PACKAGE = 114,
        GAME_FLAG_CASTLEUPDATE_PACKAGE = 115,
        GAME_FLAG_COUNTRY_NAME_UNMASK = 116,
    }

    public sealed class Flag
    {
        private readonly JValue _val;

        public int Index { get; private set; }

        public string Name { get; private set; }

        public bool Enabled
        {
            get => _val.ToObject<bool>();
            set => _val.Value = value;
        }

        public Flag(int idx, JValue val)
        {
            _val = val;
            Index = idx;
            Name = ((FlagType)idx).ToString().Substring(10).Replace("_", " ");
        }
    }

    public sealed class Flags
    {
        private readonly JArray _flags;

        public Flags(JArray flags)
        {
            _flags = flags[0] as JArray; // TODO: flags[1] = Today Flags, flags[2] = Yesterday Flags
        }

        public bool this[int idx]
        {
            get => _flags[idx].ToObject<bool>();
            set => _flags[idx] = value;
        }

        public bool this[FlagType flag]
        {
            get => _flags[(int)flag].ToObject<bool>();
            set => _flags[(int)flag] = value;
        }

        public List<Flag> AsList()
        {
            List<Flag> flags = new List<Flag>();
            for (int i = 0; i < _flags.Count; i++)
            {
                flags.Add(new Flag(i, _flags[i] as JValue));
            }
            return flags;
        }
    }
}
