namespace Python.Syntax;

internal class Lexer(string lexerText)
{
    private int position;
    public readonly DiagosticBag Diagnostics = new();
    
    private char Current => Peek(0);
    private char Lookahead => Peek(1);

    private char Peek(int offset)
    {
        var index = position + offset;

        if (index >= lexerText.Length)
        {
            return '\0';
        }

        return lexerText[index];
    }

    public SyntaxToken Lex()
    {
        if (position >= lexerText.Length)
        {
            return new SyntaxToken(SyntaxKind.EndOfFileToken, position, "\0", null);
        }
        
        var start = position;

        if (char.IsDigit(Current))
        {
            

            while (char.IsDigit(Current))
            {
                Next();
            }

            var length = position - start;
            var text = lexerText.Substring(start, length);
            
            if (!int.TryParse(text, out var value))
            {
                Diagnostics.ReportInvalidNumber(new TextSpan(start, length), text, typeof(int));
            }
            
            return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
        }

        if (char.IsWhiteSpace(Current))
        {

            while (char.IsWhiteSpace(Current))
            {
                Next();
            }

            var length = position - start;
            var text = lexerText.Substring(start, length);

            return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, null);
        }

        if (char.IsLetter(Current))
        {

            while (char.IsLetter(Current))
            {
                Next();
            }
            var length = position - start;
            var text = lexerText.Substring(start, length);
            var kind = SyntaxFacts.GetKeywordKind(text);
            
            return new SyntaxToken(kind, start, text, null);
        }

        switch (Current)
        {
            case '+':
                return new SyntaxToken(SyntaxKind.PlusToken, position++, "+", null);
            case '-':
                return new SyntaxToken(SyntaxKind.MinusToken, position++, "-", null);
            case '/':
                return new SyntaxToken(SyntaxKind.SlashToken, position++, "/", null);
            case '*':
                return new SyntaxToken(SyntaxKind.StarToken, position++, "*", null);
            case '(':
                return new SyntaxToken(SyntaxKind.OpenParenthesisToken, position++, "(", null);
            case ')':
                return new SyntaxToken(SyntaxKind.CloseParenthesisToken, position++, ")", null);
            case '=':
                if (Lookahead == '=')
                {
                    position += 2;
                    return new SyntaxToken(SyntaxKind.EqualsEqualsToken, start, "==", null);
                }
                else
                {
                    return new SyntaxToken(SyntaxKind.EqualsToken, position++, "=", null);
                }
                break;
            case '!':
                if (Lookahead == '=')
                {
                    position += 2;
                    return new SyntaxToken(SyntaxKind.BangEqualsToken, start, "!=", null);
                }
                break;
        }
        
        Diagnostics.ReportBadCharacter(position, Current);
        
        return new SyntaxToken(SyntaxKind.BadToken, position++, lexerText.Substring(position - 1, 1), null);
    }

    private void Next() => position++;
}