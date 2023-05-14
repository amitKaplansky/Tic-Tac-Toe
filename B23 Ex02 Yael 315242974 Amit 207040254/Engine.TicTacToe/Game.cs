using System;


namespace Engine.TicTacToe;

public class Game
{
    private const int k_MinBoardSize = 3;
    private const int k_MaxBoardSize = 9;
    private Symbol[,]? m_Board = null;
    private int? m_EmptySquarsInBoard = null;
    private Player? m_Player1 = null;
    private Player? m_Player2 = null;
    private PlayerTurn m_PlayerTurn = PlayerTurn.Player1; 
    private List<String> m_freeSquars = null;

    public PlayerTurn PlayerTurn
    {
        get 
        {
            return m_PlayerTurn;
        }
    }

    public int Player1Score()
    {
        return m_Player1 != null ? m_Player1.Value.Score : -1;
    }

    public int Player2Score()
    {
        return m_Player2 != null ? m_Player2.Value.Score : -1;
    }

    public bool IsPlayerTurnIsComputer()
    {
        return m_PlayerTurn == PlayerTurn.Player2 && m_Player2.Value.PlayerType == PlayerType.Computer;
    }

    public bool NextMove(int i_Row = -1, int i_Col = -1)
    {

        if (m_PlayerTurn == PlayerTurn.Player2 && m_Player2.Value.PlayerType == PlayerType.Computer)
        {
            computerMove(ref i_Row, ref i_Col);
        }
        else
        {
            if (!isSquareInRange(i_Row, i_Col) || !isSquareAvailable(i_Row, i_Col))
                return false;
        }

        makeMove(i_Row, i_Col);


        return true;
    }

    private void makeMove(int i_Row, int i_Col)
    {

        Symbol squareSymbol = m_PlayerTurn == PlayerTurn.Player1 ? m_Player1.Value.Symbol : m_Player2.Value.Symbol;

        m_Board[i_Row, i_Col] = squareSymbol;

        m_EmptySquarsInBoard--;

        removeTakenSquare(i_Row, i_Col); 

    }
   

    private bool isSquareInRange(int i_Row, int i_Col)

    {   if (m_Board != null)
        {
            return (i_Row >= 0 && i_Row < m_Board.Length) && (i_Col >= 0 && i_Row < m_Board.Length);
        }

        return false;
    }

    private bool isSquareAvailable(int i_Row, int i_Col)
    {
        if (m_Board != null)
        {
            return m_Board[i_Row, i_Col] == Symbol.Empty;
        }
        return false;
    }


    public bool InitBoard(int i_BoardSize)
    {
        
        createBoard(i_BoardSize);

        return true;
    }

    public bool IsBoardSizeValid(int i_BoardSize)
    {
        return i_BoardSize >= 3 && i_BoardSize <= 9;
    }

    public void InitPlayers(bool i_IsComputer)
    {
        this.m_Player1 = new Player(Symbol.X, PlayerType.Human);
        this.m_Player2 = i_IsComputer ? new Player(Symbol.O, PlayerType.Computer) : new Player(Symbol.O, PlayerType.Human);
    }

    public void RestartGame()
    {
        clearBoard();
        initFreeSquars();
    }

    private void createBoard(int i_BoardSize)
    {

        this.m_Board = new Symbol[i_BoardSize, i_BoardSize];
        initFreeSquars();
        clearBoard();

    }

    public bool DidPlayerLose(int i_Row, int i_Col)
    {
        Symbol squareSymbol = m_PlayerTurn == PlayerTurn.Player1 ? m_Player1.Value.Symbol : m_Player2.Value.Symbol;
        
        bool didPlayerlose = false;

        if (i_Row == i_Col && diagonalWin(squareSymbol))
        { 
            didPlayerlose = true;
        }
        else if (rowWin(i_Row, squareSymbol) || colWin(i_Col, squareSymbol))
        {
            didPlayerlose = true;
        }

        if (didPlayerlose)
        {
            if(m_Player1.Value.Symbol == squareSymbol)
            {
                m_Player2.Value.updateScore();
            }
            else
            {
                m_Player1.Value.updateScore();
            }
        }
        else
        {
            m_PlayerTurn = m_PlayerTurn == PlayerTurn.Player1 ? PlayerTurn.Player2 : PlayerTurn.Player1;
        }

        return didPlayerlose;
        
    }

