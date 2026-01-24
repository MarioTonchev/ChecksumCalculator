using ChecksumCalculator.Builder;
using ChecksumCalculator.Factory;
using ChecksumCalculator.Models;
using ChecksumCalculator.Observer;
using ChecksumCalculator.PauseResume;
using ChecksumCalculator.Verification;
using ChecksumCalculator.Visitor;
using System.CommandLine;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var pathOption = new Option<string>("--path", description: "Target file or directory")
        {
            Arity = ArgumentArity.ZeroOrOne
        };

        pathOption.SetDefaultValue(Directory.GetCurrentDirectory());

        var algorithmOption = new Option<string>(
            "--algorithm",
            () => "sha256",
            "Hash algorithm (currently only sha256 is implemented)");

        var formatOption = new Option<string>(
            "--format",
            () => "text",
            "Output format (text, json)");

        var checksumsOption = new Option<string?>(
            "--checksums",
            "Checksums file (verification mode)");

        var noFollowOption = new Option<bool>(
            "--nofollow-links",
            "Do not follow symbolic links");

        var rootCommand = new RootCommand("Checksum Calculator");

        rootCommand.AddOption(pathOption);
        rootCommand.AddOption(algorithmOption);
        rootCommand.AddOption(formatOption);
        rootCommand.AddOption(checksumsOption);
        rootCommand.AddOption(noFollowOption);

        rootCommand.SetHandler((string path, string algorithm, string format, string? checksums, bool noFollow) =>
            {
                RunApplication(path, algorithm, format, checksums, noFollow);
            },
            pathOption, algorithmOption, formatOption, checksumsOption, noFollowOption
        );

        return await rootCommand.InvokeAsync(args);
    }

    static void RunApplication(string path, string algorithm, string format, string? checksumsFile, bool noFollowLinks)
    {
        try
        {
            FileSystemBuilder builder = noFollowLinks ? new NoFollowSymlinkBuilder() : new FollowSymlinkBuilder();

            var root = builder.Build(path);

            var pauseController = new PauseController();
            var calculator = ChecksumCalculatorFactory.Create(algorithm, pauseController);
            var results = new List<ChecksumResult>();

            var hasher = new HashStreamWriter(calculator, results, pauseController);

            var progressReporter = new ProgressReporter(root.Size, pauseController);
            hasher.RegisterObserver(progressReporter);

            var worker = new Thread(() =>
            {
                root.Accept(hasher);

                Console.WriteLine("\nScan finished.");

                if (checksumsFile != null)
                {
                    var givenChecksum = ChecksumFileParser.Parse(checksumsFile);

                    var verificationResults = Verifier.Verify(results, givenChecksum);

                    foreach (var verificationResult in verificationResults)
                    {
                        Console.WriteLine($"{verificationResult.Path}: {verificationResult.Status}");
                    }

                    return;
                }

                var writer = ReportWriterFactory.Create(format);
                writer.Write(results, Console.Out);
            });

            worker.Start();

            while (true)
            {
                var command = Console.ReadLine();

                if (command == "pause")
                {
                    pauseController.Pause();
                }
                else if (command == "resume")
                {
                    pauseController.Resume();
                }
                else if (command == "exit")
                {
                    break;
                }
            }
        }
        catch (ArgumentException ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            Environment.Exit(1);
        }
        catch (FileNotFoundException ex)
        {
            Console.Error.WriteLine($"File not found: {ex.Message}");
            Environment.Exit(2);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Unexpected error occurred.");
            Console.Error.WriteLine(ex.Message);
            Environment.Exit(99);
        }
    }
}
