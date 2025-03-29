// by deich1deich modification by Vova43
public class Program {

    private static int sumOfLines   = 0;
    private static int sumOfSymbols = 0;
    private static int sumOfFiles   = 0;
    private static int tempOfLines   = 0;
    private static int tempOfSymbols = 0;
    private static string formatsString = ".java,.cs,.cpp,.c,.h,.hpp,.cxx,.cc,.txt,.xml,.json,.log,.ini,.config,.bat,.sh,.md,.sql,.html,.css,.js,.py,.rb,.go,.swift,.kt,.gradle,.properties,.asm,.s,.pas,.scala,.groovy,.lua,.pl,.dart,.cmake,.vcxproj,.sln,.vb,.m,.mm,.f,.f90,.f95,.f03,.fs,.ml,.mli,.lhs,.d";
    private static System.Text.StringBuilder logBuffer = null;
    private static string formatLog = "Lines: {0}, Symbols: {1}, Path: {2}\n";


    private static void Main() {
        logBuffer = new System.Text.StringBuilder();
        System.Console.WriteLine("Укажмте путь до папки");
        string path = System.Console.ReadLine();
        System.Console.WriteLine("Укажите форматы файлов которые хотите найти через запятую(Enter применить стандартные)");
        System.Console.WriteLine("Стандартные форматы которые выбраны для поиска: " + formatsString);
        string formatsStringInput = System.Console.ReadLine();
        string[] formats = null;
        if (!string.IsNullOrEmpty(formatsStringInput)) {
            formatsString = formatsStringInput;
        }
        formats = formatsString.Split(',');

        logBuffer.Append("Log create at: " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n");
        Get(path, formats);

        System.Console.WriteLine(string.Format("Среднее количество символов в строке: {0}\nКоличество строк: {1}\nКоличество символов: {2}", sumOfSymbols / sumOfLines, sumOfLines, sumOfSymbols));

        logBuffer.Append(string.Format("Item count: {0}, total lines: {1}, total symbols {2}\n", sumOfFiles, sumOfLines, sumOfSymbols));
        logBuffer.Append("Log end at: " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        System.IO.File.WriteAllText(System.Environment.CurrentDirectory + "\\out_log.log", logBuffer.ToString());

        System.Console.WriteLine(logBuffer.ToString());
        System.Console.ReadLine();
        //Console.WriteLine($"Среднее количество символов в строке: {sumOfSymbols / sumOfLines}\nКоличество строк: {sumOfLines}\nКоличество символов: {sumOfSymbols}");
    }

    private static void Get(string path, string[] formats) {
        foreach (var directory in System.IO.Directory.GetDirectories(path)) {
            if (System.IO.Directory.Exists(directory)) {
                Get(directory, formats);
            }
        }
        foreach (string file in System.IO.Directory.GetFiles(path)) {
            try {
                if (formats == null || formats.Length == 0 || IsFormatAllowed(file, formats)) {
                    sumOfFiles += 1;
                    string[] lines = System.IO.File.ReadAllLines(file);
                    tempOfLines = 0;
                    tempOfSymbols = 0;
                    foreach (string line in lines) {
                        sumOfLines += 1;
                        tempOfLines += 1;
                        sumOfSymbols += line.Length;
                        tempOfSymbols += line.Length;
                    }
                    logBuffer.Append(string.Format(formatLog, tempOfLines, tempOfSymbols, file));
                }
            }
            catch (System.Exception e) { logBuffer.Append("Error: " + string.Format(formatLog, tempOfLines, tempOfSymbols, file)); }
        }
    }

    private static bool IsFormatAllowed(string file, string[] formats) {
        string fileExtension = System.IO.Path.GetExtension(file).ToLower();
        foreach (string format in formats) {
            if (fileExtension == format.ToLower()) {
                return true;
            }
        }
        return false;
    }
}