    public bool IsTie()
    {
        return m_EmptySquarsInBoard == 0;
    }

    private bool diagonalWin(Symbol i_SymbolToCheck)
    {
        if (m_Board != null)
        {
            int howManyInDiagonal = m_Board.GetLength(0);
            int howManyInReverseDiagonal = m_Board.GetLength(0);

            for (int i = 0; i < m_Board.GetLength(0); i++)
            {
                if (m_Board[i, i] == i_SymbolToCheck)
                    howManyInDiagonal--;
            }

            for (int i = 0; i < m_Board.GetLength(0); i++)
            {
                if (m_Board[i, m_Board.GetLength(0) - i -1] == i_SymbolToCheck)
                    howManyInDiagonal--;
            }

            return howManyInDiagonal == 0 || howManyInReverseDiagonal == 0;
        }

        return false;
    }

    private bool rowWin(int i_Row, Symbol i_SymbolToCheck)
    {
        if (m_Board != null)
        {
            int howManyInRow = m_Board.GetLength(0);

            for (int i = 0; i < m_Board.GetLength(0); i++)
            {
                if (m_Board[i_Row, i] == i_SymbolToCheck)
                    howManyInRow--;
            }

            return howManyInRow == 0;
        }

        return false;
    }

    private bool colWin(int i_Col, Symbol i_SymbolToCheck)
    {
        if (m_Board != null)
        {

            int howManyInCol = m_Board.Length;

            for (int i = 0; i < m_Board.GetLength(1); i++)
            {
                if (m_Board[i, i_Col] == i_SymbolToCheck)
                    howManyInCol--;
            }

            return howManyInCol == 0;
        }
        return false;
    }


    private void clearBoard()
    {
        if (m_Board != null)
        {
            for (int i = 0; i < m_Board.GetLength(1); i++)
            {
                for (int j = 0; j < m_Board.GetLength(0); j++)
                {
                    m_Board[i, j] = Symbol.Empty;
                }
            }

            m_EmptySquarsInBoard = m_Board.Length * m_Board.Length;
            m_PlayerTurn = PlayerTurn.Player1;
        }
    }

    
    public Symbol[,]? CopyBoard()
    {
        if (m_Board != null)
        {
            Symbol[,] boardToCopy = new Symbol[m_Board.GetLength(1), m_Board.GetLength(0)];

            for (int i = 0; i < m_Board.GetLength(1); i++)
            {
                for (int j = 0; j < m_Board.GetLength(0); j++)
                {
                    boardToCopy[i, j] = m_Board[i, j];
                }
            }

            return boardToCopy;
        }
        return null;
    }


    private void computerMove(ref int o_Row, ref int o_Col)
    {
        bool squereAvailabel = false;
        int index;

        index = generateRandomNumber(1, m_freeSquars.Count);
        string squar = m_freeSquars[index];
        string[] squarIndex = squar.Split(',');
        o_Row = int.Parse(squarIndex[0]);
        o_Col= int.Parse(squarIndex[1]);

    }

    private int generateRandomNumber(int i_MinValue, int i_MaxValue)
    {
        Random random = new Random();
        return random.Next(i_MinValue, i_MaxValue);//maybe need add 1 to maxValue
    }

    private void initFreeSquars()
    {
        if (m_Board != null)
        {
            int boardSize = m_Board.GetLength(0);
            m_freeSquars = new List<string>(boardSize * boardSize);

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    string square = i + "," + j;
                    m_freeSquars.Add(square);
                }
            }
        }
    }

    private void removeTakenSquare(int i_Row, int i_Col)
    {
        string squar = i_Row + "," + i_Col;
        m_freeSquars.Remove(squar);
    }
}
