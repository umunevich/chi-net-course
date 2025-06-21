namespace Bank;

public class Deposit
{
    public decimal Amount
    {
        get => _amount;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            _amount = value;
            _percentage = CalculatePercentage(_amount);
        }
    }
    public decimal Percentage
    {
        get => _percentage;
    }

    public decimal GetTotalAmount()
    {
        return Amount + Amount * Percentage / 100 + BonusUnits;
    }
    
    private const int LowBound = 100;
    private const int HighBound = 200;

    private const int MinPercentage = 5;
    private const int MidPercentage = 7;
    private const int MaxPercentage = 10;
    
    private decimal _amount;
    private decimal _percentage;
    private const int BonusUnits = 15;
    private decimal CalculatePercentage(decimal amount)
    {
        switch (amount)
        {
            case < LowBound:
                return MinPercentage;
            case < HighBound:
                return MidPercentage;
            case >= HighBound:
                return MaxPercentage;
        }
    }
}