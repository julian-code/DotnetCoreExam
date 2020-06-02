using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCoreExam.Contracts
{
    public interface ICheckOrder
    {
        Guid OrderId { get; }
    }
}
