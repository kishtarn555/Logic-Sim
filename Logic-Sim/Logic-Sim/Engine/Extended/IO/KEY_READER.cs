using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
namespace Logic_Sim.Engine.Extended.IO
{
    class KEY_READER : Component {

        
        Key KeyVal;
        public KEY_READER (string key,string name="[KEY_READER]"):base(1,1,name) {            
            KeyVal = KeyToKey.KeysCode[key];
            nextstate[0] = false;
        }
        public override void DoUpdate() {
            // nextstate[0];            System.Windows.Input.
            nextstate[0]=Keyboard.IsKeyDown(KeyVal);
          
            
        }

    }
}
