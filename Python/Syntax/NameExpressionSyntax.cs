﻿namespace Python.Syntax;

public sealed class NameExpressionSyntax(SyntaxToken identifierToken)
    : ExpressionSyntax
{
    public SyntaxToken IdentifierToken { get; } = identifierToken;
    public override SyntaxKind Kind => SyntaxKind.NameExpression;
    
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return IdentifierToken;
    }
}