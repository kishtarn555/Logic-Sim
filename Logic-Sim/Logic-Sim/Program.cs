using System;
using Logic_Sim.Engine;
using Logic_Sim.Compiler;
using System.IO;
using System.Diagnostics;
using Logic_Sim.Complex;
using Logic_Sim.Complex.Pc.Memory;
using Logic_Sim.Engine.Extended.IO;
using Logic_Sim.Engine.Primitives;
namespace Logic_Sim
{
    class Program
    {
        static void OldCode() {
            EngineState.DEBUG = 0;// EngineState.DFLAGS.PRINT_TICK| EngineState.DFLAGS.UNSAFE_DUMPING| EngineState.DFLAGS.PRINT_UPDATE;
            string src = File.ReadAllText("./Examples/PC/testing/CPU_test.lg");
            Console.WriteLine(src);
            LogicCompiler compiler = new LogicCompiler(src);
            if ((EngineState.DEBUG & EngineState.DFLAGS.UNSAFE_DUMPING) != 0) Console.WriteLine("[WARN] Unsafe dumping is activated, may halt excecution");
            compiler.debug = false;
            Circuit circuit = compiler.BeginCompile();
            Stopwatch sp = new Stopwatch();

            while (true) {
                sp.Restart();
                for (int i = 0; i < 1000; i++) {
                    circuit.DoTick();
                }
                sp.Stop();
                if ((EngineState.DEBUG & EngineState.DFLAGS.PRINT_UPDATE) != 0) {
                    Console.WriteLine(sp.ElapsedMilliseconds + " " + circuit.PendingUpdates);
                }
            }
        }

        [STAThread]
        static void Main(string[] args) {
            EngineState.DEBUG =     EngineState.DFLAGS.UNSAFE_DUMPING;
            Circuit circuit = new Circuit();
            ByteRam ram = new ByteRam(circuit);
            Component enable = new KeySwitch("C","EN");
            Component write = new KEY_READER("M");
            Component print = new KEY_READER("B");
            Component i0 = new KeySwitch("Q", "B0");
            Component i1 = new KeySwitch("W", "B1");
            Component i2 = new KeySwitch("E", "B2");
            Component i3 = new KeySwitch("R", "B3");
            Component i4 = new KeySwitch("T", "B4");
            Component i5 = new KeySwitch("Y", "B5");
            Component i6 = new KeySwitch("U", "B6");
            Component i7 = new KeySwitch("I", "B7");
            Pin.Connect(circuit, enable.Pin(0), ram.Enable);
            Pin.Connect(circuit, write.Pin(0), ram.WriteEnable);
            Pin.Connect(circuit, i0.Pin(0), ram.In0);
            Pin.Connect(circuit, i1.Pin(0), ram.In1);
            Pin.Connect(circuit, i2.Pin(0), ram.In2);
            Pin.Connect(circuit, i3.Pin(0), ram.In3);
            Pin.Connect(circuit, i4.Pin(0), ram.In4);
            Pin.Connect(circuit, i5.Pin(0), ram.In5);
            Pin.Connect(circuit, i6.Pin(0), ram.In6);
            Pin.Connect(circuit, i7.Pin(0), ram.In7);

            NOT_Gate clock = circuit.RegisterComponent(new NOT_Gate("clock"));
            circuit.Connect(clock, 0, clock, 0);
            Pin.Connect(circuit, clock.Pin(0), enable.Pin(0));
            Pin.Connect(circuit, clock.Pin(0), write.Pin(0));
            Pin.Connect(circuit, clock.Pin(0), print.Pin(0));
            Pin.Connect(circuit, clock.Pin(0), ram.Clock);
            Pin.Connect(circuit, clock.Pin(0), i0.Pin(0));
            Pin.Connect(circuit, clock.Pin(0), i1.Pin(0));
            Pin.Connect(circuit, clock.Pin(0), i2.Pin(0));
            Pin.Connect(circuit, clock.Pin(0), i3.Pin(0));
            Pin.Connect(circuit, clock.Pin(0), i4.Pin(0));
            Pin.Connect(circuit, clock.Pin(0), i5.Pin(0));
            Pin.Connect(circuit, clock.Pin(0), i6.Pin(0));
            Pin.Connect(circuit, clock.Pin(0), i7.Pin(0));

            BYTE_OUTPUT outputs = new BYTE_OUTPUT();
            for (int i = 0; i < 8; i++)
                Pin.Connect(circuit, ram.Outputs[i], outputs.Pin(i));
            Pin.Connect(circuit, print.Pin(0), outputs.Pin(8));
            
            while(true) {
                circuit.DoTick();
            }
            circuit.DumpData();


        }
    }
}
