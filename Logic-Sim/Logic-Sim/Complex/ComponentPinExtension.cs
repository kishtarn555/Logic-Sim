using Logic_Sim.Engine;

namespace Logic_Sim.Complex {
    public static class ComponentPinExtension {
        public static Pin Pin(this Component component, int pin) {
            return new Pin(component, pin);
        }
    }
}
