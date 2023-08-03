using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;

namespace CrystalEditor.Core.NPCs
{
    public enum Personality
    {
        PERSONALITY1_NORMAL = 0,
        PERSONALITY1_MONEY_WEAPON = 1,
        PERSONALITY1_MONEY_ARMOR = 2,
        PERSONALITY1_SAVE_MONEY = 3,
        PERSONALITY1_WELLPREPARED = 4,
        PERSONALITY1_LIKE_PARK = 5,
        PERSONALITY1_LIKE_BAR = 6,
        PERSONALITY1_LIKE_ARENA = 7,
        PERSONALITY1_LIKE_CASINO = 8,
        PERSONALITY2_NORMAL = 10,
        PERSONALITY2_GIVESUPEASILY = 11,
        PERSONALITY2_TENACIOUS = 12,
        PERSONALITY2_FAST = 13,
        PERSONALITY2_SLOW = 14,
        PERSONALITY2_LIKE_WEAK = 15,
        PERSONALITY2_LIKE_STRONG = 16,
        PERSONALITY2_NO_LUNCH = 17,
    }

    public class Villager
    {
        public static string[] HeroNames;

        protected static readonly string[] _names = new string[758]
        {
            /* 2168 */
            "King", "Epitav", "Chime", "Hugh Yurg", "???", "Pavlov", "Dark Lord", "Stiltzkin", "Citizen", "Citizen",
            "Adventurer", "Adventurer", "Mogroe", "Mogiosh", "Mogtillo", "Mogmune", "Mogcid",
            /* 2185 */
            /* 2223 */
            "Adam", "Adlai", "Adonis", "Armand", "Albert", "Aldous", "Alex", "Alphonse", "Algy", "Allen",
            "Alvin", "Amos", "Andrew", "Ansel", "Arand", "Arnold", "Ashley", "Austin", "Barnaby", "Berthold",
            "Ben", /* 2206 */ "Benjamin", "Benny", "Bertrand", "Bradley", "Brandon", "Bruce", "Bruto", "Bud", "Byram",
            "Calvin", "Cedric", "Chester", "Christopher", "Clark", "Clifford", "Clive", "Colo", "Conrad", "Craig",
            "Currie", "Cyril", "Dale", "Daniel", "Dalmand", "David", "Demitri", "Derrick", "Dewey", "Dominic",
            "Douglas", "Dudley", "Dustin", "Dylan", "Edward", "Edmond", "Elbert", "Eric", "Emile", "Elmer",
            "Felix", "Fergus", "Francis", "Frazer", "Gabriel", "Gaston", "Gavin", "Geoffrey", "Gerald", "Gilbert",
            "Glen", "Goliath", "Graham", "Greg", "Griffith", "Guire", "Hal", "Hannibal", "Harold", "Harry",
            "Heath", "Hennessy", "Herbert", "Horace", "Howell", "Huey", "Humphrey", "Innoc", "Irvine", "Isral",
            "Izaac", "Jack", "Jackson", "Jarius", "James", "Jed", "Jim", "Joel", "Jonathan", "Jo",
            "Justin", "Keith", "Kenneth", "Keiren", "Lance", "Langley", "Lane", "Lennox", "Reino", "Leroy",
            "Lester", "Lewis", "Linus", "Lloyd", "Lucas", "Luke", "Lyle", "Marron", "Marius", "Martin",
            "Mathias", "Maurice", "Maxwell", "Marrin", "Michael", "Milton", "Nathan", "Ned", "Nicol", "Nick",
            "Noah", "Norrie", "Orlando", "Osbourne", "Olson", "Otto", "Oz", "Patrick", "Paolo", "Phillip",
            "Quenton", "Ralph", "Raphael", "Reginald", "Ryan", "Richard", "Robert", "Rod", "Roger", "Romeo",
            "Roderick", "Rudi", "Rupert", "Randal", "Samson", "Scott", "Sebastian", "Shane", "Siegfried", "Silas",
            "Simon", "Stanley", "Stephen", "Ted", "Terry", "Theodore", "Timothy", "Tobias", "Trevor", "Uriel",
            "Valentine", "Vajeem", "Walter", "Watt", "Wayne", "Wesley", "Wilfred", "Wolfgang", "Xan", "Abel",
            "Adele", "Adolphus", "Aneas", "Aaron", "Alvin", "Alec", "Alexander", "Alfred", "Allan", "Armand",
            "Ambrose", "Andreas", "Angus", "Anthony", "Almos", "Arthur", "August", "Bacchus", "Barnard", "Bartholomew",
            "Benedict", "Bennet", "Bernard", "Boris", "Bram", "Brian", "Bruno", "Byron", "Burt", "Caesar",
            "Carl", "Charles", "Christian", "Clarence", "Clement", "Clint", "Colin", "Connelly", "Cornelius", "Cruz",
            "Curtis", "Cyrus", "Damien", "Darius", "Darryl", "Dean", "Dennis", "Desmond", "Dustin", "Dodger",
            "Duane", "Duke", "Dwayne", "Earl", "Edda", "Erik", "Elias", "Elliot", "Enos", "Ernest",
            "Ferdinand", "Florence", "Franklin", "Frederick", "Gary", "Gavin", "Gawain", "George", "Gillian", "Giles",
            "Godwin", "Gordon", "Grant", "Gregory", "Guy", "Gwyn", "Hamilton", "Hank", "Harrison", "Harvey",
            "Hector", "Henry", "Hopkins", "Howard", "Hubert", "Hugo", "Ian", "Iago", "Isaac", "Ivan",
            "Javez", "Jackie", "Jair", "Jayce", "Jasper", "Jeffrey", "Joachim", "John", "Joseph", "Julian",
            "Karl", "Ken", "Kevin", "Lambert", "Lancelot", "Larry", "Lee", "Lenny", "Leopold", "Leth",
            "Levin", "Remus", "Lionel", "Louis", "Lucien", "Luther", "Maddoc", "Marcus", "Mark", "Matthew",
            "Matt", "Max", "Melvin", "Misha", "Miles", "Morris", "Neal", "Neville", "Nicholas", "Nigel",
            "Norman", "Oliver", "Orville", "Oscar", "Oswell", "Owen", "Pascal", "Paul", "Peter", "Powell",
            "Quincy", "Randolph", "Raymond", "Rene", "Reese", "Rick", "Robin", "Rodney", "Roland", "Ronald",
            "Rudolf", "Ruehl", "Russel", "Ryan", "Samuel", "Sean", "Seth", "Sidney", "Zeke", "Simeon",
            "Saul", "Stephan", "Stewart", "Terence", "Thaddeus", "Thomas", "Tirion", "Tracy", "Tristan", "Val",
            "Victor", "Wallace", "Warren", "Watkins", "Wesley", "Wilbur", "William", "Wyatt", "Zack", "Adeline",
            "Agatha", "Alakina", "Aldiss", "Alexandra", "Alice", "Alisa", "Alphina", "Amanda", "Anna", "Andrea",
            "Angelica", "Anita", "Annabelle", "Annis", "Artha", "Audrey", "Aurelia", "Beatrice", "Betty", "Bridget",
            "Carina", "Carol", "Cassandra", "Cecilia", "Charleen", "Sheri", "Christina", "Claire", "Clarice", "Colette",
            "Constance", "Cynthia", "Daisy", "Deanna", "Diana", "Dolores", "Donna", "Doris", "Effie", "Elaine",
            "Elena", "Elise", "Elizabeth", "Ellie", "Elsa", "Emma", "Emily", "Erica", "Etta", "Eunice",
            "Eve", "Felicia", "Flora", "Francisca", "Freda", "Frayda", "Gemma", "Gerda", "Giselle", "Glenda",
            "Grace", "Grizel", "Hanna", "Heather", "Henrietta", "Hillary", "Hildegard", "Ida", "Ina", "Ayla",
            "Iris", "Isabelle", "Jamie", "Janet", "Jasmine", "Jennifer", "Jessica", "Josephine", "Juliet", "Justine",
            "Kate", "Katrina", "Lana", "Lavinia", "Lena", "Lem", "Lily", "Linda", "Lisa", "Lois",
            "Loraine", "Lorna", "Luanna", "Lucille", "Lucianna", "Luisa", "Luna", "Lynette", "Madelyn", "Mally",
            "Mariah", "Margaret", "Marianne", "Martha", "Mary", "Maddy", "Megan", "Melle", "Melissa", "Melvina",
            "Michelle", "Minerva", "Mirabel", "Moira", "Myrtle", "Nannette", "Natasha", "Nicola", "Noelle", "Olivia",
            "Pamela", "Paula", "Persis", "Philene", "Felice", "Priscilla", "Rachel", "Rena", "Rina", "Rosa",
            "Rosanne", "Rosemary", "Samantha", "Sarah", "Selma", "Shannon", "Shirley", "Sian", "Sidney", "Sonia",
            "Stella", "Sue", "Tabatha", "Tallulah", "Terese", "Tiffany", "Tricia", "Val", "Vanessa", "Veronica",
            "Willa", "Virginia", "Wendy", "Yolanda", "Genovia", "Ada", "Adriana", "Aileen", "Alana", "Alecia",
            "Alexia", "Alicia", "Ally", "Alma", "Amelia", "Anissa", "Angela", "Angelina", "Anne", "Annette",
            "April", "Ashley", "Augusta", "Barbara", "Belle", "Brenda", "Camila", "Carla", "Carrie", "Catherine",
            "Celine", "Charlotte", "Cheryl", "Cindy", "Clara", "Claudia", "Colleen", "Cordelia", "Dana", "Daphne",
            "Della", "Dixie", "Dominica", "Dora", "Dorothy", "Eileen", "Electra", "Eleanor", "Eliza", "Ellen",
            "Elma", "Emilia", "Emmelina", "Enola", "Estelle", "Etty", "Evangeline", "Fay", "Fiona", "Lydia",
            "Frannie", "Frederica", "Galatea", "Jeanne", "Gillian", "Gladys", "Gloria", "Greta", "Guinevere", "Hazel",
            "Helen", "Rydia", "Hilda", "Ivy", "Ilena", "Ingrid", "Irene", "Irma", "Jacqueline", "Jane",
            "Janice", "Jean", "Jenny", "Jill", "Judy", "June", "Karen", "Kathryn", "Layla", "Laurencia",
            "Leah", "Leonora", "Jade", "Lilith", "Lynn", "Lilian", "Lora", "Loretta", "Louise", "Lucy",
            "Lucinda", "Luella", "Lulu", "Lily", "Mabel", "Marle", "Malvina", "Marcia", "Maria", "Marion",
            "Martina", "Matilda", "May", "Meg", "Melinda", "Melody", "Marlene", "Millie", "Mira", "Miranda",
            "Monica", "Nadia", "Natalia", "Nell", "Nina", "Nora", "Ophelia", "Patricia", "Fey", "Freya",
            "Philida", "Lianna", "Prunella", "Rebecca", "Rhea", "Rita", "Rosalyn", "Rose", "Sally", "Sandra",
            "Selena", "Seraphina", "Celeste", "Sheana", "Sibyl", "Silvia", "Sophia", "Stephanie", "Suzanne", "Talia",
            "Tania", "Theresa", "Liz", "Ursula", "Valeria", "Vera", "Victoria", "Viola", "Vivian", "Wilma",
            "Yvonne", "Jularend", "Sonriadd", "Heafleeth", "Riannasyle", "Chloelatte", "Susaroeth", "Loliannyx", "Marakotts", "Agavough",
            "Deoratrath", "Dekklanth", "Tordodex", "Neidhaldt", "Gelfsarrem", "Walddaen", "Veljegout", "Vladreid", "Kreigyael", "Vascokatz",
            "Ruerossta", "Lum'lii", "Ana'mirra", "May'coon", "Lu'juuli", "Lara'mell", "Na'miina", "Fa'temm", "Liu'seena", "Sasha'jee",
            "Kana'terra", "Mu'rajju", "Te'mii", "Az'raa", "Riz'ree", "Ru'latt", "Yu'arro", "Mir'raan", "Fe'ratte", "Er'miil",
            "Ne'fee", "Almrit", "Huliel", "Matreya", "Zildrel", "Remandol", "Bergsorm", "Hrassal", "Voldwal", "Gerolan",
            "Rukleigh", "Balmusti", "Harlevat", "Denilvort", "Ronmudan", "Ranscion", "Marlevarl", "Durlisceus", "Reinvaldt", "Ludviergg",
            "Zandradt", "Eleneuir", "Astolarna", "Silhalora", "Sarastea", "Almaiulna", "Eldeivrala", "Kaleridarna", "Marlnessa", "Tamilagama",
            "Rolmreiza",
        };

