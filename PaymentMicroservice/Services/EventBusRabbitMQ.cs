using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLog;
using PaymentMicroservice.Businesses;
using PaymentMicroservice.Contracts.RabbitMQ;
using PaymentMicroservice.Models.RequestModels;
using PaymentMicroservice.Models.ResponseModels;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentMicroservice.Services
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
                channel.QueueDeclare(queue: SetQueueName("order"), durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += ReceivedEvent;
                channel.BasicConsume(queue: SetQueueName("order"), autoAck: true, consumer: consumer);
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
                var paymentBusiness = (PaymentBusiness)_service.GetService(typeof(PaymentBusiness));
                if (e.RoutingKey == "order")
                {
                    var message = Encoding.UTF8.GetString(e.Body.ToArray());
                    Order orderRecord = JsonConvert.DeserializeObject<Order>(message);
                    var result = paymentBusiness.CreatePayment(orderRecord);
                }
            }
            catch(Exception ex)
            {
            }
        }
        public bool CreatePaymentMessage(PaymentResponseModel model)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = _hostName };
                _connection = factory.CreateConnection();
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queue: "payment",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonConvert.SerializeObject(model);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "payment",
                                     basicProperties: null,
                                     body: body);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
