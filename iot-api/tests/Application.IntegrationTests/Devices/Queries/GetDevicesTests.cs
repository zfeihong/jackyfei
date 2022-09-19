using FluentAssertions;
using Iot.Application.Devices.Queries.GetDevices;
using Iot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Devices.Queries
{
    using static DatabaseFixture;

    [Collection("DatabaseCollection")]
    public class GetDevicesTests
    {
        public GetDevicesTests()
        {
            ResetState().GetAwaiter().GetResult();
        }

        [Fact]
        public async Task ShouldReturnProducts()
        {
            var query = new GetDevicesQuery();
            var result = await SendAsync(query);

            result.Lists.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ShouldReturnAllProductsAndDevices()
        {
            await AddAsync(new Product
            {
                Code = "product code",
                Name = "product name",
                Secret = "123qwe",
                Devices = new List<Device>
                {
                    new()
                    {
                        Code = "device code111",
                        Name = "device name111",
                        Secret = "234wer111" 
                    }
                }
            });

            var query = new GetDevicesQuery();
            var result = await SendAsync(query);

            result.Lists.Should().HaveCount(1);
        }
    }
}
