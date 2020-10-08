using System;
using System.Collections.Generic;
using System.Text;

namespace Logic_Sim.Engine.Primitives
{
    class HIGH:Component  {

        public HIGH(string _name = "[HIGH]") : base(0, 1, _name) {

        }
        public override void DoUpdate() {
            nextstate[0]=true;            
        }
    }
}
