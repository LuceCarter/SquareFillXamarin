//using NUnit.Framework;
using SquareFillDomain.Builders;
using SquareFillDomain.Models;
using SquareFillDomain.UnitTests.TestUtils;
using SquareFillDomain.Utils;

namespace SquareFillDomain.UnitTests
{
    [TestFixture]
    public class SquareTests
    {        
		[Test]
		public void TestCentreOfSquareIsDefinedAsInsideSquare() {
			// Arrange
			var centreOfSquare = new SquareFillPoint(
				x: TestConstants.SquareWidth/2,
                y: TestConstants.SquareWidth/2);
		    var topLeftCorner = new SquareFillPoint(x: 0, y: 0);
			var square = new Square();
            square.CalculateTopLeftCorner(parentTopLeftCorner: topLeftCorner);
			
			// Act
			var isInSquare = square.IsInSquare(point: centreOfSquare);
			
			// Assert
			Asserter.AreEqual(isInSquare, true);
		}
		
		[Test]
		public void TestAnyLocationInSquareIsDefinedAsInsideSquare() {
			// Arrange
		    var topLeftCorner = new SquareFillPoint(x: 0, y: 0);
            var square = new Square();
		    square.CalculateTopLeftCorner(parentTopLeftCorner: topLeftCorner);
            var pointInQuestion = new SquareFillPoint(x: topLeftCorner.X + 10, y: topLeftCorner.Y + 11);
			
			// Act
			var isInSquare = square.IsInSquare(point: pointInQuestion);
			
			// Assert
			Asserter.AreEqual(isInSquare, true);
		}
		
		[Test]
		public void TestAnyLocationOutsideSquareIsNotDefinedAsInsideSquare() {
			// Arrange
		    var topLeftCorner = new SquareFillPoint(x: 0, y: 0);
			var square = new Square();
		    square.CalculateTopLeftCorner(parentTopLeftCorner: topLeftCorner);
            var pointInQuestion = new SquareFillPoint(x: topLeftCorner.X + 50, y: topLeftCorner.Y - 10);
			
			// Act
			var isInSquare = square.IsInSquare(point: pointInQuestion);
			
			// Assert
			Asserter.AreEqual(isInSquare, false);
		}

        [Test]
        public void TestTopLeftCornerIsCalculatedAsParentCornerAdjustedByRelativePosition()
        {
            // Arrange
            var parentTopLeftCorner = new SquareFillPoint(
                x: 4 * TestConstants.SquareWidth,
                y: 4 * TestConstants.SquareWidth);
            var square = new Square(positionRelativeToParentCorner: new SquareFillPoint(x: -2, y: -3), sprite: null);

            // Act
            square.CalculateTopLeftCorner(parentTopLeftCorner: parentTopLeftCorner);

            // Assert
            Asserter.AreEqual(square.TopLeftCornerX, parentTopLeftCorner.X
                + (square.XRelativeToParentCorner * TestConstants.SquareWidth));
            Asserter.AreEqual(square.TopLeftCornerY, parentTopLeftCorner.Y
                + (square.YRelativeToParentCorner * TestConstants.SquareWidth));
        }

        [Test]
        public void TestPotentialTopLeftCornerIsCalculatedAsParentCornerAdjustedByRelativePosition()
        {
            // Arrange
            var parentTopLeftCorner = new SquareFillPoint(
                x: 4 * TestConstants.SquareWidth,
                y: 4 * TestConstants.SquareWidth);
            var square = new Square(positionRelativeToParentCorner: new SquareFillPoint(x: -2, y: -3), sprite: null);

            // Act
            SquareFillPoint result = square.CalculatePotentialTopLeftCorner(parentTopLeftCorner: parentTopLeftCorner);

            // Assert
            Asserter.AreEqual(result.X, parentTopLeftCorner.X + (square.XRelativeToParentCorner * TestConstants.SquareWidth));
            Asserter.AreEqual(result.Y, parentTopLeftCorner.Y + (square.YRelativeToParentCorner * TestConstants.SquareWidth));
        }
	}
}