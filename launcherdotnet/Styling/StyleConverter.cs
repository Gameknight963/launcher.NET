using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace launcherdotnet.Styling
{
    public class StyleConverter : JsonConverter<ControlStyle>
    {
        static readonly JsonSerializer _plain = new();

        public override ControlStyle? ReadJson(JsonReader reader, Type objectType,
            ControlStyle? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            string? typeName = obj["Type"]?.Value<string>();
            return typeName switch
            {
                "Button" => obj.ToObject<ButtonStyle>(_plain),
                null or "Control" => obj.ToObject<ControlStyle>(_plain),
                _ => throw new InvalidDataException($"Unknown style type '{typeName}'")
            };
        }

        public override void WriteJson(JsonWriter writer, ControlStyle? value, JsonSerializer serializer)
        {
            JObject obj = JObject.FromObject(value!, _plain);
            if (value is ButtonStyle) obj["Type"] = "Button";
            obj.WriteTo(writer);
        }
    }
}
