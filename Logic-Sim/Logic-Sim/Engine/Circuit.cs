using System;
using System.Collections.Generic;
using System.Text;
using Logic_Sim.Utils;
namespace Logic_Sim.Engine
{
    class Circuit {
        Priority_queue<Update> updates;
        long curTick = 0;
        public long Tick {
            get { return curTick; }
        }
        public Circuit() {
            updates = new Priority_queue<Update>();
        }
        public void DumpData() {
            if (!EngineState.DEBUG) {
                throw new InvalidOperationException("Asked circuit to dump data, but engine is not debugging");
            }
            while (updates.Size>0) {
                Console.WriteLine("["+updates.Top.tick+"]"+updates.Top.message);
                updates.Pop();
            }

        }

        public void DoTick() {
            Update currentUpdate;
            HashSet<Component> toUpdate = new HashSet<Component>();
            while (updates.Any() && updates.Top.tick==curTick) {
                currentUpdate = updates.Pop();
                currentUpdate.target.ProcessUpdate(currentUpdate);
                toUpdate.Add(currentUpdate.target.Container);
            }
            foreach (Component comp in toUpdate) {
                comp.RenderInputs();
                comp.DoUpdate();
            }
            foreach (Component comp in toUpdate) {
                comp.Propagate(this);
            }
            curTick++;

        }

        public void RunTicks(int ticks) {
            if (ticks <= 0) throw new ArgumentOutOfRangeException("Circuid queued to run for " + ticks + " ticks, but its out of range");
            for (int i = 0; i < ticks; i++) DoTick();
        }

        public void AddUpdate(Update update) {
            updates.Push(update);
        }

    }
}
