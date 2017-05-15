﻿using System;
using System.Collections.Generic;
using SquareFillDomain.Builders;
using SquareFillDomain.Interfaces;
using SquareFillDomain.Utils;

namespace SquareFillDomain.Models
{
    public class Shape
	{
        public SquareFillPoint CentreOfShape { get; private set; }
        public List<Square> Squares { get; private set; }

	    public int NumSquaresLeftOfShapeCentre { get; private set; }
	    public int NumSquaresRightOfShapeCentre { get; private set; }
	    public int NumSquaresAboveShapeCentre { get; private set; }
	    public int NumSquaresBelowShapeCentre { get; private set; }

		public Shape(
            SquareFillColour colour,
            SquareFillPoint centreOfShape,
            List<SquareFillPoint> relativePoints,
            ISquareViewFactory squareFactory)
		{
            List<Square> squares = new List<Square>();

            foreach(var point in relativePoints)
            {
                squares.Add(new Square(
                    positionRelativeToParent: point,
                    sprite: squareFactory.MakeSquare(colour: colour)));
            }

            CentreOfShape = centreOfShape;
            Squares = squares;
        
            Initialise();
		}

        public Shape(
            SquareFillPoint centreOfShape,
            List<Square> squareDefinitions)
        {
            CentreOfShape = centreOfShape;
            Squares = squareDefinitions;
        
            Initialise();
        }

        public bool IsInShape(SquareFillPoint point)
        {
            bool isInShape = false;
            
            foreach(var square in Squares) 
            {
                isInShape = isInShape || square.IsInSquare(point: point);
            }

            return isInShape;
        }

        public void PutShapeInNewLocation(SquareFillPoint newCentreOfShape)
        {
            CentreOfShape = newCentreOfShape;
            foreach(var square in Squares) 
            {
                if (square.Sprite != null)
                {
                    square.Sprite.MoveSquare(
                        newX: CentreOfShape.X + (square.PositionRelativeToParent.X * ShapeSetBuilder.SquareWidth),
                        newY: CentreOfShape.Y + (square.PositionRelativeToParent.Y * ShapeSetBuilder.SquareWidth));
                }
            }
        }

        public void CalculateOrigins(SquareFillPoint newCentreOfShape)
        {
            foreach (var square in Squares) 
            {
                square.CalculateOrigin(parentShapeCentre: newCentreOfShape);
            }
        }

        public MovementResult AttemptToUpdateOrigins(
            List<List<GridSquare>> occupiedGridSquares,
            SquareFillPoint newShapeCentre)
        {
            bool somethingIsintheWay = false;
            var movementResult = new MovementResult();
        
            foreach (var square in Squares)
            {
                var newOrigin = square.CalculatePotentialOrigin(parentShapeCentre: newShapeCentre);

                List<int> newGridXCoords = new List<int>();
                List<int> newGridYCoords = new List<int>();

                int oldGridXCoord = square.Origin.X/ShapeSetBuilder.SquareWidth;
                int oldGridYCoord = square.Origin.Y/ShapeSetBuilder.SquareWidth;
                SquareFillPoint oldGridOrigin = new SquareFillPoint(
                    x: oldGridXCoord,
                    y: oldGridYCoord);

                bool oldXDivisibleBySquareWidth = 
                    square.Origin.X % ShapeSetBuilder.SquareWidth == 0;
                bool oldYDivisibleBySquareWidth = 
                    square.Origin.Y % ShapeSetBuilder.SquareWidth == 0;
            
                int newGridXCoord = newOrigin.X/ShapeSetBuilder.SquareWidth;
                int newGridYCoord = newOrigin.Y/ShapeSetBuilder.SquareWidth;

                var newGridOrigin = new SquareFillPoint(
                    x: newGridXCoord,
                    y: newGridYCoord);

                if (newOrigin.X < 0)
                {
                    newGridOrigin.X = newGridOrigin.X - 1;
                }

                if (newOrigin.Y < 0)
                {
                    newGridOrigin.Y = newGridOrigin.Y - 1;
                }

                bool newXDivisibleBySquareWidth = 
                    newOrigin.X % ShapeSetBuilder.SquareWidth == 0;
                bool newYDivisibleBySquareWidth = 
                    newOrigin.Y % ShapeSetBuilder.SquareWidth == 0;
            
                if (oldXDivisibleBySquareWidth != newXDivisibleBySquareWidth
                    || oldGridOrigin.X != newGridOrigin.X)
                {
                    movementResult.ShapeHasCrossedAHorizontalGridBoundary = true;
                }
            
                if (oldYDivisibleBySquareWidth != newYDivisibleBySquareWidth
                    || oldGridOrigin.Y != newGridOrigin.Y)
                {
                    movementResult.ShapeHasCrossedAVerticalGridBoundary = true;
                }
            
                if (movementResult.ShapeHasCrossedAHorizontalGridBoundary
                    || movementResult.ShapeHasCrossedAVerticalGridBoundary)
                {
                    if (newXDivisibleBySquareWidth)
                    {
                        newGridXCoords.Add(newGridOrigin.X);
                    } else
                    {
                        newGridXCoords.Add(newGridOrigin.X);
                        newGridXCoords.Add(newGridOrigin.X + 1);
                    }
                
                    if (newYDivisibleBySquareWidth)
                    {
                        newGridYCoords.Add(newGridOrigin.Y);
                    } 
                    else
                    {
                        newGridYCoords.Add(newGridOrigin.Y);
                        newGridYCoords.Add(newGridOrigin.Y + 1);
                    }
                
                    // These nested for loops work because at the moment we are just considering one square, not the whole shape.
                    foreach (var xCoord in newGridXCoords) 
                    {
                        foreach (var yCoord in newGridYCoords) {
                            if (xCoord >= occupiedGridSquares.Count
                                || yCoord >= occupiedGridSquares[0].Count
                                || xCoord < 0
                                || yCoord < 0)
                            {
                                somethingIsintheWay = true;
                            } 
                            else
                            {
                                somethingIsintheWay = somethingIsintheWay
                                    || occupiedGridSquares[xCoord][yCoord].Occupied;
                            }
                        }
                    }
                }
            }
        
            if (!somethingIsintheWay) 
            {
                foreach (var square in Squares)
                {
                    square.CalculateOrigin(parentShapeCentre: newShapeCentre);
                }
            }

             movementResult.NoShapesAreInTheWay = !somethingIsintheWay;

             return movementResult;
        }

