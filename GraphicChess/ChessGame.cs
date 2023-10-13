// Lucas Palomino
// CSCI 303
// December 12, 2022

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace GraphicChess
{
    public partial class ChessGame : Form
    {
        private const int boardSize = 8;
        private int boardSquareWidth = 40, boardSquareHeight = 40, boardHOffset = 30, boardVOffset = 30;//in pixels
	    public struct Point
	    {
		    public int x;
		    public int y;
            public Point(int xVal, int yVal) { x = xVal; y = yVal; }
		    public static bool operator==(Point p, Point q) {return p.x == q.x && p.y == q.y;}
            public static bool operator!=(Point p, Point q) { return !(p == q); }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }
	    }
         
	    public struct ChessMove
	    {
		    public Point origin;
            public Point destination;
            public ChessMove(ref Point theOrigin, ref Point theDestination) { origin = theOrigin; destination = theDestination; }
		    public static bool operator==(ChessMove m1, ChessMove m2) {return m1.origin == m2.origin && m1.destination == m2.destination;}
            public static bool operator !=(ChessMove m1, ChessMove m2) { return !(m1 == m2); }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }
	    };

        public abstract class Piece
        {
            public PLAYER whosePiece;  
            public Point boardLocation;
            public double value;
            public System.Drawing.Bitmap imageFile;
            public Piece(int xLocation, int yLocation, PLAYER owner, double theValue = 0)
            {
                boardLocation.x = xLocation;
                boardLocation.y = yLocation;
                whosePiece = owner;
                value = theValue;
                if (owner != PLAYER.WHITE) value *= -1;
            }
            public abstract int getLegalMoves(List<ChessMove> moves);
        }

        public class Pawn : Piece
        {
            public Pawn(int xLocation, int yLocation, PLAYER owner) : base(xLocation, yLocation, owner, 1)
            {
                imageFile = (owner == PLAYER.WHITE) ? bitmaps.wpawn : bitmaps.bpawn;
            }
            public override int getLegalMoves(List<ChessMove> moves)
            {
                int numMoves = 0;
                PLAYER opponent;
                int direction, baseRank, farEnd;
                ChessMove curMove;

                if (whosePiece == PLAYER.WHITE)
                {
                    opponent = PLAYER.BLACK;
                    direction = 1;
                    baseRank = 1;
                    farEnd = boardSize - 1;
                }
                else
                {
                    opponent = PLAYER.WHITE;
                    direction = -1;
                    baseRank = boardSize - 2;
                    farEnd = 0;
                }

                if (boardLocation.y != farEnd && board[boardLocation.x, boardLocation.y + direction] == null)
                {
                    curMove.origin = boardLocation;
                    curMove.destination.x = boardLocation.x;
                    curMove.destination.y = boardLocation.y + direction;
                    moves.Add(curMove);
                    numMoves++;
                }

                if (boardLocation.y == baseRank && board[boardLocation.x, boardLocation.y + direction] == null &&
                    board[boardLocation.x, boardLocation.y + 2 * direction] == null)
                {
                    curMove.origin = boardLocation;
                    curMove.destination.x = boardLocation.x;
                    curMove.destination.y = boardLocation.y + 2 * direction;
                    moves.Add(curMove);
                    numMoves++;
                }

                if (boardLocation.y != farEnd && boardLocation.x > 0 &&
                    board[boardLocation.x - 1, boardLocation.y + direction] != null &&
                    board[boardLocation.x - 1, boardLocation.y + direction].whosePiece == opponent)
                {
                    //Capture an enemy piece.
                    curMove.origin = boardLocation;
                    curMove.destination.x = boardLocation.x - 1;
                    curMove.destination.y = boardLocation.y + direction;
                    moves.Add(curMove);
                    numMoves++;
                }

                if (boardLocation.y != farEnd && boardLocation.x < boardSize - 1 &&
                    board[boardLocation.x + 1, boardLocation.y + direction] != null &&
                    board[boardLocation.x + 1, boardLocation.y + direction].whosePiece == opponent)
                {
                    //Capture an enemy piece.
                    curMove.origin = boardLocation;
                    curMove.destination.x = boardLocation.x + 1;
                    curMove.destination.y = boardLocation.y + direction;
                    moves.Add(curMove);
                    numMoves++;
                }

                return numMoves;
            }
        }

        public class Knight : Piece
        {
            public Knight(int xLocation, int yLocation, PLAYER owner) : base(xLocation, yLocation, owner, 3)
            {
                imageFile = (owner == PLAYER.WHITE) ? bitmaps.wknight : bitmaps.bknight;
            }
            public override int getLegalMoves(List<ChessMove> moves)
            {
                int numMoves = 0;
                int dx, dy;
                Point destination;
                ChessMove curMove;

                for (dx = -2; dx <= 2; dx++)
                {
                    for (dy = -2; dy <= 2; dy++)
                    {
                        //Knights can move 2 squares in one direction and 1 square in another.
                        if (Math.Abs(dx) + Math.Abs(dy) != 3) continue;

                        destination.x = boardLocation.x + dx;
                        destination.y = boardLocation.y + dy;
                        if (!isOnBoard(destination)) continue;
                        Piece p = board[destination.x, destination.y];

                        if (p == null || p.whosePiece != whosePiece)
                        {
                            curMove.origin = boardLocation;
                            curMove.destination = destination;
                            moves.Add(curMove);
                            numMoves++;
                        }
                    }
                }

                return numMoves;
            }
        }

        public class Bishop : Piece
        {
            public Bishop(int xLocation, int yLocation, PLAYER owner) : base(xLocation, yLocation, owner, 3.1)
            {
                imageFile = (owner == PLAYER.WHITE) ? bitmaps.wbishop : bitmaps.bbishop;
            }
            public override int getLegalMoves(List<ChessMove> moves)
            {
                int numMoves = 0;
                int i, xDirection, yDirection;
                Point destination;
                ChessMove curMove;

                for (xDirection = -1; xDirection <= 1; xDirection += 2)
                {
                    for (yDirection = -1; yDirection <= 1; yDirection += 2)
                    {
                        for (i = 1; i < boardSize; i++)
                        {
                            destination.x = boardLocation.x + i * xDirection;
                            destination.y = boardLocation.y + i * yDirection;
                            if (!isOnBoard(destination)) break; // If is out of bounds
                            Piece p = board[destination.x, destination.y];

                            if (p == null || p.whosePiece != whosePiece)
                            {
                                curMove.origin = boardLocation;
                                curMove.destination = destination;
                                moves.Add(curMove);
                                numMoves++;
                            }

                            //For same player pieces, can't go past them
                            if (board[destination.x, destination.y] != null) break;
                        }
                    }
                }

                return numMoves;
            }
        }

        public class Rook : Piece
        {
            public Rook(int xLocation, int yLocation, PLAYER owner) : base(xLocation, yLocation, owner, 5)
            {
                imageFile = (owner == PLAYER.WHITE) ? bitmaps.wrook : bitmaps.brook;
            }
            public override int getLegalMoves(List<ChessMove> moves)
            {
                int numMoves = 0;
                int i, xDirection, yDirection;
                Point destination;
                ChessMove curMove;

                for (xDirection = -1; xDirection <= 1; xDirection++)
                {
                    for (yDirection = -1; yDirection <= 1; yDirection++)
                    {
                        if (Math.Abs(xDirection) + Math.Abs(yDirection) > 1) continue;

                        for (i = 1; i < boardSize; i++)
                        {
                            destination.x = boardLocation.x + i * xDirection;
                            destination.y = boardLocation.y + i * yDirection;
                            if (!isOnBoard(destination)) break;
                            Piece p = board[destination.x, destination.y];

                            if (p == null || p.whosePiece != whosePiece)
                            {
                                curMove.origin = boardLocation;
                                curMove.destination = destination;
                                moves.Add(curMove);
                                numMoves++;
                            }

                            //A rook cannot move through other pieces.
                            if (board[destination.x, destination.y] != null) break;
                        }
                    }
                }

                return numMoves;
            }
        }

        public class Queen : Piece
        {
            public Queen(int xLocation, int yLocation, PLAYER owner) : base(xLocation, yLocation, owner, 9)
            {
                imageFile = (owner == PLAYER.WHITE) ? bitmaps.wqueen : bitmaps.bqueen;
            }
            public override int getLegalMoves(List<ChessMove> moves)
            {
                int numMoves = 0;
                int i, xDirection, yDirection;
                Point destination;
                ChessMove curMove;

                // YOUR CODE HERE

                for(xDirection = -1; xDirection <= 1; xDirection++)
                {
                    for(yDirection = -1; yDirection <= 1; yDirection++)
                    {
                        if (Math.Abs(xDirection) + Math.Abs(yDirection) == 0) continue;

                        for(i = 1; i < boardSize; i++)
                        {
                            destination.x = boardLocation.x + i * xDirection;
                            destination.y = boardLocation.y + i * yDirection;
                            if (!isOnBoard(destination)) break;
                            Piece p = board[destination.x, destination.y];

                            if (p == null || p.whosePiece != whosePiece)
                            {
                                curMove.origin = boardLocation;
                                curMove.destination = destination;
                                moves.Add(curMove);
                                numMoves++;
                            }
                            if (p != null) break;
                        }
                    }
                }
                return numMoves;
            }
        }

        public class King : Piece
        {
            public King(int xLocation, int yLocation, PLAYER owner) : base(xLocation, yLocation, owner, 1000)
            {
                imageFile = (owner == PLAYER.WHITE) ? bitmaps.wking : bitmaps.bking;
            }
            public override int getLegalMoves(List<ChessMove> moves)
            {
                int numMoves = 0;
                int xDirection, yDirection;
                Point destination;
                ChessMove curMove;

                for (xDirection = -1; xDirection <= 1; xDirection++)
                {
                    for (yDirection = -1; yDirection <= 1; yDirection++)
                    {
                        destination.x = boardLocation.x + xDirection;
                        destination.y = boardLocation.y + yDirection;
                        if (!isOnBoard(destination)) continue;
                        Piece p = board[destination.x, destination.y];

                        if (p == null || p.whosePiece != whosePiece)
                        {
                            curMove.origin = boardLocation;
                            curMove.destination = destination;
                            moves.Add(curMove);
                            numMoves++;
                        }
                    }
                }

                return numMoves;
            }
        }

        private static Piece[,] board;
        private bool[] rook1Moved;
	    private bool[] rook2Moved;
	    private bool[] kingMoved;

	    public enum PLAYER
        {
            INVALID = -1, WHITE, BLACK
        }
	    
	    private PLAYER whoseTurn;
        private List<ChessMove> moveHistory;
        private int moveIndex;

        public ChessGame()
        {
            InitializeComponent();

            board = new Piece[boardSize, boardSize];
            rook1Moved = new bool[2];
            rook2Moved = new bool[2];
            kingMoved = new bool[2];

            setupBoard();     

            moveHistory = new List<ChessMove>();
            moveIndex = 0;
            undoMoveToolStripMenuItem.Enabled = redoMoveToolStripMenuItem.Enabled = false;
            btnSubmit.Enabled = false;
        }

        private void form_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            int x = (me.X - boardHOffset) / boardSquareWidth;
            int y = boardSize - 1 - (me.Y - boardVOffset) / boardSquareHeight;
            if (!isOnBoard(new Point(x, y))) return;
            if (me.Button == System.Windows.Forms.MouseButtons.Left)
            {
                textBox2.Text = x + "," + y;
            }
            else
            {
                textBox1.Text = x + "," + y;
            }
            drawBoard();
        }

        private void setupBoard()
        {
            int i, j;
            PLAYER player;

            for (i = 0; i < boardSize; i++)
            {
                for(j = 0; j < boardSize; j++)
                {
                    board[i, j] = null;
                }
            }

            for (player = PLAYER.WHITE; player <= PLAYER.BLACK; player++)
            {
                int pawnRank = (player == PLAYER.WHITE) ? 1 : 6;
                int baseRank = (player == PLAYER.WHITE) ? 0 : 7;
                for (i = 0; i < boardSize; i++)
                {
                    board[i, pawnRank] = new Pawn(i, pawnRank, player);
                }

                for (i = 0; i < 2; i++)
                {
                    board[i * 7, baseRank] = new Rook(i * 7, baseRank, player);
                    board[1 + i * 5, baseRank] = new Knight(1 + i * 5, baseRank, player);
                    board[2 + i * 3, baseRank] = new Bishop(2 + i * 3, baseRank, player);
                }

                board[3, baseRank] = new Queen(3, baseRank, player);
                board[4, baseRank] = new King(4, baseRank, player);
            }

            for (i = 0; i < 2; i++)
            {
                rook1Moved[i] = false;
                rook2Moved[i] = false;
                kingMoved[i] = false;
            }

            whoseTurn = PLAYER.WHITE;
        }

        private void setupBoardTPQ1()
        {
            int i, j;
            PLAYER player;

            for (i = 0; i < boardSize; i++)
            {
                for (j = 0; j < boardSize; j++)
                {
                    board[i, j] = null;
                }
            }

            board[0, 1] = new Pawn(0, 1, PLAYER.WHITE);
            board[2, 1] = new Pawn(2, 1, PLAYER.WHITE);
            board[2, 2] = new Pawn(2, 2, PLAYER.WHITE);
            board[3, 3] = new Pawn(3, 3, PLAYER.WHITE);
            board[4, 3] = new Pawn(4, 3, PLAYER.WHITE);
            board[5, 2] = new Pawn(5, 2, PLAYER.WHITE);
            board[6, 1] = new Pawn(6, 1, PLAYER.WHITE);
            board[7, 3] = new Pawn(7, 3, PLAYER.WHITE);

            board[0, 3] = new Pawn(0, 3, PLAYER.BLACK);
            for(int repeatedIndex = 1; repeatedIndex < 8; repeatedIndex++)
            {
                if (repeatedIndex == 4) continue;
                board[repeatedIndex, 6] = new Pawn(repeatedIndex, 6, PLAYER.BLACK);
            }
            board[4, 5] = new Pawn(4, 5, PLAYER.BLACK);

            board[0, 0] = new Rook(0, 0, PLAYER.WHITE);
            board[7, 0] = new Rook(7, 0, PLAYER.WHITE);
            board[0, 7] = new Rook(0, 7, PLAYER.BLACK);
            board[7, 7] = new Rook(7, 7, PLAYER.BLACK);

            board[4, 4] = new Knight(4, 4, PLAYER.WHITE);
            board[1, 7] = new Knight(1, 7, PLAYER.BLACK);
            board[6, 7] = new Knight(6, 7, PLAYER.BLACK);

            board[5, 0] = new Bishop(5, 0, PLAYER.WHITE);
            board[6, 4] = new Bishop(6, 4, PLAYER.WHITE);
            board[2, 7] = new Bishop(2, 7, PLAYER.BLACK);

            board[3, 0] = new Queen(3, 0, PLAYER.WHITE);
            board[5, 5] = new Queen(5, 5, PLAYER.BLACK);

            board[4, 0] = new King(4, 0, PLAYER.WHITE);
            board[4, 7] = new King(4, 7, PLAYER.BLACK);

            for (i = 0; i < 2; i++)
            {
                rook1Moved[i] = false;
                rook2Moved[i] = false;
                kingMoved[i] = false;
            }

            whoseTurn = PLAYER.WHITE;
        }

        private void setUpBoardTPQ2()
        {
            int i, j;
            PLAYER player;

            for (i = 0; i < boardSize; i++)
            {
                for (j = 0; j < boardSize; j++)
                {
                    board[i, j] = null;
                }
            }

            for (int col = 0; col < 4; col++)
            {
                board[col, 1] = new Pawn(col, 1, PLAYER.WHITE);
            }
            board[5, 2] = new Pawn(5, 2, PLAYER.WHITE);
            board[7, 1] = new Pawn(7, 1, PLAYER.WHITE);

            board[0, 3] = new Pawn(0, 3, PLAYER.BLACK);
            board[1, 3] = new Pawn(1, 3, PLAYER.BLACK);
            board[2, 5] = new Pawn(2, 5, PLAYER.BLACK);
            board[4, 6] = new Pawn(4, 6, PLAYER.BLACK);
            board[6, 3] = new Pawn(6, 3, PLAYER.BLACK);
            board[6, 6] = new Pawn(6, 6, PLAYER.BLACK);
            board[7, 6] = new Pawn(7, 6, PLAYER.BLACK);

            board[0, 0] = new Rook(0, 0, PLAYER.WHITE);
            board[7, 0] = new Rook(7, 0, PLAYER.WHITE);
            board[0, 4] = new Rook(0, 4, PLAYER.BLACK);
            board[7, 7] = new Rook(7, 7, PLAYER.BLACK);

            board[5, 5] = new Knight(5, 5, PLAYER.WHITE);
            board[6, 0] = new Knight(6, 0, PLAYER.WHITE);
            board[1, 7] = new Knight(1, 7, PLAYER.BLACK);

            board[2, 0] = new Bishop(2, 0, PLAYER.WHITE);
            board[4, 5] = new Bishop(4, 5, PLAYER.WHITE);
            board[1, 6] = new Bishop(1, 6, PLAYER.BLACK);
            board[5, 7] = new Bishop(5, 7, PLAYER.BLACK);

            board[4, 4] = new Queen(4, 4, PLAYER.WHITE);
            board[3, 7] = new Queen(3, 7, PLAYER.BLACK);

            board[4, 0] = new King(4, 0, PLAYER.WHITE);
            board[4, 7] = new King(4, 7, PLAYER.BLACK);

            for (i = 0; i < 2; i++)
            {
                rook1Moved[i] = false;
                rook2Moved[i] = false;
                kingMoved[i] = false;
            }

            whoseTurn = PLAYER.WHITE;
        }

        private void playChess()
        {
            drawBoard();
        }

        private PLAYER opponent(PLAYER player)
        {
            if (player == PLAYER.WHITE) return PLAYER.BLACK;
            return PLAYER.WHITE;
        }

        public bool makeMove(ChessMove move, bool checkLegality = true)
        {
	        if(checkLegality && !isLegalMove(move)) return false;

	        Piece piece = board[move.origin.x, move.origin.y];
	        board[move.destination.x, move.destination.y] = piece;
	        board[move.origin.x, move.origin.y] = null;
            piece.boardLocation = move.destination;

	        whoseTurn = opponent(whoseTurn);
	        return true;
        }

        public bool isLegalMove(ChessMove move)
        {
	        if(!isOnBoard(move.origin)) return false;
	        if(!isOnBoard(move.destination)) return false;

	        List<ChessMove> moves = new List<ChessMove>();
	        int numMoves = getLegalMoves(moves);
	
	        return moves.Contains(move);
        }

        private void drawBoard()
        {
            System.Drawing.Graphics graphics = CreateGraphics();

	        int i,j;
	        Piece piece;
            Rectangle boardRect = new Rectangle(boardHOffset, boardVOffset, boardSize * boardSquareWidth, boardSize * boardSquareHeight);
            Rectangle squareRect, innerRect;
            graphics.Clip = new Region(boardRect);
            Pen greenPen = new Pen(new SolidBrush(Color.Green), 5), redPen = new Pen(new SolidBrush(Color.Red), 5);
            SolidBrush whiteBrush = new SolidBrush(Color.White), blackBrush = new SolidBrush(Color.Black);
            graphics.FillRectangle(whiteBrush, boardRect);

	        for(i = 0; i <= boardSize; i++)
	        {
                graphics.DrawLine(System.Drawing.Pens.Black, i * boardSquareWidth + boardHOffset, boardVOffset, i * boardSquareWidth + boardHOffset, boardVOffset + boardSquareHeight * boardSize);
                graphics.DrawLine(System.Drawing.Pens.Black, boardHOffset, i * boardSquareHeight + boardVOffset, boardHOffset + boardSquareWidth * boardSize, i * boardSquareHeight + boardVOffset);
	        }

	        Rectangle Rect;

	        for(j = 0; j < boardSize; j++)
	        {
		        for(i = 0; i < boardSize; i++)
		        {
                    Rect = new Rectangle(i * boardSquareWidth + boardHOffset, (boardSize - 1 - j) * boardSquareHeight + boardVOffset, boardSquareWidth, boardSquareHeight);
			        squareRect = Rect;
                    squareRect.Y = squareRect.Bottom - boardSquareHeight;
			        innerRect = squareRect;
                    innerRect.Y += boardSquareHeight / 5;
                    innerRect.Height -= boardSquareHeight / 5;
                    innerRect.X += boardSquareWidth / 5;
                    innerRect.Width -= boardSquareWidth / 5;

			        if((i + j) % 2 == 0)
			        {
                        graphics.FillRectangle(blackBrush, squareRect);
			        }

			        piece = board[i, j];
			        if(piece != null)
			        {
                        graphics.DrawImage(piece.imageFile, innerRect);
			        }
		        }
	        }

            Point from, to;
            getFromTo(out from, out to);
            if (isOnBoard(from)) graphics.DrawRectangle(greenPen, getSquareRect(from));
            if (isOnBoard(to)) graphics.DrawRectangle(redPen, getSquareRect(to));
        }

        private Rectangle getSquareRect(Point p)
        {
            Rectangle squareRect = new Rectangle(p.x * boardSquareWidth + boardHOffset,
                (boardSize - 1 - p.y) * boardSquareHeight + boardVOffset, boardSquareWidth, boardSquareHeight);
            return squareRect;
        }

        private static bool isOnBoard(Point p)
        {
            if (p.x >= 0 && p.y >= 0 && p.x < boardSize && p.y < boardSize) return true;
	        return false;
        }

        private bool inCheck()
        {
            return false;
        }

        private bool inCheckmate()
        {
            // Idea:
            // 1) If there is a legal move that makes the point difference 1000 from previous state, checkmate
            // - Loop over legal moves list (create list and call getlegalmoves)
            // - Check whoseturn to know if it should be positive or negative difference

            List<ChessMove> moves = new List<ChessMove>();
            int numMoves = getLegalMoves(moves);
            if (numMoves == 0) return false;

            for (int i = 0; i < numMoves; i++)
            {
                Piece toBeReplaced = board[moves[i].destination.x, moves[i].destination.y];
                makeMove(moves[i]);
                double moveStateVal = objectiveFunction();
                undoMove(moves[i], toBeReplaced);

                if (whoseTurn == PLAYER.WHITE && objectiveFunction() == moveStateVal - 1000)
                {
                    return true;
                }
                if (whoseTurn == PLAYER.BLACK && objectiveFunction() == moveStateVal + 1000)
                {
                    return true;
                }
            }
            return false;
        }

        private int getLegalMoves(List<ChessMove> moves)
        {
	        int numMoves = 0;

	        foreach(Piece p in board)
            {
                if (p == null || p.whosePiece != whoseTurn) continue;
                numMoves += p.getLegalMoves(moves);
            }

	        return numMoves;
        }
        private ChessMove bestMove(int depth, ref int totalMoves, CancellationToken token)
        {
            double[] BEST_VALUE = new double[] {double.MaxValue, double.MinValue};
	        ChessMove niceMove = new ChessMove();
            niceMove.origin.x = -1;
            List<ChessMove> moves = new List<ChessMove>();
	        totalMoves = 0;
	        int numMoves = getLegalMoves(moves);  
	        if(numMoves == 0) return niceMove;
            if (token.IsCancellationRequested)
            {
                return moves[0];//we really should throw here, but...
            }

            // YOUR CODE HERE

            int bestIndex = 0;

            for(int i = 0; i < numMoves; i++)
            {
                Piece toBeReplaced = board[moves[i].destination.x, moves[i].destination.y];
                makeMove(moves[i]);
                double currentMoveValue = evaluatePosition(depth-1, ref totalMoves, token);
                undoMove(moves[i], toBeReplaced);

                if (whoseTurn == PLAYER.WHITE && currentMoveValue > BEST_VALUE[1])
                {
                    BEST_VALUE[1] = currentMoveValue;
                    bestIndex = i;
                }
                if (whoseTurn == PLAYER.BLACK && currentMoveValue < BEST_VALUE[0])
                {
                    BEST_VALUE[0] = currentMoveValue;
                    bestIndex = i;
                }
            }
            return moves[bestIndex];
        }
        private double evaluatePosition(int depth, ref int totalMoves, CancellationToken token)
        {

            if(token.IsCancellationRequested)
            {
                return 0;//we really should throw here, but...
            }
            List<ChessMove> moves = new List<ChessMove>();
            int numMoves = getLegalMoves(moves);
            totalMoves += numMoves;
            if (numMoves == 0) return 0;
            double currentBestValue = (whoseTurn == PLAYER.WHITE) ? -10000 : 10000;

            //YOUR CODE HERE

            if (depth == 0) return objectiveFunction();

            for(int i = 0; i < numMoves; i++)
            {
                Piece toBeReplaced = board[moves[i].destination.x, moves[i].destination.y];
                makeMove(moves[i]);
                double currentValue = evaluatePosition(depth - 1, ref totalMoves, token);
                undoMove(moves[i], toBeReplaced);

                if (whoseTurn == PLAYER.WHITE && currentValue > currentBestValue)
                {
                    currentBestValue = currentValue;
                }
                if (whoseTurn == PLAYER.BLACK && currentValue < currentBestValue)
                {
                    currentBestValue = currentValue;
                }
            }
            return currentBestValue;
        }

        private double objectiveFunction()
        {
            double value = 0;
            
            foreach(Piece p in board)
            {
                if (p == null) continue;
                value += p.value;
            }
            return value;
        }

        private void undoMove(ChessMove m, Piece formerOccupant)
        {
            Piece mover = board[m.destination.x, m.destination.y];
            mover.boardLocation = m.origin;
            board[m.origin.x, m.origin.y] = mover;
            board[m.destination.x, m.destination.y] = formerOccupant;
            whoseTurn = opponent(whoseTurn);
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if(radioButtonResign.Checked)
            {
                txtMessage.Text = "I accept your resignation. Well played.";
                btnSubmit.Enabled = false;
                return;
            }
            string prependMsg = "";
            if (radioButtonUserDecides.Checked)
            {
                Point from, to;
                if (!getFromTo(out from, out to, true)) return;
                ChessMove theMove = new ChessMove(ref from, ref to);
                if (!makeMove(theMove))
                {
                    txtMessage.Text = "Sorry, that move is illegal. Please try again.";
                    return;
                }
                moveHistory.Add(theMove); moveIndex++; undoMoveToolStripMenuItem.Enabled = true;
                drawBoard();
                if (inCheckmate())
                {
                    txtMessage.Text = "I am checkmated! Nice game.";
                    btnSubmit.Enabled = false;
                    return;
                }
                if (inCheck())
                {
                    prependMsg = "I am in check!";
                }
            }
            int totalMoves = 0;
            int lookAhead = 1;
            ChessMove toMake = new ChessMove();
            const int MAX_TIME_IN_MILLISECONDS = 5000;
            int timeLimitInMilliseconds = MAX_TIME_IN_MILLISECONDS;
            DateTime startTime = DateTime.Now;
            txtMessage.Text = prependMsg + "Thinking...";
            ChessGame.ActiveForm.UseWaitCursor = true;
            ChessGame.ActiveForm.Update();
            while (timeLimitInMilliseconds >= 0)
            {
                int tempMoveTotal = 0;
                var tokenSource = new CancellationTokenSource();
                var token = tokenSource.Token;
                Task<ChessMove> findMoveThread = new Task<ChessMove>(() => bestMove(lookAhead, ref tempMoveTotal, token), token);
                findMoveThread.Start();
                findMoveThread.Wait(timeLimitInMilliseconds);
                if (!findMoveThread.IsCompleted)
                {
                    tokenSource.Cancel();
                    findMoveThread.Wait();
                    break;
                }
                toMake = findMoveThread.Result;
                totalMoves = tempMoveTotal;
                DateTime currentTime = DateTime.Now;
                TimeSpan elapsedTime = currentTime - startTime;
                timeLimitInMilliseconds = MAX_TIME_IN_MILLISECONDS - (int)elapsedTime.TotalMilliseconds;
                lookAhead++;
            }
            ChessGame.ActiveForm.UseWaitCursor = false;
            makeMove(toMake);
            txtMessage.Text = "After considering " + totalMoves + " moves, I have decided to move from " + toMake.origin.x + "," + toMake.origin.y + " to " + toMake.destination.x + "," + toMake.destination.y + ".";
            if(inCheckmate())
            {
                txtMessage.Text += " Checkmate! Nice game.";
                btnSubmit.Enabled = false;
            }
            else if(inCheck())
            {
                txtMessage.Text += " Check!";
            }
            moveHistory.Add(toMake); moveIndex++; undoMoveToolStripMenuItem.Enabled = true;
            drawBoard();
        }

        private bool getFromTo(out Point from, out Point to, bool report = false)
        {
            bool retValue = true;
            try
            {
                string[] fromStr = textBox2.Text.Split(',');
                from = new Point(Int32.Parse(fromStr[0]), Int32.Parse(fromStr[1]));
                string[] toStr = textBox1.Text.Split(',');
                to = new Point(Int32.Parse(toStr[0]), Int32.Parse(toStr[1]));
            }
            catch (Exception)
            {
                retValue = false;
                from = to = new Point();
                if (report) txtMessage.Text = "Sorry, that move is not in the correct format. Please try again.";
            }
            return retValue;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
   
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void undoMoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moveIndex--;
            recapitulateGame();
            drawBoard();
            redoMoveToolStripMenuItem.Enabled = true;
            if(moveIndex == 0)
            {
                undoMoveToolStripMenuItem.Enabled = false;
            }
        }

        private void redoMoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moveIndex++;
            recapitulateGame();
            drawBoard();
            undoMoveToolStripMenuItem.Enabled = true;
            if(moveIndex == moveHistory.Count)
            {
                redoMoveToolStripMenuItem.Enabled = false;
            }
        }

        private void recapitulateGame()
        {
            setupBoard();
            for (int i = 0; i < moveIndex; i++)
            {
                makeMove(moveHistory[i]);
            }
        }

        private void ChessGame_Load(object sender, EventArgs e)
        {
            drawBoard();
        }

        private void panelChessboard_Paint()
        {

        }

        private void startNewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setupBoard(); 
            drawBoard();
            this.Click += form_Click;
            moveHistory = new List<ChessMove>();
            moveIndex = 0;
            undoMoveToolStripMenuItem.Enabled = redoMoveToolStripMenuItem.Enabled = false;
            btnSubmit.Enabled = true;
        }
    }
}
