using System;
using System.Collections.Generic;
using System.Text;
using Logic_Sim.Utils;
namespace Logic_Sim.Engine
{
    abstract class Component {
        protected int delay=1;
        public string name { get; private set; }
        public Port updatePin { get; private set; }
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
            updatePin = new Port(this);
        }
        public bool GetOutput(int pin) {
            if (pin < 0 || pin >= outputs.Length) {
                throw new IndexOutOfRangeException("Asked for output pin out of bounds, asked for " + pin + "from " + this.ToString() + ", that has " + outputs.Length.ToString() + " pins");
            }
            return outputstate[pin];
        }
        public abstract void DoUpdate();
         public Port GetPort(int pin) {
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
                outputstate[i] = nextstate[i];
            }
        }
        public override string ToString() {
            string s=name+" ";
            for (int i = 0; i < renderedInputs.Length; i++) s += renderedInputs[i] ? '1' : '0';
            s += " -> ";
            for (int i = 0; i < outputstate.Length; i++) s += outputstate[i] ? '1' : '0';
            return s;
        }

        public class Port
        {
            //Component, pin
            List<Pair<Component, int>> components;
            public Component Container { get; private set; }
            int value;
            public int Value { get { return value; } set { this.value = value; } }
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
