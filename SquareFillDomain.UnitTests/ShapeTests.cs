//using NUnit.Framework;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SquareFillDomain.Interfaces;
using SquareFillDomain.Models;
using SquareFillDomain.UnitTests.TestUtils;
using SquareFillDomain.Utils;
using Linq = System.Collections.Generic; 

namespace SquareFillDomain.UnitTests
{
    // class ShapeTests: XCTestCase
    [TestClass]
    public class ShapeTests
    {
        // These all have to be initialised in XCode to keep Swift happy.
        private Linq.List<Square> _simpleSingleSquareList;
        private Linq.List<Square> _rightHydrantSquareList;
        private Linq.List<Square> _crossShapeSquareList;
        private Linq.List<Square> _threePoleSquareList;
        private Linq.List<Square> _twoPoleSquareList;
        private Linq.List<Square> _fourBarSquareList;
        private Linq.List<Square> _nineSquareSquareList;
        private Linq.List<Square> _leftHydrantSquareList;
        private Linq.List<Square> _upsideDownTSquareList;
        private Linq.List<Square> _rightWayUpTList;

        private readonly Grid _occupiedGridSquares = TestConstants.MakeGridSquares();

        // These all have to be initialised in XCode to keep Swift happy 
        // (which means they have to be of type Shape!, and then initialised to nil).
        private readonly Shape _shape01;
        private readonly Shape _shape02;
        private readonly Shape _shape03;
        private readonly Shape _shape04;
        private readonly Shape _centralShape;
        private readonly Shape _shape06;
        private readonly Shape _shape07;
        private readonly Shape _shape08;
        private readonly Shape _shape09;

        private SquareFillPoint SquareFillPoint(int x, int y)
        {
            return new SquareFillPoint(x: x, y: y);
        }

        private ISquareView MockSquareView()
        {
            return new MockSquareView();
        }

        private Shape Shape(
            SquareFillPoint topLeftCorner,
            Linq.List<Square> squareDefinitions,
            bool topLeftCornerIsGridRef = true)
        {
            return new Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: squareDefinitions,
                topLeftCornerIsGridRef: topLeftCornerIsGridRef);
        }

        private Square Square(SquareFillPoint positionRelativeToParentCorner, ISquareView sprite)
        {
            return new Square(
                positionRelativeToParentCorner: positionRelativeToParentCorner,
                sprite: sprite);
        }

        // private func CreateSimpleSingleSquareList() -> [Square]
        private Linq.List<Square> CreateSimpleSingleSquareList()
        {
            // return [
            return new Linq.List<Square>
            {
                Square(
                    positionRelativeToParentCorner: SquareFillPoint(x: 0, y: 0), 
                    sprite: MockSquareView())
            };
            // ];
        }

        // override func tearDown() 
        // {
        //      // This method is called after the invocation of each test method in the class.
        //      super.tearDown();
        // }

        // override func setUp() 
        // {
        //      // This method is called before the invocation of each test method in the class.
        //      super.setUp();
        //      !! The code in the constructor (below) has to be moved in here for Swift
        [TestInitialize]
        public void Setup()
        {
            _occupiedGridSquares.VacateAllSquares();
            var squareFactory = new MockSquareFactory();
            var testShapeSetBuilder = new TestShapeSetBuilder(squareViewFactory: new MockSquareFactory());

            _simpleSingleSquareList = testShapeSetBuilder.MakeSquares(
                colour: SquareFillColour.Red,
                relativePointsTopLeftCorner: ShapeConstants.SingleSquarePoints,
                squareFactory: squareFactory);

            _rightHydrantSquareList = testShapeSetBuilder.MakeSquares(
                colour: SquareFillColour.Red,
                relativePointsTopLeftCorner: ShapeConstants.RightHydrantPoints,
                squareFactory: squareFactory);

            _crossShapeSquareList = testShapeSetBuilder.MakeSquares(
                colour: SquareFillColour.Red,
                relativePointsTopLeftCorner: ShapeConstants.CrossShapePoints,
                squareFactory: squareFactory);

            _threePoleSquareList = testShapeSetBuilder.MakeSquares(
                colour: SquareFillColour.Red,
                relativePointsTopLeftCorner: ShapeConstants.ThreePolePoints,
                squareFactory: squareFactory);

            _twoPoleSquareList = testShapeSetBuilder.MakeSquares(
                colour: SquareFillColour.Red,
                relativePointsTopLeftCorner: ShapeConstants.TwoPolePoints,
                squareFactory: squareFactory);

            _fourBarSquareList = testShapeSetBuilder.MakeSquares(
                colour: SquareFillColour.Red,
                relativePointsTopLeftCorner: ShapeConstants.FourBarPoints,
                squareFactory: squareFactory);

            _nineSquareSquareList = testShapeSetBuilder.MakeSquares(
                colour: SquareFillColour.Red,
                relativePointsTopLeftCorner: ShapeConstants.NineSquarePoints,
                squareFactory: squareFactory);

            _leftHydrantSquareList = testShapeSetBuilder.MakeSquares(
                colour: SquareFillColour.Red,
                relativePointsTopLeftCorner: ShapeConstants.LeftHydrantPoints,
                squareFactory: squareFactory);

            _upsideDownTSquareList = testShapeSetBuilder.MakeSquares(
                colour: SquareFillColour.Red,
                relativePointsTopLeftCorner: ShapeConstants.UpsideDownTPoints,
                squareFactory: squareFactory);

            _rightWayUpTList = testShapeSetBuilder.MakeSquares(
                colour: SquareFillColour.Red,
                relativePointsTopLeftCorner: ShapeConstants.RightWayUpTPoints,
                squareFactory: squareFactory);
        }

        public ShapeTests()
        {
            // !! In Swift, all this code has to go in the setUp method (above). 
            _shape01 = Shape(topLeftCorner: SquareFillPoint(x: 0, y: 0), squareDefinitions: CreateSimpleSingleSquareList());
            _shape02 = Shape(topLeftCorner: SquareFillPoint(x: 1, y: 0), squareDefinitions: CreateSimpleSingleSquareList());
            _shape03 = Shape(topLeftCorner: SquareFillPoint(x: 2, y: 0), squareDefinitions: CreateSimpleSingleSquareList());
            _shape04 = Shape(topLeftCorner: SquareFillPoint(x: 0, y: 1), squareDefinitions: CreateSimpleSingleSquareList());
            _centralShape = Shape(topLeftCorner: SquareFillPoint(x: 1, y: 1), squareDefinitions: CreateSimpleSingleSquareList());
            _shape06 = Shape(topLeftCorner: SquareFillPoint(x: 2, y: 1), squareDefinitions: CreateSimpleSingleSquareList());
            _shape07 = Shape(topLeftCorner: SquareFillPoint(x: 0, y: 2), squareDefinitions: CreateSimpleSingleSquareList());
            _shape08 = Shape(topLeftCorner: SquareFillPoint(x: 1, y: 2), squareDefinitions: CreateSimpleSingleSquareList());
            _shape09 = Shape(topLeftCorner: SquareFillPoint(x: 2, y: 2), squareDefinitions: CreateSimpleSingleSquareList());
        }

