namespace Python.Binding;

internal sealed class BoundLiteralExpression(object value) : BoundExpression
{
    public object Value { get; } = value;
    public override Type Type => Value.GetType();
    public override BoundNodeKind Kind => BoundNodeKind.LiteralExpression;
}