using System.Linq.Expressions;
using Python.Binding;
using Python.Syntax;

namespace Python;

public sealed class Compilation(SyntaxTree syntaxTree)
{
    public SyntaxTree Syntax { get; } = syntaxTree;

    public EvaluationResult Evaluate(Dictionary<VariableSymbol, object> variables)
    {
        var binder = new Binder(variables);
        var boundExpression = binder.BindExpression(Syntax.Root);
        
        var diagnostics = binder.Diagnostics.Concat(Syntax.Diagnostics).ToArray();

        if (diagnostics.Length != 0)
        {
            return new EvaluationResult(diagnostics, null);
        }
        
        var evaluator = new Evaluator(boundExpression, variables);
        var value = evaluator.Evaluate();
        
        return new EvaluationResult(diagnostics, value);
    }
}