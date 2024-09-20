using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Ensure the executable is found in the system's PATH
        string? pythonPath = Environment.GetEnvironmentVariable("PATH")?
            .Split(Path.PathSeparator)
            .Select(p => Path.Combine(p, "python.EXE"))
            .FirstOrDefault(File.Exists);

        if (pythonPath is null)
        {
            Console.WriteLine("Python executable not found in system PATH.");
            return;
        }

        var processStartInfo = new ProcessStartInfo
        {
            FileName = pythonPath,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        
        processStartInfo.ArgumentList.Add("main.py");
        processStartInfo.ArgumentList.Add("arg1");
        processStartInfo.ArgumentList.Add("arg2");

        processStartInfo.EnvironmentVariables["PYTHONDONTWRITEBYTECODE"] = "1";
        processStartInfo.EnvironmentVariables["PYTHONUNBUFFERED"] = "1";

        using (var process = new Process { StartInfo = processStartInfo })
        {
            process.Start();

            using (var writer = process.StandardInput)
            using (var reader = process.StandardOutput.BaseStream)
            {
                // Read output asynchronously
                var outputTask = Task.Run(async () =>
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead;
                    while ((bytesRead = await reader.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        string output = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine("Subprocess output: " + output);

                        // Check for specific text in stdout
                        if (output.Contains("input:"))
                        {
                            // Provide input to stdin
                            await writer.WriteLineAsync("12");
                            await writer.FlushAsync();
                        }
                    }
                });

                // Wait for the process to exit
                await process.WaitForExitAsync();
                await outputTask;
            }
        }
    }
}
