using System;
using SquareFillDomain.Builders;
using SquareFillDomain.Models;

namespace SquareFillDomain.Utils
{
    internal class ShapeMover
    {
        public Shape ShapeToMove { get; private set; }
        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }

        private SquareFillPoint _shapeCentreRelativeToCursorPosition = new SquareFillPoint(x: 0, y: 0);

        public ShapeMover(int screenWidth, int screenHeight)
        {
            ScreenWidth = RoundDimensionDownToMultipleOfSquareWidth(screenDimension: screenWidth);
            ScreenHeight = RoundDimensionDownToMultipleOfSquareWidth(screenDimension: screenHeight);
        }

        public void StartMove(SquareFillPoint cursorPositionAtStart, Shape shapeToMove)
        {
            ShapeToMove = shapeToMove;
            _shapeCentreRelativeToCursorPosition.X = ShapeToMove.CentreOfShape.X - cursorPositionAtStart.X;
            _shapeCentreRelativeToCursorPosition.Y = ShapeToMove.CentreOfShape.Y - cursorPositionAtStart.Y;
        }

        public SquareFillPoint CalculateShapeCentre(SquareFillPoint newCursorPosition)
        {
            return new SquareFillPoint(
                x: newCursorPosition.X + _shapeCentreRelativeToCursorPosition.X,
                y: newCursorPosition.Y + _shapeCentreRelativeToCursorPosition.Y);
        }

        public SquareFillPoint CalculateCursorPosition(SquareFillPoint centreOfShape)
        {
            return new SquareFillPoint(
                x: centreOfShape.X - _shapeCentreRelativeToCursorPosition.X,
                y: centreOfShape.Y - _shapeCentreRelativeToCursorPosition.Y);
        }

        public void MoveToNewCursorPosition(SquareFillPoint newCursorPosition)
        {
            if (ShapeToMove != null)
            {
                var newShapeCentre = CalculateShapeCentre(newCursorPosition: newCursorPosition);
                ShapeToMove.PutShapeInNewLocation(newCentreOfShape: newShapeCentre);
            }
        }

        public void SnapToGrid(SquareFillPoint newCursorPosition)
        {
            if (ShapeToMove != null)
            {
                var shapeCentreTakingRelativePositionIntoAccount =
                    CalculateShapeCentre(newCursorPosition: newCursorPosition);
                var newShapeCentre = new SquareFillPoint(
                    x: CalculateSnappedX(newShapeCentreX: shapeCentreTakingRelativePositionIntoAccount.X),
                    y: CalculateSnappedY(newShapeCentreY: shapeCentreTakingRelativePositionIntoAccount.Y));
        
                ShapeToMove.PutShapeInNewLocation(newCentreOfShape: newShapeCentre);
                ShapeToMove.CalculateOrigins(newCentreOfShape: newShapeCentre);
            }
        }

        public int CalculateSnappedX(int newShapeCentreX)
        {
            return CalculateSnappedCoordinate(
                newShapeCentreCoord: newShapeCentreX,
                boundaryRectangleOriginCoord: 0,
                boundaryRectangleDimension: ScreenWidth,
                numSquaresOnSmallestSide: ShapeToMove.NumSquaresLeftOfShapeCentre,
                numSquaresOnLargestSide:  ShapeToMove.NumSquaresRightOfShapeCentre);
        }

        public int CalculateSnappedY(int newShapeCentreY)
        {
            return CalculateSnappedCoordinate(
                newShapeCentreCoord: newShapeCentreY,
                boundaryRectangleOriginCoord: 0,
                boundaryRectangleDimension: ScreenHeight,
                numSquaresOnSmallestSide: ShapeToMove.NumSquaresAboveShapeCentre,
                numSquaresOnLargestSide: ShapeToMove.NumSquaresBelowShapeCentre);
        }

        private int RoundDimensionDownToMultipleOfSquareWidth(int screenDimension)
        {
            var maxNumberOfGridSquaresInDimension = screenDimension / ShapeSetBuilder.SquareWidth;
            return maxNumberOfGridSquaresInDimension * ShapeSetBuilder.SquareWidth;
        }

        private int CalculateSnappedCoordinate(
            int newShapeCentreCoord, 
            int boundaryRectangleOriginCoord, 
            int boundaryRectangleDimension, 
            int numSquaresOnSmallestSide, 
            int numSquaresOnLargestSide)
        {
            var squareWidth = ShapeSetBuilder.SquareWidth;

            int shapeCentreCoord = newShapeCentreCoord;
            int numberOfSquaresFromEdgeOfScreen = shapeCentreCoord / squareWidth;

            var potentialNewSquareCentre = numberOfSquaresFromEdgeOfScreen * squareWidth +
                                           (squareWidth/2) ;

            var squareCentreAtOneEndOfContainer = boundaryRectangleOriginCoord + squareWidth/2;

            var squareCentreAtOtherEndOfContainer =
                boundaryRectangleOriginCoord + boundaryRectangleDimension - squareWidth/2;

            var potentialCentreOfShapeEdgeOnOneSide =
                potentialNewSquareCentre - numSquaresOnSmallestSide * squareWidth;

            var centreOfShapeEdgeOnOneSideAdjustedForSmallestContainerEdge =
                Math.Max(potentialCentreOfShapeEdgeOnOneSide, squareCentreAtOneEndOfContainer);

            var shapeCentreAdjustedForSmallestContainerEdge =
                centreOfShapeEdgeOnOneSideAdjustedForSmallestContainerEdge
                + (numSquaresOnSmallestSide * squareWidth);

            var potentialCentreOfShapeEdgeOnOtherSide = shapeCentreAdjustedForSmallestContainerEdge +
                                                        (numSquaresOnLargestSide * squareWidth);

            var centreOfShapeEdgeOnOtherSideAdjustedForBothContainerEdges =
                Math.Min(potentialCentreOfShapeEdgeOnOtherSide, squareCentreAtOtherEndOfContainer);

            int actualSquareCentre = centreOfShapeEdgeOnOtherSideAdjustedForBothContainerEdges
                                     - (numSquaresOnLargestSide * squareWidth);

            return actualSquareCentre;
        }
    }
}