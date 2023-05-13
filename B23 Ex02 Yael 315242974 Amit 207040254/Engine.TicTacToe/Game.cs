using System;


namespace Engine.TicTacToe;

public class Game
{

    private Symbol[,]? m_Board = null;

    private int? m_EmptySquarsInBoard = null;
 
    private Player? m_Player1 = null;
    private Player? m_Player2 = null;

    private bool m_PlayerTurn = false; // false = player1 turn, true = player2 turn

   
    private bool makeMove(int i_row, int i_col)
    {
        // add exception
        if (!isSquareInRange(i_row, i_col) || !isSquareAvailable(i_row, i_col))
            return false;

        Symbol squareSymbol = m_PlayerTurn ? m_Player2.Value.Symbol : m_Player1.Value.Symbol;

        m_Board[i_row, i_col] = squareSymbol;
        m_EmptySquarsInBoard--;
        

        return true;
    }
   

    private bool isSquareInRange(int i_row, int i_col)
    {
        return (i_row >= 0 && i_row < m_Board.Length) && (i_col >= 0 && i_row < m_Board.Length);
    }

    private bool isSquareAvailable(int i_row, int i_col)
    {
        return m_Board[i_row,i_col] == Symbol.Empty;
    }


    public bool InitislizeGame(int i_BoardSize, bool i_IsComputer)
    {
        // todo add exception??
        if (i_BoardSize < 3 || i_BoardSize > 9)
            return false; // chagne this to execption

        createBoard(i_BoardSize);
        this.m_Player1 = new Player(Symbol.X, PlayerType.Human);
        this.m_Player2 = i_IsComputer ? new Player(Symbol.O, PlayerType.Computer) : new Player(Symbol.O, PlayerType.Human);

        return true;
    }

    public void RestartGame()
    {
        clearBoard();
    }

    private void createBoard(int i_BoardSize)
    {
        
        this.m_Board = new Symbol[i_BoardSize, i_BoardSize];
        clearBoard();

    }

    public bool IsGameOver(int i_row, int i_col)
    {
        Symbol squareSymbol = m_PlayerTurn ? m_Player2.Value.Symbol : m_Player1.Value.Symbol;
        // is add resart game if game is over
        return true;
        
    }

    private bool checkPotentialCentralDiagonalWin()
    {
        int howManyInDiagonal = m_Board.Length;
        Symbol squareSymbol = m_PlayerTurn ? m_Player2.Value.Symbol : m_Player1.Value.Symbol;

        for (int i = 0; i < m_Board.Length; i++)
        {
            if (m_Board[i, i] == squareSymbol)
                howManyInDiagonal--;
        }

        return howManyInDiagonal == 0;
    }

    private bool checkPotentialRowWin(int i_row)
    {
        int howManyRow = m_Board.Length;
        Symbol squareSymbol = m_PlayerTurn ? m_Player2.Value.Symbol : m_Player1.Value.Symbol;

        return true;
    }

    private bool checkPotentialcolWin(int i_col)
    {
        int howManyInCol = m_Board.Length;
        Symbol squareSymbol = m_PlayerTurn ? m_Player2.Value.Symbol : m_Player1.Value.Symbol;

        return true;
    }


    private void clearBoard()
    {
        // todo add exception??

        for (int i = 0; i < m_Board.Length; i++)
        {
            for(int j = 0; j < m_Board.Length; j++)
            {
                m_Board[i, j] = Symbol.Empty;
            }
        }

        m_EmptySquarsInBoard = m_Board.Length * m_Board.Length;
    }

    
    public Symbol[,] CopyBoard()
    {
        // todo add exception??
        Symbol[,] boardToCopy = new Symbol[m_Board.Length, m_Board.Length];

        for(int i = 0; i < m_Board.Length; i++)
        {
            for(int j = 0; j < m_Board.Length; j++)
            {
                boardToCopy[i, j] = m_Board[i, j];
            }
        }

        return boardToCopy;
    }

    public bool NextMove(int? i_row = null, int? i_col = null)
    {

        m_PlayerTurn = m_PlayerTurn ? false : true;
        return true;
    }
}