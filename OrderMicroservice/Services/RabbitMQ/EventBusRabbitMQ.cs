using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLog;
using OrderMicroservice.Businesses;
using OrderMicroservice.Contracts.RabbitMQ;
using OrderMicroservice.Models.ResponseModels;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace OrderMicroservice.Services.RabbitMQ
{
    public class EventBusRabbitMQ : IEventBusRabbitMQ
    {
        IConnection _connection;
        private string _queueName;
        private string _hostName = "rabbitmq";
        private IServiceProvider _service;
        protected readonly IHttpContextAccessor _httpContext;
        public EventBusRabbitMQ(IServiceProvider service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContext = httpContextAccessor;
        }
        public IModel CreateConsumerChannel()
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = _hostName };
                _connection = factory.CreateConnection();

                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queue: SetQueueName("Payment"), durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += ReceivedEvent;
                channel.BasicConsume(queue: SetQueueName("Payment"), autoAck: true, consumer: consumer);
                return channel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private string SetQueueName(string name)
        {
           return _queueName = name;
        }
        private void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                _service = _httpContext.HttpContext.RequestServices;
                var orderBusiness = (OrderBusiness)_service.GetService(typeof(OrderBusiness));
                if (e.RoutingKey == "payment")
                {
                    var message = Encoding.UTF8.GetString(e.Body.ToArray());
                    Payment paymentRecord = JsonConvert.DeserializeObject<Payment>(message);
                    var result = orderBusiness.UpdateOrder(paymentRecord);
                }
            }
            catch(Exception ex)
            {
            }

        }
        public void CreateOrderMessage(OrderResponseModel model)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = _hostName };
                _connection = factory.CreateConnection();
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queue: "order",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonConvert.SerializeObject(model);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "order",
                                     basicProperties: null,
                                     body: body);
            }
            catch(Exception ex)
            {
            }
        }
    }
}
