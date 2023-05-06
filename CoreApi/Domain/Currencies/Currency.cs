namespace Domain.Currencies;

public record Currency(string Symbol)
{
    public static readonly Currency CAD = new Currency("CAD");
    public static readonly Currency USD = new Currency("USD");


    void UpdateCurrency(string Symbol)
    {

    }

}

public record Money(Currency Currency, decimal Amount);
