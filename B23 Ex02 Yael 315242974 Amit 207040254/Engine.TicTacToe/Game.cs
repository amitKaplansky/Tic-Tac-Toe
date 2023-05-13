using System;


namespace Engine.TicTacToe;

public class Game
{

    private Symbol[,]? m_Board = null;
    private int? m_EmptySquarsInBoard = null;
    private Player? m_Player1 = null;
    private Player? m_Player2 = null;
    private bool m_PlayerTurn = false; // false = player1 turn, true = player2 turn
    private List<String> m_freeSquars = null;
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
        initFreeSquars(i_BoardSize);
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
        
        for (int i = 0; i < m_Board.GetLength(1); i++)
        {
            for(int j = 0; j < m_Board.GetLength(0) ; j++)
            {
                m_Board[i, j] = Symbol.Empty;
            }
        }

        m_EmptySquarsInBoard = m_Board.Length * m_Board.Length;
    }

    
    public Symbol[,] CopyBoard()
    {
        // todo add exception??
        Symbol[,] boardToCopy = new Symbol[m_Board.GetLength(1), m_Board.GetLength(0)];

        for(int i = 0; i < m_Board.GetLength(1); i++)
        {
            for(int j = 0; j < m_Board.GetLength(0); j++)
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

    public void computerMove()
    {
        bool squereAvailabel = false;
        int row, col, index;

        index = generateRandomNumber(1, m_freeSquars.Count);
        string squar = m_freeSquars[index];
        string[] squarIndex = squar.Split(',');
        row = int.Parse(squarIndex[0]);
        col = int.Parse(squarIndex[1]);

        m_Board[row, col] = m_Player2.Value.Symbol;//?
        removeTakenSquar(row,col);

    }
    private int generateRandomNumber(int i_minValue, int i_maxValue)
    {
        Random random = new Random();
        return random.Next(i_minValue, i_maxValue);//maybe need add 1 to maxValue
    }

    private void initFreeSquars(int i_boarsSize)
    {
        m_freeSquars = new List<string>(i_boarsSize * i_boarsSize);
        for(int i = 0; i< i_boarsSize;i++)
            for(int j = 0; j < i_boarsSize; j++)
            {
                string squar = i + "," + j;
                m_freeSquars.Add(squar);
            }
    }

    private void removeTakenSquar(int i_row, int i_col)
    {
        string squar = i_row + "," + i_col;
        m_freeSquars.Remove(squar);
    }
}