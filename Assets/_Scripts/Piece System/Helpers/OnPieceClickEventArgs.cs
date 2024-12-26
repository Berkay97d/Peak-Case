namespace Blast
{
    public class OnPieceClickEventArgs
    {
        private Piece piece;
        private Cell cell;

        public OnPieceClickEventArgs(Piece piece, Cell cell)
        {
            this.piece = piece;
            this.cell = cell;
        }

        public Piece GetPiece()
        {
            return piece;
        }

        public Cell GetCell()
        {
            return cell;
        }
    }
}