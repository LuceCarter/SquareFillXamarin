namespace SquareFillDomain.Models
{
    public class MovementResult
    {
        public bool ShapeHasCrossedAHorizontalGridBoundary { get; set; }
        public bool ShapeHasCrossedAVerticalGridBoundary { get; set; }
        public bool NoShapesAreInTheWay { get; set; }
    }
}