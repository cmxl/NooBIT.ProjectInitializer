using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Initializer.Services
{
    public class WebService
    {
        public async Task CreateGitIgnore(DirectoryInfo path)
        {
            var gitignore = System.IO.Path.Combine(path.FullName, ".gitignore");
            using (var client = new HttpClient())
            {
                var content = await client.GetStringAsync("https://raw.githubusercontent.com/github/gitignore/master/VisualStudio.gitignore");
                using (var sw = File.CreateText(gitignore))
                {
                    sw.Write(content);
                }
            }
        }
    }
}
