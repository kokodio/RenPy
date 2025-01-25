using Python.Syntax;
using Shouldly;

namespace PythonTest.Syntax;

public class LexerTests
{
    [TestCaseSource(nameof(GetTokensData))]
    public void Lexer_Lexes_Token(SyntaxKind kind, string text)
    {
        var tokens = SyntaxTree.ParseTokens(text);

        var token = tokens.Single();
        kind.ShouldBe(token.Kind);
        text.ShouldBe(token.Text);
    }
    
    [TestCaseSource(nameof(GetTokenPairsData))]
    public void Lexer_Lexes_TokenPairs_TokenPairs(
        SyntaxKind firstKind, string firstText,
        SyntaxKind secondKind, string secondText)
    {
        var text = firstText + secondText;
        var tokens = SyntaxTree.ParseTokens(text).ToArray();

        SyntaxKind[] expectedKinds = [firstKind, secondKind];
        string[] expectedTexts = [firstText, secondText];

        tokens.Length.ShouldBe(2);
        tokens.Select(x => x.Kind).ShouldBe(expectedKinds);
        tokens.Select(x => x.Text).ShouldBe(expectedTexts);
    }

    [TestCaseSource(nameof(GetTokenPairsWithSeparatorData))]
    public void Lexer_Lexes_TokenPairs_WithSeparators(
        SyntaxKind firstKind, string firstText,
        SyntaxKind separatorKind, string separatorText,
        SyntaxKind secondKind, string secondText)
    {
        var text = firstText + separatorText + secondText;
        var tokens = SyntaxTree.ParseTokens(text).ToArray();

        SyntaxKind[] expectedKinds = [firstKind, separatorKind, secondKind];
        string[] expectedTexts = [firstText, separatorText, secondText];

        tokens.Length.ShouldBe(3);
        tokens.Select(x => x.Kind).ShouldBe(expectedKinds);
        tokens.Select(x => x.Text).ShouldBe(expectedTexts);
    }

    private static IEnumerable<TestCaseData> GetTokensData()
    {
        return GetTokens()
            .Concat(GetSeparators())
            .Select(t => new TestCaseData(t.kind, t.text));
    }

    private static IEnumerable<TestCaseData> GetTokenPairsData()
    {
        return GetTokenPairs().Select(t => 
            new TestCaseData(t.firstKind, t.firstText, t.secondKind, t.secondText));
    }

    private static IEnumerable<TestCaseData> GetTokenPairsWithSeparatorData()
    {
        return GetTokenPairsWithSeparator().Select(t =>
            new TestCaseData(t.firstKind, t.firstText, t.separatorKind, t.separatorText, t.secondKind, t.secondText));
    }

    private static IEnumerable<(SyntaxKind kind, string text)> GetTokens()
    {
        return
        [
            (SyntaxKind.PlusToken, "+"),
            (SyntaxKind.MinusToken, "-"),
            (SyntaxKind.StarToken, "*"),
            (SyntaxKind.SlashToken, "/"),
            (SyntaxKind.BangToken, "not"),
            (SyntaxKind.EqualsToken, "="),
            (SyntaxKind.AmpersandToken, "and"),
            (SyntaxKind.PipeToken, "or"),
            (SyntaxKind.EqualsEqualsToken, "=="),
            (SyntaxKind.BangEqualsToken, "!="),
            (SyntaxKind.OpenParenthesisToken, "("),
            (SyntaxKind.CloseParenthesisToken, ")"),
            (SyntaxKind.FalseKeyword, "False"),
            (SyntaxKind.TrueKeyword, "True"),
            (SyntaxKind.NumberToken, "1"),
            (SyntaxKind.NumberToken, "123"),
            (SyntaxKind.IdentifierToken, "a"),
            (SyntaxKind.IdentifierToken, "abc")
        ];
    }

    private static IEnumerable<(SyntaxKind kind, string text)> GetSeparators()
    {
        return
        [
            (SyntaxKind.WhitespaceToken, " "),
            (SyntaxKind.WhitespaceToken, "  "),
            (SyntaxKind.WhitespaceToken, "\r"),
            (SyntaxKind.WhitespaceToken, "\n"),
            (SyntaxKind.WhitespaceToken, "\r\n")
        ];
    }

