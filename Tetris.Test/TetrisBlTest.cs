using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Tetris.BL;

namespace Tetris.Test
{
    [TestClass]
    public class TetrisBlTest
    {
        private TetrisBL _tetris;
        [TestInitialize]
        public void Init()
        {
            Shape[] shapes = new Shape[] {new Square(), new Lightning1(),
                new Lightning2(), new Line()
            };
            _tetris = new TetrisBL(20, 30, shapes);

        }

        [TestMethod]
        public void PlaceShapeOnMap()
        {
            //Arrange
            _tetris.xShape = 0;
            _tetris.CurrentShape = 0;

            //Act
            _tetris.PlaceShapeOnMap();

            //Assert
            Assert.IsTrue(_tetris.Map[0, 0]);
            Assert.IsTrue(_tetris.Map[1, 0]);
            Assert.IsTrue(_tetris.Map[1, 1]);
            Assert.IsTrue(_tetris.Map[0, 1]);
            Assert.IsTrue(_tetris.Map[0, 2]);
            Assert.IsTrue(_tetris.Map[1, 2]);
            Assert.IsTrue(_tetris.Map[2, 0]);
            Assert.IsTrue(_tetris.Map[2, 1]);
            Assert.IsTrue(_tetris.Map[2, 2]);
        }

        [TestMethod]
        public void CurrentShapeInRange()
        {
            Assert.IsTrue(_tetris.CurrentShape < _tetris.shapes.Length);
            Assert.IsTrue(_tetris.CurrentShape >= 0);
        }

        [TestMethod]
        public void ShapeOnMap()
        {
            CollectionAssert.Contains(_tetris.Map, true);
        }

        [TestMethod]
        public void ClearShape()
        {
            //Act
            _tetris.ClearShape();

            //Assert
            CollectionAssert.DoesNotContain(_tetris.Map, true);
        }

        [TestMethod]
        public void ClearShapeOnlyCurrentShape()
        {
            //Arrenge
            //Place true all over the map
            for(int y = 0; y < _tetris.Map.GetLength(0); y++)
            {
                for(int x = 0; x < _tetris.Map.GetLength(1); x++)
                {
                    _tetris.Map[y, x] = true;
                }
            }
            _tetris.yShape = 0;
            _tetris.xShape = 0;
            _tetris.CurrentShape = 1;
            _tetris.PlaceShapeOnMap();

            //Act
            _tetris.ClearShape();

            //Assert
            Assert.IsFalse(_tetris.Map[0, 0]);
            Assert.IsFalse(_tetris.Map[1, 0]);
            Assert.IsFalse(_tetris.Map[1, 1]);
            Assert.IsFalse(_tetris.Map[1, 2]);
            Assert.IsFalse(_tetris.Map[2, 2]);
        }

        [TestMethod]
        public void CheckForCollision()
        {
            //Arrange
            _tetris.ClearShape();
            _tetris.yShape = 0;
            _tetris.xShape = 0;
            _tetris.CurrentShape = 2;
          //  _tetris.PlaceShapeOnMap();
            _tetris.Map[2, 0] = true;

            //Act
            bool actual = _tetris.CheckForCollision(1, 0);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsFullLineFalse()
        {
            //Act
            int actual = _tetris.IsFullLine();

            //Assert
            Assert.AreEqual(-1, actual);
        }

        [TestMethod]
        public void IsFullLineTrue()
        {
            //Arrange
            int y = 29;
            for(int x = 0; x < _tetris.Map.GetLength(1); x++)
            {
                _tetris.Map[y, x] = true;
            }

            //Act
            int actual = _tetris.IsFullLine();

            //Assert
            Assert.AreEqual(y, actual);
        }

    [TestMethod]
    public void SliceLineFullTrueMap()
    {
            //Arrange
            int yLine = 1;
            for (int y = 0; y < _tetris.Map.GetLength(0); y++)
            {
                for (int x = 0; x < _tetris.Map.GetLength(1); x++)
                {
                    if (y == yLine)
                        _tetris.Map[y, x] = true;
                    else
                        _tetris.Map[y, x] = false;
                }
            }

            //Act
            _tetris.SliceLine(yLine);

            //Assert
            Assert.IsFalse(_tetris.Map[yLine, 0]);
            Assert.IsFalse(_tetris.Map[yLine, 1]);
            Assert.IsFalse(_tetris.Map[yLine, 3]);
            Assert.IsFalse(_tetris.Map[yLine, 4]);
            Assert.IsFalse(_tetris.Map[yLine, 5]);

    }


    }





}
