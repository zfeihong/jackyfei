using FluentAssertions;
using Iot.Application.Common.Exceptions;
using Iot.Application.Devices.Commands.CreateDevice;
using Iot.Application.Devices.Commands.DeleteDevice;
using Iot.Application.Products.Command.CreateProduct;
using Iot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Devices.Commands
{
    using static DatabaseFixture;

    [Collection("DatabaseCollection")]
    public class DeleteDeviceTests
    {
        public DeleteDeviceTests()
        {
            ResetState().GetAwaiter().GetResult();
        }

        [Fact]
        public void ShouldRequireValidDeviceId()
        {
            var command = new DeleteDeviceCommand
            {
                Id = 69
            };

            FluentActions.Invoking(() => SendAsync(command))
                .Should().
                ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task ShouldDeleteDevice()
        {
            var productId = await SendAsync(new CreateProductCommand
            {
                Code = "product code",
                Name = "product name",
                Secret = "123qwe"
            });

            var deviceId = await SendAsync(new CreateDeviceCommand
            {
                Code = "device code111",
                Name = "device name111",
                Secret = "234wer111",
                ProductId = productId
            });

            await SendAsync(new DeleteDeviceCommand
            {
                Id = deviceId
            });

            var list = await FindAsync<Device>(productId);

            list.Should().BeNull();
        }
    }
}
