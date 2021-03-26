using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Logic_Sim.Engine;
namespace Logic_Sim.Engine.Extended.IO
{
    
    internal class BYTE_OUTPUT : Component
    {
        
       bool output;
        int dum = 0;
        public BYTE_OUTPUT(string _name="[BYTE_OUTPUT]"):base(9,0,_name) {
            
        }
        public override void DoUpdate() {
            
            if (renderedInputs[8] !=output && renderedInputs[8]) {

                byte l = 0;
                dum++;
                Console.Write(this.name + " ");
                int ds = 0;
                for (int i = 7; i >= 0; i--) {
                    ds *= 2;
                    ds += (renderedInputs[i] ? 1 : 0);
                    Console.Write(renderedInputs[i] ? 1 : 0);
                }
                Console.Write(" {0:X} ", ds);
                Console.Write( ds);

                if (dum == 1)
                    Console.Write("!");
                Console.WriteLine();
                
            }
            output = renderedInputs[8];
        }
    }
}
