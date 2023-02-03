using Logic_Sim.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic_Sim.Complex.Pc.Memory {
    public class ByteRam : ComplexChip {
        public Input Enable { get; private set; }
        public Input WriteEnable { get; private set; }
        public Input Clock { get; private set; }
        public Input In0 { get; private set; }
        public Input In1 { get; private set; }
        public Input In2 { get; private set; }
        public Input In3 { get; private set; }
        public Input In4 { get; private set; }
        public Input In5 { get; private set; }
        public Input In6 { get; private set; }
        public Input In7 { get; private set; }
        public Input[] Inputs { get; private set; }
        public Output Out0 { get; private set; }
        public Output Out1 { get; private set; }
        public Output Out2 { get; private set; }
        public Output Out3 { get; private set; }
        public Output Out4 { get; private set; }
        public Output Out5 { get; private set; }
        public Output Out6 { get; private set; }
        public Output Out7 { get; private set; }
        public Output[] Outputs { get; private set; }
        public ByteRam(Circuit circuit) {
            DLatch[] latches = new DLatch[8];
            for (int i =0; i < latches.Length; i++) {
                latches[i] = new DLatch(circuit);
            }
            In0 = latches[0].T;
            In1 = latches[1].T;
            In2 = latches[2].T;
            In3 = latches[3].T;
            In4 = latches[4].T;
            In5 = latches[5].T;
            In6 = latches[6].T;
            In7 = latches[7].T;
            Inputs = new Input[] {
                In0, In1, In2, In3, In4, In5, In6,  In7
            };
            Out0 = latches[0].DataOut;
            Out1 = latches[1].DataOut;
            Out2 = latches[2].DataOut;
            Out3 = latches[3].DataOut;
            Out4 = latches[4].DataOut;
            Out5 = latches[5].DataOut;
            Out6 = latches[6].DataOut;
            Out7 = latches[7].DataOut;
            Outputs = new Output[] {
                Out0, Out1, Out2, Out3, Out4, Out5, Out6, Out7
            };
            Enable = new BufferInput(circuit);
            WriteEnable = new BufferInput(circuit);
            Clock = new BufferInput(circuit);

            for (int i = 0;  i < 8; i++) {
                Pin.Connect(circuit, Enable, latches[i].Enable);
                Pin.Connect(circuit, WriteEnable, latches[i].WR);
                Pin.Connect(circuit, Clock, latches[i].Clock);
            }

        }

    }
}
