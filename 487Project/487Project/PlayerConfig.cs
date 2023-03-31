using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    internal class PlayerConfig
    {
        Dictionary<Keys, string> keyBindings = new Dictionary<Keys, string>();
        public PlayerConfig(Dictionary<Keys, string> kBind) 
        {
            this.keyBindings = kBind;
        }

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
    }
}
