using Logic_Sim.Engine;
using Logic_Sim.Engine.Primitives;


namespace Logic_Sim.Complex {
    public class BufferInput : Input {
        public BufferInput(Circuit circuit): base(null,0) {            
            BUFFER_Gate pin = circuit.RegisterComponent(new BUFFER_Gate(1));
            Component = pin;
            PinNumber = 0;
        }

    }
}
