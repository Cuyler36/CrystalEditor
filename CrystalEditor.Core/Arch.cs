using System;
using CrystalEditor.Core.Parsing;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CrystalEditor.Core
{
    public sealed class SaveSlot
    {
        public string CountryName;
        public int Days;
        public bool End;

        // Custom
        public bool Current = false;
    }

    public sealed class Arch : SquirrelDeserializer
    {
        private readonly List<SaveSlot> _saveSlots = new List<SaveSlot>();

        public Arch(string fileLocation) : base(fileLocation)
        {
            JArray arr = GetData();
            if (arr?[0] == null) throw new Exception("Cannot instantiate Arch class without backing JArray!");
            foreach (JToken slot in arr[0]["game"])
            {
                if (slot.Type != JTokenType.Object) continue;
                SaveSlot saveSlot = new SaveSlot { CountryName = slot["countryName"].ToObject<string>(), Days = slot["days"].ToObject<int>(), End = slot["endSave"].ToObject<bool>() };
                _saveSlots.Add(saveSlot);
            }
            int? currentSlot = arr[0]["saveSlot"]?.ToObject<int>();
            if (currentSlot > -1 && currentSlot.Value < _saveSlots.Count)
            {
                _saveSlots[currentSlot.Value].Current = true;
            }
        }

        public IReadOnlyList<SaveSlot> GetSlots()
        {
            return _saveSlots;
        }
    }
}
