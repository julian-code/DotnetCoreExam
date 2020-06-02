using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCoreExam.Consumers;
using DotnetCoreExam.Contracts;
using DotnetCoreExam.Sagas.StateMachines;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace DotnetCoreExam.SampleService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
                    services.AddMassTransit(cfg =>
                    {
                        cfg.AddSagaStateMachine<OrderStateMachine, OrderState>().RedisRepository();
                        cfg.AddConsumersFromNamespaceContaining<SubmitOrderConsumer>();
                        cfg.AddBus(ConfigureBus);
                    });
                    services.AddHostedService<Worker>();
                });

        private static IBusControl ConfigureBus(IRegistrationContext<IServiceProvider> arg)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.UseMessageScheduler(new Uri("queue:quartz"));

                cfg.ConfigureEndpoints(arg);
            });
        }
    }
}
