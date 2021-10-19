using System;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace eventhubclient
{
    public class ClientTrigger{

        private const string ehubNamespaceConnectionString = "Endpoint=sb://devbzbseventhubsci.servicebus.windows.net/;SharedAccessKeyName=masterpolicy;SharedAccessKey=Uk63k1NIHhP/bFyI88cE7oLab26asGKr9aR6TaPEZSo=;EntityPath=datasci";
        private const string eventHubName = "datasci";
        private const string blobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=ppbzbs;AccountKey=wRmWhQl47u3hjJ2u2Vdh7wGBN68kx9ZJ8Ohem4PcEampZJZQ+pZIJPUs2iRjCGaz4F/eAAoIwTmcsisO/EOmlw==;EndpointSuffix=core.windows.net";
        private const string blobContainerName = "datascicheckpoint";

        static BlobContainerClient storageClient;

        // The Event Hubs client types are safe to cache and use as a singleton for the lifetime
        // of the application, which is best practice when events are being published or read regularly.        
        static EventProcessorClient processor;

        public ClientTrigger(){
            ClientTrigger.Start();

        }
        static async Task Start(){
            // Read from the default consumer group: $Default
            string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

            // Create a blob container client that the event processor will use 
            storageClient = new BlobContainerClient(blobStorageConnectionString, blobContainerName);

            // Create an event processor client to process events in the event hub
            processor = new EventProcessorClient(storageClient, consumerGroup, ehubNamespaceConnectionString, eventHubName);

            // Register handlers for processing events and handling errors
            processor.ProcessEventAsync += ProcessEventHandler;
            processor.ProcessErrorAsync += ProcessErrorHandler;

            // Start the processing
            await processor.StartProcessingAsync();

            // Wait for 30 seconds for the events to be processed
            //await Task.Delay(TimeSpan.FromSeconds(30));
            //string s = Console.ReadLine();
            // Stop the processing
            //await processor.StopProcessingAsync();
        }

        static async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            string ev_data = Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray());

            var headers = eventArgs.Data.Properties;

            if (!URL.StartsWith("http"))
            {
                await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
                return;
            }

            Console.WriteLine(ev_data);
            //Console.ReadLine();


            try {
                /*
                if (!URL.StartsWith("http")) {
                    await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
                    return;
                }

                var responseMessage = SendToMotomo(URL, headers);
                var statusCode = responseMessage.Result.StatusCode;
                Console.Clear();
                Console.Write(eventArgs.Data.SequenceNumber);
                */
                /*
                if (System.Net.HttpStatusCode.OK == statusCode)
                {
                    //Console.WriteLine(statusCode);
                    //Console.WriteLine(eventArgs.Data.PartitionKey);
                    //Console.WriteLine(eventArgs.Data.MessageId);
                    //Console.WriteLine(eventArgs.Data.Offset);
                    //Console.WriteLine(eventArgs.Data.SequenceNumber);
                   
                    await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
                }*/
            }
            catch (Exception ex) {
                //await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
                /*
                if (System.Net.HttpStatusCode.OK == (await SendToMotomo(URL, headers)).StatusCode) {
                    await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
                }*/

                Console.WriteLine(ex.Message);
            }

            
        }
        
        static Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            // Write details about the error to the console window
            Console.WriteLine($"\tPartition '{ eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
            Console.WriteLine(eventArgs.Exception.Message);
            return Task.CompletedTask;
        }
    }
}