        protected readonly JArray _data;

        public int ActorId
        {
            get => _data[0].ToObject<int>();
            set => _data[0] = value;
        }

        public string DataLabel
        {
            get => _data[1].ToObject<string>();
            set => _data[1] = value;
        }

        [ReadOnly(true)]
        [Description("The id of the actor's name.")]
        public int NameId
        {
            get => _data[2].ToObject<int>();
            set => _data[2] = value;
        }

        public int HomeId
        {
            get => _data[3].ToObject<int>();
            set => _data[3] = value;
        }

        public int Relation
        {
            get => _data[4].ToObject<int>();
            set => _data[4] = value;
        }

        public int Days
        {
            get => _data[5].ToObject<int>();
            set => _data[5] = value;
        }

        public Personality Personality1
        {
            get => _data[6].ToObject<Personality>();
            set => _data[6] = (int)value;
        }

        public Personality Personality2
        {
            get => _data[7].ToObject<Personality>();
            set => _data[7] = (int)value;
        }

        public bool IsTraveller
        {
            get => _data[8].ToObject<bool>();
            set => _data[8] = value;
        }

        public int ProfileId
        {
            get => _data[9].ToObject<int>();
            set => _data[9] = value;
        }

        public string ModelName
        {
            get => _data[10].ToObject<string>();
            set => _data[10] = value;
        }

        [ReadOnly(true)]
        [Description("The actor's name.")]
        public virtual string Name
        {
            get
            {
                /* 2223 2963, 2206, 2963 */
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

        public Villager(JArray data)
        {
            _data = data;
        }
    }
}
