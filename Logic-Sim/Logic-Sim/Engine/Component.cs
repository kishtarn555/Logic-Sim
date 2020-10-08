﻿using System;
using System.Collections.Generic;
using System.Text;
using Logic_Sim.Utils;
namespace Logic_Sim.Engine
{
    abstract class Component {
        protected int delay=1;
        string name;
        Port[] inputs;
        bool[] outputstate;
        protected bool[] nextstate;
        protected bool[] renderedInputs;
        List<Port>[] outputs;

        public Component(int inputSize, int outputSize, string _name = "[DEFAULT]") {
            name = _name;
            inputs = new Port[inputSize];
            for (int i =0; i< inputSize; i++) {
                inputs[i] = new Port(this);
            }
            renderedInputs = new bool[inputSize];
            outputstate = new bool[outputSize];
            nextstate = new bool[outputSize];
            outputs = new List<Port>[outputSize];
            for (int i =0; i< outputSize; i++) {
                outputs[i] = new List<Port>();
            }
        }

        public abstract void DoUpdate();
         Port GetPort(int pin) {
            if (pin < 0 ||pin >= inputs.Length) {
                throw new IndexOutOfRangeException("Asked for pin out of bounds, asked for "+pin+ "from " +this.ToString()+", that has "+inputs.Length.ToString()+" pins");
            }
            return inputs[pin];
        }
        public void RenderInputs() {
            for (int i =0; i < inputs.Length; i++) {
                if (inputs[i].Value == 0) renderedInputs[i] = false;
                else renderedInputs[i] = true;
            }
        }
        public void AddConnection(Component destination, int srcPin, int desPin) {
            outputs[srcPin].Add(destination.GetPort(desPin));
            destination.inputs[desPin].AddComponent(destination, srcPin);
        }
        
        public virtual void Propagate (Circuit circuit){
            for (int i=0; i < outputstate.Length; i++) {
                if (outputstate[i] == nextstate[i]) continue;
                foreach (Port port in outputs[i]) {
                    Update tmp=new Update();
                    tmp.tick = circuit.Tick+delay;
                    tmp.message = "From " + this.ToString() + " at port " + i;
                    tmp.target = port;
                    tmp.delta = nextstate[i] ? 1 : -1;
                    circuit.AddUpdate(tmp);
                }
            }
        }
        public override string ToString() {
            return base.ToString()+" "+name;
        }

        public class Port
        {
            //Component, pin
            List<Pair<Component, int>> components;
            public Component Container { get; private set; }
            int value;
            public int Value { get { return value; } }
            public Port(Component container) {
                components = new List<Pair<Component, int>>();
                value = 0;
                Container = container;
            }
            public void AddComponent(Component comp, int pin) {
                components.Add(new Pair<Component, int>(comp, pin));
            }
            public void ProcessUpdate(Update update) {
                if (update.target!=this) {
                    throw new Exception("Wrong port updated, something went wrong!");
                }
                value += update.delta;

            }
        }
        
    }
}