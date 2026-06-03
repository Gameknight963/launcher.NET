using Newtonsoft.Json;

namespace launcherdotnet.Styling
{
    public class DwmColorConverter : JsonConverter<DwmColor>
    {
        public override void WriteJson(JsonWriter writer, DwmColor? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue(value.ToAbgr());
        }

        public override DwmColor ReadJson(JsonReader reader, Type objectType, DwmColor? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return new DwmColor(Color.Empty);

            int abgr = Convert.ToInt32(reader.Value);
            return DwmColor.FromAbgr(abgr);
        }
    }
}
