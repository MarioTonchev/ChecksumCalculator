using ChecksumCalculator.Composite;

namespace ChecksumCalculator.Tests.Composite
{
    [TestFixture]
    public class CompositeTreeTests
    {
        [Test]
        public void DirectorySize_IsSumOfChildren()
        {
            var file1 = new FileNode("a.txt", 10);
            var file2 = new FileNode("b.txt", 20);

            var dir = new DirectoryNode("dir");
            dir.AddChild(file1);
            dir.AddChild(file2);
            
            Assert.That(dir.Size, Is.EqualTo(30));
        }
    }
}
