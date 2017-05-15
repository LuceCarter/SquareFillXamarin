﻿using System;
using NUnit.Framework;
using SquareFillDomain.Builders;
using SquareFillDomain.Controllers;
using SquareFillDomain.Interfaces;
using SquareFillDomain.Models;
using SquareFillDomain.Utils;

namespace SquareFillDomain.UnitTests
{
    public class MockSquareView : ISquareView
    {
        private readonly SquareFillPoint _centre = new SquareFillPoint(x: 0, y: 0);

        public void MoveSquare(int newX, int newY)
        {
            _centre.X = newX;
            _centre.Y = newY;
        }

        public SquareFillPoint Centre()
        {
            return _centre;
        }
    }
}