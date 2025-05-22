// Base class
public abstract class Discount

{
    public abstract decimal GetDiscount(decimal amount);
}

// Subclass 1
public class PrimeUser : Discount
{
    public override decimal GetDiscount(decimal amount)
    {
        // Assume 20% discount
        return amount * 0.20m;
    }
}

// Subclass 2
public class NonPrimeUser : Discount
{
    public override decimal GetDiscount(decimal amount)
    {
        // Assume 5% discount
        return amount * 0.05m;
    }
}

// Example usage
public class Program
{
    public static void Main()
    {
        

        // Substituting base class references with derived class objects
        Discount primeUser = new PrimeUser();                   // Substitution
        Discount nonPrimeUser = new NonPrimeUser();             // Substitution

    }
}
