﻿using System;

namespace DotnetCoreExam.Contracts
{
    public interface ISubmitOrder
    {
        Guid OrderId { get; }
        DateTime Timestamp { get; }
        string CustomerNumber { get; }
    }
}
