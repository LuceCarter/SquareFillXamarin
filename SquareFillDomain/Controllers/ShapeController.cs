using System.Collections.Generic;
using System.Diagnostics;
using SquareFillDomain.Builders;
using SquareFillDomain.Interfaces;
using SquareFillDomain.Models;
using SquareFillDomain.Utils;

namespace SquareFillDomain.Controllers
{
    public class ShapeController
    {
        public Shape ShapeToMove = null;
        public Grid OccupiedGridSquares;
        public ShapeSet ShapeSet = null;
    
        private readonly ShapeMover _shapeMover;
        private SquareFillPoint _lastGoodLocation;
        private bool _colliding = false;

        public ShapeController(IShapeSetBuilder shapeSetBuilder)
        {
            OccupiedGridSquares = shapeSetBuilder.MakeGridSquares();
            ShapeSet = shapeSetBuilder.GetShapeSet();
            PutAllShapesIntoGrid(shapeSetBuilder);

            _shapeMover = new ShapeMover();
            _lastGoodLocation = new SquareFillPoint(x:0, y:0);
        }

        public void StartMove(SquareFillPoint cursorPositionAtStart)
        {
            ShapeToMove = ShapeSet.SelectShape(selectedPoint: cursorPositionAtStart);
        
            if (ShapeToMove != null) 
            {
                ShapeToMove.VacateGridSquares(occupiedGridSquares: OccupiedGridSquares);
                _shapeMover.StartMove(cursorPositionAtStart: cursorPositionAtStart, shapeToMove: ShapeToMove);
                _lastGoodLocation = cursorPositionAtStart;
            }
        }

        public void ContinueMove(SquareFillPoint newLocation) 
        {
            if (ShapeToMove != null)
            {
                SquareFillPoint newTopLeftCorner = _shapeMover.CalculateTopLeftCorner(newCursorPosition: newLocation);
                SquareFillPoint positionInGrid = CalculateGridPosition(topLeftCorner: newTopLeftCorner);
                var logger = new Logger();

                bool cursorIsInShape = ShapeToMove.IsInShape(point: newLocation);
                MovementResult movementResult = CheckWhetherMovementIsPossible(
                    newTopLeftCorner: newTopLeftCorner,
                    logger: logger);

                logger = logger
                    .Clear()
                    .Plus(desc: "Cursor", point: newLocation)
                    .Plus(desc: "NewCorner", point: newTopLeftCorner)
                    .Plus(desc: "NewGrid", point: positionInGrid);
            
                if(_colliding == false) {
                    if(movementResult.NoShapesAreInTheWay)
                    {
                        _shapeMover.MoveToNewCursorPosition(newCursorPosition: newLocation);
                        _lastGoodLocation = newLocation;
                        LogMessagePlusOrigins(logger: logger, message: "Clear. ");
                    } 
                    else
                    {
                        _colliding = true;
                        _lastGoodLocation = _shapeMover.CalculateCursorPosition(topLeftCorner: ShapeToMove.TopLeftCorner);

                        SnapToGridInRelevantDimensionsIfPossible(
                            movementResult: movementResult,
                            previousTopLeftCorner: ShapeToMove.TopLeftCorner);
                        ShapeToMove.CalculateTopLeftCorners(newTopLeftCorner: ShapeToMove.TopLeftCorner);

                        LogMessagePlusOrigins(logger: logger, message: "Blocked. ");
                    }
                }
                else 
                {
                    if (cursorIsInShape)
                    {
                        _shapeMover.StartMove(cursorPositionAtStart: newLocation, shapeToMove: ShapeToMove);
                        _lastGoodLocation = newLocation;
                        _colliding = false;
                        LogMessagePlusOrigins(logger: logger, message: "Moving again. ");
                    }
                }
            }
        }
    
