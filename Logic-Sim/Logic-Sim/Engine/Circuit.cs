﻿    using System;
using System.Collections.Generic;
using System.Text;
using Logic_Sim.Utils;
namespace Logic_Sim.Engine
{
    class Circuit {
        Priority_queue<Update> updates;
        List<Component> components;//Serves no purpourse, but maybe later
        long curTick = 0;
        public long Tick {
            get { return curTick; }
        }
        public Circuit() {
            updates = new Priority_queue<Update>();
            curTick = 0;
            components = new List<Component>();
        }

        public void RegisterComponent(Component component) {
            Update n = new Update();
            n.target = component.updatePin;
            n.message = "Created " + component.name;
            n.tick = curTick + 1;
            AddUpdate(n);
            components.Add(component);
        }
        public void DumpData() {
            if ((EngineState.DEBUG&EngineState.DFLAGS.UNSAFE_DUMPING)==0) {
                throw new InvalidOperationException("Asked circuit to dump data, but engine is not debugging");
            }
            Console.WriteLine("Dumping Data---------------");
            while (updates.Size>0) {
                Console.WriteLine("Update ["+updates.Top.tick+"]"+updates.Top.message);
                updates.Pop();
            }
            foreach (Component comps in components) {
                Console.WriteLine("Component " + comps.ToString());
            }

        }

        public void DoTick() {
            if ((EngineState.DEBUG & EngineState.DFLAGS.PRINT_TICK) != 0) Console.WriteLine("Ticking " + curTick+"....");  
            Update currentUpdate;
            HashSet<Component> toUpdate = new HashSet<Component>();
            while (updates.Any() && updates.Top.tick<=curTick) {
                currentUpdate = updates.Pop();
                //if ((EngineState.DEBUG & EngineState.DFLAGS.PRINT_UPDATE) != 0) Console.WriteLine("REG " + curTick + " " + currentUpdate.message); 
                currentUpdate.target.ProcessUpdate(currentUpdate);
               
                toUpdate.Add(currentUpdate.target.Container);
                //currentUpdate.target.Value = currentUpdate.target.Value+ currentUpdate.delta;
                
            }
            foreach (Component comp in toUpdate) {
                comp.RenderInputs();

               // if ((EngineState.DEBUG & EngineState.DFLAGS.PRINT_UPDATE) != 0) Console.WriteLine("RENDER" + curTick + " " + comp.ToString());
                comp.DoUpdate();
                if ((EngineState.DEBUG & EngineState.DFLAGS.PRINT_UPDATE) != 0) Console.WriteLine("UPD_" + curTick + " " + comp.ToString());
            }
            foreach (Component comp in toUpdate) {
                comp.Propagate(this);
            }
            curTick++;

        }
        public void Connect(Component src, int pinsrc, Component des, int pindes) {
            src.AddConnection(des, pinsrc, pindes);
            Update connection_update;
            if (src.GetOutput(pinsrc)==true) {
                connection_update.delta = 1;
            }else {
                connection_update.delta = 0;
            }
            connection_update.message = "Connection createrd";
            connection_update.tick = curTick + 1;
            connection_update.target = des.GetPort(pindes);
            AddUpdate(connection_update);
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
