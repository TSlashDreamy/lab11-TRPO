namespace lab9;

public class Computer
{
    public string Model { get; set; }
    public string Manufacturer { get; set; }
    public double Price { get; set; }

    public override string ToString()
    {
        return $"Model: {Model}, Manufacturer: {Manufacturer}, Price: {Price}";
    }
}
