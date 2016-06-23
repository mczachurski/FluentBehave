using FluentBehave.Tools.GherkinModel;
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
                Name = "dotnet fb",
                FullName = "FluentBehave Tools for .NET CLI Commands"
            };

            var featureFileOption = app.Option(
                "-f|--feature <FEATURE_FILE>",
                "Feature file which be translated", CommandOptionType.SingleValue);

            var outputOption = app.Option(
                "-o|--output <OUTPUT_DIR>",
                "Directory in which to find outputs", CommandOptionType.SingleValue);

            var namespaceOption = app.Option(
                "-n|--namespace <NAMESPACE>",
                "Namespace for new C# class", CommandOptionType.SingleValue);

            var classOption = app.Option(
                "-c|--class <CLASS_NMAE>",
                "Name of new C# class", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                string varsion = PlatformServices.Default.Application.ApplicationVersion;
                Reporter.Output.WriteLine($"FluentBehave Tools for .NET CLI Commands ({varsion})".Yellow().Bold());

                if (!featureFileOption.HasValue())
                {
                    throw new OperationException($"Feature file wasn't specify.");
                }

                if (!namespaceOption.HasValue())
                {
                    throw new OperationException($"Namespace wasn't specify.");
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

                Feature feature = ParseFeature(featureFilePath);
                string className = GetClassName(classOption, featureFilePath);
                string classBody = GenerateClassBody(namespaceOption, feature, className);
                SaveClass(outputDir, className, classBody);

                return 0;
            });

            return app;
        }

        private static void SaveClass(string outputDir, string className, string classBody)
        {
            string outputFilePath = Path.Combine(outputDir, $"{className}.cs");
            File.WriteAllText(outputFilePath, classBody);
        }

        private static string GenerateClassBody(CommandOption namespaceOption, Feature feature, string className)
        {
            ClassGenerator classGenerator = new ClassGenerator(feature, className, namespaceOption.Value());
            string classBody = classGenerator.Generate();
            return classBody;
        }

        private static Feature ParseFeature(string featureFilePath)
        {
            FeatureParser parser = new FeatureParser();
            var featureText = File.ReadAllText(featureFilePath);
            Feature feature = parser.Parse(featureText);
            return feature;
        }

        private static string GetClassName(CommandOption classOption, string featureFilePath)
        {
            string className = string.Empty;
            if (classOption.HasValue())
            {
                className = classOption.Value();
            }
            else
            {
                className = Path.GetFileNameWithoutExtension(featureFilePath);
            }

            return className;
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
