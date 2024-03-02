using System.Collections.Concurrent;

namespace lab9;

public class ProducerConsumer
{
    private static readonly ConcurrentBag<Computer> Computers = new ConcurrentBag<Computer>();
    private static readonly ManualResetEventSlim Signal = new ManualResetEventSlim(false);

    public static void Run()
    {
        // Start producer and consumer in parallel
        Parallel.Invoke(Producer, Consumer);

        Console.ReadLine();
    }

    private static void Producer()
    {
        for (int i = 0; i < 10; i++)
        {
            // Create a random computer
            var computer = new Computer
            {
                Model = $"Model-{i}",
                Manufacturer = $"Manufacturer-{i}",
                Price = new Random().Next(500, 1500)
            };

            // Add to the ConcurrentBag
            Computers.Add(computer);

            Console.WriteLine($"Produced: {computer}");
            // Notify the consumer that there is a new computer
            Signal.Set();

            // Wait a bit before producing the next computer
            Thread.Sleep(1000);
        }
    }

    private static void Consumer()
    {
        while (true)
        {
            // Wait for a signal from the producer
            Signal.Wait();

            // Try to take a computer from the ConcurrentBag
            if (Computers.TryTake(out var computer))
            {
                Console.WriteLine($"Purchased: {computer}");
            }
            else
            {
                Console.WriteLine("No new computers available right now.");
            }

            // Reset the signal to allow the producer to continue producing
            Signal.Reset();

            // Wait a bit before consuming the next computer
            Thread.Sleep(1100);
        }
    }
}

