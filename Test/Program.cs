using System;

public class FishEvent(string name, Fish fish)
{
    public string Name { get; set; } = name;
    public Fish Fish { get; set; } = fish;
}

public class Fish(long id, string name)
{
    public long Id { get; set; } = id;
    public string Name { get; set; } = name;
}


static class Program
{
    static void Main()
    {
        Fish fish = new Fish(1, "sds");
        FishEvent fishEvent = new FishEvent("XD", fish);
        
        Console.WriteLine(fishEvent.Fish.Name);
    }
}
