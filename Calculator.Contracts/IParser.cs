namespace Calculator.Contracts
{
    public interface IParser
    {
        ComputeTypes CurrentMode { get; }

        bool DenyNegative { get; set; }

        uint UpperBound{ get; set; }

        int[] ParseIntegers(string input);

        string GetDelimiters();

        bool HandleCommand(string input);

        string GetCommandText();
    }
}