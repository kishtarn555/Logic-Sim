using System;
using System.Collections.Generic;
using System.Text;

namespace Logic_Sim.Utils
{
    struct Pair<P,Q>
    {
        public P first;
        public Q second;
        
        public Pair(P p, Q q) {
            first = p;
            second = q;
        }
    }
}
