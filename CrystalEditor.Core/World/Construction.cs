using Newtonsoft.Json.Linq;

namespace CrystalEditor.Core.World
{
    public enum BuildingType
    {
        RubbleA = 1,
        RubbleB = 2,
        RubbleC = 3,
        Minkas = 101,
        Minkas2 = 102,
        MinkaM = 103,
        MinkaL = 104,
        MinkaG = 105,
        LiltyM = 113,
        SelkieM = 123,
        YukeM = 133,
        ParkS1 = 201,
        ParkS = 202,
        ParkL = 203,
        Zakkaya = 301,
        Market = 302,
        Yadoya = 303,
        Bukiya = 401,
        Bouguya = 402,
        Douguya = 403,
        Arena = 404,
        Bar = 406,
        Casino = 407,
        Guild = 408,
        MagicSchool = 501,
        Shudoin = 502,
        Worshipping = 503,
        Library = 504,
        Max = 505
    }

    public enum BuildingDirection
    {
        S, SE, E, NE, N, NW, W, SW, HackS, HackSE
    }

    public sealed class Construction
    {
        private readonly JArray _data;

        public int LotID
        {
            get => _data[0].ToObject<int>();
            set => _data[0] = value;
        }

        public BuildingType Type
        {
            get => _data[1].ToObject<BuildingType>();
            set => _data[1] = (int)value;
        }

        public int InstanceID
        {
            get => _data[2].ToObject<int>();
            set => _data[2] = value;
        }

        public BuildingDirection Direction
        {
            get => _data[3].ToObject<BuildingDirection>();
            set => _data[3] = (int)value;
        }

        // TODO: VisitorCount = _data[4] | It's an array of visitors that have been there?

        public int Days
        {
            get => _data[5].ToObject<int>();
            set => _data[5] = value;
        }

        public int Roof
        {
            get => _data[6].ToObject<int>();
            set => _data[6] = value;
        }

        // Internally called TatefudaIndex
        public int NoticeboardIndex
        {
            get => _data[7].ToObject<int>();
            set => _data[7] = value;
        }

        public Construction(JArray data)
        {
            _data = data;
        }
    }
}
