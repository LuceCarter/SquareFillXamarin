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

        public Shape(
            SquareFillPoint topLeftCorner,
            List<Square> squareDefinitions,
            bool topLeftCornerIsInPixels = true)
        {
            InitialiseTopLeftCorner(
                topLeftCorner: topLeftCorner,
                topLeftCornerIsInPixels: topLeftCornerIsInPixels);

            _squares = squareDefinitions;
            foreach (var square in _squares)
            {
                square.CalculateTopLeftCorner(parentTopLeftCorner: _topLeftCorner);
            }

            CalculateNumSquaresAroundTopLeftCorner();
            MoveAllShapeSquares(newTopLeftCorner: _topLeftCorner);
        }

        public bool IsInShape(SquareFillPoint point)
        {
            bool isInShape = false;
            
            foreach(var square in _squares) 
            {
                isInShape = isInShape || square.IsInSquare(point: point);
            }

            return isInShape;
        }

        public void MoveAllShapeSquares(SquareFillPoint newTopLeftCorner)
        {
            _topLeftCorner = newTopLeftCorner;
            foreach (var square in _squares)
            {
                square.MoveTopLeftCorner(newTopLeftCorner: newTopLeftCorner);
            }
        }

        public void CalculateTopLeftCorners(SquareFillPoint newTopLeftCorner)
        {
            foreach (var square in _squares)
            {
                square.CalculateTopLeftCorner(parentTopLeftCorner: newTopLeftCorner);
            }
        }

        public MovementResult AttemptToUpdateOrigins(
            Grid occupiedGridSquares,
            SquareFillPoint newTopLeftCorner)
        {
            bool somethingIsInTheWay = false;
            var movementResult = new MovementResult();

            foreach (var square in _squares)
            {
                SquareFillPoint oldGridOrigin = square.GetGridOrigin();
                SquareFillPoint newOrigin = square.CalculatePotentialTopLeftCorner(parentTopLeftCorner: newTopLeftCorner);
                SquareFillPoint newGridOrigin = square.CalculateNewGridOrigin(newOrigin: newOrigin);

                IsDivisibleBySquareWidth isDivisibleBySquareWidth = square.HasCrossedBoundaries(
                    movementResult: movementResult, 
                    newPixels: newOrigin, 
                    oldGridVal: oldGridOrigin, 
                    newGridVal: newGridOrigin);

                if (movementResult.ShapeHasCrossedAHorizontalGridBoundary
                    || movementResult.ShapeHasCrossedAVerticalGridBoundary)
                {
                    somethingIsInTheWay = IsSomethingInTheWay(
                        isDivisibleBySquareWidth: isDivisibleBySquareWidth,
                        newGridOrigin: newGridOrigin,
                        somethingWasAlreadyInTheWay: somethingIsInTheWay,
                        occupiedGridSquares: occupiedGridSquares);
                }
            }

            if (!somethingIsInTheWay)
            {
                CalculateTopLeftCorners(newTopLeftCorner: newTopLeftCorner);
            }

            movementResult.NoShapesAreInTheWay = !somethingIsInTheWay;
            return movementResult;
        }

	    public void VacateGridSquares(Grid occupiedGridSquares) 
        {
            foreach (var square in _squares)
            {
                square.VacateGridSquare(occupiedGridSquares: occupiedGridSquares);
            }
        }

        public void OccupyGridSquares(Grid occupiedGridSquares)
        {
            foreach (var square in _squares)
            {
                square.OccupyGridSquare(occupiedGridSquares: occupiedGridSquares, shapeInSquare: this);
            }
        }

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

	    public string TopLeftCornersAsString()
        {
            string topLeftCornerAsString = String.Empty;
            foreach (var square in _squares)
            {
                topLeftCornerAsString = topLeftCornerAsString + square.TopLeftCornerAsString();
            }
            return topLeftCornerAsString;
        }

        public int CalculateSnappedX(int newTopLeftCornerX)
        {
            return CalculateSnappedCoordinate(
                newTopLeftCornerCoord: newTopLeftCornerX,
                boundaryRectangleOriginCoord: 0,
                boundaryRectangleDimension: ShapeConstants.ScreenWidth,
                numSquaresOnSmallestSide: _numSquaresLeftOfTopLeftCorner,
                numSquaresOnLargestSide: _numSquaresRightOfTopLeftCorner);
        }

        public int CalculateSnappedY(int newTopLeftCornerY)
        {
            return CalculateSnappedCoordinate(
                newTopLeftCornerCoord: newTopLeftCornerY,
                boundaryRectangleOriginCoord: 0,
                boundaryRectangleDimension: ShapeConstants.ScreenHeight,
                numSquaresOnSmallestSide: _numSquaresAboveTopLeftCorner,
                numSquaresOnLargestSide: _numSquaresBelowTopLeftCorner);
        }

        public SquareFillPoint CalculateTopLeftCornerRelativeToCursorPosition(SquareFillPoint cursorPosition)
        {
            return new SquareFillPoint(
                x: _topLeftCorner.X - cursorPosition.X,
                y: _topLeftCorner.Y - cursorPosition.Y);
        }

        public SquareFillPoint CalculateCursorPosition(SquareFillPoint topLeftCornerRelativeToCursorPosition)
        {
            return new SquareFillPoint(
                x: _topLeftCorner.X - topLeftCornerRelativeToCursorPosition.X,
                y: _topLeftCorner.Y - topLeftCornerRelativeToCursorPosition.Y);
        }

        public SquareFillPoint CalculateTopLeftCorner(
            SquareFillPoint newCursorPosition,
            SquareFillPoint topLeftCornerRelativeToCursorPosition)
        {
            return new SquareFillPoint(
                x: newCursorPosition.X + topLeftCornerRelativeToCursorPosition.X,
                y: newCursorPosition.Y + topLeftCornerRelativeToCursorPosition.Y);
        }

        public void SnapToGrid(SquareFillPoint newCursorPosition, SquareFillPoint topLeftCornerRelativeToCursorPosition)
        {
            // There's a lot of confucion in this class and calling classes about when and how top left corners are calculated!!
            // Needs clarifying. Apart from anything else, the word "calculate" is sometimes used to mean change the value,
            // but sometimes used to mean just calculate a new value and return it without making any internal changes.
            var topLeftCornerTakingRelativePositionIntoAccount = CalculateTopLeftCorner(
                newCursorPosition: newCursorPosition,
                topLeftCornerRelativeToCursorPosition: topLeftCornerRelativeToCursorPosition);

            var newTopLeftCorner = new SquareFillPoint(
                x: CalculateSnappedX(newTopLeftCornerX: topLeftCornerTakingRelativePositionIntoAccount.X),
                y: CalculateSnappedY(newTopLeftCornerY: topLeftCornerTakingRelativePositionIntoAccount.Y));

            MoveAllShapeSquares(newTopLeftCorner: newTopLeftCorner);
            CalculateTopLeftCorners(newTopLeftCorner: newTopLeftCorner);
        }

        public void SnapToGridInRelevantDimensionsIfPossible(MovementResult movementResult, Grid occupiedGridSquares)
        {
            var previousTopLeftCorner = _topLeftCorner;
            var newTopLeftCorner = new SquareFillPoint(x: previousTopLeftCorner.X, y: previousTopLeftCorner.Y);

            if (movementResult.ShapeHasCrossedAHorizontalGridBoundary)
            {
                newTopLeftCorner.X = CalculateSnappedX(newTopLeftCornerX: newTopLeftCorner.X);
            }

            if (movementResult.ShapeHasCrossedAVerticalGridBoundary)
            {
                newTopLeftCorner.Y = CalculateSnappedY(newTopLeftCornerY: newTopLeftCorner.Y);
            }

            MovementResult newMovementResult = AttemptToUpdateOrigins(
                occupiedGridSquares: occupiedGridSquares,
                newTopLeftCorner: newTopLeftCorner);

            if (newMovementResult.NoShapesAreInTheWay)
            {
                MoveAllShapeSquares(newTopLeftCorner: newTopLeftCorner);
            }

            // !! I removed this because CalculateTopLeftCorners is already called from AttemptToUpdateOrigins,
            // and this seemed like it would override that unnecessarily?
            // Needs checking though!
            // CalculateTopLeftCorners(newTopLeftCorner: _topLeftCorner);
        }

        private void InitialiseTopLeftCorner(SquareFillPoint topLeftCorner, bool topLeftCornerIsInPixels)
        {
            if (topLeftCornerIsInPixels)
            {
                _topLeftCorner = topLeftCorner;
            }
            else
            {
                _topLeftCorner = new SquareFillPoint(
                    x: topLeftCorner.X * ShapeConstants.SquareWidth,
                    y: topLeftCorner.Y * ShapeConstants.SquareWidth);
            }
        }

        private bool IsSomethingInTheWay(
            IsDivisibleBySquareWidth isDivisibleBySquareWidth,
            SquareFillPoint newGridOrigin,
            bool somethingWasAlreadyInTheWay,
            Grid occupiedGridSquares)
        {
            bool somethingIsInTheWay = somethingWasAlreadyInTheWay;

            List<int> newGridXCoords = GetNewGridCoordinates(isDivisibleBySquareWidth: isDivisibleBySquareWidth.X, newGridCoord: newGridOrigin.X);
            List<int> newGridYCoords = GetNewGridCoordinates(isDivisibleBySquareWidth: isDivisibleBySquareWidth.Y, newGridCoord: newGridOrigin.Y);

            // These nested for loops work because at the moment we are just considering one square, not the whole shape.
            foreach (var xCoord in newGridXCoords)
            {
                foreach (var yCoord in newGridYCoords)
                {
                    if (xCoord >= occupiedGridSquares.Width
                        || yCoord >= occupiedGridSquares.Height
                        || xCoord < 0
                        || yCoord < 0)
                    {
                        somethingIsInTheWay = true;
                    }
                    else
                    {
                        somethingIsInTheWay = somethingIsInTheWay || occupiedGridSquares.IsSquareOccupied(x: xCoord, y: yCoord);
                    }
                }
            }

            return somethingIsInTheWay;
        }

        private List<int> GetNewGridCoordinates(bool isDivisibleBySquareWidth, int newGridCoord)
        {
            List<int> newGridCoords = new List<int>();

            if (isDivisibleBySquareWidth)
            {
                newGridCoords.Add(newGridCoord);
            }
            else
            {
                newGridCoords.Add(newGridCoord);
                newGridCoords.Add(newGridCoord + 1);
            }

            return newGridCoords;
        }

        private void CalculateNumSquaresAroundTopLeftCorner()
        {
            foreach (var square in _squares)
            {
                _numSquaresLeftOfTopLeftCorner = Math.Min(_numSquaresLeftOfTopLeftCorner, square.XRelativeToParentCorner);
                _numSquaresRightOfTopLeftCorner = Math.Max(_numSquaresRightOfTopLeftCorner, square.XRelativeToParentCorner);
                _numSquaresAboveTopLeftCorner = Math.Min(_numSquaresAboveTopLeftCorner, square.YRelativeToParentCorner);
                _numSquaresBelowTopLeftCorner = Math.Max(_numSquaresBelowTopLeftCorner, square.YRelativeToParentCorner);
            }

            DealWithNegativeNumbersOfSquares();
        }

        private void DealWithNegativeNumbersOfSquares()
        {
            _numSquaresLeftOfTopLeftCorner = Math.Abs(_numSquaresLeftOfTopLeftCorner);
            _numSquaresAboveTopLeftCorner = Math.Abs(_numSquaresAboveTopLeftCorner);
        }

        private int CalculateSnappedCoordinate(
            int newTopLeftCornerCoord,
            int boundaryRectangleOriginCoord,
            int boundaryRectangleDimension,
            int numSquaresOnSmallestSide,
            int numSquaresOnLargestSide)
        {
            var squareWidth = ShapeConstants.SquareWidth;

            int topLeftCornerCoord = newTopLeftCornerCoord;
            int numberOfSquaresFromEdgeOfScreen = topLeftCornerCoord / squareWidth;
            if (newTopLeftCornerCoord % squareWidth > squareWidth / 2)
            {
                numberOfSquaresFromEdgeOfScreen++;
            }

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

            int actualTopLeftCorner = topLeftCornerOfShapeEdgeOnOtherSideAdjustedForBothContainerEdges
                                     - (numSquaresOnLargestSide * squareWidth);

            return actualTopLeftCorner;
        }
    }
}