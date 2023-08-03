using CrystalEditor.Core.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalEditor.Core.Parsing
{
    public class SquirrelSerializer
    {
        protected readonly JArray _data = new JArray();

        public SquirrelSerializer() { }

        public SquirrelSerializer(JArray json, Stream outStream)
        {
            Serialize(json, outStream);
        }

        public SquirrelSerializer(SquirrelDeserializer ds, Stream outStream) : this(ds.GetData(), outStream)
        {

        }

        private static void SerializeJSON(JToken obj, BigEndianBinaryWriter writer, bool key = false)
        {
            switch (obj)
            {
                // Determine type
                case JArray array:
                {
                    writer.Write((int)SQObjectType.OT_ARRAY);
                    writer.Write(array.Count); // Member count of array
                    foreach (JToken token in array)
                    {
                        SerializeJSON(token, writer);
                    }

                    break;
                }
                case JObject jObject:
                {
                    writer.Write((int)SQObjectType.OT_TABLE);
                    writer.Write(jObject.Count); // Member count
                    foreach (KeyValuePair<string, JToken> o in jObject)
                    {
                        SerializeJSON(o.Value, writer); // Serialize Value first
                        SerializeJSON(JToken.FromObject(o.Key), writer, true); // Serialize key next
                    }

                    break;
                }
                case JToken token:
                    switch (token.Type)
                    {
                        case JTokenType.Null:
                            writer.Write((int)SQObjectType.OT_NULL);
                            writer.Write(0);
                            break;
                        case JTokenType.Boolean:
                            writer.Write((int)SQObjectType.OT_BOOL);
                            writer.Write(token.ToObject<bool>() ? 1 : 0);
                            break;
                        case JTokenType.Integer:
                            writer.Write((int)SQObjectType.OT_INTEGER);
                            writer.Write(token.ToObject<int>());
                            break;
                        case JTokenType.Float:
                            writer.Write((int)SQObjectType.OT_FLOAT);
                            writer.Write(token.ToObject<float>());
                            break;
                        case JTokenType.String:
                            string s = token.ToObject<string>();

                            // Check if we've got a key that's been converted to a string.
                            if (key && int.TryParse(s, out int res))
                            {
                                writer.Write((int)SQObjectType.OT_INTEGER);
                                writer.Write(res);
                                break;
                            }

                            writer.Write((int)SQObjectType.OT_STRING);
                            writer.Write(s.Length);
                            writer.Write(Encoding.ASCII.GetBytes(s));
                            if ((s.Length & 3) == 0)
                                writer.Write(0); // 0 pad
                            else
                            {
                                for (int i = 0; i < 4 - (s.Length & 3); i++)
                                    writer.Write((byte)0); // 0 Pad
                            }
                            break;
                        
                        default:
                            throw new Exception($"Unhandled JToken type {token.Type}");
                    }

                    break;
            }
        }

        public void Serialize(JArray baseArr, Stream s, int fileSize = -1)
        {
            using (BigEndianBinaryWriter writer = new BigEndianBinaryWriter(s))
            {
                // Reserve space for size
                writer.Write(0);
                writer.Write(0);

                // Serialize
                SerializeJSON(baseArr[0], writer);

                // Write size
                writer.Seek(0, SeekOrigin.Begin);
                writer.Write((int)writer.BaseStream.Length - 8);

                if (fileSize > 0)
                    writer.BaseStream.SetLength(fileSize);
            }
        }
    }
}
