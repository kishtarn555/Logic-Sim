using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Logic_Sim.Engine;
using Logic_Sim.Engine.Primitives;
using Logic_Sim.Engine.Extended.IO;

namespace Logic_Sim.Compiler
{
    class LogicCompiler {
        public bool debug = false;
        string code;
        Tokenizer tokenizer;
        Circuit circuit;
        Dictionary<string, Component> components;
        public LogicCompiler(string _code) {
            code = _code;
        }

        public Circuit Compile() {
            components = new Dictionary<string, Component>();
            circuit = new Circuit();
            tokenizer = new Tokenizer(code);
            
            while (tokenizer.HasTokens()) {
                string command = tokenizer.NextString();
                if (command=="ADD") {
                    createPrimitive();
                } else if (command=="CON") {
                    connect();

                } else if (command=="/*"){
                    tokenizer.SkipUntil("*/", true);
                } else {
                    throw new Exception("Unknown instruction: "+command);

                }

            circuit.RunTicks(3);
            }

            return circuit;
        }
        void connect() {
            
            string src, des;
            int pinSrc, pinDes;
            src = tokenizer.NextIdentifier();
            pinSrc = tokenizer.NextInt();
            des = tokenizer.NextIdentifier();
            pinDes = tokenizer.NextInt();
            if (components.ContainsKey(src) == false) throw new NullReferenceException("Unknown identifier as src " + src);
            if (components.ContainsKey(des) == false) throw new NullReferenceException("Unknown identifier as des" + des);
            if (debug) {
                Console.WriteLine("Compiling: Conection created " + src + " " + pinSrc + "," + des + " " + pinDes);
            }
            circuit.Connect(components[src], pinSrc, components[des], pinDes);
        }
        void createPrimitive() {
            Component component;
            string type = tokenizer.NextIdentifier();
            string name = tokenizer.NextIdentifier();
            if (components.ContainsKey(name)) {
                throw new Exception("While compailing, found duplicate identifier");
            }
            if (type=="AND") {
                component = new AND_Gate(name);
            } else if (type == "OR") {

                component = new OR_Gate(name);
            } else if (type == "NOT") {

                component = new NOT_Gate(name);
            } else if (type == "BUFFER") {
                int data = tokenizer.NextInt();
                component = new BUFFER_Gate(data, name);
            } else if (type == "HIGH") {
                component = new HIGH(name);
            } else if (type == "LOW") {
                component = new LOW(name);
            } else if (type == "OUT_BYTE") {
                component = new BYTE_OUTPUT(name);
            } else {

                throw new Exception("While compiling, found unknown primitive " + type);
            }

            components.Add(name, component);
            circuit.RegisterComponent(component);

        }
    }

}
