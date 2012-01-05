using System;
using Antlr.Runtime;
using Antlr.Runtime.Tree;

namespace TeaseMe.FlashConversion
{
    public class Converter
    {
        public static void Main()
        {
            var stream = new ANTLRFileStream("flashtease-sample.txt");
            var lexer = new FlashTeaseScriptLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new FlashTeaseScriptParser(tokens);

            IAstRuleReturnScope<CommonTree> teaseReturn = parser.tease();

            if (parser.HasError)
            {
                Console.WriteLine("ERROR:" + parser.ErrorMessage + " " + parser.ErrorPosition);
            }

            CommonTree tree = teaseReturn.Tree;
        }
    }
}
