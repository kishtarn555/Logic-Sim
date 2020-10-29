using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Logic_Sim.Engine;
using Logic_Sim.Engine.Primitives;
using Logic_Sim.Engine.Extended.IO;
using Logic_Sim.Engine.Extended.AdvancedGates;
using System.IO;
using System.CodeDom;

namespace Logic_Sim.Compiler
{
    class LogicCompiler {
        public bool debug = false;
        string code;
        Circuit circuit;
        Dictionary<string, Component> components;
        public LogicCompiler(string _code) {
            code = _code;
        }
        public Circuit BeginCompile() {
            Tokenizer tokenizer = new Tokenizer(code);
            circuit = new Circuit();
            components = new Dictionary<string, Component>();
            Compile(tokenizer,"") ;
            return circuit;
        }
        void Compile(Tokenizer tokenizer, string root) {

            int updated;
            while (tokenizer.HasTokens()) {
                string command = tokenizer.NextString();
                updated = 0;
                if (command == "ADD") {
                    createPrimitive(tokenizer, root);
                    updated = 2;
                } else if (command == "CON") {
                    connect(tokenizer, root);
                    updated = 2;
                } else if (command == "/*") {
                    tokenizer.SkipUntil("*/", true);
                    updated = 0;
                } else if (command == "IMP") {
                    import(tokenizer, root); ;
                    updated = 0;
                } else if (command == "--print") {
                    int when = tokenizer.NextInt();
                    string name = root + "_" + tokenizer.NextIdentifier();
                    circuit.DebugComplist.Push((when, components[name]));
                } else if (command == "ALI") {
                    string alias, original;
                    alias = root+"_"+tokenizer.NextIdentifier();
                    original = root+"_"+tokenizer.NextIdentifier();
                    if (debug)
                        Console.WriteLine("a" + alias + " " + original);
                    if (components.ContainsKey(alias)) throw new Exception("Repeated alias " + alias + ", renaming " + original);
                    if (!components.ContainsKey(original)) throw new Exception("Trying to give alias: " + alias + ", but" + original+" is missing");
                    components.Add(alias, components[original]);
                    
                } else if (command == "--printTICK") {
                    string text = tokenizer.NextString();
                    Console.WriteLine("printing: " + text + " " + circuit.Tick);
                } else { 
                    throw new Exception("["+root+"] Unknown instruction: "+command);

                }
                circuit.RunTicks(updated);
            }

        }
        void import(Tokenizer tokenizer, string root) {
            string name = root+"_"+tokenizer.NextIdentifier();
            if (components.ContainsKey(name)) {
                throw new Exception("Redefinition of" + name + " as ALT");
            }
            string path = tokenizer.NextString();
            Compile(new Tokenizer(File.ReadAllText(path)), name);
        }
        void connect(Tokenizer tokenizer,string root) {
            
            string src, des;
            int pinSrc, pinDes;
            src = root+"_"+tokenizer.NextIdentifier();
            pinSrc = tokenizer.NextInt();
            des = root+"_"+tokenizer.NextIdentifier();
            pinDes = tokenizer.NextInt();
            if (components.ContainsKey(src) == false) throw new NullReferenceException("["+root+"] Unknown identifier as src: " + src);
            if (components.ContainsKey(des) == false) throw new NullReferenceException("["+root+"] Unknown identifier as des: " + des);
            if (debug) {
                Console.WriteLine("Compiling: Conection created " + src + " " + pinSrc + "," + des + " " + pinDes);
            }
            circuit.Connect(components[src], pinSrc, components[des], pinDes);
        }
        void createPrimitive(Tokenizer tokenizer,string root) {
            Component component;
            string type = tokenizer.NextIdentifier();
            string name = root+"_"+tokenizer.NextIdentifier();
            if (components.ContainsKey(name)) {
                throw new Exception("While compailing, found duplicate identifier: "+name);
            }
            if (type=="AND") {
                component = new AND_Gate(name);
            } else if (type == "OR") {

                component = new OR_Gate(name);
            } else if (type == "NOT") {
                component = new NOT_Gate(name);
            } else if (type == "XOR") {
                component = new XOR_Gate(name);
            } else if (type == "BUFFER") {
                int data = tokenizer.NextInt();
                component = new BUFFER_Gate(data, name);
            } else if (type == "HIGH") {
                component = new HIGH(name);
            } else if (type == "LOW") {
                component = new LOW(name);
            } else if (type == "OUT_BYTE") {
                component = new BYTE_OUTPUT(name);
            } else if (type == "KEY") {
                string k = tokenizer.NextString();
                component = new KEY_READER(k, name);
            }else if (type == "JK") {
                component = new JK_Gate(name);
            } else {

                throw new Exception("While compiling, found unknown primitive " + type);
            }

            components.Add(name, component);
            circuit.RegisterComponent(component);

        }
    }

}
