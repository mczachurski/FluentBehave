using FluentBehave.Tools.Model;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace FluentBehave.Tools
{
    public class DispatchCommand
    {
        public static CommandLineApplication Create()
        {
            var app = new CommandLineApplication(throwOnUnexpectedArg: false)
            {
                Name = "dotnet feature",
                FullName = "FluentBehave Tools for .NET CLI Commands"
            };

            var featureFileOption = app.Option(
                "-f|--feature <FEATURE_FILE>",
                "Feature file which be translated", CommandOptionType.SingleValue);

            var outputOption = app.Option(
                "-o|--output <OUTPUT_DIR>",
                "Directory in which to find outputs", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                string varsion = PlatformServices.Default.Application.ApplicationVersion;
                Reporter.Output.WriteLine($"FluentBehave Tools for .NET CLI Commands ({varsion})".Yellow().Bold());

                if (!featureFileOption.HasValue())
                {
                    throw new OperationException($"Feature file wasn't specify.");
                }

                string featureFilePath = GetFeatureFile(featureFileOption);
                if (!File.Exists(featureFilePath))
                {
                    throw new OperationException($"Feature file not found.");
                }

                string outputDir = GetOutputDir(outputOption);
                if (!Directory.Exists(outputDir))
                {
                    throw new OperationException($"Output directory not found.");
                }

                Reporter.Output.WriteLine($"Feature: {featureFilePath}");
                Reporter.Output.WriteLine($"Output: {outputDir}");

                FeatureParser parser = new FeatureParser();
                var featureText = File.ReadAllText(featureFilePath);
                Feature feature = parser.Parse(featureText);



                return 0;
            });

            return app;
        }

        private static string GetFeatureFile(CommandOption featureFileOption)
        {
            string featureFilePath = featureFileOption.Value();
            if (!Path.IsPathRooted(featureFilePath))
            {
                featureFilePath = Path.Combine(Directory.GetCurrentDirectory(), featureFilePath);
            }

            return featureFilePath;
        }

        private static string GetOutputDir(CommandOption outputOption)
        {
            string outputDir = Directory.GetCurrentDirectory();
            if (outputOption.HasValue())
            {
                var outputOptionDir = outputOption.Value();
                if (Path.IsPathRooted(outputOptionDir))
                {
                    outputDir = outputOptionDir;
                }
                else
                {
                    outputDir = Path.Combine(outputDir, outputOptionDir);
                }
            }

            return outputDir;
        }
    }
}
