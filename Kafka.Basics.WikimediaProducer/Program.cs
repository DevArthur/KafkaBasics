using Confluent.Kafka;

string bootstrapServer = "localhost:9092";
string topic = "wikimedia.recentchange";
string url = "https://stream.wikimedia.org/v2/stream/recentchange";

// Configurations 
var config = new ProducerConfig
{
    BootstrapServers = bootstrapServer
};

// Create producer
using var producer = new ProducerBuilder<string, string>(config).Build();

// Use a timer to finish the app execution in 10 minutes.
var timer = new System.Timers.Timer(600000);
timer.Elapsed += FinishAppExcution;
timer.Start();


try
{
    using (var httpClient = new HttpClient())
    using (var stream = await httpClient.GetStreamAsync(url))
    using (var reader = new StreamReader(stream))
    {
        int counter = 1;
        while (!reader.EndOfStream)
        {
            Message<string, string> record = new();
            record.Key = $"KEY_{counter}";
            record.Value = reader.ReadLine() ?? string.Empty;
            producer.Produce(topic, record);
            counter++;
        }
    }
}
catch (Exception e)
{
    Console.WriteLine($"Error reading the stream {e}");
}


void FinishAppExcution(object sender, System.Timers.ElapsedEventArgs e)
{
    Environment.Exit(0);
}