        public void VacateGridSquares(List<List<GridSquare>>occupiedGridSquares) 
        {
            foreach (var square in Squares)
            {
                int occupiedXCoordinate = square.Origin.X/ShapeSetBuilder.SquareWidth;
                int occupiedYCoordinate = square.Origin.Y/ShapeSetBuilder.SquareWidth;
            
                occupiedGridSquares[occupiedXCoordinate][occupiedYCoordinate].Occupied = false;
                occupiedGridSquares[occupiedXCoordinate][occupiedYCoordinate].ShapeInSquare = null;
            }
        }

        public void OccupyGridSquares(List<List<GridSquare>> occupiedGridSquares)
        {
            foreach (var square in Squares)
            {
                int occupiedXCoordinate = square.Origin.X / ShapeSetBuilder.SquareWidth;
                int occupiedYCoordinate = square.Origin.Y / ShapeSetBuilder.SquareWidth;

                occupiedGridSquares[occupiedXCoordinate][occupiedYCoordinate].Occupied = true;
                occupiedGridSquares[occupiedXCoordinate][occupiedYCoordinate].ShapeInSquare = this;
            }
        }

        private void Initialise()
        {
            foreach(var square in Squares)
            {
                square.CalculateOrigin(parentShapeCentre: CentreOfShape);
            }
        
            CalculateNumSquaresAroundCentre();
            PutShapeInNewLocation(newCentreOfShape: CentreOfShape);
        }

        private void CalculateNumSquaresAroundCentre()
        {
            foreach (var square in Squares) {
                NumSquaresLeftOfShapeCentre = Math.Min(NumSquaresLeftOfShapeCentre, square.PositionRelativeToParent.X);
            
                NumSquaresRightOfShapeCentre = Math.Max(NumSquaresRightOfShapeCentre, square.PositionRelativeToParent.X);
            
                NumSquaresAboveShapeCentre = Math.Min(NumSquaresAboveShapeCentre, square.PositionRelativeToParent.Y);
            
                NumSquaresBelowShapeCentre = Math.Max(NumSquaresBelowShapeCentre, square.PositionRelativeToParent.Y);
            }
        
            DealWithNegativeNumbersOfSquares();
        }

        private void DealWithNegativeNumbersOfSquares()
        {
           NumSquaresLeftOfShapeCentre = Math.Abs(NumSquaresLeftOfShapeCentre);
           NumSquaresAboveShapeCentre = Math.Abs(NumSquaresAboveShapeCentre);
        }
	}
}