using System;
using System.IO;

namespace Initializer.Services
{
    public class DirectoryService
    {
        private static readonly string _srcDirName = "src";
        private static readonly string _testDirName = "test";
        private static readonly string _samplesDirName = "samples";
        private static readonly string _docsDirName = "docs";
        private static readonly string _toolsDirName = "tools";

        private readonly DirectoryInfo _workingDirectory;

        public DirectoryService(string workingDirectory, string projectName)
        {
            _workingDirectory = new DirectoryInfo(workingDirectory ?? Environment.CurrentDirectory);
            ProjectDirectory = Create(_workingDirectory, projectName);
        }

        private DirectoryInfo Create(DirectoryInfo dir, string subDirectory) => dir.CreateSubdirectory(subDirectory);

        public DirectoryInfo ProjectDirectory { get; }

        private DirectoryInfo _src;
        public DirectoryInfo Src => _src ?? (_src = Create(ProjectDirectory, _srcDirName));

        private DirectoryInfo _test;
        public DirectoryInfo Test => _test ?? (_test = Create(ProjectDirectory, _testDirName));

        private DirectoryInfo _samples;
        public DirectoryInfo Samples => _samples ?? (_samples = Create(ProjectDirectory, _samplesDirName));

        private DirectoryInfo _docs;
        public DirectoryInfo Docs => _docs ?? (_docs = Create(ProjectDirectory, _docsDirName));

        private DirectoryInfo _tools;
        public DirectoryInfo Tools => _tools ?? (_tools = Create(ProjectDirectory, _toolsDirName));


        public void CreateAll()
        {
            var src = Src;
            src = Test;
            src = Samples;
            src = Docs;
            src = Tools;
        }
    }
}
