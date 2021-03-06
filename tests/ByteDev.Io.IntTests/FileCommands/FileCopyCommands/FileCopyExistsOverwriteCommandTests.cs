﻿using System.IO;
using System.Reflection;
using ByteDev.Io.FileCommands.FileCopyCommands;
using ByteDev.Testing.NUnit;
using NUnit.Framework;

namespace ByteDev.Io.IntTests.FileCommands.FileCopyCommands
{
    [TestFixture]
    public class FileCopyExistsOverwriteCommandTests : FileCommandTestBase
    {
        [SetUp]
        public void SetUp()
        {
            SetupWorkingDir(MethodBase.GetCurrentMethod().DeclaringType.ToString());

            SetupSourceDir();
            SetupDestinationDir();
        }

        [Test]
        public void WhenSourceFileExists_ThenCopyFile()
        {
            var sourceFile = CreateSourceFile(FileName1);

            var result = Act(sourceFile.FullName, Path.Combine(DestinationDir, FileName1));

            AssertFile.Exists(sourceFile);
            AssertFile.Exists(result);
        }

        [Test]
        public void WhenSourceFileDoesNotExist_ThenThrowException()
        {
            Assert.Throws<FileNotFoundException>(() => Act(Path.Combine(SourceDir, FileName1), Path.Combine(DestinationDir, FileName1)));
        }

        [Test]
        public void WhenDestinationFileAlreadyExists_ThenOverwriteFile()
        {
            var sourceFile = CreateSourceFile(FileName1, 1);
            var destinationFile = CreateDestinationFile(FileName1, 10);

            var result = Act(sourceFile.FullName, destinationFile.FullName);

            AssertFile.SizeEquals(sourceFile, 1);
            AssertFile.SizeEquals(result, 1);
        }

        private FileInfo Act(string sourceFile, string destinationFile)
        {
            var command = new FileCopyExistsOverwriteCommand(sourceFile, destinationFile);

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