using System;
using System.Collections.Generic;
using System.Text;


namespace Logic_Sim.Engine
{
    struct Update: IComparable {
        public long tick;
        public string message;
        public Component.Port target;
        public int delta; 
        public int CompareTo(object obj) {
            if ( obj is Update) {
                if (tick < ((Update)obj).tick) return -1;
                if (tick == ((Update)obj).tick) return 0;
                if (tick > ((Update)obj).tick) return 1;
            }
            return 0;
        }

        
  

    }
}
