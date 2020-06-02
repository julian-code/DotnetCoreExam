using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCoreExam.Contracts
{
    public interface IOrderSubmitted
    {
        Guid OrderId { get; }
        DateTime Timestamp { get; }
        string CustomerNumber { get; }
    }
}
