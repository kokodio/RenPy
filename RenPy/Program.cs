using Python;
using Python.Binding;
using Python.Syntax;

namespace RenPy;

public static class Program
{
    public static void Main()
    {
        var showTree = true;
        var variables = new Dictionary<VariableSymbol, object>();
        
        while (true)
        {
            Console.Write("> ");
            var line = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(line))
            {
                return;
            }

            if (line == "#showTree")
            {
                showTree = !showTree;
                Console.WriteLine(showTree ? "Show Tree" : "Hide Tree");
                continue;
            }
            
            var syntaxTree = SyntaxTree.Parse(line);
            var compilation = new Compilation(syntaxTree);
            var result = compilation.Evaluate(variables);
            
            if (showTree)
            {
                PrettyPrint(syntaxTree.Root);
            }
            
            if (result.Diagnostics.Count == 0)
            {
                Console.WriteLine(result.Value);
            }
            else
            {
                foreach (var diagnostic in result.Diagnostics)
                {
                    Console.Out.WriteLine();
                    Console.WriteLine(diagnostic);
                    var span = line.AsSpan();
                    
                    var prefix = span.Slice(0, diagnostic.Span.Start);
                    var error = span.Slice(diagnostic.Span.Start, diagnostic.Span.Length);
                    var suffix = span.Slice(diagnostic.Span.End);
                    
                    Console.Write("    ");
                    Console.Out.Write(prefix);
                    
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Out.Write(error);
                    Console.ResetColor();
                    
                    Console.Out.Write(suffix);
                    Console.Out.WriteLine();
                }
            }
        }
    }

    private static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
    {
        var marker = isLast ? "└──" : "├──";
        
        Console.Write(indent);
        Console.Write(marker);
        Console.Write(node.Kind);
        
        if (node is SyntaxToken t)
        {
            Console.Write(" ");
            Console.Write(t.Value);
        }
        Console.WriteLine();
            
        indent += isLast ? "   " : "│  ";
        var lastChild = node.GetChildren().LastOrDefault();
        
        foreach (var child in node.GetChildren())            
            PrettyPrint(child, indent, child == lastChild);
    }
}