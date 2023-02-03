using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
namespace Logic_Sim.Engine.Extended.IO
{
    class KeySwitch : Component {

        
        Key KeyVal;
        bool previousInputState;
        bool active;
        public KeySwitch(string key,string name="[KeySwitch]"):base(1,1,name) {            
            KeyVal = KeyToKey.KeysCode[key];
            nextstate[0] = false;
            active = false;
            previousInputState = false;
        }
        public override void DoUpdate() {
            // nextstate[0];            System.Windows.Input.
            bool current = Keyboard.IsKeyDown(KeyVal);
            if (current && previousInputState == false) {
                active = !active;
                nextstate[0] = active;
                Console.WriteLine($"{name} switch: " + (active ? "ON" : "OFF"));
            }
            previousInputState = current;
          
            
        }

    }
}
