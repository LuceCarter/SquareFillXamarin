﻿using System;
using System.Collections.Generic;
using SquareFillDomain.Utils;

namespace SquareFillDomain.Models
{
    public class Shape
    {
        public int CentreOfShapeX { get { return _topLeftCorner.X + ShapeConstants.SquareWidth / 2; } }
        public int CentreOfShapeY { get { return _topLeftCorner.Y + ShapeConstants.SquareWidth / 2; } }
        public int TopLeftCornerX { get { return _topLeftCorner.X; } }
        public int TopLeftCornerY { get { return _topLeftCorner.Y; } }
        public int NumSquaresLeftOfTopLeftCorner { get { return _numSquaresLeftOfTopLeftCorner; } }
        public int NumSquaresRightOfTopLeftCorner { get { return _numSquaresRightOfTopLeftCorner; } }
        public int NumSquaresAboveTopLeftCorner { get { return _numSquaresAboveTopLeftCorner; } }
        public int NumSquaresBelowTopLeftCorner { get { return _numSquaresBelowTopLeftCorner; } }

        private SquareFillPoint _topLeftCorner;
        private readonly List<Square> _squares;

        private int _numSquaresLeftOfTopLeftCorner;
        private int _numSquaresRightOfTopLeftCorner;
        private int _numSquaresAboveTopLeftCorner;
        private int _numSquaresBelowTopLeftCorner;

        // init(
        //      topLeftCorner: SquareFillPoint,
        //      squareDefinitions: [Square],
        //      topLeftCornerIsGridRef: Bool = true)
        public Shape(
            SquareFillPoint topLeftCorner,
            List<Square> squareDefinitions,
            bool topLeftCornerIsGridRef = true)
        {
            // Even though these values are initialised in the methods below, 
            // we have to set them here too otherwise Swift will complain.
            _topLeftCorner = SquareFillPoint(x: 0, y: 0);
            _squares = squareDefinitions;
            _numSquaresLeftOfTopLeftCorner = 0;
            _numSquaresRightOfTopLeftCorner = 0;
            _numSquaresAboveTopLeftCorner = 0;
            _numSquaresBelowTopLeftCorner = 0;

            InitialiseTopLeftCorner(
                topLeftCorner: topLeftCorner,
                topLeftCornerIsGridRef: topLeftCornerIsGridRef);

            CalculateNumSquaresAroundTopLeftCorner();
            UpdateTopLeftCorner(newTopLeftCorner: _topLeftCorner);
        }

        // public func IsInShape(point: SquareFillPoint) -> Bool
        public bool IsInShape(SquareFillPoint point)
        {
            var isInShape = false;
            
            foreach (var element in _squares) {
                isInShape = isInShape || element.IsInSquare(point: point);
            }

            return isInShape;
        }

        // public func UpdateTopLeftCorner (newTopLeftCorner: SquareFillPoint)
        public void UpdateTopLeftCorner(SquareFillPoint newTopLeftCorner)
        {
            _topLeftCorner = newTopLeftCorner;
            foreach (var element in _squares) {
                element.MoveTopLeftCorner(newTopLeftCorner: newTopLeftCorner);
            }
        }

        // public func CheckWhetherMovementIsPossible(
        //      occupiedGridSquares: Grid,
        //      newTopLeftCorner: SquareFillPoint) -> MovementAnalyser
        public MovementAnalyser CheckWhetherMovementIsPossible(
            Grid occupiedGridSquares,
            SquareFillPoint newTopLeftCorner)
        {
            var movementAnalyser = new MovementAnalyser(
                squares: _squares,
                occupiedGridSquares: occupiedGridSquares,
                newTopLeftCorner: newTopLeftCorner);
            
            return movementAnalyser;
        }

        // public func VacateGridSquares(occupiedGridSquares: Grid)
	    public void VacateGridSquares(Grid occupiedGridSquares) 
        {
            foreach (var element in _squares) {
                element.VacateGridSquare(occupiedGridSquares: occupiedGridSquares);
            }
        }

        // public func OccupyGridSquares(occupiedGridSquares: Grid)
        public void OccupyGridSquares(Grid occupiedGridSquares)
        {
            foreach (var element in _squares) {
                element.OccupyGridSquare(occupiedGridSquares: occupiedGridSquares, shapeInSquare: this);
            }
        }

        // public func WeStartedWithinTheContainingRectangle() -> Bool
        public bool WeStartedWithinTheContainingRectangle()
        {
            var leftEdge = _topLeftCorner.X - _numSquaresLeftOfTopLeftCorner * ShapeConstants.SquareWidth;
            var topEdge = _topLeftCorner.Y - _numSquaresAboveTopLeftCorner * ShapeConstants.SquareWidth;
            var rightEdge = _topLeftCorner.X + _numSquaresRightOfTopLeftCorner * ShapeConstants.SquareWidth;
            var bottomEdge = _topLeftCorner.Y + _numSquaresBelowTopLeftCorner * ShapeConstants.SquareWidth;

            return leftEdge >= ShapeConstants.ContainingRectangle.X
                && topEdge >= ShapeConstants.ContainingRectangle.Y
                && rightEdge <= (ShapeConstants.ContainingRectangle.X + ShapeConstants.ContainingRectangle.Width)
                && bottomEdge <= (ShapeConstants.ContainingRectangle.Y + ShapeConstants.ContainingRectangle.Height);
        }

