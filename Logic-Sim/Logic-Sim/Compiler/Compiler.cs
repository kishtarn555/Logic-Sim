using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Logic_Sim.Engine;


namespace Logic_Sim.Compiler
{
    class Compiler {

        string code;
        Tokenizer tokenizer;
        Circuit circuit;
        Compiler(string _code) {
            code = _code;
        }

        public Circuit Compile() {
            circuit = new Circuit();
            tokenizer = new Tokenizer(code);
            
            while (tokenizer.HasTokens()) {
                string command = tokenizer.NextString();
                if (command=="PRIMITIVE") {
                    createPrimitive();
                    circuit.RunTicks(2);
                } else if (command=="CONNECT") {
                    //TODO: connect

                } else if (command=="/*"){
                    tokenizer.SkipUntil("*/",true)

                }

            } 
            return circuit;
        }
        public void createPrimitive() {
            Component component;

        }
    }

}
