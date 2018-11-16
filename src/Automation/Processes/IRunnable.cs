using System.Threading.Tasks;

namespace Initializer
{
    public interface IRunnable
    {
        int Run();
        Task<int> RunAsync();
    }
}
