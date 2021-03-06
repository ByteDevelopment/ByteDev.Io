﻿using System.IO;
using System.Reflection;
using ByteDev.Io.FileCommands.FileCopyCommands;
using ByteDev.Testing.NUnit;
using NUnit.Framework;

namespace ByteDev.Io.IntTests.FileCommands.FileCopyCommands
{
    [TestFixture]
    public class FileCopyExistsDoNothingCommandTests : FileCommandTestBase
    {
        [SetUp]
        public void SetUp()
        {
            SetupWorkingDir(MethodBase.GetCurrentMethod().DeclaringType.ToString());

            SetupSourceDir();
            SetupDestinationDir();
        }

        [Test]
        public void WhenDestinationFileDoesNotExist_ThenCopyFile()
        {
            var sourceFile = CreateSourceFile(FileName1);

            var result = Act(sourceFile.FullName, Path.Combine(DestinationDir, FileName1));

            AssertFile.Exists(sourceFile);
            AssertFile.Exists(result);
        }

        [Test]
        public void WhenDestinationFileExists_ThenDoNothing()
        {
            var sourceFile = CreateSourceFile(FileName1, 1);
            var destinationFile = CreateDestinationFile(FileName1, 10);

            var result = Act(sourceFile.FullName, destinationFile.FullName);

            AssertFile.SizeEquals(sourceFile, 1);
            AssertFile.SizeEquals(result, 10);
        }

        private FileInfo Act(string sourceFile, string destinationFile)
        {
            var command = new FileCopyExistsDoNothingCommand(sourceFile, destinationFile);

            command.Execute();

            return command.DestinationFileResult;
        }

        private void SetupWorkingDir(string methodName)
        {
            var type = MethodBase.GetCurrentMethod().DeclaringType;
            SetWorkingDir(type, methodName);
        }
    }
}