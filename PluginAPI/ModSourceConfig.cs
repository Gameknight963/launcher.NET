using launcherdotnet.Launcher;
using Newtonsoft.Json;

namespace launcherdotnet.PluginAPI
{
    public abstract class ModSourceConfig<T> where T : ModSourceConfig<T>, new()
    {
        /// <summary>
        /// Called after the 
        /// </summary>
        protected virtual void OnLoaded() { }

        /// <summary>
        /// Called just before this <see cref="ModSourceConfig{T}"/> config is saved.
        /// </summary>
        protected virtual void OnSaving() { }

        public static T Load(GameInfo game, string sourceId)
        {
            string path = Path.Combine(game.DataDirectory, $"{sourceId}.json");
            if (!File.Exists(path)) return new T();
            T instance = File.Exists(path)
                ? JsonConvert.DeserializeObject<T>(File.ReadAllText(path)) ?? new T()
                : new T();
            instance.OnLoaded();
            return instance;
        }

        public void Save(GameInfo game, string sourceId)
        {
            OnSaving();
            string path = Path.Combine(game.DataDirectory, $"{sourceId}.json");
            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
