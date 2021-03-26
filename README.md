# Logic-Sim
I wanted to do a PC with PCB,MCU and stuff, but I got no money.
So I tried to do one in Minecraft redstone, but it was too slow.
Theeeeen I tried to make one with logic simulators, but they were too limited or expensive (Mainly bc they do graphics and stuff)
So, I created my own logic simulator.
If you wanna use it. Have fun.

# How to use

Right now it's not made to "compile" a circuit easely, but if you want to create your own. You got to create a text file with the circuit and change that path in the Program.cs (I should change it to an input argument... maybe later).

# Syntax

This is a compiler that only uses words separated by one or more blank spaces, meaning: it ignores line-breaks, identation, etc. (This also means that finding Compilation errors is a pain, should change later).

It works in a command base manner. Each command is a key-word followed by arguments. Each argumment is separated by blanks (space, tab, break-line,etc).

# Concepts
  * **Component**: The basic unit of a circuit, most of them are logic-gates, but basically they have N inputs and M outputs. They also work as a connection between a circuit and IO. think of them as integrated circuits.
  * **Connection**: A connection links a component output with a component input. If multiple connections are connected to the same input, then the input is in High state if at least one connection connects to an output in high state.
  * **Update**: When an output changes from one state to another, it notifies the change to all inputs connected to that output. this is done with an update. The update always happens in the next tick or even later.
  * **Tick**: A tick represents a unit of simulated time. All updates have a tick. The circuit is simulated tick by tick from 0 to tick 2^64-1 (If a tick were to be created after this, then undefined behaivour will happen, due to integer overflow). NOTE: the state of inputs only changes at the end of a tick, after all updates where processed. Ticks are processed in a single thread as fast as possible.
  * **Circuit**. Its the set of all components and connections.
  
  
# Commands

**ADD** -  This command adds a primitive component to the system. The estructure is as follows:

ADD &lt;type&gt; &lt;name&gt; [arguments] 
  
The type indicates what kind of component you want to add to the circuit, think of it as an integrated circuit.

The name will be used to connect things to and from it. Arguments may be needed depending on the component.

EX

    ADD AND andGate
    ADD BUFFER buffer 1

**CON** - This command creates a connection between an output and an input.

CON &lt;outputComponent&gt; &lt;outPin&gt; &lt;inputComponent&gt; &lt;inputPin&gt;

EX

    CON a 0 andGate 0
    CON b 0 andGate 1

**IMP** This allows to add a circuit defined in another file to the current circuit (this is similar to instancing objects).

IMP &lt;prefixName&gt; &lt;filePath&gt; [arguments] 

Note, when refering to a component from an imported circuit you must do it with the prefixName specified when it was imported followed by a underscore '\_' and the name it was created in the imported file.

EX

Program to make C = A and B

FILE: util.lg

    ADD AND UtilAnd
    
File: Program.lg

    ADD BUFFER A 1
    ADD BUFFER B 1
    ADD BUFFER C 1
    IMP prefix ./Util.lg
    CON A 0 prefix_UtilAnd 0
    CON B 0 prefix_UtilAnd 1
    CON prefix_UtilAnd 0 C 0

**ALI** - Creates an alias to an already named component, basically, a component may now be called by the original name or the alias one.

ALI &lt;alias&gt; &lt;originalName&gt;
  
 Ex
 
 Program to make C = A and B
 
    ADD BUFFER A 1
    ADD BUFFER B 1
    ADD BUFFER C 1
    ADD AND and 
    ALI alias and
    ALI alias2 alias
    
    CON A 0 and 0
    CON B 0 alias 1
    CON alias2 0 C 0
    
# Notes
If you have further questions,  there are examples in the file called examples.

The one in the subfolder called PC is a full project with multiple files and complex interactions. It emulates a computer.
 
 It only works on windows due to IO, you may wanna change IO
  



