using Logic_Sim.Engine;

namespace Logic_Sim.Complex {
    public class Pin {
        public Component Component {
            get;
            protected set;
        }
        public int PinNumber {
            get;
            protected set;
        }

        public Pin(Component component, int pinNumber) {
            Component = component;
            PinNumber = pinNumber;
        }

        public static void Connect(Circuit circuit, Pin source, Pin destination) {
            circuit.Connect(source.Component, source.PinNumber, destination.Component, destination.PinNumber);
        }
        public static void Connect(Circuit circuit, Pin source, Component destComponent, int destPin) {
            circuit.Connect(source.Component, source.PinNumber, destComponent, destPin);
        }
        public static void Connect(Circuit circuit, Component srcComponent, int srcPin, Pin destination) {
            circuit.Connect(srcComponent, srcPin, destination.Component, destination.PinNumber);
        }


    }
}
