namespace Python.Syntax;

public sealed class LiteralExpressionSyntax(SyntaxToken literalToken, object value) : ExpressionSyntax
{
    public LiteralExpressionSyntax(SyntaxToken literalToken) : this(literalToken, literalToken.Value)
    {
    } 
    
    public SyntaxToken LiteralToken { get; } = literalToken;
    public object Value { get; } = value;
    public override SyntaxKind Kind => SyntaxKind.LiteralExpression;
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return LiteralToken;
    }
}

public sealed class AssignmentExpressionSyntax(SyntaxToken identifierToken, SyntaxNode equalsToken, ExpressionSyntax expression) : ExpressionSyntax
{
    public SyntaxToken IdentifierToken { get; } = identifierToken;
    public SyntaxNode EqualsToken { get; } = equalsToken;
    public ExpressionSyntax Expression { get; } = expression;
    public override SyntaxKind Kind => SyntaxKind.AssignmentExpression;
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return IdentifierToken;
        yield return EqualsToken;
        yield return Expression;
    }
}
