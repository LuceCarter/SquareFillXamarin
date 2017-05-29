﻿using SquareFillDomain.Builders;
using SquareFillDomain.Interfaces;
using SquareFillDomain.Utils;

namespace SquareFillDomain.Models
{
    public class Square
    {
        public SquareFillPoint PositionRelativeToParent { get; set; }
        public SquareFillPoint PositionRelativeToParentCorner { get; set; }
        public ISquareView Sprite { get; private set; }
        public SquareFillPoint TopLeftCorner { get; private set; }

        public Square()
        {
            TopLeftCorner = new SquareFillPoint(x: 0, y: 0);
            PositionRelativeToParent = new SquareFillPoint(x: 0, y: 0);
            PositionRelativeToParentCorner = new SquareFillPoint(x: 0, y: 0);
        }

        public Square(SquareFillPoint positionRelativeToParent, SquareFillPoint positionRelativeToParentCorner, ISquareView sprite)
        {
            PositionRelativeToParent = positionRelativeToParent;
            PositionRelativeToParentCorner = positionRelativeToParentCorner;
            Sprite = sprite;
            TopLeftCorner = new SquareFillPoint(x: 0, y: 0);
        }

        public void CalculateOrigin(SquareFillPoint parentShapeCentre)
        {
            TopLeftCorner = CalculatePotentialOrigin(parentShapeCentre: parentShapeCentre);
        }

        public void CalculateTopLeftCorner(SquareFillPoint parentTopLeftCorner)
        {
            TopLeftCorner = CalculatePotentialTopLeftCorner(parentTopLeftCorner: parentTopLeftCorner);
        }

        public SquareFillPoint CalculatePotentialOrigin(SquareFillPoint parentShapeCentre)
        {
            return new SquareFillPoint(
                x: parentShapeCentre.X + (PositionRelativeToParent.X * ShapeConstants.SquareWidth)
                    - ShapeConstants.SquareWidth / 2,
                y: parentShapeCentre.Y + (PositionRelativeToParent.Y * ShapeConstants.SquareWidth)
                    - ShapeConstants.SquareWidth / 2);
        }

        public SquareFillPoint CalculatePotentialTopLeftCorner(SquareFillPoint parentTopLeftCorner)
        {
            return new SquareFillPoint(
                x: parentTopLeftCorner.X + (PositionRelativeToParentCorner.X * ShapeConstants.SquareWidth),
                y: parentTopLeftCorner.Y + (PositionRelativeToParentCorner.Y * ShapeConstants.SquareWidth));
        }

        public bool IsInSquare(SquareFillPoint point)
        {
            return TopLeftCorner.X <= point.X
                   && point.X <= (TopLeftCorner.X + ShapeConstants.SquareWidth)
                   && TopLeftCorner.Y <= point.Y
                   && point.Y <= (TopLeftCorner.Y + ShapeConstants.SquareWidth);
        }
    }
}