using Logic_Sim.Engine;
using Logic_Sim.Engine.Primitives;
using Logic_Sim.Engine.Extended.AdvancedGates;

namespace Logic_Sim.Complex.Pc.Memory {
    public class DLatch: ComplexChip {
        public Input T { get; private set; }
        public Input WR { get; private set; }
        public Input Enable { get; private set; }
        public Input Clock { get; private set; }

        public Output DataOut { get; private set; }
        public DLatch(Circuit circuit) {
            JK_Gate dataHolder = circuit.RegisterComponent(new JK_Gate());
            NOT_Gate nt = circuit.RegisterComponent(new NOT_Gate());
            BUFFER_Gate bt = circuit.RegisterComponent(new BUFFER_Gate(1));
            AND_Gate cand = circuit.RegisterComponent(new AND_Gate());
            AND_Gate cand2 = circuit.RegisterComponent(new AND_Gate());
            Enable = new BufferInput(circuit);
            T = new BufferInput(circuit);
            Clock = new BufferInput(circuit);
            WR = new BufferInput(circuit);

            Pin.Connect(circuit, T, nt.Pin(0));
            Pin.Connect(circuit, T, bt.Pin(0));

            Pin.Connect(circuit, bt.Pin(0), dataHolder.Pin(0));
            Pin.Connect(circuit, nt.Pin(0), dataHolder.Pin(1));

            Pin.Connect(circuit, Clock, cand.Pin(0));
            Pin.Connect(circuit, Enable, cand.Pin(1));

            Pin.Connect(circuit, cand.Pin(0), cand2.Pin(0));
            Pin.Connect(circuit, WR, cand2.Pin(1));
            Pin.Connect(circuit, cand2.Pin(0), dataHolder.Pin(2));

            AND_Gate output = circuit.RegisterComponent(new AND_Gate());
            Pin.Connect(circuit, dataHolder.Pin(0), output.Pin(0));
            Pin.Connect(circuit, Enable, output.Pin(1));
            DataOut = new Output(output, 0);
            

        }
    }
}
