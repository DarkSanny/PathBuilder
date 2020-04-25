using System.Linq;
using NUnit.Framework;
using PathBuilder.v2;

namespace Tests
{
    [TestFixture]
    public class BfsWith2DGraphTest
    {
        [Test]
        public void TestName()
        {
            var field = new bool[,]
            {
                {false, false, false, false, false},
                {false, false, false, true, false},
                {false, true, true, true, false},
                {false, false, false, true, false},
                {false, false, false, true, false}
            };
            var graph = new Field2DGraph(field);
            var fromNode = graph[4, 0];
            var toNode = graph[4, 4];
            var bfs = new BFS();
            var result = bfs.FindPath(fromNode, toNode).ToList();
        }
    }
}