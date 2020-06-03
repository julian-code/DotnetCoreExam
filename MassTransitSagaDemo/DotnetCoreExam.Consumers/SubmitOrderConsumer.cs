using DotnetCoreExam.Contracts;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace DotnetCoreExam.Consumers
{
    public class SubmitOrderConsumer : IConsumer<ISubmitOrder>
    {
        public async Task Consume(ConsumeContext<ISubmitOrder> context)
        {
            if (context.Message.CustomerNumber.Contains("test"))
            {
                await context.RespondAsync<IOrderSubmissionRejected>(new
                {
                    context.Message.OrderId,
                    context.Message.CustomerNumber,
                    InVar.Timestamp,
                    Reason = "Test brugere kan ikke lave ordre."
                });
            }
            else
            {
                await context.Publish<IOrderSubmitted>(new
                {
                    context.Message.OrderId,
                    context.Message.CustomerNumber,
                    InVar.Timestamp,
                });

                await context.RespondAsync<IOrderSubmissionAccepted>(new
                {
                    context.Message.OrderId,
                    context.Message.CustomerNumber,
                    InVar.Timestamp,
                });
            }
        }
    }
}
