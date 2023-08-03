using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;

namespace CrystalEditor.Core.NPCs
{
    public sealed class TravellerHero : Hero
    {
        public TravellerHero(JArray data) : base(data) { }

        [ReadOnly(true)]
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
                throw new Exception("Unable to set the name of a villager directly!");
            }
        }
    }
}
