using System.Text;

var arguments = Environment.GetCommandLineArgs();
if (arguments.Length < 2 || arguments.Length > 2)
{
    Console.WriteLine("This program takes exactly one argument: [filename]");
    return;
}
    

var filename = arguments[1];

Console.WriteLine(filename);

var sr = File.OpenText(filename);

string? s;
var counter = 0;
var sb = new StringBuilder();

while ((s = sr.ReadLine()) != null)
{
    if (s.Length > 4)
    {
        s = s.TrimStart(' '); // Remove whitespaces first
        s = s.TrimStart('0', '1', '2', '3', '4', '5', '6', '7', '8', '9'); // Then remove line numbers
    }

    // Substitute new line numbers
    var fill = new string(' ', 5 - Convert.ToInt32(counter.ToString().Length)); 
    var lineOutput = $"{fill}{counter++}{s}\n";
    Console.Write(lineOutput);
    sb.Append(lineOutput);
}

const string suffix = ".renumbered";

if (File.Exists(filename + suffix))
{
    File.Delete(filename + suffix);
}
sr.Close();
Console.WriteLine();
var sw = File.AppendText(filename + suffix);
sw.Write(sb);

sw.Close();