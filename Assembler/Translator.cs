class Translator
{
    public List<string> CodeLines { get; set; }
    
    public Translator()
    {
        CodeLines = [];
    }

    public List<string> TranslateCode()
    {
        for (int i = 0; i < CodeLines.Count; i++)
        {
            string lOutput = "";
            if ( CodeLines[i].StartsWith("@") )
            {
                // a-instruction
                int nb = int.Parse(CodeLines[i].Split("@")[1]);
                string bn = Convert.ToString(nb, 2).PadLeft(15,'0');
                lOutput = "0"+bn;
            }
            else
            {
                lOutput = "1110";
                string cString = "000000";
                string dString = "000";
                string jString = "000";
                // c-instruction
                string[] jumpSplit = CodeLines[i].Split(";");
                if ( jumpSplit.Length > 1 )
                {
                    // jump. manipulate lOutput[13..15]
                    switch(jumpSplit[1])
                    {
                        case "JGT":
                            jString = "001";
                            break;
                        case "JEQ":
                            jString = "010";
                            break;
                        case "JGE":
                            jString = "011";
                            break;
                        case "JLT":
                            jString = "100";
                            break;
                        case "JNE":
                            jString = "101";
                            break;
                        case "JLE":
                            jString = "110";
                            break;
                        case "JMP":
                            jString = "111";
                            break;
                        default:
                            throw new ArgumentException($"Invaled jump code at line {i}");
                    }
                }

                string[] compSplit = jumpSplit[0].Split("=");
                if ( compSplit.Length > 1 )
                {
                    // we have a computation and a destination
                    string dest = compSplit[0];

                    // destination casework. manipulating indexes [10..12]
                    switch(dest)
                    {
                        case "M":
                            dString = "001";
                            break;
                        case "D":
                            dString = "010";
                            break;
                        case "MD":
                            dString = "011";
                            break;
                        case "A":
                            dString = "100";
                            break;
                        case "AM":
                            dString = "101";
                            break;
                        case "AD":
                            dString = "110";
                            break;
                        case "AMD":
                            dString = "111";
                            break;
                        default:
                            throw new ArgumentException($"Invaled destination code at line {i}");
                    }
                }
                string comp = compSplit[compSplit.Length-1];
                // check if we need to set a to 0 or 1
                if ( comp.Contains('M') )
                {
                    lOutput = "1111";
                }

                // comp casework
                comp = comp.Replace("M","A"); // only worry about a cases
                switch(comp)
                {
                    case "0":
                        cString = "101010";
                        break;
                    case "1":
                        cString = "111111";
                        break;
                    case "-1":
                        cString = "111010";
                        break;
                    case "D":
                        cString = "001100";
                        break;
                    case "A":
                        cString = "110000";
                        break;
                    case "!D":
                        cString = "001101";
                        break;
                    case "!A":
                        cString = "110001";
                        break;
                    case "-D":
                        cString = "001111";
                        break;
                    case "-A":
                        cString = "110011";
                        break;
                    case "D+1":
                        cString = "011111";
                        break;
                    case "A+1":
                        cString = "110111";
                        break;
                    case "D-1":
                        cString = "001110";
                        break;
                    case "A-1":
                        cString = "110010";
                        break;
                    case "D+A":
                        cString = "000010";
                        break;
                    case "D-A":
                        cString = "010011";
                        break;
                    case "A-D":
                        cString = "000111";
                        break;
                    case "D&A":
                        cString = "000000";
                        break;
                    case "D|A":
                        cString = "010101";
                        break;
                    default:
                        throw new ArgumentException($"Invalid computation code at line {i} (\"{comp}\")");
                }

                lOutput = lOutput + cString + dString + jString;
            }
            CodeLines[i] = lOutput;
        }
        return CodeLines;
    }
}