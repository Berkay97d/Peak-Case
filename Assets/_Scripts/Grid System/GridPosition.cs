namespace Blast
{
    public struct GridPosition
    {
        private int x;
        private int y;
        private Grid grid;
        
        
        public GridPosition(Grid grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }
        
        public int GetX() {
            return x;
        }

        public int GetY() {
            return y;
        }
        
        public override string ToString()
        {
            return "x: " + x + " y: " + y;
        }
    }
}