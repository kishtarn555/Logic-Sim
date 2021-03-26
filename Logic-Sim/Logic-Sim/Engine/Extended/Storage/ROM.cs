using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Logic_Sim.Engine;
namespace Logic_Sim.Engine.Extended.Storage
{
    /// <summary>
    /// Input: 
    /// Adress 0-14
    /// chip-enable 16
    /// 
    /// </summary>
    class ROM : Component
    {
        byte[] data;

        void FillData(params byte[] _data) {
            for (int i =0; i < _data.Length; i++) {
                data[i] = _data[i];
            }
        }
        public ROM(string name="[ROM]"):base(16,8,name) {
            data = new byte[(1 << 15)];
            for (int i =0;i < data.Length; i++) {
                data[i] = 0;
                //Console.WriteLine(data[i]);
            }

            FillData(
                0b11100000,
                10,
                0b01100001,
                0b11111111,
                0b11111111,
                0b11100000,
                14
            );
            //data[6] = 0b10100011;
        }
        public override void DoUpdate() {
            
            if (renderedInputs[15]) {
                
                int ind = 0;
                for (int i=14;i >= 0; i--) {
                    ind *= 2;
                    if (renderedInputs[i]) ind++;
                }
                //if (data[ind] == 0 && data[ind + 1] == 0 && data[ind + 2] == 0) {
                //    Console.WriteLine("DONE");
                //    Console.ReadLine();
                //}
                for (int i =0;i <8; i++) {
                    nextstate[i] = ((int)(data[ind])&(1<<i))!=0;
                    //Console.WriteLine("..."+ (((int)(data[ind]) & (1 << i)) != 0?1:0));
                }

                
                //Console.WriteLine("Rom adress" + ind+"."+ Convert.ToString(data[ind], 2).PadLeft(8, '0'));
                //Console.ReadLine();
            } else {
                for (int i = 0; i < 8; i++) {
                    renderedInputs[i] = false;
                }

            }
        }
    }
}
