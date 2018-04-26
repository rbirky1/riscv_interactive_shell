using System;
using System.Collections.Generic;

namespace InteractiveShell
{
    class Register
    {
        public string Name { get; }
        public string Mneumonic { get; }
        public int Value { get; set; }

        public Register(string Name, string Mneumonic, int Value) {
            this.Name = Name;
            this.Mneumonic = Mneumonic;
            this.Value = Value;
        }
    }

    class Program
    {
        static Register[] registers = new Register[] {
            new Register("x0", "zero", 0),
            new Register("x1", "ra", 0),
            new Register("x2", "sp", 0),
            new Register("x3", "gp", 0),
            new Register("x4", "tp", 0),
            new Register("x5", "t0", 0),
            new Register("x6", "t1", 0),
            new Register("x7", "t2", 0),
            new Register("x8", "s0", 0),
            new Register("x9", "s1", 0),
            new Register("x10", "a0", 0),
            new Register("x11", "a1", 0),
            new Register("x12", "a2", 0),
            new Register("x13", "a3", 0),
            new Register("x14", "a4", 0),
            new Register("x15", "a5", 0),
            new Register("x16", "a6", 0),
            new Register("x17", "a7", 0),
            new Register("x18", "s2", 0),
            new Register("x19", "s3", 0),
            new Register("x20", "s4", 0),
            new Register("x21", "s5", 0),
            new Register("x22", "s6", 0),
            new Register("x23", "s7", 0),
            new Register("x24", "s8", 0),
            new Register("x25", "s9", 0),
            new Register("x26", "s10", 0),
            new Register("x27", "s11", 0),
            new Register("x28", "t3", 0),
            new Register("x29", "t4", 0),
            new Register("x30", "t5", 0),
            new Register("x31", "t6", 0)
        };

        static void addi(string input) {
            Register rd = null, rs1 = null;
            string rd_str, rs1_str;
            int i = 0;
            int imm = 0;

            string[] instruction = input.Split(",");
            if (instruction.Length != 3) {
                Console.WriteLine("Invalid number of arguments: "
                                  + instruction.Length + " given, 3 expected");
                return;
            }

            rd_str = instruction[0].Replace(",","").Trim();
            rs1_str = instruction[1].Replace(",", "").Trim();
            try {
                imm = Int32.Parse(instruction[2]);
            } catch (FormatException e) {
                Console.WriteLine(e.Message);
            }

            // Find registers
            while ( i < registers.Length && (rd == null || rs1 == null)) {
                if (rd_str.Equals(registers[i].Name) || 
                    rd_str.Equals(registers[i].Mneumonic)) {
                    rd = registers[i];
                }
                if (rs1_str.Equals(registers[i].Name) || 
                    rs1_str.Equals(registers[i].Mneumonic))
                {
                    rs1 = registers[i];
                }
                i++;
            }

            if (rd == null)
            {
                Console.WriteLine("Invalid rd register: " + rd_str);
                return;
            }

            if (rs1 == null)
            {
                Console.WriteLine("Invalid rs1 register: " + rs1_str);
                return;
            }

            rd.Value = rs1.Value + imm;
            Console.WriteLine("Register ["+rd.Name+"/"+rd.Mneumonic
                              +"] updated: "+rd.Value);
        }

        static void printRegisters() {
            foreach (var reg in registers) {
                Console.WriteLine("\t"+reg.Name+"\t"+reg.Mneumonic+"\t"+reg.Value);
            }
        }

        static void printHelp() {
            Console.WriteLine("Currently supported instruction:");
            Console.WriteLine("\taddi");
            Console.WriteLine("Example: addi s0, s1, 10");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Type a RISCV instruction to begin.");
            Console.WriteLine("Type `quit` or <Enter> to exit"
                            + "Type `show` to view registers");
            Console.Write("> ");

            while (true) {
                string input = Console.ReadLine();
                if (input.Length != 0) {
                    int split = input.IndexOf(' ');
                    if (split <= 0) split = input.Length;
                    switch (input.Substring(0, split))
                    {
                        case "quit":
                            return;
                        case "help":
                            printHelp();
                            break;
                        case "show":
                            printRegisters();
                            break;
                        case "addi":
                            addi(input.Substring(split + 1));
                            break;
                        default:
                            Console.WriteLine("Unrecognized/Unsupported Instruction!");
                            break;

                    }
                }
                Console.Write("> ");
            }

        }
    }
}
