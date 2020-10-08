using System;
using System.Collections.Generic;
using System.Text;

namespace Logic_Sim.Engine.Primitives
{
    class BUFFER_Gate:Component  {
        
        public BUFFER_Gate(int delay,string _name = "[BUFFER_Gate]") : base(1, 1, _name) {
            if (delay <= 0) throw new ArgumentOutOfRangeException("Created buffer at invalid delay");
            this.delay = delay;
        }
        public override void DoUpdate() {
            nextstate[0] = renderedInputs[0];
        }


    }

}
