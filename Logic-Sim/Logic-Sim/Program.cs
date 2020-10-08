using System;
using Logic_Sim.Engine;
namespace Logic_Sim
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            EngineState.DEBUG = true;
            Circuit circuit = new Circuit();
            for (int i =0; i < 10; i++) {
                Update up = new Update() ;
                up.message = "Hola amigo " + i.ToString();
                up.tick = i*2;
                circuit.AddUpdate(up);
            }
            circuit.DumpData();
           // Console.ReadKey();
        }
    }
}
