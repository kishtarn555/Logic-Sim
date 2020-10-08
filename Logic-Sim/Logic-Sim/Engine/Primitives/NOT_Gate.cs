using System;
using System.Collections.Generic;
using System.Text;

namespace Logic_Sim.Engine.Primitives
{
    class NOT_Gate:Component  {

        public NOT_Gate(string _name = "[NOT_Gate]") : base(1, 1, _name) {

        }
        public override void DoUpdate() {
            nextstate[0] = !renderedInputs[0];            
        }
    }
}
