using FluentAssertions;
using Iot.Application.Devices.Commands.CreateDevice;
using Iot.Application.Products.Command.CreateProduct;
using Iot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Devices.Commands
{
    using static DatabaseFixture;
    
    [Collection("DatabaseCollection")]
    public class CreateDeviceTests
    {
        public CreateDeviceTests()
        {
            ResetState().GetAwaiter().GetResult();
        }

        [Fact]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateDeviceCommand();

            FluentActions.Invoking(() => SendAsync(command))
                .Should()
                .ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task ShouldCreateDevice()
        {
            var productId = await SendAsync(new CreateProductCommand
            {
                Code = "product code",
                Name = "product name",
                Secret = "123qwe"
            });

            var command = new CreateDeviceCommand
            {
                Code = "device code111",
                Name = "device name111",
                Secret = "234wer111",
                ProductId = productId
            };

            var deviceId = await SendAsync(command);
            var device = await FindAsync<Device>(deviceId);

            device.Should().NotBeNull();
            device.ProductId.Should().Be(command.ProductId);
            device.Name.Should().Be(command.Name);
            device.Code.Should().Be(command.Code); 
            device.Secret.Should().Be(command.Secret); 
        }
    }
}
