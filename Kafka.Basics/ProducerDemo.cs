using Confluent.Kafka;

namespace Kafka.Basics
{
    public class ProducerDemo
    {
        public void CreateProducer()
        {
            // You can have a lots of configuration here...
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            // Create the producer.
            using var producer = new ProducerBuilder<Null, string>(config).Build();

            // Create the record that will be send.
            Message<Null, string> message = new();
            message.Value = "*** Hello Kafka from .NET!! This is a PRODUCER ***";

            try
            {
                // Of course, the message and the send data could be declared as one single line.
                //var response = await producer.ProduceAsync("java_demo", 
                //    new Message<Null, string> { Value = "Hello Kafka from .NET!!" });

                producer.Produce("java_demo", message);
                producer.Flush();
                producer.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
