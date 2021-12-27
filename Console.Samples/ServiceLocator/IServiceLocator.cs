using Console.Samples.Services;

namespace Console.Samples.ServiceLocator
{
    public interface IServiceLocator
    {
        IConsoleService Tokenizer { get; }
        IConsoleService FilesystemDiffSimulator { get; }
    }
}
