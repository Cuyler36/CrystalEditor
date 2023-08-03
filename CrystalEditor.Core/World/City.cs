using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalEditor.Core.World
{
    public sealed class City
    {
        private readonly JObject _data;

        public readonly List<Construction> Constructions = new List<Construction>();

        public City(JObject data)
        {
            _data = data;
            LoadConstructions();
        }

        private void LoadConstructions()
        {
            foreach (JArray constructionData in _data["constructions"])
            {
                Constructions.Add(new Construction(constructionData));
            }
        }
    }
}