    private static bool RequiresSeparator(SyntaxKind firstKind, SyntaxKind secondKind)
    {
        var firstIsKeyword = firstKind.ToString().EndsWith("Keyword");
        var secondIsKeyword = secondKind.ToString().EndsWith("Keyword");
        
        if (firstIsKeyword && secondIsKeyword)
            return true;

        if (firstIsKeyword && secondKind == SyntaxKind.IdentifierToken)
            return true;
        
        switch (firstKind)
        {
            case SyntaxKind.IdentifierToken when secondIsKeyword:
            case SyntaxKind.IdentifierToken when secondKind == SyntaxKind.IdentifierToken:
            case SyntaxKind.IdentifierToken when secondKind == SyntaxKind.AmpersandToken:
            case SyntaxKind.IdentifierToken when secondKind == SyntaxKind.BangToken:
            case SyntaxKind.IdentifierToken when secondKind == SyntaxKind.PipeToken:
            case SyntaxKind.AmpersandToken when secondKind == SyntaxKind.IdentifierToken:
            case SyntaxKind.AmpersandToken when secondKind == SyntaxKind.AmpersandToken:
            case SyntaxKind.AmpersandToken when secondKind == SyntaxKind.BangToken:
            case SyntaxKind.AmpersandToken when secondKind == SyntaxKind.PipeToken:
            case SyntaxKind.AmpersandToken when secondKind == SyntaxKind.FalseKeyword:
            case SyntaxKind.AmpersandToken when secondKind == SyntaxKind.TrueKeyword:
            case SyntaxKind.BangToken when secondKind == SyntaxKind.IdentifierToken:
            case SyntaxKind.BangToken when secondKind == SyntaxKind.AmpersandToken:
            case SyntaxKind.BangToken when secondKind == SyntaxKind.BangToken:
            case SyntaxKind.BangToken when secondKind == SyntaxKind.PipeToken:
            case SyntaxKind.BangToken when secondKind == SyntaxKind.FalseKeyword:
            case SyntaxKind.BangToken when secondKind == SyntaxKind.TrueKeyword:
            case SyntaxKind.BangToken when secondKind == SyntaxKind.EqualsToken:
            case SyntaxKind.BangToken when secondKind == SyntaxKind.EqualsEqualsToken:
            case SyntaxKind.PipeToken when secondKind == SyntaxKind.IdentifierToken:
            case SyntaxKind.PipeToken when secondKind == SyntaxKind.AmpersandToken:
            case SyntaxKind.PipeToken when secondKind == SyntaxKind.BangToken:
            case SyntaxKind.PipeToken when secondKind == SyntaxKind.PipeToken:
            case SyntaxKind.PipeToken when secondKind == SyntaxKind.FalseKeyword:
            case SyntaxKind.PipeToken when secondKind == SyntaxKind.TrueKeyword:
            case SyntaxKind.FalseKeyword when secondKind == SyntaxKind.AmpersandToken:
            case SyntaxKind.FalseKeyword when secondKind == SyntaxKind.BangToken:
            case SyntaxKind.FalseKeyword when secondKind == SyntaxKind.PipeToken:
            case SyntaxKind.TrueKeyword when secondKind == SyntaxKind.AmpersandToken:
            case SyntaxKind.TrueKeyword when secondKind == SyntaxKind.BangToken:
            case SyntaxKind.TrueKeyword when secondKind == SyntaxKind.PipeToken:
            case SyntaxKind.NumberToken when secondKind == SyntaxKind.NumberToken:
            case SyntaxKind.EqualsToken when secondKind == SyntaxKind.EqualsToken:
            case SyntaxKind.EqualsToken when secondKind == SyntaxKind.EqualsEqualsToken:
                return true;
            default:
                return false;
        }
    }

    private static IEnumerable<(
            SyntaxKind firstKind, string firstText,
            SyntaxKind secondKind, string secondText)>
        GetTokenPairs()
    {
        foreach (var firstToken in GetTokens())
        {
            foreach (var secondToken in GetTokens())
            {
                if (!RequiresSeparator(firstToken.kind, secondToken.kind))
                    yield return (firstToken.kind, firstToken.text, secondToken.kind, secondToken.text);
            }
        }
    }

    private static IEnumerable<(
            SyntaxKind firstKind, string firstText,
            SyntaxKind separatorKind, string separatorText,
            SyntaxKind secondKind, string secondText)>
        GetTokenPairsWithSeparator()
    {
        foreach (var firstToken in GetTokens())
        {
            foreach (var secondToken in GetTokens())
            {
                foreach (var separator in GetSeparators())
                    yield return (
                        firstToken.kind, firstToken.text,
                        separator.kind, separator.text,
                        secondToken.kind, secondToken.text
                    );
            }
        }
    }
}