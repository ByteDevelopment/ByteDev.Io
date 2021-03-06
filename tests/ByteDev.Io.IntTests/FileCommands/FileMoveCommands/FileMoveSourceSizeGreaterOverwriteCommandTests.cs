﻿using System;
using System.IO;
using System.Reflection;
using ByteDev.Io.FileCommands.FileMoveCommands;
using ByteDev.Testing.NUnit;
using NUnit.Framework;

namespace ByteDev.Io.IntTests.FileCommands.FileMoveCommands
{
    [TestFixture]
    public class FileMoveSourceSizeGreaterOverwriteCommandTests : FileCommandTestBase
    {
        [SetUp]
        public void SetUp()
        {
            SetupWorkingDir(MethodBase.GetCurrentMethod().DeclaringType.ToString());

            SetupSourceDir();
            SetupDestinationDir();
        }

        [Test]
        public void WhenDestinationFileDoesNotExist_ThenMoveFile()
        {
            var sourceFile = CreateSourceFile(FileName1);

            var result = Act(sourceFile.FullName, Path.Combine(DestinationDir, FileName1));

            AssertFile.NotExists(sourceFile);
            AssertFile.Exists(result);
        }

        [Test]
        public void WhenSourceFileDoesNotExist_ThenThrowException()
        {
            Assert.Throws<FileNotFoundException>(() => Act(Path.Combine(SourceDir, FileName1), Path.Combine(DestinationDir, FileName1)));
        }

        [Test]
        public void WhenDestinationFileIsSmaller_ThenOverwriteWithSourceFile()
        {
            const int sourceSize = 3;

            var sourceFile = CreateSourceFile(FileName1, sourceSize);
            var destinationFile = CreateDestinationFile(FileName1, sourceSize - 1);

            var result = Act(sourceFile.FullName, destinationFile.FullName);

            AssertFile.NotExists(sourceFile);
            AssertFile.SizeEquals(result, sourceSize);
        }

        [Test]
        public void WhenDestinationFileIsBigger_ThenThrowException()
        {
            const int destinationSize = 3;

            var sourceFile = CreateSourceFile(FileName1, destinationSize - 1);
            var destinationFile = CreateDestinationFile(FileName1, destinationSize);

            Assert.Throws<InvalidOperationException>(() => Act(sourceFile.FullName, destinationFile.FullName));
        }

        private FileInfo Act(string sourceFile, string destinationFile)
        {
            var command = new FileMoveSourceSizeGreaterOverwriteCommand(sourceFile, destinationFile);

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