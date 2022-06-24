using Kafka.Basics;

Console.WriteLine("***** APACHE KAFKA .NET *****");

//var producer = new ProducerDemo();
//producer.CreateProducer();

var producerWithCallback = new ProducerDemoWithCallback();
producerWithCallback.CreateProducer();

//var producerDemoWithKeys = new ProducerDemoWithKeys();
//producerDemoWithKeys.CreateProducer();

var consumer = new ConsumerDemo();
consumer.CreateConsumer();