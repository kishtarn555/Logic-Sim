using System;
using System.Collections.Generic;
using System.Text;

namespace Logic_Sim.Engine.Primitives
{
    class XOR_Gate:Component  {

        public XOR_Gate(string _name = "[XOR_Gate]") : base(2, 1, _name) {

        }
        public override void DoUpdate() {
            nextstate[0] = renderedInputs[0] ^ renderedInputs[1];
        }
    }
}
