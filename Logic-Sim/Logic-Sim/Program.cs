using System;
using Logic_Sim.Engine;
using Logic_Sim.Compiler;
using System.IO;
namespace Logic_Sim
{
    class Program
    {
        [STAThread]
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            EngineState.DEBUG = 0;// EngineState.DFLAGS.PRINT_TICK| EngineState.DFLAGS.UNSAFE_DUMPING| EngineState.DFLAGS.PRINT_UPDATE;
            Console.WriteLine(File.ReadAllText("./EXAMPLES/GATES.lg"));
           LogicCompiler compiler = new LogicCompiler(File.ReadAllText("./EXAMPLES/GATES.lg"));
            if ((EngineState.DEBUG & EngineState.DFLAGS.UNSAFE_DUMPING)!=0) Console.WriteLine("[WARN] Unsafe dumping is activated, may halt excecution");
            compiler.debug = false;
            Circuit circuit = compiler.BeginCompile();
            while (true) {
                circuit.DoTick();
            }
            
        }
    }
}
