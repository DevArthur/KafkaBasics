using Confluent.Kafka;

namespace Kafka.Basics
{
    public class ConsumerDemo
    {
        public void CreateConsumer()
        {
            // You can have a lots of configuration here...}
             var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "demo_group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            // Create the producer.
            using var consumer = new ConsumerBuilder<string, string>(config).Build();

            // Subscribe to a topic the consumer created
            consumer.Subscribe("java_demo");

            // Cancellation token
            CancellationTokenSource token = new();

            try
            {
                Console.WriteLine("\n**********************************************************\n");
                while (true)
                {
                    var response = consumer.Consume(token.Token);
                    if (response.Message != null)
                    {
                        Console.WriteLine($"Topic: {response.Topic}");                        
                        Console.WriteLine($"Partition {response.Partition.Value}");
                        Console.WriteLine($"Offset: {response.Offset.Value}");
                        Console.WriteLine($"Timestamp: {response.Message.Timestamp.UtcDateTime}");
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
