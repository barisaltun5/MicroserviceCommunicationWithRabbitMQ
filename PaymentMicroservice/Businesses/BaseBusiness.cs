using Microsoft.AspNetCore.Http;
using PaymentMicroservice.Models.ResponseModels;
using PaymentMicroservice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PaymentMicroservice.Businesses
{
    public class BaseBusiness
    {
        public static IServiceProvider service;
        public static IHttpContextAccessor httpContextAccessor;
        public LoggerManager _logger = new();
        public BaseResponseModel baseResponseModel = new();
        public EventBusRabbitMQ eventBus = new(service,httpContextAccessor);
        public BaseResponseModel ResponseIsSuccess(object? model)
        {
            var VariableType = model;
            if (model is string)
            {
                MessageModel messageModel = new MessageModel();
                messageModel.Message = (string)model;
                baseResponseModel.Data = messageModel;
                baseResponseModel.IsSuccess = true;
                baseResponseModel.Message = "Successful Request - Ok";
                baseResponseModel.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                baseResponseModel.Data = model;
                baseResponseModel.IsSuccess = true;
                baseResponseModel.Message = "Successful Request - Ok";
                baseResponseModel.StatusCode = HttpStatusCode.OK;
            }
            return baseResponseModel;
        }
        public BaseResponseModel ResponseIsFailed(string? message, object? model, object? statusCode)
        {
            baseResponseModel.IsSuccess = false;
            baseResponseModel.Message = message ?? "An Unexpected Error Occurred";
            baseResponseModel.StatusCode = statusCode;
            baseResponseModel.Data = model;
            return baseResponseModel;
        }

        public BaseResponseModel ResponseCatch()
        {
            baseResponseModel.IsSuccess = false;
            baseResponseModel.Message = "An Unexpected Error Occurred - InternalServerError";
            baseResponseModel.StatusCode = HttpStatusCode.InternalServerError;
            baseResponseModel.Data = null;
            return baseResponseModel;
        }
    }
}
