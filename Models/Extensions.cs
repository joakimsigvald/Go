namespace Go.Models
{
    public static class Extensions
    {
        public static BoardState GetOpposite(this BoardState color)
        {
            return (BoardState)(BoardState.Outside - color);
        }

        public static bool IsStoneColor(this BoardState state)
        {
            return state == BoardState.Black || state == BoardState.White;
        }
    }
}
