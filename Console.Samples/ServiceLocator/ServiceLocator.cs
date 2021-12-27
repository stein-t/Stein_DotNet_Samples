using Console.Samples.Services;
using Microsoft.Extensions.DependencyInjection;
using Samples.Services.FileSystemCompareService;
using Samples.Services.TextTokenizerService;
using System;

namespace Console.Samples.ServiceLocator
{
    public class ServiceLocator : IServiceLocator
    {
        public static IServiceLocator Create()
        {
            // add services
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ITokenizerService, TokenizerService>()
                .AddSingleton<TokenizerConsoleService>()
                .AddSingleton<IFileSystemCompareService, FileSystemCompareService>()
                .AddSingleton<FileSystemCompareConsoleService>()
                .BuildServiceProvider();
            return new ServiceLocator(serviceProvider);
        }

        private readonly IServiceProvider _serviceProvider;
        private ServiceLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IConsoleService Tokenizer
        {
            get
            {
                return _serviceProvider?.GetService<TokenizerConsoleService>();
            }
        }

        public IConsoleService FilesystemDiffSimulator
        {
            get
            {
                return _serviceProvider?.GetService<FileSystemCompareConsoleService>();
            }
        }
    }
}
