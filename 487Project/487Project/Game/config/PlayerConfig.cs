using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace BulletBlaster.Game.config
{
    internal class PlayerConfig
    {
        public string player_sprite { get; set; }
        public int player_speed { get; set; }
        public int maxHealth { get; set; }
        public SpritePosition position { get; set; }
        public int patternSeed { get; set; }
        public List<BulletPatternConfig> attackPatterns { get; set; }

        Dictionary<Keys, string> keyBindings = new Dictionary<Keys, string>();

        public List<string> lookUpKeys(Keys[] pressedKeys)
        {
            // NEED to loop through and find the keys in the dictionary and return their values
            List<string> returnValues = new List<string>();

            foreach (Keys key in pressedKeys)
            {
                if (keyBindings.ContainsKey(key))
                {
                    returnValues.Add(keyBindings[key]);
                }
            }

            return returnValues;
        }

        public void BindKeys(Dictionary<Keys, string> keyBindings)
        {
            this.keyBindings = keyBindings;
        }
    }
}
