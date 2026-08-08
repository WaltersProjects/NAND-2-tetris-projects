string fileName = args[0];
bool exists = File.Exists(fileName);
if (!exists)
{
    Console.Error.WriteLine("File does not exist");
    return;
}
Parser p = new Parser();
p.FilePath = fileName;
List<string> parsedFile = p.ParseFile();
Translator t = new Translator();
t.CodeLines = parsedFile;
List<string> translatedFile = t.TranslateCode();
/** Output **/
foreach (string line in translatedFile)
    Console.WriteLine(line);