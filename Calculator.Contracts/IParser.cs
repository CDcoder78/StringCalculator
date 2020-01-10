namespace Calculator.Contracts
{
    public interface IParser
    {
        bool DenyNegative { get; }

        int[] ParseIntegers(string input);

        string GetDelimiters();

        void SetDenyNegative(bool state);
    }
}