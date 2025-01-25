namespace Python.Syntax;

public sealed class ParenthesizedExpressionSyntax(SyntaxToken openParenthesisToken, ExpressionSyntax expression, SyntaxToken closeParenthesisToken) : ExpressionSyntax
{
    public SyntaxToken OpenParenthesisToken { get; } = openParenthesisToken;
    public ExpressionSyntax Expression { get; } = expression;
    public SyntaxToken CloseParenthesisToken { get; } = closeParenthesisToken;
    public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return openParenthesisToken;
        yield return expression;
        yield return closeParenthesisToken;
    }
}