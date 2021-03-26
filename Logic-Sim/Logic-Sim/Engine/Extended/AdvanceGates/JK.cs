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

                } else if (renderedInputs[0] == true && renderedInputs[1] == false) {
                    Q = true;

                } else if (renderedInputs[0] == false && renderedInputs[1] == true) {
                    Q = false;
                } else {
                    Q = !Q;
                    Console.WriteLine(this);
                    Console.ReadLine();
                }
            }

            nextstate[0] = Q; nextstate[1] = !Q;
            output = renderedInputs[2];
        }
    }
}