        public void EndMove(SquareFillPoint finalLocation) 
        {
            if (ShapeToMove != null)
            {
                SquareFillPoint newTopLeftCorner = _shapeMover.CalculateTopLeftCorner(newCursorPosition: finalLocation);
                MovementResult movementResult = ShapeToMove.AttemptToUpdateOrigins(
                    occupiedGridSquares: OccupiedGridSquares,
                    newTopLeftCorner: newTopLeftCorner);
            
                if(movementResult.NoShapesAreInTheWay && !_colliding)
                {
                    LogLocation(message: "End move with no obstacles.", locationName: "Final", location: finalLocation);
                    _shapeMover.SnapToGrid(newCursorPosition: finalLocation);
                } 
                else
                {
                    LogLocation(message: "End move with obstacles.", locationName: "LastGood",
                        location: _lastGoodLocation);
                    _shapeMover.SnapToGrid(newCursorPosition: _lastGoodLocation);
                }
            
                ShapeToMove.OccupyGridSquares(occupiedGridSquares: OccupiedGridSquares);
                ShapeToMove = null;
                _colliding = false;
            }
        }

        private MovementResult CheckWhetherMovementIsPossible(SquareFillPoint newTopLeftCorner, Logger logger)
        {
            logger.Clear().Plus(desc: "Origins1", squares: ShapeToMove.Squares).Log();
            var movementResult = ShapeToMove.AttemptToUpdateOrigins(
                occupiedGridSquares: OccupiedGridSquares,
                newTopLeftCorner: newTopLeftCorner);
            logger.Clear().Plus(desc: "Origins2", squares: ShapeToMove.Squares).Log();

            return movementResult;
        }

        private SquareFillPoint CalculateGridPosition(SquareFillPoint topLeftCorner)
        {
            return new SquareFillPoint(
                x: topLeftCorner.X / ShapeConstants.SquareWidth,
                y: topLeftCorner.Y / ShapeConstants.SquareWidth);
        }

        private void LogMessagePlusOrigins(Logger logger, string message)
        {
            logger
                .Plus(desc: "Origins", squares: ShapeToMove.Squares)
                .Make(message: message)
                .Log();
        }

        private void LogLocation(string message, string locationName, SquareFillPoint location)
        {
            string xCoord = location.X.ToString();
            string yCoord = location.Y.ToString();
            Debug.WriteLine(message + " " + locationName + "(x:" + xCoord + ",y:" + yCoord + ")");
        }

        private void PutAllShapesIntoGrid(IShapeSetBuilder shapeSetBuilder)
        {
            shapeSetBuilder.OccupyBorderSquares(occupiedGridSquares: OccupiedGridSquares);
            foreach (var shape in ShapeSet.Shapes)
            {
                shape.OccupyGridSquares(occupiedGridSquares: OccupiedGridSquares);
            }
        }

        private void SnapToGridInRelevantDimensionsIfPossible(
            MovementResult movementResult,
            SquareFillPoint previousTopLeftCorner)
        {
            var newTopLeftCorner = new SquareFillPoint(x: previousTopLeftCorner.X, y: previousTopLeftCorner.Y);

            if (movementResult.ShapeHasCrossedAHorizontalGridBoundary)
            {
                newTopLeftCorner.X = _shapeMover.CalculateSnappedX(newTopLeftCornerX: newTopLeftCorner.X);
            }

            if (movementResult.ShapeHasCrossedAVerticalGridBoundary)
            {
                newTopLeftCorner.Y = _shapeMover.CalculateSnappedY(newTopLeftCornerY: newTopLeftCorner.Y);
            }

            MovementResult newMovementResult = ShapeToMove.AttemptToUpdateOrigins(
                occupiedGridSquares: OccupiedGridSquares,
                newTopLeftCorner: newTopLeftCorner);

            if (newMovementResult.NoShapesAreInTheWay)
            {
                ShapeToMove.MoveAllShapeSquares(newTopLeftCorner: newTopLeftCorner);
                ShapeToMove.CalculateTopLeftCorners(newTopLeftCorner: newTopLeftCorner);
            }
        }
    }
}