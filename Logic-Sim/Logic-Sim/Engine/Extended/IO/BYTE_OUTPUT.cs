using System;
using System.Collections.Generic;
using System.Text;
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
                Console.Write(this.name+" ");
                for (int i =0; i<8; i++) {
                    Console.Write(renderedInputs[i] ? 1 : 0);
                }
                if (dum==1)
                    Console.Write("!");
                Console.WriteLine();
            }
            output = renderedInputs[8];
        }
    }
}
