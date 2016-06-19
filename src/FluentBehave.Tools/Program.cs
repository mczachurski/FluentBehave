using Microsoft.DotNet.Cli.Utils;
using System;
using System.Reflection;

namespace FluentBehave.Tools
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                return DispatchCommand.Create().Execute(args);
            }
            catch (Exception ex)
            {
                if (ex is TargetInvocationException)
                {
                    ex = ex.InnerException;
                }

                if (!(ex is OperationException))
                {
                    Reporter.Error.WriteLine(ex.ToString());
                }

                Reporter.Error.WriteLine(ex.Message.Bold().Red());
                return 1;
            }
        }
    }
}
