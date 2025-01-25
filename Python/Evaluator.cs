using Python.Binding;
using Python.Syntax;

namespace Python;

internal sealed class Evaluator(BoundExpression root, Dictionary<VariableSymbol, object> variables)
{
    public object Evaluate()
    {
        return EvaluateExpression(root);
    }

    private object EvaluateExpression(BoundExpression node)
    {
        if (node is BoundLiteralExpression n)
        {
            return n.Value;
        }
        
        if (node is BoundVariableExpression v)
        {
            var value = variables[v.Variable];
            return value;
        }

        if (node is BoundAssignmentExpression a)
        {
            var value = EvaluateExpression(a.Expression);
            variables[a.Variable] = value;
            return value;
        }

        if (node is BoundUnaryExpression u)
        {
            var operand = EvaluateExpression(u.Operand);

            return u.Op.Kind switch
            {
                BoundUnaryOperatorKind.Identity => (int)operand,
                BoundUnaryOperatorKind.Negation => -(int)operand,
                BoundUnaryOperatorKind.LogicNegation => !(bool)operand,
                _ => throw new Exception($"Unexpected unary operator {u.Op}")
            };
        }

        if (node is BoundBinaryExpression b)
        {
            var left = EvaluateExpression(b.Left);
            var right = EvaluateExpression(b.Right);

            return b.Op.Kind switch
            {
                BoundBinaryOperatorKind.Addition => (int)left + (int)right,
                BoundBinaryOperatorKind.Subtraction => (int)left - (int)right,
                BoundBinaryOperatorKind.Multiplication => (int)left * (int)right,
                BoundBinaryOperatorKind.Division => (int)left / (int)right,
                BoundBinaryOperatorKind.LogicalAnd => (bool)left && (bool)right,
                BoundBinaryOperatorKind.LogicalOr => (bool)left || (bool)right,
                BoundBinaryOperatorKind.Equals => Equals(left, right),
                BoundBinaryOperatorKind.NotEquals => !Equals(left, right),
                _ => throw new Exception($"Unexpected binary operator {b.Op}")
            };
        }

        // if (node is ParenthesizedExpressionSyntax p)
        // {
        //     return EvaluateExpression(p.Expression);
        // }
        
        throw new Exception($"Unexpected node {node.Kind}");
    }
}

public record struct TextSpan(int Start, int Length)
{
    public readonly int End = Start + Length;
}

public sealed class Diagnostic(TextSpan span, string message)
{
    public TextSpan Span { get; } = span;
    public string Message { get; } = message;
    
    public override string ToString() => Message;
}