        // public func TopLeftCornersAsString() -> String
	    public string TopLeftCornersAsString()
        {
            var topLeftCornerAsString = "";

            foreach (var element in _squares) {
                topLeftCornerAsString = topLeftCornerAsString + element.TopLeftCornerAsString();
            }

            return topLeftCornerAsString;
        }

        // public func CalculateTopLeftCornerRelativeToCursorPosition(cursorPosition: SquareFillPoint) -> SquareFillPoint
        public SquareFillPoint CalculateTopLeftCornerRelativeToCursorPosition(SquareFillPoint cursorPosition)
        {
            return SquareFillPoint(
                x: _topLeftCorner.X - cursorPosition.X,
                y: _topLeftCorner.Y - cursorPosition.Y);
        }

        // public func CalculateCursorPositionBasedOnTopLeftCorner(topLeftCornerRelativeToCursorPosition: SquareFillPoint) -> SquareFillPoint
        public SquareFillPoint CalculateCursorPositionBasedOnTopLeftCorner(SquareFillPoint topLeftCornerRelativeToCursorPosition)
        {
            return SquareFillPoint(
                x: _topLeftCorner.X - topLeftCornerRelativeToCursorPosition.X,
                y: _topLeftCorner.Y - topLeftCornerRelativeToCursorPosition.Y);
        }

        // public func SnapToGrid(newTopLeftCorner: SquareFillPoint)
        public void SnapToGrid(SquareFillPoint newTopLeftCorner)
        {
            var snappedTopLeftCorner = SquareFillPoint(
                x: CalculateSnappedX(newTopLeftCornerX: newTopLeftCorner.X),
                y: CalculateSnappedY(newTopLeftCornerY: newTopLeftCorner.Y));

            UpdateTopLeftCorner(newTopLeftCorner: snappedTopLeftCorner);
        }

        // public func SnapToGridInRelevantDimensionsIfPossible(movementResult: MovementAnalyser, occupiedGridSquares: Grid)
        public void SnapToGridInRelevantDimensionsIfPossible(MovementAnalyser movementResult, Grid occupiedGridSquares)
        {
            var previousTopLeftCorner = _topLeftCorner;
            var newTopLeftCorner = SquareFillPoint(x: previousTopLeftCorner.X, y: previousTopLeftCorner.Y);

            if (movementResult.ShapeHasCrossedAHorizontalGridBoundary)
            {
                newTopLeftCorner.X = CalculateSnappedX(newTopLeftCornerX: newTopLeftCorner.X);
            }

            if (movementResult.ShapeHasCrossedAVerticalGridBoundary)
            {
                newTopLeftCorner.Y = CalculateSnappedY(newTopLeftCornerY: newTopLeftCorner.Y);
            }

            var newMovementResult = CheckWhetherMovementIsPossible(
                occupiedGridSquares: occupiedGridSquares,
                newTopLeftCorner: newTopLeftCorner);

            if (newMovementResult.NoShapesAreInTheWay)
            {
                UpdateTopLeftCorner(newTopLeftCorner: newTopLeftCorner);
            }
        }

        // public func CalculateSnappedX(newTopLeftCornerX: Int) -> Int
        public int CalculateSnappedX(int newTopLeftCornerX)
        {
            return CalculateSnappedCoordinate(
                newTopLeftCornerCoord: newTopLeftCornerX,
                boundaryRectangleOriginCoord: 0,
                boundaryRectangleDimension: ShapeConstants.ScreenWidth,
                numSquaresOnSmallestSide: _numSquaresLeftOfTopLeftCorner,
                numSquaresOnLargestSide: _numSquaresRightOfTopLeftCorner);
        }

        // public func CalculateSnappedY(newTopLeftCornerY: Int) -> Int
        public int CalculateSnappedY(int newTopLeftCornerY)
        {
            return CalculateSnappedCoordinate(
                newTopLeftCornerCoord: newTopLeftCornerY,
                boundaryRectangleOriginCoord: 0,
                boundaryRectangleDimension: ShapeConstants.ScreenHeight,
                numSquaresOnSmallestSide: _numSquaresAboveTopLeftCorner,
                numSquaresOnLargestSide: _numSquaresBelowTopLeftCorner);
        }

