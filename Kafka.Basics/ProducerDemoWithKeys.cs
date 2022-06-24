using Confluent.Kafka;

namespace Kafka.Basics
{
    public class ProducerDemoWithKeys
    {
        public void CreateProducer()
        {
            // You can have a lots of configuration here...
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            // Create the producer.
            using var producer = new ProducerBuilder<string, string>(config).Build();

            for (int i = 0; i < 10; i++)
            {
                // Create the record that will be send.
                Message<string, string> message = new()
                {
                    Key = $"Key_{i + 1}",
                    Value = $"Hello Kafka from .NET!! This is a PRODUCER with a response {i + 1}"
                };

                // Send data asynchronously.
               var response = producer.ProduceAsync("java_demo", message);

                Console.WriteLine();
                Console.WriteLine($"Topic: {response.Result.Topic}");
                Console.WriteLine($"Partition: {response.Result.Partition.Value}");
                Console.WriteLine($"Key: {message.Key}");
                Console.WriteLine($"Offset: {response.Result.Offset}");
                Console.WriteLine($"Timestamp: {response.Result.Timestamp.UtcDateTime}");
            }

            producer.Flush();
            producer.Dispose();
        }
    }
}
