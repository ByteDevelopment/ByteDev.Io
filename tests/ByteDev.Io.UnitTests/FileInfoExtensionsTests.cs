﻿using System;
using System.IO;
using NUnit.Framework;

namespace ByteDev.Io.UnitTests
{
    public class FileInfoExtensionsTests
    {
        [TestFixture]
        public class GetNextAvailableFileName : FileInfoExtensionsTests
        {
            [Test]
            public void WhenIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => FileInfoExtensions.GetNextAvailableFileName(null));
            }
        }

        [TestFixture]
        public class HasExtension : FileInfoExtensionsTests
        {
            [Test]
            public void WhenIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => FileInfoExtensions.HasExtension(null));
            }

            [Test]
            public void WhenFileHasExtension_ThenReturnTrue()
            {
                var sut = new FileInfo(@"C:\Temp\Test.txt");

                var result = sut.HasExtension();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenFileHasNoExtension_ThenReturnFalse()
            {
                var sut = new FileInfo(@"C:\Temp\Test");

                var result = sut.HasExtension();

                Assert.That(result, Is.False);
            }
        }

        [TestFixture]
        public class GetExtension : FileInfoExtensionsTests
        {
            [Test]
            public void WhenIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => FileInfoExtensions.GetExtension(null));
            }

            [Test]
            public void WhenFileHasNoExtension_ThenReturnEmpty()
            {
                var sut = new FileInfo(@"C:\Temp\Test");

                var result = sut.GetExtension();

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenFileHasExtension_ThenReturnExtension()
            {
                var sut = new FileInfo(@"C:\Temp\Test.txt");

                var result = sut.GetExtension();

                Assert.That(result, Is.EqualTo(".txt"));
            }

            [Test]
            public void WhenFileHasExtension_AndNotIncludeDotPrefix_ThenReturnExtension()
            {
                var sut = new FileInfo(@"C:\Temp\Test.txt");

                var result = sut.GetExtension(false);

                Assert.That(result, Is.EqualTo("txt"));
            }

            [Test]
            public void WhenFileHasNoExtension_AndNotIncludeDotPrefix_ThenReturnEmpty()
            {
                var sut = new FileInfo(@"C:\Temp\Test");

                var result = sut.GetExtension(false);

                Assert.That(result, Is.Empty);
            }
        }
        
        [TestFixture]
        public class AddExtension : FileInfoExtensionsTests
        {
            [Test]
            public void WhenIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => FileInfoExtensions.AddExtension(null, ".txt"));
            }

            [Test]
            public void WhenExtensionIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => new FileInfo(@"C:\").AddExtension(null));
            }
        }

        [TestFixture]
        public class AddOrRenameExtension : FileInfoExtensionsTests
        {
            [Test]
            public void WhenIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => FileInfoExtensions.AddOrRenameExtension(null, ".txt"));
            }

            [Test]
            public void WhenNewExtensionIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => new FileInfo(@"C:\").AddOrRenameExtension(null));
            }
        }
    }
}