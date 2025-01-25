namespace Python.Syntax;

public enum SyntaxKind
{
    //Tokens
    BadToken,
    EndOfFileToken,
    WhitespaceToken,
    NumberToken,
    PlusToken,
    MinusToken,
    StarToken,
    SlashToken,
    OpenParenthesisToken,
    CloseParenthesisToken,
    EqualsToken,
    BangToken,
    PipeToken,
    AmpersandToken,
    EqualsEqualsToken,
    BangEqualsToken,
    IdentifierToken,
    
    //Keywords
    FalseKeyword,
    TrueKeyword,
    
    //Expressisons
    NameExpression,
    LiteralExpression,
    BinaryExpression,
    UnaryExpression,
    ParenthesizedExpression,
    AssignmentExpression,
    
}