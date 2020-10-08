using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Logic_Sim.Compiler
{
    class Tokenizer {
        string[] tokens;
        int current = 0;
        public Tokenizer(string code) {
            tokens = Regex.Split(code, @"\s+");
        }

        public int NextInt() {
            if (current >= tokens.Length) {
                throw new Exception("While compiling, expecting number reached EOF");
            }
            int number;
            if (int.TryParse(tokens[current], out number)) {
                current++;
                return number;
            }
            throw new Exception("Expecting an integer, found: " + tokens[current]);
        }
        public string NextIdentifier() {
            if (current >= tokens.Length) {
                throw new Exception("While compiling, expecting identifier reached EOF");
            }
            if (!Regex.IsMatch(tokens[current], "^[a-zA-Z][a-zA-Z0-9]*")) throw new Exception("While compiling, expecting identifier, found " + tokens[current]);

            return tokens[current++];
        }
        public string NextString() {
            if (current >= tokens.Length) {
                throw new Exception("While compiling, expecting identifier reached EOF");
            }
            return tokens[current++];
        }
        public bool SkipUntil(string subject, bool mustFind=true) {
            if (!HasTokens()) {
                if (mustFind) throw new Exception("While compiling, expecting \"" + subject + "\", reached EOF");
                return false;
            }
            string s = NextString();
            while (s != subject) {

                if (!HasTokens()) {
                    if (mustFind) throw new Exception("While compiling, expecting \"" + subject + "\", reached EOF");
                    return false;
                }
                s = NextString();
            }
            return true;

        }
        public  bool HasTokens() {
            return current < tokens.Length;
        }
    }
}
