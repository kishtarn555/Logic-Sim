using System;
using System.Collections.Generic;
using System.Text;

namespace Logic_Sim.Engine
{
    public static class EngineState {

        public static uint DEBUG;
        public static class DFLAGS {
            public const uint
            UNSAFE_DUMPING  = 1<<0,
            PRINT_TICK      = 1<<1,
            PRINT_UPDATE    = 1<<2;
        }
        
    }
}
