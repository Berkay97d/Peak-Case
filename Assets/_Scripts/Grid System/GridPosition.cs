namespace Blast
{
    public struct GridPosition
    {
        private int m_x;
        private int m_y;
        private Grid m_grid;
        
        
        public GridPosition(Grid grid, int x, int y)
        {
            m_grid = grid;
            m_x = x;
            m_y = y;
        }
        
        public int GetX() {
            return m_x;
        }

        public int GetY() {
            return m_y;
        }
        
        public override string ToString()
        {
            return $"x: {m_x} y: {m_y}";
        }
    }
}