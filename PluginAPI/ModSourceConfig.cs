using launcherdotnet.Launcher;
using Newtonsoft.Json;

namespace launcherdotnet.PluginAPI
{
    public abstract class ModSourceConfig<T> where T : ModSourceConfig<T>, new()
    {
        public static T Load(GameInfo game, string sourceId)
        {
            string path = Path.Combine(game.DataDirectory, $"{sourceId}.json");
            if (!File.Exists(path)) return new T();
            T instance = File.Exists(path)
                ? JsonConvert.DeserializeObject<T>(File.ReadAllText(path)) ?? new T()
                : new T();
            return instance;
        }

        public void Save(GameInfo game, string sourceId)
        {
            string path = Path.Combine(game.DataDirectory, $"{sourceId}.json");
            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
