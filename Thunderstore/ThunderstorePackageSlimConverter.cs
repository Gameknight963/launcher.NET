using Newtonsoft.Json;

namespace launcherdotnet.Thunderstore
{
    public class ThunderstorePackageSlimConverter : JsonConverter<ThunderstorePackageSlim>
    {
        public override ThunderstorePackageSlim ReadJson(JsonReader reader, Type objectType, ThunderstorePackageSlim? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            ThunderstorePackageSlim slim = new();
            bool inLatest = false;

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject)
                {
                    if (inLatest)
                        inLatest = false;
                    else
                        break;
                }

                if (reader.TokenType != JsonToken.PropertyName) continue;
                string? prop = reader.Value as string;
                reader.Read();

                if (inLatest)
                {
                    switch (prop)
                    {
                        case "download_url": slim.DownloadUrl = reader.Value as string; break;
                        case "icon": slim.IconUrl = reader.Value as string; break;
                        default: reader.Skip(); break;
                    }
                }
                else
                {
                    switch (prop)
                    {
                        case "name": slim.Name = reader.Value as string ?? ""; break;
                        case "owner": slim.Owner = reader.Value as string ?? ""; break;
                        case "is_deprecated": slim.IsDeprecated = reader.Value is bool b && b; break;
                        case "latest": inLatest = true; break;
                        case "uuid4": slim.Uuid4 = reader.Value as string ?? ""; break;
                        default: reader.Skip(); break;
                    }
                }
            }
            return slim;
        }
        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, ThunderstorePackageSlim? value, JsonSerializer serializer)
            => throw new NotImplementedException();
    }
}
