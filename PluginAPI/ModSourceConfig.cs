using launcherdotnet.Launcher;
using Newtonsoft.Json;

namespace launcherdotnet.PluginAPI
{
    public abstract class ModSourceConfig<T> where T : ModSourceConfig<T>, new()
    {
        public static T Load(string gameDataDirectory, string sourceId)
        {
            string path = Path.Combine(gameDataDirectory, $"{sourceId}.json");
            if (!File.Exists(path)) return new T();
            T instance = File.Exists(path)
                ? JsonConvert.DeserializeObject<T>(File.ReadAllText(path)) ?? new T()
                : new T();
            return instance;
        }

        public static T Load(GameInfo game, string sourceId) => Load(game.DataDirectory, sourceId);

        public void Save(string gameDataDirectory, string sourceId)
        {
            string path = Path.Combine(gameDataDirectory, $"{sourceId}.json");
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public void Save(GameInfo game, string sourceId) => Save(game.DataDirectory, sourceId);
    }
}
