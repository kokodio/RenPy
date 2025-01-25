namespace Python;

public sealed class EvaluationResult(IEnumerable<Diagnostic> diagnostics, object value)
{
    public object Value { get; } = value;
    public IReadOnlyList<Diagnostic> Diagnostics { get; } = diagnostics.ToArray();
}