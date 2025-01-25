namespace Python.Binding;

internal sealed class BoundUnaryExpression(BoundUnaryOperator op, BoundExpression operand)
    : BoundExpression
{
    public BoundExpression Operand { get; } = operand;
    public override Type Type => Op.ResultType;
    public BoundUnaryOperator Op { get; } = op;
    public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;
}