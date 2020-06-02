using System;

namespace DotnetCoreExam.Contracts
{
    public interface IOrderStatus
    {
        Guid OrderId { get; }
        string State { get; }
    }
}
