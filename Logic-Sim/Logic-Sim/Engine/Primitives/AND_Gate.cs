using System;
using System.Collections.Generic;
using System.Text;
using Logic_Sim.Engine;
namespace Logic_Sim.Engine.Primitives
{
    internal class AND_Gate : Component
    {
        public AND_Gate(string _name="[AND_Gate]"):base(2,1,_name) {
            
        }
        public override void DoUpdate() {
            nextstate[0] = renderedInputs[0] && renderedInputs[1];
        }
    }
}
