namespace Python.Syntax;

public static class SyntaxFacts
{
    public static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
    {
        return kind switch
        {
            SyntaxKind.PlusToken or SyntaxKind.MinusToken or SyntaxKind.BangToken => 6,
            _ => 0
        };
    }
    
    public static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
    {
        return kind switch
        {
            SyntaxKind.StarToken or SyntaxKind.SlashToken => 5,
            SyntaxKind.MinusToken or SyntaxKind.PlusToken => 4,
            SyntaxKind.BangEqualsToken or SyntaxKind.EqualsEqualsToken => 3,
            SyntaxKind.AmpersandToken => 2,
            SyntaxKind.PipeToken => 1,
            _ => 0
        };
    }

    public static SyntaxKind GetKeywordKind(string text)
    {
        return text switch
        {
            "True" => SyntaxKind.TrueKeyword,
            "False" => SyntaxKind.FalseKeyword,
            "and" => SyntaxKind.AmpersandToken,
            "not" => SyntaxKind.BangToken,
            "or" => SyntaxKind.PipeToken,
            _ => SyntaxKind.IdentifierToken
        };
    }
}