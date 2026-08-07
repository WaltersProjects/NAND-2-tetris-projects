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
/** Output **/
foreach (string line in parsedFile)
    Console.WriteLine(line);