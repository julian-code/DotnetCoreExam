using Automatonymous;
using DotnetCoreExam.Contracts;
using GreenPipes;
using MassTransit;
using MassTransit.Definition;
using MassTransit.RedisIntegration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCoreExam.Sagas.StateMachines
{

    public class OrderStateMachineDefinition : SagaDefinition<OrderState>
    {
        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<OrderState> sagaConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 5000, 10000));
            endpointConfigurator.UseInMemoryOutbox();
        }
    }
}
