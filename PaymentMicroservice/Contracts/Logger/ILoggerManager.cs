using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Contracts.Logger
{
    public interface ILoggerManager
    {
        void Info(string message);
        void Warning(string message);
        void Debug(string message);
        void Error(Exception ex, string message);
    }
}
