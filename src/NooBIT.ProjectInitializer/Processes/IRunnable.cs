using System.Threading.Tasks;

namespace NooBIT.ProjectInitializer
{
    public interface IRunnable
    {
        int Run();
        Task<int> RunAsync();
    }
}
