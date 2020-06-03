using System;
using Xunit;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Consumer;
using System.Collections.Generic;

namespace tests
{
    public class ConsumerPactTests : IClassFixture<ConsumerPactClassFixture>
    {
        private IMockProviderService _mockProviderService;
        private string _mockProviderServiceBaseUri;

        public ConsumerPactTests(ConsumerPactClassFixture fixture)
        {
            _mockProviderService = fixture.MockProviderService;
            _mockProviderService.ClearInteractions(); //NOTE: Clears any previously registered interactions before the test is run
            _mockProviderServiceBaseUri = fixture.MockProviderServiceBaseUri;
        }

        [Fact]
        public void ItGetsAValue()
        {
            // Arange
            var stringConsumerNeeds = "Hello from provider!";
            _mockProviderService
                                .UponReceiving("A invalid GET request for Date Validation with invalid date parameter")
                                .With(new ProviderServiceRequest
                                {
                                    Method = HttpVerb.Get,
                                    Path = "/api/provider",
                                })
                                .WillRespondWith(new ProviderServiceResponse {
                                    Status = 200,
                                    Headers = new Dictionary<string, object>
                                    {
                                        { "Content-Type", "application/json; charset=utf-8" }
                                    },
                                    Body = new
                                    {
                                        message = stringConsumerNeeds
                                    }
                                });

            // Act
            var result = ConsumerApiClient.ValidateDateTimeUsingProviderApi(_mockProviderServiceBaseUri).GetAwaiter().GetResult();
            var resultBodyText = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            // Assert
            Assert.Contains(stringConsumerNeeds, resultBodyText);
        }
    }
}
