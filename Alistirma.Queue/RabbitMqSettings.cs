using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alistirma.Queue
{
    public class RabbitMQSetting
    {
        public string? HostName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    //RabbitMQ Queue name
    public static class RabbitMQQueues
    {
        public const string OrderValidationQueue = "deneme1";
        public const string AnotherQueue = "deneme2";
        public const string ThirdQueue = "deneme3";
    }
}
