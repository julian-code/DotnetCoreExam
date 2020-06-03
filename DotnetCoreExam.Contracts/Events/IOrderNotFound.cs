using System;

namespace DotnetCoreExam.Contracts
{
    public interface IOrderNotFound
    {
        Guid OrderId { get; }
    }
}
