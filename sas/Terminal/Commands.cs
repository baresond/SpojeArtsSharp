using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using sas.Math;

namespace sas.Terminal
{
    public static class Commands
    {
        private static bool isUnixTree = false;
        public static void Execute(string line)
        {
            var _command = String.Concat(line.Split(' ')[0].ToLower().Where(c => !Char.IsWhiteSpace(c)));
            var _value = line.Split(' ').Length > 1 ?  line.Substring(line.IndexOf(' ') + 1) : string.Empty;

            switch (_command)
            {
                #region command 'Math'
                case "math":
                    var syntaxTree = SyntaxTree.Parse(_value);

                    if (isUnixTree)
                        Terminal.PrettyPrint(syntaxTree.Root);

                    if (!syntaxTree.Diagnostics.Any())
                    {
                        var evaluate = new Evaluator(syntaxTree.Root);
                        var evaluateResult = evaluate.Evaluate();
                        Terminal.WriteOutput("The result of 'Math' operation is: " + evaluateResult.ToString());
                    }
                    else
                    {
                        string output = string.Empty;

                        foreach (var item in syntaxTree.Diagnostics)
                        {
                            output += item.ToString() + Environment.NewLine;
                        }

                        Terminal.WriteOutput(output, true);
                    }
                    //WriteOutput($"It works{new string(' ', sizeOfTab)}'{_value}'");
                    break;
                #endregion
                #region command 'ShowUnixTree'
                case "showunixtree":
                case "showunixt":
                case "showutree":
                case "sunixtree":
                case "sutree":
                case "sunixt":
                case "sut":
                    isUnixTree = !isUnixTree;
                    Terminal.WriteOutput(isUnixTree ? "Showing parse trees." : "Not showing parse trees.");
                    break;
                #endregion
                #region command 'Clear'
                case "cls":
                case "clear":
                    Terminal.Clear();
                    break;
                #endregion
                #region command 'Exit'
                case "end":
                case "exit":
                    Environment.Exit(0);
                    break;
                #endregion
                #region command 'Help'
                case "get-help":
                case "help":
                case "h":
                case "?":
                    if (int.TryParse(_value, out int result))
                        Terminal.WriteHelp(result);
                    else if (!string.IsNullOrWhiteSpace(_value))
                        Terminal.WriteHelp(_value);
                    else Terminal.WriteHelp();
                    break;
                #endregion
                #region bad command
                default:
                    Terminal.WriteOutput($"Command not found!{Environment.NewLine}{Environment.NewLine}Try command help {{page}} - 1 -> 100", true);
                    break;
                #endregion
            }
        }
    }
}
