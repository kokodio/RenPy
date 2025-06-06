﻿namespace Python.Syntax;

public sealed class SyntaxToken(SyntaxKind kind, int position, string text, object value) : SyntaxNode
{
    public override SyntaxKind Kind { get; } = kind;

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        return [];
    }

    public int Position { get; } = position;
    public string Text { get; } = text;
    public object Value { get; } = value;
    public TextSpan Span => new TextSpan(Position, Text.Length);
}