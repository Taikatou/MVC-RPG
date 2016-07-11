using System.Collections.Generic;

namespace Game.Render
{
    public class RendererConfig
    {
        private Dictionary<string, bool> values;
        public RendererConfig()
        {
            values = new Dictionary<string, bool>();
        }

        public void AddParam(string key, bool value=true)
        {
            values.Add(key, value);
        }

        public bool GetKey(string key)
        {
            bool return_value = false;
            return values.TryGetValue(key, out return_value);
        }
    }
}
