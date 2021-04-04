using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Tetris.BL;

namespace Tetris.Test
{
    [TestClass]
    public class ShapeTest //Class should be public!!! Otherwise this class won't be available for 
    {
        [TestMethod]
        public void TestShapeLineCoordinates()
        {
            //Arrange
            Line line = new Line();
            List<Tuple<int, int>> expectedCords = new List<Tuple<int, int>> { 
                new Tuple<int, int>(0,1),
                new Tuple<int, int>(1,1),
                new Tuple<int, int>(2,1),
            };
          
            //Act
            var coords = line.Coordinates;


            //Assert
            CollectionAssert.AreEqual(coords, expectedCords);
        }

        [TestMethod]
        public void GetCordsTest()
        {
            //Arrange
            Square square = new Square();
            List<int> expected = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

            //Act
            List<int> actual = square.cords;

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
