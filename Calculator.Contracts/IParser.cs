namespace Calculator.Contracts
{
    public interface IParser
    {
        bool DenyNegative { get; set; }

        uint UpperBound{ get; set; }

        int[] ParseIntegers(string input);

        string GetDelimiters();

        bool HandleCommand(string input);

        string GetCommandText();
    }
}