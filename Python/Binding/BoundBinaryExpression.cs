namespace Python.Binding;

internal sealed class BoundBinaryExpression(BoundExpression left, BoundBinaryOperator op, BoundExpression right)
    : BoundExpression
{
    public BoundExpression Left { get; } = left;
    public BoundExpression Right { get; } = right;
    public override Type Type => Op.ResultType;
    public BoundBinaryOperator Op { get; } = op;
    public override BoundNodeKind Kind => BoundNodeKind.BinaryExpression;
}