using System;
using System.Collections.Generic;
using System.Text;

namespace Logic_Sim.Engine.Primitives
{
    class LOW:Component  {

        public LOW(string _name = "LOW") : base(0, 1, _name) {

        }
        public override void DoUpdate() {
            nextstate[0]=false;            
        }
    }
}