        [TestMethod]
		public void TestCentreOfShapeIsDefinedAsInsideShape() 
        {
			// Arrange
			var centreOfShape = SquareFillPoint(
				x: TestConstants.SquareWidth / 2, 
				y: TestConstants.SquareWidth / 2);
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _simpleSingleSquareList);
			
			// Act
			var isInShape = shape.IsInShape(point: centreOfShape);
			
			// Assert
			Asserter.AreEqual(actual: isInShape, expected: true);
		}

        [TestMethod]
        public void TestAnyLocationInCentralSquareIsDefinedAsInsideShape()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _simpleSingleSquareList);
            var pointInQuestion = SquareFillPoint(x: topLeftCorner.X + 10, y: topLeftCorner.Y + 11);

            // Act
            var isInShape = shape.IsInShape(point: pointInQuestion);

            // Assert
            Asserter.AreEqual(actual: isInShape, expected: true);
        }

        [TestMethod]
        public void TestAnyLocationOutsideShapeIsNotDefinedAsInsideShape()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _simpleSingleSquareList );
            var pointInQuestion = SquareFillPoint(x: topLeftCorner.X + 10, y: topLeftCorner.Y - 10);

            // Act
            var isInShape = shape.IsInShape(point: pointInQuestion);

            // Assert
            Asserter.AreEqual(actual: isInShape, expected: false);
        }

        [TestMethod]
        public void TestAnyLocationInNonCornerSquareIsDefinedAsInsideShape()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _rightHydrantSquareList);
            var pointInQuestion = SquareFillPoint(
                x: topLeftCorner.X + TestConstants.SquareWidth + 10,
                y: topLeftCorner.Y + TestConstants.SquareWidth + 10);

            // Act
            var isInShape = shape.IsInShape(point: pointInQuestion);

            // Assert
            Asserter.AreEqual(actual: isInShape, expected: true);
        }

        [TestMethod]
        public void TestAnyLocationInNonCentralSquareIsDefinedAsInsideShape()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _rightHydrantSquareList);
            var pointInQuestion = SquareFillPoint(
                x: topLeftCorner.X + 10,
                y: topLeftCorner.Y + 10);

            // Act
            var isInShape = shape.IsInShape(point: pointInQuestion);

            // Assert
            Asserter.AreEqual(actual: isInShape, expected: true);
        }

        [TestMethod]
        public void TestWhenShapeIsMovedToNewLocationThenAllSpritesArePlacedRelativeToTopLeftCorner()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 1, y: 0);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _crossShapeSquareList);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + 80,
                y: topLeftCorner.Y + 90);

            // Act
            shape.UpdateTopLeftCorner(newTopLeftCorner: newTopLeftCorner);

            // Assert
            var start = 0;
            var end = _crossShapeSquareList.Count - 1;
            for (int count = start; count <= end; count++) {
                Asserter.AreEqual(actual: _crossShapeSquareList[count].SpriteCornerX,
                    expected: newTopLeftCorner.X + (ShapeConstants.CrossShapePoints[count].X * TestConstants.SquareWidth));
                Asserter.AreEqual(actual: _crossShapeSquareList[count].SpriteCornerY,
                    expected: newTopLeftCorner.Y + (ShapeConstants.CrossShapePoints[count].Y * TestConstants.SquareWidth));
            }
        }

        [TestMethod]
        public void TestNumSquaresLeftOfTopLeftCornerIsInitialisedAccordingToRelativePosition()
        {
            // Arrange
            var numSquaresLeftOfTopLeftCorner = 2;

            // Act
            var shape = Shape(
                topLeftCorner: SquareFillPoint(x: 0, y: 0),
                squareDefinitions: new Linq.List<Square> {
                    Square(positionRelativeToParentCorner: SquareFillPoint(x: -numSquaresLeftOfTopLeftCorner, y:0), sprite: MockSquareView()),
                    Square(positionRelativeToParentCorner: SquareFillPoint(x:0, y:0), sprite: MockSquareView())
                });

            // Assert
            Asserter.AreEqual(actual: shape.NumSquaresLeftOfTopLeftCorner, expected: numSquaresLeftOfTopLeftCorner);
        }

        [TestMethod]
        public void TestNumSquaresRightOfTopLeftCornerIsInitialisedAcordingToRelativePosition()
        {
            // Arrange
            var numSquaresRightOfTopLeftCorner = 2;

            // Act
            var shape = Shape(
                topLeftCorner: SquareFillPoint(x: 0, y: 0),
                squareDefinitions: new Linq.List<Square> {
                    Square(positionRelativeToParentCorner: SquareFillPoint(x: numSquaresRightOfTopLeftCorner, y:0), sprite: MockSquareView()),
                    Square(positionRelativeToParentCorner: SquareFillPoint(x:0, y:0), sprite: MockSquareView())
                });

            // Assert
            Asserter.AreEqual(actual: shape.NumSquaresRightOfTopLeftCorner, expected: numSquaresRightOfTopLeftCorner);
        }

        [TestMethod]
        public void TestNumSquaresAboveTopLeftCornerIsInitialisedAcordingToRelativePosition()
        {
            // Arrange
            var numSquaresAboveTopLeftCorner = 2;

            // Act
            var shape = Shape(
                topLeftCorner: SquareFillPoint(x: 0, y: 0),
                squareDefinitions: new Linq.List<Square> {
                    Square(positionRelativeToParentCorner: SquareFillPoint(x:0, y: -numSquaresAboveTopLeftCorner), sprite: MockSquareView()),
                    Square(positionRelativeToParentCorner: SquareFillPoint(x:0, y:0), sprite: MockSquareView())
                });

            // Assert
            Asserter.AreEqual(actual: shape.NumSquaresAboveTopLeftCorner, expected: numSquaresAboveTopLeftCorner);
        }

        [TestMethod]
        public void TestNumSquaresBelowTopLeftCornerIsInitialisedAcordingToRelativePosition()
        {
            // Arrange
            var numSquaresBelowTopLeftCorner = 2;

            // Act
            var shape = Shape(
                topLeftCorner: SquareFillPoint(x: 0, y: 0),
                squareDefinitions: new Linq.List<Square> {
                    Square(positionRelativeToParentCorner: SquareFillPoint(x:0, y: numSquaresBelowTopLeftCorner), sprite: MockSquareView()),
                    Square(positionRelativeToParentCorner: SquareFillPoint(x:0, y:0), sprite: MockSquareView())
                });

            // Assert
            Asserter.AreEqual(actual: shape.NumSquaresBelowTopLeftCorner, expected: numSquaresBelowTopLeftCorner);
        }

        [TestMethod]
        public void TestWeAreWithinTheContainingRectangleIfAllEdgesAreWithinIt()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(
                x: TestConstants.TopLeftGridSquare.X + TestConstants.SquareWidth,
                y: TestConstants.TopLeftGridSquare.Y + TestConstants.SquareWidth);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _rightHydrantSquareList,
                topLeftCornerIsGridRef: false);

            // Act
            var result = shape.WeStartedWithinTheContainingRectangle();

            // Assert
            Asserter.AreEqual(actual: result, expected: true);
        }

        [TestMethod]
        public void TestWeAreNotInTheContainingRectangleIfWeOverlapTheLeftEdge()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(
                x: TestConstants.TopLeftGridSquare.X - TestConstants.SquareWidth,
                y: TestConstants.TopLeftGridSquare.Y);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _rightHydrantSquareList,
                topLeftCornerIsGridRef: false);

            // Act
            var result = shape.WeStartedWithinTheContainingRectangle();

            // Assert
            Asserter.AreEqual(actual: result, expected: false);
        }

        [TestMethod]
        public void TestWeAreNotInTheContainingRectangleIfWeOverlapTheTopEdge()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(
                x: TestConstants.TopLeftGridSquare.X,
                y: TestConstants.TopLeftGridSquare.Y - TestConstants.SquareWidth);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _rightHydrantSquareList,
                topLeftCornerIsGridRef: false);

            // Act
            var result = shape.WeStartedWithinTheContainingRectangle();

            // Assert
            Asserter.AreEqual(actual: result, expected: false);
        }

        [TestMethod]
        public void TestWeAreNotInTheContainingRectangleIfWeOverlapTheRightEdge()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(
                x: TestConstants.TopLeftGridSquare.X + TestConstants.ContainingRectangle.Width,
                y: TestConstants.TopLeftGridSquare.Y);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _rightHydrantSquareList,
                topLeftCornerIsGridRef: false);

            // Act
            var result = shape.WeStartedWithinTheContainingRectangle();

            // Assert
            Asserter.AreEqual(actual: result, expected: false);
        }

        [TestMethod]
        public void TestWeAreNotInTheContainingRectangleIfWeOverlapTheBottomEdge()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(
                x: TestConstants.TopLeftGridSquare.X,
                y: TestConstants.TopLeftGridSquare.Y + TestConstants.ContainingRectangle.Height - TestConstants.SquareWidth);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _rightHydrantSquareList,
                topLeftCornerIsGridRef: false);

            // Act
            var result = shape.WeStartedWithinTheContainingRectangle();

            // Assert
            Asserter.AreEqual(actual: result, expected: false);
        }

        [TestMethod]
        public void TestVacateGridSquaresWillVacateOnlyTheSquaresOccupiedByTheShape()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _rightHydrantSquareList);
            foreach (var element in TestConstants.RightHydrantPoints) {
                _occupiedGridSquares.OccupyGridSquare(x: element.X, y: element.Y);
                _occupiedGridSquares.PlaceShapeInSquare(x: element.X, y: element.Y, shapeInSquare: shape);
            }
            // Occupy some other squares too, so we can check they're still occupied afterwards
            var start = 0;
            var end = _occupiedGridSquares.Height - 1;
            for (int count = start; count <= end; count++) {
                _occupiedGridSquares.OccupyGridSquare(x: _occupiedGridSquares.Width - 1, y: count);
            }

            // Act
            shape.VacateGridSquares(occupiedGridSquares: _occupiedGridSquares);

            // Assert
            var start1 = 0;
            var end1 = _occupiedGridSquares.Width - 2;
            for (int count1 = start1; count1 <= end1; count1++) {
                var start2 = 0;
                var end2 = _occupiedGridSquares.Height - 1;
                for (int count2 = start2; count2 <= end2; count2++) {
                    Asserter.AreEqual(actual: _occupiedGridSquares.IsSquareOccupied(x: count1, y: count2), expected: false);
                }
            }
            // Check the other occupied squares are still occupied
            start = 0;
            end = _occupiedGridSquares.Height - 1;
            for (int count = start; count <= end; count++) {
                Asserter.AreEqual(actual: _occupiedGridSquares.IsSquareOccupied(x: _occupiedGridSquares.Width - 1, y: count), expected: true);
            }
        }

        [TestMethod]
        public void TestOccupyGridSquaresWillOccupyOnlyTheSquaresOccupiedByTheShape()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList);

            // Act
            shape.OccupyGridSquares(occupiedGridSquares: _occupiedGridSquares);

            // Assert
            foreach (var element in TestConstants.ThreePolePoints) {
                Asserter.AreEqual(actual: _occupiedGridSquares.IsSquareOccupied(x: element.X, y: element.Y), expected: true);
            }
            var start = 3;
            var end = _occupiedGridSquares.Height - 1;
            for (int count = start; count <= end; count++) {
                Asserter.AreEqual(actual: _occupiedGridSquares.IsSquareOccupied(x: 0, y: count), expected: false);
            }
            var start1 = 1;
            var end1 = _occupiedGridSquares.Width - 1;
            for (int count1 = start1; count1 <= end1; count1++) {
                var start2 = 0;
                var end2 = _occupiedGridSquares.Height - 1;
                for (int count2 = start2; count2 <= end2; count2++) {
                    Asserter.AreEqual(actual: _occupiedGridSquares.IsSquareOccupied(x: count1, y: count2), expected: false);
                }
            }
        }

        [TestMethod]
        public void TestWillNotDetectCollisionWhenNewPositionIsAlongLeftEdgeOfGrid()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(x: 0, y: 2);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: false);
        }

        [TestMethod]
        public void TestWillNotDetectCollisionWhenNewPositionIsAlongRightEdgeOfGrid()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + TestConstants.ScreenWidth - TestConstants.SquareWidth,
                y: topLeftCorner.Y + 2 * TestConstants.SquareWidth);
            var xMovement = (TestConstants.ScreenWidth / TestConstants.SquareWidth) - 1;
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: false);
        }

        [TestMethod]
        public void TestWillNotDetectCollisionWhenNewPositionIsAlongTopEdgeOfGrid()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + 2 * TestConstants.SquareWidth,
                y: topLeftCorner.Y);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: false);
        }

        [TestMethod]
        public void TestWillNotDetectCollisionWhenNewPositionIsAlongBottomEdgeOfGrid()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + 2 * TestConstants.SquareWidth,
                y: topLeftCorner.Y + TestConstants.ScreenHeight - 3 * TestConstants.SquareWidth);
            var yMovement = (TestConstants.ScreenHeight / TestConstants.SquareWidth) - 3;
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: false);
        }

        [TestMethod]
        public void TestWillCalculateTopLeftCornersCorrectlyWhenNewPositionIsAlongLeftEdgeOfGrid()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(x: 0, y: 2 * TestConstants.SquareWidth);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList);
            var originalSquareOrigins = new Linq.List<SquareFillPoint>();
            var start = 0;
            var end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                originalSquareOrigins.Add(SquareFillPoint(
                    x: _threePoleSquareList[count].TopLeftCornerX, 
                    y: _threePoleSquareList[count].TopLeftCornerY));
            }

            // Act
            shape.UpdateTopLeftCorner(newTopLeftCorner: newTopLeftCorner);

            // Assert
            start = 0;
            end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerX, 
                    expected: originalSquareOrigins[count].X);
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerY,
                    expected: originalSquareOrigins[count].Y + 2 * TestConstants.SquareWidth);
            }
        }

        [TestMethod]
        public void TestWillCalculateTopLeftCornersCorrectlyWhenNewPositionIsAlongRightEdgeOfGrid()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + TestConstants.ScreenWidth - TestConstants.SquareWidth,
                y: topLeftCorner.Y + 2 * TestConstants.SquareWidth);
            var xMovement = (TestConstants.ScreenWidth / TestConstants.SquareWidth) - 1;
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList);
            var originalSquareOrigins = new Linq.List<SquareFillPoint>();
            var start = 0;
            var end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                originalSquareOrigins.Add(SquareFillPoint(
                    x: _threePoleSquareList[count].TopLeftCornerX, 
                    y: _threePoleSquareList[count].TopLeftCornerY));
            }

            // Act
            shape.UpdateTopLeftCorner(newTopLeftCorner: newTopLeftCorner);

            // Assert
            start = 0;
            end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerX,
                    expected: originalSquareOrigins[count].X + xMovement * TestConstants.SquareWidth);
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerY,
                    expected: originalSquareOrigins[count].Y + 2 * TestConstants.SquareWidth);
            }
        }

        [TestMethod]
        public void TestWillCalculateTopLeftCornersCorrectlyWhenNewPositionIsAlongTopEdgeOfGrid()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + 2 * TestConstants.SquareWidth,
                y: topLeftCorner.Y);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList);
            var originalSquareOrigins = new Linq.List<SquareFillPoint>();
            var start = 0;
            var end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                originalSquareOrigins.Add(SquareFillPoint(x: _threePoleSquareList[count].TopLeftCornerX, y: _threePoleSquareList[count].TopLeftCornerY));
            }

            // Act
            shape.UpdateTopLeftCorner(newTopLeftCorner: newTopLeftCorner);

            // Assert
            start = 0;
            end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerX,
                    expected: originalSquareOrigins[count].X + 2 * TestConstants.SquareWidth);
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerY,
                    expected: originalSquareOrigins[count].Y);
            }
        }

        [TestMethod]
        public void TestWillCalculateTopLeftCornersCorrectlyWhenNewPositionIsAlongBottomEdgeOfGrid()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + 2 * TestConstants.SquareWidth,
                y: topLeftCorner.Y + TestConstants.ScreenHeight - 3 * TestConstants.SquareWidth);
            var yMovement = (TestConstants.ScreenHeight / TestConstants.SquareWidth) - 3;
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList);
            var originalSquareOrigins = new Linq.List<SquareFillPoint>();
            var start = 0;
            var end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                originalSquareOrigins.Add(SquareFillPoint(x: _threePoleSquareList[count].TopLeftCornerX, y: _threePoleSquareList[count].TopLeftCornerY));
            }

            // Act
            shape.UpdateTopLeftCorner(newTopLeftCorner: newTopLeftCorner);

            // Assert
            start = 0;
            end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerX,
                    expected: originalSquareOrigins[count].X + 2 * TestConstants.SquareWidth);
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerY,
                    expected: originalSquareOrigins[count].Y + yMovement * TestConstants.SquareWidth);
            }
        }

        [TestMethod]
        public void TestWillCalculateTopLeftCornersCorrectlyWhenNewShapeCentreIsInMiddleOfGrid()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + 2 * TestConstants.SquareWidth,
                y: topLeftCorner.Y + 3 * TestConstants.SquareWidth);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList);
            var originalSquareOrigins = new Linq.List<SquareFillPoint>();
            var start = 0;
            var end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                originalSquareOrigins.Add(SquareFillPoint(x: _threePoleSquareList[count].TopLeftCornerX, y: _threePoleSquareList[count].TopLeftCornerY));
            }

            // Act
            shape.UpdateTopLeftCorner(newTopLeftCorner: newTopLeftCorner);

            // Assert
            start = 0;
            end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerX,
                    expected: originalSquareOrigins[count].X + 2 * TestConstants.SquareWidth);
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerY,
                    expected: originalSquareOrigins[count].Y + 3 * TestConstants.SquareWidth);
            }
        }

        [TestMethod]
        public void TestOriginsAreNotUpdatedIfAnotherShapeIsInTheWayOnTheLeft()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 1, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X - 1,
                y: topLeftCorner.Y);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList);
            var originalSquareOrigins = new Linq.List<SquareFillPoint>();
            var start = 0;
            var end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                originalSquareOrigins.Add(SquareFillPoint(x: _threePoleSquareList[count].TopLeftCornerX, y: _threePoleSquareList[count].TopLeftCornerY));
            }
            _occupiedGridSquares.OccupyGridSquare(x: TestConstants.ThreePolePoints[0].X, y: TestConstants.ThreePolePoints[0].X);
            _occupiedGridSquares.OccupyGridSquare(x: TestConstants.ThreePolePoints[1].X, y: TestConstants.ThreePolePoints[1].X);
            _occupiedGridSquares.OccupyGridSquare(x: TestConstants.ThreePolePoints[2].X, y: TestConstants.ThreePolePoints[2].X);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
            start = 0;
            end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerX, expected: originalSquareOrigins[count].X);
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerY, expected: originalSquareOrigins[count].Y);
            }
        }

        [TestMethod]
        public void TestOriginsAreNotUpdatedIfAnotherShapeIsInTheWayOnTheRight()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + 1,
                y: topLeftCorner.Y);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList);
            var originalSquareOrigins = new Linq.List<SquareFillPoint>();
            var start = 0;
            var end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                originalSquareOrigins.Add(SquareFillPoint(x: _threePoleSquareList[count].TopLeftCornerX, y: _threePoleSquareList[count].TopLeftCornerY));
            }
            _occupiedGridSquares.OccupyGridSquare(x: 1 + TestConstants.ThreePolePoints[0].X, y: TestConstants.ThreePolePoints[0].Y);
            _occupiedGridSquares.OccupyGridSquare(x: 1 + TestConstants.ThreePolePoints[1].X, y: TestConstants.ThreePolePoints[1].Y);
            _occupiedGridSquares.OccupyGridSquare(x: 1 + TestConstants.ThreePolePoints[2].X, y: TestConstants.ThreePolePoints[2].Y);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
            start = 0;
            end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerX, expected: originalSquareOrigins[count].X);
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerY, expected: originalSquareOrigins[count].Y);
            }
        }

        [TestMethod]
        public void TestOriginsAreNotUpdatedIfAnotherShapeIsInTheWayBelow()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X,
                y: topLeftCorner.Y + 1);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList);
            var originalSquareOrigins = new Linq.List<SquareFillPoint>();
            var start = 0;
            var end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                originalSquareOrigins.Add(SquareFillPoint(x: _threePoleSquareList[count].TopLeftCornerX, y: _threePoleSquareList[count].TopLeftCornerY));
            }
            _occupiedGridSquares.OccupyGridSquare(x: 0, y: 3);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
            start = 0;
            end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerX, expected: originalSquareOrigins[count].X);
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerY, expected: originalSquareOrigins[count].Y);
            }
        }

        [TestMethod]
        public void TestOriginsAreNotUpdatedIfAnotherShapeIsInTheWayAbove()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 1);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X,
                y: topLeftCorner.Y - 1);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList);
            var originalSquareOrigins = new Linq.List<SquareFillPoint>();
            var start = 0;
            var end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                originalSquareOrigins.Add(SquareFillPoint(x: _threePoleSquareList[count].TopLeftCornerX, y: _threePoleSquareList[count].TopLeftCornerY));
            }
            _occupiedGridSquares.OccupyGridSquare(x: 0, y: 0);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
            start = 0;
            end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerX, expected: originalSquareOrigins[count].X);
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerY, expected: originalSquareOrigins[count].Y);
            }
        }

        [TestMethod]
        public void TestOriginsAreNotUpdatedIfAnotherShapeIsInTheWayDiagonallyTopLeft()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 1, y: 1);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X - 1,
                y: topLeftCorner.Y - 1);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList);
            var originalSquareOrigins = new Linq.List<SquareFillPoint>();
            var start = 0;
            var end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                originalSquareOrigins.Add(SquareFillPoint(x: _threePoleSquareList[count].TopLeftCornerX, y: _threePoleSquareList[count].TopLeftCornerY));
            }
            _occupiedGridSquares.OccupyGridSquare(x: 0, y: 0);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
            start = 0;
            end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerX, expected: originalSquareOrigins[count].X);
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerY, expected: originalSquareOrigins[count].Y);
            }
        }

        [TestMethod]
        public void TestOriginsAreNotUpdatedIfAnotherShapeIsInTheWayDiagonallyTopRight()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 1);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + 1,
                y: topLeftCorner.Y - 1);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList);
            var originalSquareOrigins = new Linq.List<SquareFillPoint>();
            var start = 0;
            var end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                originalSquareOrigins.Add(SquareFillPoint(x: _threePoleSquareList[count].TopLeftCornerX, y: _threePoleSquareList[count].TopLeftCornerY));
            }
            _occupiedGridSquares.OccupyGridSquare(x: 1, y: 0);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
            start = 0;
            end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerX, expected: originalSquareOrigins[count].X);
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerY, expected: originalSquareOrigins[count].Y);
            }
        }

        [TestMethod]
        public void TestOriginsAreNotUpdatedIfAnotherShapeIsInTheWayDiagonallyBottomLeft()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 1, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X - 1,
                y: topLeftCorner.Y + 1);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList);
            var originalSquareOrigins = new Linq.List<SquareFillPoint>();
            var start = 0;
            var end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                originalSquareOrigins.Add(SquareFillPoint(x: _threePoleSquareList[count].TopLeftCornerX, y: _threePoleSquareList[count].TopLeftCornerY));
            }
            _occupiedGridSquares.OccupyGridSquare(x: 0, y: 3);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
            start = 0;
            end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerX, expected: originalSquareOrigins[count].X);
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerY, expected: originalSquareOrigins[count].Y);
            }
        }

        [TestMethod]
        public void TestOriginsAreNotUpdatedIfAnotherShapeIsInTheWayDiagonallyBottomRight()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + 1,
                y: topLeftCorner.Y + 1);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList);
            var originalSquareOrigins = new Linq.List<SquareFillPoint>();
            var start = 0;
            var end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                originalSquareOrigins.Add(SquareFillPoint(
                    x: _threePoleSquareList[count].TopLeftCornerX, y: _threePoleSquareList[count].TopLeftCornerY));
            }
            _occupiedGridSquares.OccupyGridSquare(x: 1, y: 3);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
            start = 0;
            end = TestConstants.ThreePolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerX, expected: originalSquareOrigins[count].X);
                Asserter.AreEqual(actual: _threePoleSquareList[count].TopLeftCornerY, expected: originalSquareOrigins[count].Y);
            }
        }

        [TestMethod]
        public void TestWhenShapeHasOnlyMovedVerticallyThenTopLeftCornersAreUpdatedCorrectly()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(
                x: 0 + 1,
                y: TestConstants.SquareWidth + 1);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X,
                y: topLeftCorner.Y - TestConstants.SquareWidth);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _twoPoleSquareList,
                topLeftCornerIsGridRef: false);
            var originalSquareOrigins = new Linq.List<SquareFillPoint>();
            var start = 0;
            var end = TestConstants.TwoPolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                originalSquareOrigins.Add(SquareFillPoint(x: _twoPoleSquareList[count].TopLeftCornerX, y: _twoPoleSquareList[count].TopLeftCornerY));
            }

            // Act
            shape.UpdateTopLeftCorner(newTopLeftCorner: newTopLeftCorner);

            // Assert
            start = 0;
            end = TestConstants.TwoPolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                Asserter.AreEqual(actual: _twoPoleSquareList[count].TopLeftCornerX, 
                    expected: originalSquareOrigins[count].X);
                Asserter.AreEqual(actual: _twoPoleSquareList[count].TopLeftCornerY,
                    expected: originalSquareOrigins[count].Y - TestConstants.SquareWidth);
            }
        }

        [TestMethod]
        public void TestWhenShapeHasOnlyMovedHorizontallyThenTopLeftCornersAreUpdatedCorrectly()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(
                x: 0 + 1,
                y: 0 + 1);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + TestConstants.SquareWidth,
                y: topLeftCorner.Y);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _twoPoleSquareList,
                topLeftCornerIsGridRef: false);
            var originalSquareOrigins = new Linq.List<SquareFillPoint>();
            var start = 0;
            var end = TestConstants.TwoPolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                originalSquareOrigins.Add(SquareFillPoint(x: _twoPoleSquareList[count].TopLeftCornerX, y: _twoPoleSquareList[count].TopLeftCornerY));
            }

            // Act
            shape.UpdateTopLeftCorner(newTopLeftCorner: newTopLeftCorner);

            // Assert
            start = 0;
            end = TestConstants.TwoPolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                Asserter.AreEqual(actual: _twoPoleSquareList[count].TopLeftCornerX,
                    expected: originalSquareOrigins[count].X + TestConstants.SquareWidth);
                Asserter.AreEqual(actual: _twoPoleSquareList[count].TopLeftCornerY, 
                    expected: originalSquareOrigins[count].Y);
            }
        }

        [TestMethod]
        public void TestWhenShapeHasOnlyMovedVerticallyThenNoCollisionIsDetected()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(
                x: 0 + 1,
                y: TestConstants.SquareWidth + 1);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X,
                y: topLeftCorner.Y - TestConstants.SquareWidth);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _twoPoleSquareList,
                topLeftCornerIsGridRef: false);
            var originalSquareOrigins = new Linq.List<SquareFillPoint>();
            var start = 0;
            var end = TestConstants.TwoPolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                originalSquareOrigins.Add(SquareFillPoint(x: _twoPoleSquareList[count].TopLeftCornerX, y: _twoPoleSquareList[count].TopLeftCornerY));
            }

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: false);
        }

        [TestMethod]
        public void TestWhenShapeHasOnlyMovedHorizontallyThenNoCollisionIsDetected()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(
                x: 0 + 1,
                y: 0 + 1);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + TestConstants.SquareWidth,
                y: topLeftCorner.Y);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _twoPoleSquareList,
                topLeftCornerIsGridRef: false);
            var originalSquareOrigins = new Linq.List<SquareFillPoint>();
            var start = 0;
            var end = TestConstants.TwoPolePoints.Count - 1;
            for (int count = start; count <= end; count++) {
                originalSquareOrigins.Add(SquareFillPoint(x: _twoPoleSquareList[count].TopLeftCornerX, y: _twoPoleSquareList[count].TopLeftCornerY));
            }

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: false);
        }

        [TestMethod]
        public void TestWhenShapeHasOnlyMovedVerticallyThenWeCanStillDetectAnotherShapeInTheWay()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(
                x: 0 + 1,
                y: TestConstants.SquareWidth);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X,
                y: topLeftCorner.Y - 1);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _twoPoleSquareList,
                topLeftCornerIsGridRef: false);
            _occupiedGridSquares.OccupyGridSquare(x: 0, y: 0);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
        }

        [TestMethod]
        public void TestWhenShapeHasOnlyMovedHorizontallyThenWeCanStillDetectAnotherShapeInTheWay()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(
                x: 0,
                y: 0 + 1);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + 1,
                y: topLeftCorner.Y);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _twoPoleSquareList,
                topLeftCornerIsGridRef: false);
            _occupiedGridSquares.OccupyGridSquare(x: 1, y: 0);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
        }

        [TestMethod]
        public void TestAShapeCanBeDetectedInTheWayWhenItHasASquareStickingOutInTheMiddleOfOurLeftSide()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(
                x: TestConstants.SquareWidth,
                y: 0 + 1);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X - 1,
                y: topLeftCorner.Y);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList,
                topLeftCornerIsGridRef: false);
            _occupiedGridSquares.OccupyGridSquare(x: 0, y: 1);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
        }

        [TestMethod]
        public void TestAShapeCanBeDetectedInTheWayWhenItHasASquareStickingOutInTheMiddleOfOurRightSide()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(
                x: 0,
                y: 0 + 1);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + 1,
                y: topLeftCorner.Y);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _threePoleSquareList,
                topLeftCornerIsGridRef: false);
            _occupiedGridSquares.OccupyGridSquare(x: 1, y: 1);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
        }

        [TestMethod]
        public void TestAShapeCanBeDetectedInTheWayWhenItHasASquareStickingOutInTheMiddleOfOurTopSide()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 1);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X,
                y: topLeftCorner.Y - 1);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _fourBarSquareList);
            _occupiedGridSquares.OccupyGridSquare(x: 2, y: 0);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
        }

        [TestMethod]
        public void TestAShapeCanBeDetectedInTheWayWhenItHasASquareStickingOutInTheMiddleOfOurBottomSide()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(
                x: 0 + 1,
                y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X,
                y: topLeftCorner.Y + 1);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _fourBarSquareList,
                topLeftCornerIsGridRef: false);
            _occupiedGridSquares.OccupyGridSquare(x: 2, y: 1);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
        }

        [TestMethod]
        public void TestAShapeCanBeDetectedInTheWayWhenItIsCompletelyInsideTheMovingShape()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + 3 * TestConstants.SquareWidth,
                y: topLeftCorner.Y);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _nineSquareSquareList);
            _occupiedGridSquares.OccupyGridSquare(x: 4, y: 1);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
        }

        [TestMethod]
        public void TestAShapeCanBeDetectedInTheWayWhenWeHaveASquareStickingOutInTheMiddleOfOurLeftSide()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 2, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X - 1,
                y: topLeftCorner.Y);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _leftHydrantSquareList);
            _occupiedGridSquares.OccupyGridSquare(x: 0, y: 1);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
        }

        [TestMethod]
        public void TestAShapeCanBeDetectedInTheWayWhenWeHaveASquareStickingOutInTheMiddleOfOurRightSide()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(
                x: 0,
                y: 0 + 1);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + 1,
                y: topLeftCorner.Y);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _rightHydrantSquareList,
                topLeftCornerIsGridRef: false);
            _occupiedGridSquares.OccupyGridSquare(x: 2, y: 1);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
        }

        [TestMethod]
        public void TestAShapeCanBeDetectedInTheWayWhenWeHaveASquareStickingOutInTheMiddleOfOurTopSide()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(
                x: TestConstants.SquareWidth + 1,
                y: TestConstants.SquareWidth);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X,
                y: topLeftCorner.Y - 1);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _upsideDownTSquareList,
                topLeftCornerIsGridRef: false);
            _occupiedGridSquares.OccupyGridSquare(x: 1, y: 0);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
        }

        [TestMethod]
        public void TestAShapeCanBeDetectedInTheWayWhenWeHaveASquareStickingOutInTheMiddleOfOurBottomSide()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(
                x: 0 + 1,
                y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X,
                y: topLeftCorner.Y + 1);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _rightWayUpTList,
                topLeftCornerIsGridRef: false);
            _occupiedGridSquares.OccupyGridSquare(x: 1, y: 2);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
        }

        [TestMethod]
        public void TestShapeIsNotAllowedToMoveBeyondTheTopOfTheScreen()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X,
                y: topLeftCorner.Y - 1);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _simpleSingleSquareList);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
        }

        [TestMethod]
        public void TestShapeIsNotAllowedToMoveBeyondTheBottomOfTheScreen()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X,
                y: topLeftCorner.Y + _occupiedGridSquares.Height * TestConstants.SquareWidth);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _simpleSingleSquareList);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
        }

        [TestMethod]
        public void TestShapeIsNotAllowedToMoveBeyondTheLeftEdgeOfTheScreen()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X - 1,
                y: topLeftCorner.Y);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _simpleSingleSquareList);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
        }

        [TestMethod]
        public void TestShapeIsNotAllowedToMoveBeyondTheRightEdgeOfTheScreen()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + _occupiedGridSquares.Width * TestConstants.SquareWidth,
                y: topLeftCorner.Y);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _simpleSingleSquareList);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
        }

        [TestMethod]
        public void TestAShapeCanBeDetectedInTheWayWhenWeAreHorizontallyAlignedWithTheGrid()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + 10,
                y: topLeftCorner.Y);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _simpleSingleSquareList);
            _occupiedGridSquares.OccupyGridSquare(x: 1, y: 0);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
        }

        [TestMethod]
        public void TestAShapeCanBeDetectedInTheWayWhenWeAreVerticallyAlignedWithTheGrid()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X,
                y: topLeftCorner.Y + 10);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _simpleSingleSquareList);
            _occupiedGridSquares.OccupyGridSquare(x: 0, y: 1);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
        }

        [TestMethod]
        public void TestAShapeCanBeDetectedInTheWayWhenWeArePerfectlyAlignedWithTheGrid()
        {
            // Arrange
            var topLeftCorner = SquareFillPoint(x: 0, y: 0);
            var newTopLeftCorner = SquareFillPoint(
                x: topLeftCorner.X + TestConstants.SquareWidth,
                y: topLeftCorner.Y + TestConstants.SquareWidth);
            var shape = Shape(
                topLeftCorner: topLeftCorner,
                squareDefinitions: _simpleSingleSquareList);
            _occupiedGridSquares.OccupyGridSquare(x: 1, y:1);

            // Act
            var result = shape.CheckWhetherMovementIsPossible(occupiedGridSquares: _occupiedGridSquares, newTopLeftCorner: newTopLeftCorner);

            // Assert
            Asserter.AreEqual(actual: result.ThereAreShapesInTheWay, expected: true);
        }

        [TestMethod]
        public void TestWhenOneSquareShapeIsSurroundedByEightOtherSquareShapesThenTopLeftCornerIsConsideredToBeInsideThatShapeOnly()
        {
            // Arrange
            var topLeftCornerCentralShape = SquareFillPoint(x: _centralShape.TopLeftCornerX, y: _centralShape.TopLeftCornerY);

            // Act
            var isInShape01 = _shape01.IsInShape(point: topLeftCornerCentralShape);
            var isInShape02 = _shape02.IsInShape(point: topLeftCornerCentralShape);
            var isInShape03 = _shape03.IsInShape(point: topLeftCornerCentralShape);
            var isInShape04 = _shape04.IsInShape(point: topLeftCornerCentralShape);
            var isInCentralShape = _centralShape.IsInShape(point: topLeftCornerCentralShape);
            var isInShape06 = _shape06.IsInShape(point: topLeftCornerCentralShape);
            var isInShape07 = _shape07.IsInShape(point: topLeftCornerCentralShape);
            var isInShape08 = _shape08.IsInShape(point: topLeftCornerCentralShape);
            var isInShape09 = _shape09.IsInShape(point: topLeftCornerCentralShape);

            // Assert
            Asserter.AreEqual(actual: isInShape01, expected: false);
            Asserter.AreEqual(actual: isInShape02, expected: false);
            Asserter.AreEqual(actual: isInShape03, expected: false);
            Asserter.AreEqual(actual: isInShape04, expected: false);
            Asserter.AreEqual(actual: isInCentralShape, expected: true);
            Asserter.AreEqual(actual: isInShape06, expected: false);
            Asserter.AreEqual(actual: isInShape07, expected: false);
            Asserter.AreEqual(actual: isInShape08, expected: false);
            Asserter.AreEqual(actual: isInShape09, expected: false);
        }

        [TestMethod]
        public void TestWhenOneSquareShapeIsSurroundedByEightOtherSquareShapesThenTopEdgeIsConsideredToBeInsideThatShapeOnly()
        {
            // Arrange
            var topEdgeCentralShape = SquareFillPoint(
                x: _centralShape.TopLeftCornerX + ShapeConstants.SquareWidth / 2, 
                y: _centralShape.TopLeftCornerY);

            // Act
            var isInShape01 = _shape01.IsInShape(point: topEdgeCentralShape);
            var isInShape02 = _shape02.IsInShape(point: topEdgeCentralShape);
            var isInShape03 = _shape03.IsInShape(point: topEdgeCentralShape);
            var isInShape04 = _shape04.IsInShape(point: topEdgeCentralShape);
            var isInCentralShape = _centralShape.IsInShape(point: topEdgeCentralShape);
            var isInShape06 = _shape06.IsInShape(point: topEdgeCentralShape);
            var isInShape07 = _shape07.IsInShape(point: topEdgeCentralShape);
            var isInShape08 = _shape08.IsInShape(point: topEdgeCentralShape);
            var isInShape09 = _shape09.IsInShape(point: topEdgeCentralShape);

            // Assert
            Asserter.AreEqual(actual: isInShape01, expected: false);
            Asserter.AreEqual(actual: isInShape02, expected: false);
            Asserter.AreEqual(actual: isInShape03, expected: false);
            Asserter.AreEqual(actual: isInShape04, expected: false);
            Asserter.AreEqual(actual: isInCentralShape, expected: true);
            Asserter.AreEqual(actual: isInShape06, expected: false);
            Asserter.AreEqual(actual: isInShape07, expected: false);
            Asserter.AreEqual(actual: isInShape08, expected: false);
            Asserter.AreEqual(actual: isInShape09, expected: false);
        }

        [TestMethod]
        public void TestWhenOneSquareShapeIsSurroundedByEightOtherSquareShapesThenTopRightCornerIsConsideredToBeInsideOneNeighbouringShapeOnly()
        {
            // Arrange
            var topRightCornerCentralShape = SquareFillPoint(
                x: _centralShape.TopLeftCornerX + ShapeConstants.SquareWidth, 
                y: _centralShape.TopLeftCornerY);

            // Act
            var isInShape01 = _shape01.IsInShape(point: topRightCornerCentralShape);
            var isInShape02 = _shape02.IsInShape(point: topRightCornerCentralShape);
            var isInShape03 = _shape03.IsInShape(point: topRightCornerCentralShape);
            var isInShape04 = _shape04.IsInShape(point: topRightCornerCentralShape);
            var isInCentralShape = _centralShape.IsInShape(point: topRightCornerCentralShape);
            var isInShape06 = _shape06.IsInShape(point: topRightCornerCentralShape);
            var isInShape07 = _shape07.IsInShape(point: topRightCornerCentralShape);
            var isInShape08 = _shape08.IsInShape(point: topRightCornerCentralShape);
            var isInShape09 = _shape09.IsInShape(point: topRightCornerCentralShape);

            // Assert
            Asserter.AreEqual(actual: isInShape01, expected: false);
            Asserter.AreEqual(actual: isInShape02, expected: false);
            Asserter.AreEqual(actual: isInShape03, expected: false);
            Asserter.AreEqual(actual: isInShape04, expected: false);
            Asserter.AreEqual(actual: isInCentralShape, expected: false);
            Asserter.AreEqual(actual: isInShape06, expected: true);
            Asserter.AreEqual(actual: isInShape07, expected: false);
            Asserter.AreEqual(actual: isInShape08, expected: false);
            Asserter.AreEqual(actual: isInShape09, expected: false);
        }

        [TestMethod]
        public void TestWhenOneSquareShapeIsSurroundedByEightOtherSquareShapesThenLeftEdgeIsConsideredToBeInsideThatShapeOnly()
        {
            // Arrange
            var leftEdgeCentralShape = SquareFillPoint(
                x: _centralShape.TopLeftCornerX, 
                y: _centralShape.TopLeftCornerY + ShapeConstants.SquareWidth / 2);

            // Act
            var isInShape01 = _shape01.IsInShape(point: leftEdgeCentralShape);
            var isInShape02 = _shape02.IsInShape(point: leftEdgeCentralShape);
            var isInShape03 = _shape03.IsInShape(point: leftEdgeCentralShape);
            var isInShape04 = _shape04.IsInShape(point: leftEdgeCentralShape);
            var isInCentralShape = _centralShape.IsInShape(point: leftEdgeCentralShape);
            var isInShape06 = _shape06.IsInShape(point: leftEdgeCentralShape);
            var isInShape07 = _shape07.IsInShape(point: leftEdgeCentralShape);
            var isInShape08 = _shape08.IsInShape(point: leftEdgeCentralShape);
            var isInShape09 = _shape09.IsInShape(point: leftEdgeCentralShape);

            // Assert
            Asserter.AreEqual(actual: isInShape01, expected: false);
            Asserter.AreEqual(actual: isInShape02, expected: false);
            Asserter.AreEqual(actual: isInShape03, expected: false);
            Asserter.AreEqual(actual: isInShape04, expected: false);
            Asserter.AreEqual(actual: isInCentralShape, expected: true);
            Asserter.AreEqual(actual: isInShape06, expected: false);
            Asserter.AreEqual(actual: isInShape07, expected: false);
            Asserter.AreEqual(actual: isInShape08, expected: false);
            Asserter.AreEqual(actual: isInShape09, expected: false);
        }

        [TestMethod]
        public void TestWhenOneSquareShapeIsSurroundedByEightOtherSquareShapesThenRightEdgeIsConsideredToBeInsideOneNeighbouringShapeOnly()
        {
            // Arrange
            var rightEdgeCentralShape = SquareFillPoint(
                x: _centralShape.TopLeftCornerX + ShapeConstants.SquareWidth, 
                y: _centralShape.TopLeftCornerY + ShapeConstants.SquareWidth / 2);

            // Act
            var isInShape01 = _shape01.IsInShape(point: rightEdgeCentralShape);
            var isInShape02 = _shape02.IsInShape(point: rightEdgeCentralShape);
            var isInShape03 = _shape03.IsInShape(point: rightEdgeCentralShape);
            var isInShape04 = _shape04.IsInShape(point: rightEdgeCentralShape);
            var isInCentralShape = _centralShape.IsInShape(point: rightEdgeCentralShape);
            var isInShape06 = _shape06.IsInShape(point: rightEdgeCentralShape);
            var isInShape07 = _shape07.IsInShape(point: rightEdgeCentralShape);
            var isInShape08 = _shape08.IsInShape(point: rightEdgeCentralShape);
            var isInShape09 = _shape09.IsInShape(point: rightEdgeCentralShape);

            // Assert
            Asserter.AreEqual(actual: isInShape01, expected: false);
            Asserter.AreEqual(actual: isInShape02, expected: false);
            Asserter.AreEqual(actual: isInShape03, expected: false);
            Asserter.AreEqual(actual: isInShape04, expected: false);
            Asserter.AreEqual(actual: isInCentralShape, expected: false);
            Asserter.AreEqual(actual: isInShape06, expected: true);
            Asserter.AreEqual(actual: isInShape07, expected: false);
            Asserter.AreEqual(actual: isInShape08, expected: false);
            Asserter.AreEqual(actual: isInShape09, expected: false);
        }

        [TestMethod]
        public void TestWhenOneSquareShapeIsSurroundedByEightOtherSquareShapesThenBottomLeftCornerIsConsideredToBeInsideOneNeighbouringShapeOnly()
        {
            // Arrange
            var bottomLeftCornerCentralShape = SquareFillPoint(
                x: _centralShape.TopLeftCornerX, 
                y: _centralShape.TopLeftCornerY + ShapeConstants.SquareWidth);

            // Act
            var isInShape01 = _shape01.IsInShape(point: bottomLeftCornerCentralShape);
            var isInShape02 = _shape02.IsInShape(point: bottomLeftCornerCentralShape);
            var isInShape03 = _shape03.IsInShape(point: bottomLeftCornerCentralShape);
            var isInShape04 = _shape04.IsInShape(point: bottomLeftCornerCentralShape);
            var isInCentralShape = _centralShape.IsInShape(point: bottomLeftCornerCentralShape);
            var isInShape06 = _shape06.IsInShape(point: bottomLeftCornerCentralShape);
            var isInShape07 = _shape07.IsInShape(point: bottomLeftCornerCentralShape);
            var isInShape08 = _shape08.IsInShape(point: bottomLeftCornerCentralShape);
            var isInShape09 = _shape09.IsInShape(point: bottomLeftCornerCentralShape);

            // Assert
            Asserter.AreEqual(actual: isInShape01, expected: false);
            Asserter.AreEqual(actual: isInShape02, expected: false);
            Asserter.AreEqual(actual: isInShape03, expected: false);
            Asserter.AreEqual(actual: isInShape04, expected: false);
            Asserter.AreEqual(actual: isInCentralShape, expected: false);
            Asserter.AreEqual(actual: isInShape06, expected: false);
            Asserter.AreEqual(actual: isInShape07, expected: false);
            Asserter.AreEqual(actual: isInShape08, expected: true);
            Asserter.AreEqual(actual: isInShape09, expected: false);
        }

        [TestMethod]
        public void TestWhenOneSquareShapeIsSurroundedByEightOtherSquareShapesThenBottomEdgeIsConsideredToBeInsideOneNeighbouringShapeOnly()
        {
            // Arrange
            var bottomEdgeCentralShape = SquareFillPoint(
                x: _centralShape.TopLeftCornerX + ShapeConstants.SquareWidth / 2, 
                y: _centralShape.TopLeftCornerY + ShapeConstants.SquareWidth);

            // Act
            var isInShape01 = _shape01.IsInShape(point: bottomEdgeCentralShape);
            var isInShape02 = _shape02.IsInShape(point: bottomEdgeCentralShape);
            var isInShape03 = _shape03.IsInShape(point: bottomEdgeCentralShape);
            var isInShape04 = _shape04.IsInShape(point: bottomEdgeCentralShape);
            var isInCentralShape = _centralShape.IsInShape(point: bottomEdgeCentralShape);
            var isInShape06 = _shape06.IsInShape(point: bottomEdgeCentralShape);
            var isInShape07 = _shape07.IsInShape(point: bottomEdgeCentralShape);
            var isInShape08 = _shape08.IsInShape(point: bottomEdgeCentralShape);
            var isInShape09 = _shape09.IsInShape(point: bottomEdgeCentralShape);

            // Assert
            Asserter.AreEqual(actual: isInShape01, expected: false);
            Asserter.AreEqual(actual: isInShape02, expected: false);
            Asserter.AreEqual(actual: isInShape03, expected: false);
            Asserter.AreEqual(actual: isInShape04, expected: false);
            Asserter.AreEqual(actual: isInCentralShape, expected: false);
            Asserter.AreEqual(actual: isInShape06, expected: false);
            Asserter.AreEqual(actual: isInShape07, expected: false);
            Asserter.AreEqual(actual: isInShape08, expected: true);
            Asserter.AreEqual(actual: isInShape09, expected: false);
        }

        [TestMethod]
        public void TestWhenOneSquareShapeIsSurroundedByEightOtherSquareShapesThenBottomRightCornerIsConsideredToBeInsideDiagonallyBottomRightNeighbour()
        {
            // Arrange
            var bottomRightCornerCentralShape = SquareFillPoint(
                x: _centralShape.TopLeftCornerX + ShapeConstants.SquareWidth, 
                y: _centralShape.TopLeftCornerY + ShapeConstants.SquareWidth);

            // Act
            var isInShape01 = _shape01.IsInShape(point: bottomRightCornerCentralShape);
            var isInShape02 = _shape02.IsInShape(point: bottomRightCornerCentralShape);
            var isInShape03 = _shape03.IsInShape(point: bottomRightCornerCentralShape);
            var isInShape04 = _shape04.IsInShape(point: bottomRightCornerCentralShape);
            var isInCentralShape = _centralShape.IsInShape(point: bottomRightCornerCentralShape);
            var isInShape06 = _shape06.IsInShape(point: bottomRightCornerCentralShape);
            var isInShape07 = _shape07.IsInShape(point: bottomRightCornerCentralShape);
            var isInShape08 = _shape08.IsInShape(point: bottomRightCornerCentralShape);
            var isInShape09 = _shape09.IsInShape(point: bottomRightCornerCentralShape);

            // Assert
            Asserter.AreEqual(actual: isInShape01, expected: false);
            Asserter.AreEqual(actual: isInShape02, expected: false);
            Asserter.AreEqual(actual: isInShape03, expected: false);
            Asserter.AreEqual(actual: isInShape04, expected: false);
            Asserter.AreEqual(actual: isInCentralShape, expected: false);
            Asserter.AreEqual(actual: isInShape06, expected: false);
            Asserter.AreEqual(actual: isInShape07, expected: false);
            Asserter.AreEqual(actual: isInShape08, expected: false);
            Asserter.AreEqual(actual: isInShape09, expected: true);
        }
    }
}
