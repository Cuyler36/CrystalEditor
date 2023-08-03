using CrystalEditor.Core.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace CrystalEditor.Core.Parsing
{
    public class SquirrelDeserializer
    {
        private readonly JArray _data = new JArray();
        public JArray Data => _data;

        public SquirrelDeserializer(Stream squirrelSerializedStream)
        {
            Deserialize(squirrelSerializedStream);
        }

        public SquirrelDeserializer(string path)
        {
            using (FileStream f = File.OpenRead(path))
                Deserialize(f);
        }

        private static void ProcessPackedSquirrelObject(JContainer parent, BigEndianBinaryReader reader)
        {
            int type = reader.ReadInt32();
            if (!Enum.IsDefined(typeof(SQObjectType), type))
                throw new Exception($"Encountered invalid SQObjectType of 0x{type:X8}!");
            switch ((SQObjectType)type)
            {
                case SQObjectType.OT_NULL:
                    reader.ReadInt32(); // Skip next 4 bytes, they should be null
                    parent.Add(null); // Can't push null sadly
                    break;
                case SQObjectType.OT_BOOL:
                    parent.Add(JToken.FromObject(reader.ReadInt32() != 0));
                    break;
                case SQObjectType.OT_INTEGER:
                    parent.Add(JToken.FromObject(reader.ReadInt32()));
                    break;
                case SQObjectType.OT_FLOAT:
                    parent.Add(JToken.FromObject(reader.ReadSingle()));
                    break;
                case SQObjectType.OT_STRING:
                    int len = reader.ReadInt32();
                    long endPos = reader.BaseStream.Position + ((len & 3) == 0 ? len + 4 : (len + 3) & ~3);
                    string s = new string(reader.ReadChars(len));
                    parent.Add(JToken.FromObject(s));
                    reader.Seek(endPos);
                    break;
                case SQObjectType.OT_ARRAY:
                    int members = reader.ReadInt32();
                    JArray arr = new JArray();
                    for (int i = 0; i < members; i++)
                        ProcessPackedSquirrelObject(arr, reader);
                    parent.Add(arr);
                    break;
                case SQObjectType.OT_TABLE:
                    int memberCount = reader.ReadInt32();
                    JObject tbl = new JObject();
                    for (int i = 0; i < memberCount; i++)
                    {
                        JArray keyObj = new JArray();
                        JArray valueObj = new JArray();
                        ProcessPackedSquirrelObject(valueObj, reader);
                        ProcessPackedSquirrelObject(keyObj, reader);

                        tbl.Add(keyObj.First?.ToObject<string>() ?? "", valueObj.First);
                    }
                    parent.Add(tbl);
                    break;
                default:
                    break;
            }
        }

        private void Deserialize(Stream s)
        {
            using (BigEndianBinaryReader reader = new BigEndianBinaryReader(s))
            {
                int dataSize = reader.ReadInt32();
                reader.Seek(4, SeekOrigin.Current); // Skip next 4 bytes, they're unused.
                ProcessPackedSquirrelObject(_data, reader);
            }
        }

        public void DumpJSON(string path)
        {
            File.WriteAllText(path, _data.ToString());
        }

        public JArray GetData() => _data;
    }
}
