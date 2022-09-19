using FluentAssertions;
using Iot.Application.Common.Exceptions;
using Iot.Application.Products.Command.CreateProduct;
using Iot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Products.Commands
{
    using static DatabaseFixture;
    [Collection("DatabaseCollection")]
    public class CreateProductTests
    {
        public CreateProductTests()
        {
            ResetState().GetAwaiter().GetResult();
        }

        [Fact]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateProductCommand();

            FluentActions.Invoking(() => SendAsync(command))
                .Should()
                .ThrowAsync<ValidationException>();
        }

        [Fact]
        public void ShouldRequireSecret()
        {
            var command = new CreateProductCommand
            {
                Code = "ele3",
                Name = "电梯",
                Secret = "123qwe"
            };

            FluentActions.Invoking(() => SendAsync(command))
                .Should()
                .ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task ShouldCreateProucts()
        {
            var command = new CreateProductCommand
            {
                Code = "ele",
                Name = "电梯",
                Secret = "123qwe"
            };

            var id = await SendAsync(command);

            var list = await FindAsync<Product>(id);

            list.Should().NotBeNull();
            list.Code.Should().Be(command.Code);
            list.Name.Should().Be(command.Name);
            list.Secret.Should().Be(command.Secret);
        }
    }
}
