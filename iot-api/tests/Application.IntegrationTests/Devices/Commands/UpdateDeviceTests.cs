using FluentAssertions;
using Iot.Application.Common.Exceptions;
using Iot.Application.Devices.Commands.CreateDevice;
using Iot.Application.Devices.Commands.UpdateDevice;
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
    public class UpdateDeviceTests
    {
        public UpdateDeviceTests()
        {
            ResetState().GetAwaiter().GetResult();
        }

        [Fact]
        public void ShouldRequireValidDeviceId()
        {
            var command = new UpdateDeviceCommand
            {
                Id = 1000,
                Name = "发电机",
                Code="power",
            };

            FluentActions.Invoking(() => SendAsync(command))
                .Should()
                .ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task ShouldUpdateDevice()
        {
            var productId = await SendAsync(new CreateProductCommand
            {
                Code = "power",
                Name = "发电机",
                Secret = "123qwe"
            });

            var deviceId = await SendAsync(new CreateDeviceCommand
            {
                Code = "kms",
                Name = "康明斯",
                Secret = "234wer111",
                ProductId = productId
            });

            var command = new UpdateDeviceCommand
            {
                Id = deviceId,
                Name = "玉柴",
                Code = "yc",
                Secret = "",
            };

            await SendAsync(command);

            var item = await FindAsync<Device>(deviceId);

            item.Name.Should().Be(command.Name);
            item.Secret.Should().NotBeNull();
        }
    }
}