﻿using SquareFillDomain.Interfaces;
using SquareFillDomain.Utils;

namespace SquareFillDomain.UnitTests
{
    public class MockSquareFactory : ISquareViewFactory
    {
        public ISquareView MakeSquare(SquareFillColour colour)
        {
            return new MockSquareView();
        }
    }
}