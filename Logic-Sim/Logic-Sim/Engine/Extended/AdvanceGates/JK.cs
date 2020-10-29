using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Animation;
using Logic_Sim.Engine;
namespace Logic_Sim.Engine.Extended.AdvancedGates
{

    internal class JK_Gate : Component
    {
        bool output;
        bool Q;
        public JK_Gate(string _name = "[BYTE_OUTPUT]") : base(3, 2, _name) {

        }
        public override void DoUpdate() {
            
            if (renderedInputs[2] != output && renderedInputs[2]) {
                if (renderedInputs[0] == false && renderedInputs[1] == false) {

                    Console.BackgroundColor = ConsoleColor.Green;
                } else if (renderedInputs[0] == true && renderedInputs[1] == false) {
                    Q = true;

                    Console.BackgroundColor = ConsoleColor.Black;
                } else if (renderedInputs[0] == false && renderedInputs[1] == true) {
                    Q = false;

                    Console.BackgroundColor = ConsoleColor.Black;
                } else {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Q = !Q;
                }
                    nextstate[0] = Q; nextstate[1] = !Q;
            }
            renderedInputs[2] = output;
        }
    }
}
