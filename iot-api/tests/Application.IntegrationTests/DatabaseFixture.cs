using Iot.Application;
using Iot.Data;
using Iot.Identity;
using Iot.Shared;
using Iot.WebApi.Extensions;
using Iot.WebApi.Filter;
using Iot.WebApi.Helpers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Moq;
using Respawn;
using Respawn.Graph;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests
{
    public class DatabaseFixture : IDisposable
    {
        private static IConfigurationRoot _config;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkPoint;


        public DatabaseFixture() 
        {

            //构建配置
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _config = builder.Build();

            //服务注册
            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "Iot.WebApi"));

            services.AddLogging();

            services.AddApplication(_config);
            services.AddInfrastructureData(_config);

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _checkPoint = new Checkpoint
            {
                TablesToIgnore = new Table[] { "__EFMigrationsHistory" }
            };

            EnsureDatabase();
        }

        /// <summary>
        /// 执行数据库迁移
        /// </summary>
        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IotDbContext>();
            context.Database.Migrate();
        }

        /// <summary>
        /// 重置数据库状态
        /// </summary>
        /// <returns></returns>
        public static async Task ResetState()
        {
            await _checkPoint.Reset(_config.GetConnectionString("DefaultConnection"));
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();
            return await mediator.Send(request);
        }

        public static async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IotDbContext>();
            context.Add(entity);
            await context.SaveChangesAsync();
        }

        public static async Task<TEntity> FindAsync<TEntity>(int id)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IotDbContext>();

            return await context.FindAsync<TEntity>(id);
        }

        public void Dispose()
        {
            
        }
    }
}
