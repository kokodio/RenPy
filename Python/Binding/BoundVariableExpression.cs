namespace Python.Binding;

internal sealed class BoundVariableExpression(VariableSymbol variable) : BoundExpression
{
    public VariableSymbol Variable { get; } = variable;
    public override Type Type { get; } = variable.Type;
    public override BoundNodeKind Kind => BoundNodeKind.VariableExpression;
}