        // private func CalculateSnappedCoordinate(
        //      newTopLeftCornerCoord: Int,
        //      boundaryRectangleOriginCoord: Int,
        //      boundaryRectangleDimension: Int,
        //      numSquaresOnSmallestSide: Int,
        //      numSquaresOnLargestSide: Int) -> Int
        private int CalculateSnappedCoordinate(
            int newTopLeftCornerCoord,
            int boundaryRectangleOriginCoord,
            int boundaryRectangleDimension,
            int numSquaresOnSmallestSide,
            int numSquaresOnLargestSide)
        {
            var squareWidth = ShapeConstants.SquareWidth;

            var numberOfSquaresFromEdgeOfScreen = CalculateNumSquaresFromEdgeOfScreen(topLeftCornerCoordinate: newTopLeftCornerCoord);

            var potentialNewTopLeftCorner = numberOfSquaresFromEdgeOfScreen * squareWidth;

            var topLeftCornerAtOneEndOfContainer = boundaryRectangleOriginCoord;

            var topLeftCornerAtOtherEndOfContainer = boundaryRectangleOriginCoord + boundaryRectangleDimension;

            var potentialTopLeftCornerOfShapeEdgeOnOneSide = potentialNewTopLeftCorner - (numSquaresOnSmallestSide * squareWidth);

            var topLeftCornerOfShapeEdgeOnOneSideAdjustedForSmallestContainerEdge =
                Math.Max(potentialTopLeftCornerOfShapeEdgeOnOneSide, topLeftCornerAtOneEndOfContainer);

            var topLeftCornerAdjustedForSmallestContainerEdge =
                topLeftCornerOfShapeEdgeOnOneSideAdjustedForSmallestContainerEdge
                + (numSquaresOnSmallestSide * squareWidth);

            var potentialTopLeftCornerOfShapeEdgeOnOtherSide = topLeftCornerAdjustedForSmallestContainerEdge +
                                                        (numSquaresOnLargestSide * squareWidth);

            var topLeftCornerOfShapeEdgeOnOtherSideAdjustedForBothContainerEdges =
                Math.Min(potentialTopLeftCornerOfShapeEdgeOnOtherSide, topLeftCornerAtOtherEndOfContainer);

            var actualTopLeftCorner = topLeftCornerOfShapeEdgeOnOtherSideAdjustedForBothContainerEdges
                                     - (numSquaresOnLargestSide * squareWidth);

            return actualTopLeftCorner;
        }

        // private func CalculateNumSquaresFromEdgeOfScreen(topLeftCornerCoordinate: Int) -> Int
        private int CalculateNumSquaresFromEdgeOfScreen(int topLeftCornerCoordinate)
        {
            var numberOfSquaresFromEdgeOfScreen = topLeftCornerCoordinate / ShapeConstants.SquareWidth;

            if (MoreThanHalfWayAcrossASquare(topLeftCornerCoordinate: topLeftCornerCoordinate))
            {
                numberOfSquaresFromEdgeOfScreen += 1;
            }

            return numberOfSquaresFromEdgeOfScreen;
        }

        // private func MoreThanHalfWayAcrossASquare(topLeftCornerCoordinate: Int) -> Bool
        private bool MoreThanHalfWayAcrossASquare(int topLeftCornerCoordinate)
        {
            return (topLeftCornerCoordinate % ShapeConstants.SquareWidth) > (ShapeConstants.SquareWidth / 2);
        }

        // private func InitialiseTopLeftCorner(topLeftCorner: SquareFillPoint, topLeftCornerIsGridRef: Bool)
        private void InitialiseTopLeftCorner(SquareFillPoint topLeftCorner, bool topLeftCornerIsGridRef)
        {
            if (topLeftCornerIsGridRef)
            {
                _topLeftCorner = topLeftCorner.ConvertToPixels();
            }
            else
            {
                _topLeftCorner = SquareFillPoint(x: topLeftCorner.X, y: topLeftCorner.Y);
            }
        }

        // private func CalculateNumSquaresAroundTopLeftCorner()
        private void CalculateNumSquaresAroundTopLeftCorner()
        {
            foreach (var element in _squares) {
                _numSquaresLeftOfTopLeftCorner = Math.Min(_numSquaresLeftOfTopLeftCorner, element.XRelativeToParentCorner);
                _numSquaresRightOfTopLeftCorner = Math.Max(_numSquaresRightOfTopLeftCorner, element.XRelativeToParentCorner);
                _numSquaresAboveTopLeftCorner = Math.Min(_numSquaresAboveTopLeftCorner, element.YRelativeToParentCorner);
                _numSquaresBelowTopLeftCorner = Math.Max(_numSquaresBelowTopLeftCorner, element.YRelativeToParentCorner);
            }

            DealWithNegativeNumbersOfSquares();
        }

        // private func DealWithNegativeNumbersOfSquares()
        private void DealWithNegativeNumbersOfSquares()
        {
            _numSquaresLeftOfTopLeftCorner = Math.Abs(_numSquaresLeftOfTopLeftCorner);
            _numSquaresAboveTopLeftCorner = Math.Abs(_numSquaresAboveTopLeftCorner);
        }

        private SquareFillPoint SquareFillPoint(int x, int y)
        {
            return new SquareFillPoint(x: x, y: y);
        }
    }
}