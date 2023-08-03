using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CrystalEditor.Core.NPCs
{
    public sealed class Inventory
    {
        private readonly JArray _data;
        private readonly ObservableCollection<Item> _items;

        public ObservableCollection<Item> Items { get => _items; }

        public Inventory(JArray items)
        {
            _data = items;

            List<Item> temp = new List<Item>(items.Count);

            for (int i = 0; i < items.Count; i++)
            {
                JArray item = items[i] as JArray;
                temp.Add(new Item(item));
            }

            _items = new ObservableCollection<Item>(temp);
        }

        public Item GetItem(int index)
        {
            if (index < _items.Count)
            {
                return _items[index];
            }

            return null;
        }

        public void SetItem(int index, Item item)
        {
            if (index < _items.Count)
            {
                /* Create a new backing array and insert it */
                JArray backing_array = new JArray((int)item.Type, item.Level);
                _data[index] = backing_array;
                _items[index] = new Item(backing_array); // create a new item to update the backing array reference
            }
        }

        public void AddItem(ItemType type, int level)
        {
            JArray backing_array = new JArray((int)type, level);

            _data.Add(backing_array);
            _items.Add(new Item(backing_array));
        }

        public void RemoveItem(int index)
        {
            if (index < _items.Count)
            {
                _items.RemoveAt(index);
                _data.RemoveAt(index);
            }
        }

        public Item this[int index]
        {
            get => GetItem(index);
            set => SetItem(index, value);
        }
    }

    public sealed class Combatant
    {
        private readonly JObject _data;


        public readonly Inventory Inventory;

        [Description("How much motivation the hero has.")]
        public int Motivation
        {
            get => _data["motivation"].ToObject<int>();
            set => _data["motivation"] = value;
        }

        public int Vitality
        {
            get => _data["vit"].ToObject<int>();
            set => _data["vit"] = value;
        }

        public int ParamType
        {
            get => _data["paramType"].ToObject<int>();
            set => _data["paramType"] = value;
        }

        public int Strength // Attack
        {
            get => _data["str"].ToObject<int>();
            set => _data["str"] = value;
        }

        public int Agility // Evasion
        {
            get => _data["qui"].ToObject<int>();
            set => _data["qui"] = value;
        }

        public int Willpower
        {
            get => _data["fai"].ToObject<int>();
            set => _data["fai"] = value;
        }

        public int Toughness // Defense
        {
            get => _data["end"].ToObject<int>();
            set => _data["end"] = value;
        }

        public int Dexterity
        {
            get => _data["dex"].ToObject<int>();
            set => _data["dex"] = value;
        }

        public int Intellect
        {
            get => _data["int"].ToObject<int>();
            set => _data["int"] = value;
        }

        public int StrengthBonus
        {
            get => _data["strB"].ToObject<int>();
            set => _data["strB"] = value;
        }

        public int VitalityBonus
        {
            get => _data["vitB"].ToObject<int>();
            set => _data["vitB"] = value;
        }

        public int WillpowerBonus
        {
            get => _data["faiB"].ToObject<int>();
            set => _data["faiB"] = value;
        }

        public int ToughnessBonus
        {
            get => _data["endB"].ToObject<int>();
            set => _data["endB"] = value;
        }

        public int DexterityBonus
        {
            get => _data["dexB"].ToObject<int>();
            set => _data["dexB"] = value;
        }

        public int IntellectBonus
        {
            get => _data["intB"].ToObject<int>();
            set => _data["intB"] = value;
        }

        public int AgilityBonus
        {
            get => _data["quiB"].ToObject<int>();
            set => _data["quiB"] = value;
        }

        [Description("How much money the hero has saved.")]
        public int Money
        {
            get => _data["money"].ToObject<int>();
            set => _data["money"] = value;
        }

        public int JobID
        {
            get => _data["jobID"].ToObject<int>();
            set => _data["jobID"] = value;
        }

        [Description("The raw level experience value for the hero. Changing this can alter the hero's Level property.")]
        public int Experience
        {
            get => _data["exp"].ToObject<int>();
            set => _data["exp"] = value;
        }

        public Combatant(JObject data)
        {
            _data = data;
            Inventory = new Inventory(data["items"] as JArray);
        }
    }

    public class Hero : Villager
    {
        private static readonly int[] _levelExpTable = {
            0, 4, 12, 25, 42, 64, 91, 123, 161, 204,
            252, 306, 366, 431, 504, 582, 667, 758, 857, 962,
            1075, 1195, 1322, 1457, 1600, 1750, 1909, 2076, 2251, 2436,
            2628, 2830, 3041, 3261, 3490, 3729, 3978, 4236, 4505, 4784,
            5073, 5372, 5682, 6003, 6336, 6679, 7033, 7399, 7777, 8166,
            8568, 8981, 9407, 9845, 10296, 10759, 11235, 11725, 12227, 12744,
            13273, 13816, 14374, 14945, 15530, 16130, 16744, 17373, 18017, 18676,
            19349, 20039, 20743, 21463, 22200, 22952, 23720, 24504, 25305, 26122,
            26956, 27807, 28675, 29561, 30464, 31384, 32322, 33278, 34251, 35244,
            36254, 37283, 38330, 39397, 40482, 41587, 42711, 43854, 45017, 46200
        };

        public int Likes
        {
            get => _data[11].ToObject<int>();
            set => _data[11] = value;
        }

        // TODO: Shop

        public readonly Combatant Combatant;

        // TODO: Injured -> ??

        // Custom

        [Description("The hero's level.")]
        public int Level
        {
            get
            {
                int exp = Combatant.Experience;
                for (int i = 0; i < _levelExpTable.Length; i++)
                {
                    if (_levelExpTable[i] > exp) return i;
                }
                return 0;
            }
            set
            {
                if (value < 1) value = 1;
                else if (value > _levelExpTable.Length) value = _levelExpTable.Length;
                Combatant.Experience = _levelExpTable[value - 1];
            }
        }

        [ReadOnly(false)]
        [Description("The actor's name.")]
        public override string Name
        {
            get
            {
                if (NameId >= 2168 && NameId <= 2925)
                {
                    return _names[NameId - 2168];
                }
                else if (NameId >= 1000000)
                {
                    return HeroNames[NameId - 1000000];
                }
                else
                {
                    return NameId.ToString();
                }
            }
            set
            {
                if (NameId < 1_000_000)
                {
                    throw new ArgumentOutOfRangeException(nameof(NameId), $"{nameof(NameId)} was out of the valid name range, {nameof(NameId)} == {NameId}!");
                }
                else
                {
                    HeroNames[NameId - 1000000] = value;
                }
            }
        }

        public Hero(JArray data) : base(data)
        {
            Combatant = new Combatant(data[13] as JObject);
        }
    }
}
