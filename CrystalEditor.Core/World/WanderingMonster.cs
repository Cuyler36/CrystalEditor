using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace CrystalEditor.Core.World
{
    public enum WanderingMonsterState
    {
        Dormant,
        Sleeping,
        Active,
        InBattle
    }

    public sealed class WanderingMonster : Monster
    {

        private static readonly string[] _wanderingMonsterNames =
        {
            "The Corruptor of Minds",
            "The Unstoppable Fiend",
            "The Clockwork Executioner",
            "The Everliving Drake",
            "The Ravenous Howler",
            "The Demonic Duo",
            "The Tiny Tomb Twins",
            "The Silver-Shelled Beast"
        };


        private readonly JArray _data;
        private readonly int _idx;

        public WanderingMonster(JArray data, int idx)
        {
            _data = data;
            _idx = idx;
        }

        [ReadOnly(true)]
        public string Name
        {
            get => _wanderingMonsterNames[_idx];
        }

        [ReadOnly(true)]
        public string DungeonName
        {
            get => DungeonId < 0 ? "None" : Dungeon.DungeonNames[DungeonId];
        }

        public WanderingMonsterState State
        {
            get => _data[0].ToObject<WanderingMonsterState>();
            set => _data[0] = (int)value;
        }

        public int DungeonId
        {
            get => _data[1].ToObject<int?>() ?? -1;
            set
            {
                if (value < 0)
                {
                    _data[1] = null;
                }
                else
                {
                    _data[1] = value;
                }
            }
        }
    }
}
