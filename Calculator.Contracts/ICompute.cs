namespace Calculator.Contracts
{
    public enum ComputeTypes
    {
        Add,
        Subtract,
        Division,
        Multiplication
    }

    public interface ICompute
    {
        string Compute(string input);
    }
}