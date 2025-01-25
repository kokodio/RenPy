using System.Collections;
using System.Diagnostics;
using Python.Syntax;

namespace Python;

internal sealed class DiagosticBag : IEnumerable<Diagnostic>
{
    public IEnumerator<Diagnostic> GetEnumerator() => diagnostics.GetEnumerator();
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    private readonly List<Diagnostic> diagnostics = [];
    
    private void Report(TextSpan span, string message)
    {
        var diagnostic = new Diagnostic(span, message);
        diagnostics.Add(diagnostic);
    }
    
    public void AddRange(DiagosticBag diagostic)
    {
        diagnostics.AddRange(diagostic);
    }

    public void ReportInvalidNumber(TextSpan span, string text, Type type)
    {
        var massage = $"The number '{text}' isn't valid {type}";
        Report(span, massage);
    }

    public void ReportBadCharacter(int position, char current)
    {
        var massage = $"Bad character input: '{current}'";
        var span = new TextSpan(position, 1);
        Report(span, massage);
    }

    public void ReportUnexpectedToken(TextSpan span, SyntaxKind actualKind, SyntaxKind expectedKind)
    {
        var massage = $"Unexpected token '{actualKind}'. expected '{expectedKind}'";
        Report(span, massage);
    }

    public void ReportUndefinedUnaryOperator(TextSpan span, string operatorText, Type operandType)
    {
        var massage = $"Unary operator '{operatorText}' is not defined for type {operandType}";
        Report(span, massage);
    }
    
    public void ReportUndefinedBinaryOperator(TextSpan span, string operatorText, Type leftType, Type rightType)
    {
        var massage = $"Binary operator '{operatorText}' is not defined for type {leftType} and {rightType}";
        Report(span, massage);
    }

    public void ReportUndefinedName(TextSpan span, string name)
    {
        var massage = $"Variable '{name}' doesn't exist";
        Report(span, massage);
    }
}