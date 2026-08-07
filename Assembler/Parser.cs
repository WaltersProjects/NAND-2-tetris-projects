class Parser
{
    public string FilePath { get; set; }
    
    public Parser()
    {
        FilePath = "";
    }

    public List<string> ParseFile()
    {
        Dictionary<string, string> valueSubstitutions = [];
        for ( int i = 0; i < 16; i++)
        {
            valueSubstitutions.Add("R"+i.ToString(), i.ToString()); // add R0-R15 substitutions
        }
        // defining predefined constants
        valueSubstitutions.Add("SP","0");
        valueSubstitutions.Add("LCL","1");
        valueSubstitutions.Add("ARG","2");
        valueSubstitutions.Add("THIS","3");
        valueSubstitutions.Add("THAT","4");
        // I/O pointers
        valueSubstitutions.Add("SCREEN","16384");
        valueSubstitutions.Add("KBD","24576");
        string[] oldCodeLines = File.ReadAllLines(FilePath);
        List<string> codeLines = [];
        /** Gets rid of whitepsace and comments and substitutes builtins **/
        // whitespace + comment removal
        for ( int i = 0; i < oldCodeLines.Length; i++ )
        {
            oldCodeLines[i] = oldCodeLines[i].Split("/")[0];
            oldCodeLines[i] = oldCodeLines[i].Trim();
        }
        foreach (string line in oldCodeLines)
        {
            if (line != "")
                codeLines.Add(line);
        }
        // substitution (builtins)
        for ( int i = 0; i < codeLines.Count; i++ )
        {
            foreach( KeyValuePair<string, string> kvp in valueSubstitutions )
            {
                codeLines[i] = codeLines[i].Replace(kvp.Key, kvp.Value);
            }
        }

        // now we loop through all of the lines and look for labels
        int memVarCount = 16;
        Dictionary<string, string> jumpSubstitutions = [];
        // look for jumps
        for ( int i = 0; i < codeLines.Count; i++ )
        {
            try
            {
                if(codeLines[i].StartsWith("("))
                {
                    // we have a jump label
                    string jmpLbl = codeLines[i].Split("(")[1][..^1];
                    jumpSubstitutions.Add(jmpLbl, i.ToString());
                    codeLines.RemoveAt(i);
                }
            }
            catch(IndexOutOfRangeException)
            {
                
            }
        }
        for ( int i = 0; i < codeLines.Count; i++ )
        {
            try
            {
                // a-instruction; possibility of having a label
                if (codeLines[i][0].ToString() == "@")
                {
                    string curLine = codeLines[i].Split("@")[1];
                    try
                    {
                        // we have an integer; we can keep the line as it is
                        int parsedLine = int.Parse(curLine);
                    }
                    catch(FormatException)
                    {
                        // variable we need to deal with
                        // if the variable is a jump variable, we replace it with its dictionary value
                        // otherwise, we replace it with a memory address
                        if( jumpSubstitutions.ContainsKey(curLine) )
                        {
                            codeLines[i] = codeLines[i].Replace(curLine, jumpSubstitutions[curLine]);
                        }
                        else if ( !valueSubstitutions.ContainsKey(curLine) )
                        {
                            valueSubstitutions.Add(curLine, memVarCount.ToString());
                            memVarCount+=1;
                        }
                        else
                        {
                            codeLines[i] = codeLines[i].Replace(curLine, valueSubstitutions[curLine]);
                        }
                    }
                }
            }
            catch(IndexOutOfRangeException)
            {
                
            }
        }
        // substitution
        for ( int i = 0; i < codeLines.Count; i++ )
        {
            foreach( KeyValuePair<string, string> kvp in valueSubstitutions )
            {
                codeLines[i] = codeLines[i].Replace(kvp.Key, kvp.Value);
            }
        }
        return codeLines;
    }
}