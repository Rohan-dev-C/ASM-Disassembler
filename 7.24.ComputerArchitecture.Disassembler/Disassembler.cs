using System;
using System.IO;
using System.Collections.Generic;
#nullable enable
namespace _7._24.ComputerArchitecture.Disassembler
{
    class Disassembler
    {
        public static Dictionary<byte, string> OpCodes = new Dictionary<byte, string>()
        {
            [0x40] = "SET", 
            [0x10] = "ADD",
            [0x11] = "SUB",
            [0x12] = "MUL",
            [0x13] = "DIV",
            [0x14] = "MOD",
            [0x30] = "JMP",
            [0x32] = "JMPT",
            [0x20] = "NOT",
            [0x21] = "AND",
            [0x22] = "OR",
            [0x23] = "XOR",
            [0x24] = "EQ",
            [0x28] = "GT",
        };
        public static Dictionary<byte, string> Registers = new Dictionary<byte, string>()
        {
            [0x00] = "R0",
            [0x01] = "R1",
            [0x02] = "R2",
            [0x03] = "R3",
            [0x04] = "R4",
            [0x05] = "R5",
            [0x06] = "R6",
            [0x07] = "R7",
            [0x08] = "R8",
            [0x09] = "R9",
            [0x10] = "R10",
            [0x11] = "R11",
            [0x12] = "R12",
            [0x13] = "R13",
            [0x14] = "R14",
            [0x15] = "R15",
        };
        public static Dictionary<int, string> Labels = new Dictionary<int, string>()
        { };

        static void Main(string[] args)
       {
            var Fbytes = File.ReadAllBytes(args[0]);
            byte[] bytes = new byte[Fbytes.Length+1];
            for (int i = 0; i < Fbytes.Length; i++)
            {
                bytes[i] = Fbytes[i]; 
            }
            List<string[]> DisassembledBytes = new List<string[]>();
            int lineCounter = 0;

            byte[] temp = new byte[4];
            for (int i = 0; i < bytes.Length; i++)
            {
                if(lineCounter % 4 == 0 && lineCounter != 0)
                {
                    DisassembledBytes.Add(ParseBinary(temp));
                    temp = new byte[4];
                    lineCounter = 0;
                }
                temp[lineCounter] = bytes[i];
                lineCounter++; 

            }

            for (int i = 0; i < DisassembledBytes.Count; i++)
            {
                for (int j = 0; j < DisassembledBytes[i].Length; j++)
                {
                    Console.Write(DisassembledBytes[i][j]);
                }
                Console.WriteLine(); 
            }
            ;

            static string[] ParseBinary(byte[] bytes)
            {
                string[] value = new string[4];



                value[0] = OpCodes[bytes[0]];
                switch (value[0])
                {
                    case "ADD":
                    case "SUB":
                    case "MUL":
                    case "DIV":
                    case "OR":
                    case "AND":
                    case "XOR":
                    case "NOT":
                    case "MOD":
                        value[1] = Registers[bytes[1]];
                        value[2] = Registers[bytes[2]]; 
                        value[3] = Registers[bytes[3]];
                        break;
                    case "SET":
                        value[1] = Registers[bytes[1]];
                        if (bytes[2].ToString().Length == 1)
                        {
                            value[2] = "0" + bytes[2].ToString();
                        }
                        if (bytes[3].ToString().Length == 1)
                        {
                            value[3] = "0" + bytes[3].ToString();
                        }
                            break;
                    case "JMP":

                     
                        if (bytes[1].ToString().Length == 1)
                        {
                            value[1] = "0" + bytes[1].ToString();
                        }
                        if (bytes[2].ToString().Length == 1)
                        {
                            value[2] = "0" + bytes[2].ToString();
                        }
                        value[3] = "FF";
                        break; 
                    default:
                        throw new Exception("not real binary");
                        break; 
                }
                return value; 
            }

            
        }
    }
}
