using Python.Syntax;

namespace Python.Binding;

internal class BoundUnaryOperator
{
    private BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind, Type operandType, Type resultType)
    {
        SyntaxKind = syntaxKind;
        Kind = kind;
        OperandType = operandType;
        ResultType = resultType;
    }

    private BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind, Type operandType) : this(syntaxKind,
        kind, operandType, operandType)
    {
    }

    public SyntaxKind SyntaxKind { get; }
    public BoundUnaryOperatorKind Kind { get; }
    public Type OperandType { get; }
    public Type ResultType { get; }

    private static readonly BoundUnaryOperator[] Operators =
    [
        new(SyntaxKind.BangToken, BoundUnaryOperatorKind.LogicNegation, typeof(bool)),

        new(SyntaxKind.PlusToken, BoundUnaryOperatorKind.Identity, typeof(int)),
        new(SyntaxKind.MinusToken, BoundUnaryOperatorKind.Negation, typeof(int))
    ];

    internal static BoundUnaryOperator? Bind(SyntaxKind syntaxKind, Type operatorType)
    {
        return Operators.FirstOrDefault(op =>
            op.SyntaxKind == syntaxKind && op.OperandType == operatorType);
    }
}