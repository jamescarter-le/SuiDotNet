using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SuiDotNet.Client.Converters
{
    public class VecMapConverter<TKey, TValue> : JsonConverter<Dictionary<TKey, TValue>> where TKey : notnull
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, Dictionary<TKey, TValue> value, JsonSerializer serializer)
        {
        }

        public override Dictionary<TKey, TValue> ReadJson(JsonReader reader, Type objectType, Dictionary<TKey, TValue> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var result = new Dictionary<TKey, TValue>();

            var jObject = serializer.Deserialize<JObject>(reader);
            var topFields = jObject["fields"];
            var contents = topFields["contents"];

            if (contents is JArray)
            {
                var contentArray = (JArray)topFields["contents"];

                foreach (var content in contentArray)
                {
                    var fields = content["fields"];
                    object tkey;
                    if (typeof(TKey) == typeof(byte[]))
                    {
                        tkey = Convert.FromBase64String(fields["key"].Value<string>());
                    }
                    else
                    {
                        tkey = fields["key"].Value<TKey>();
                    }

                    var valueData = fields["value"];
                    if (valueData is JValue)
                    {
                        result.Add((TKey)tkey, valueData.Value<TValue>());
                    }
                    else
                    {
                        result.Add((TKey)tkey, valueData.ToObject<TValue>());
                    }
                }
            }
            else if (contents is JValue)
            {

            }

            return result;
        }
    }
}