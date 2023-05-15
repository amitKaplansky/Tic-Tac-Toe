using System;


namespace Engine.Reverse.TicTacToe;

public class Game
{
    private const int k_MinBoardSize = 3;
    private const int k_MaxBoardSize = 9;
    private eBoardSymbols[,]? m_Board = null;
    private int? m_EmptySquarsInBoard = null;
    private Player m_Player1;
    private Player m_Player2;
    private ePlayerTurn m_PlayerTurn = ePlayerTurn.Player1; 
    private List<String> m_freeSquars = null;
    private int[] m_LastMove = new int[2];

    public ePlayerTurn PlayerTurn
    {
        get 
        {
            return m_PlayerTurn;
        }
    }

    public int Player1Score()
    {
        return  m_Player1.Score;
    }

    public int Player2Score()
    {
        return m_Player2.Score;
    }

    public bool IsPlayerTurnIsComputer()
    {
        return m_PlayerTurn == ePlayerTurn.Player2 && m_Player2.PlayerType == ePlayerType.Computer;
    }

    public bool NextMove(int i_Row = -1, int i_Col = -1)
    {
        
        if (m_PlayerTurn == ePlayerTurn.Player2 && m_Player2.PlayerType == ePlayerType.Computer)
        {
            computerMove(ref i_Row, ref i_Col);
        }
        else
        {
            i_Row--;
            i_Col--;

            if (!isSquareGood(i_Row, i_Col))
            {
                return false;
            }
        }

        makeMove(i_Row, i_Col);

        m_LastMove[0] = i_Row;
        m_LastMove[1] = i_Col;

        return true;
    }

    private void makeMove(int i_Row, int i_Col)
    {

        eBoardSymbols squareSymbol = m_PlayerTurn == ePlayerTurn.Player1 ? m_Player1.Symbol : m_Player2.Symbol;

        m_Board[i_Row, i_Col] = squareSymbol;

        m_EmptySquarsInBoard--;

        removeTakenSquare(i_Row, i_Col); 

    }
   

    private bool isSquareGood(int i_Row, int i_Col)
    {
        bool isSquareGood = true;

        if (m_Board != null)
        {
            if ((i_Row >= 0 && i_Row < m_Board.GetLength(0)) && (i_Col >= 0 && i_Col < m_Board.GetLength(1)))
            {
                isSquareGood = isSquareAvailable(i_Row, i_Col);
            }
            else
            {
                isSquareGood = false;
            }
        }

        return isSquareGood;
    }

    private bool isSquareAvailable(int i_Row, int i_Col)
    {
        if (m_Board != null)
        {
            return m_Board[i_Row, i_Col] == eBoardSymbols.Empty;
        }
        return false;
    }


    public bool InitBoard(int i_BoardSize)
    {
        bool boardSizeInRange;

        if(i_BoardSize >= k_MinBoardSize && i_BoardSize <= k_MaxBoardSize)
        {
            createBoard(i_BoardSize);
            boardSizeInRange = true;
        }
        else
        {
            boardSizeInRange = false;
        }

        return boardSizeInRange;
    }

    public void InitPlayers(bool i_IsComputer)
    {
        this.m_Player1 = new Player(eBoardSymbols.X, ePlayerType.Human);
        this.m_Player2 = i_IsComputer ? new Player(eBoardSymbols.O, ePlayerType.Computer) : new Player(eBoardSymbols.O, ePlayerType.Human);
    }

    public void RestartGame()
    {
        clearBoard();
        initFreeSquars();
    }

    private void createBoard(int i_BoardSize)
    {
        if (m_Board == null)
        {
            this.m_Board = new eBoardSymbols[i_BoardSize, i_BoardSize];
            initFreeSquars();
            clearBoard();
        }
    }

    public bool DidPlayerLose()
    {
        eBoardSymbols squareSymbol = m_PlayerTurn == ePlayerTurn.Player1 ? m_Player1.Symbol : m_Player2.Symbol;
        
        bool didPlayerlose = false;

        if (diagonalWin(squareSymbol))
        { 
            didPlayerlose = true;
        }
        else if (rowWin(m_LastMove[0], squareSymbol) || colWin(m_LastMove[1], squareSymbol))
        {
            didPlayerlose = true;
        }

        if (didPlayerlose)
        {
            if(m_Player1.Symbol == squareSymbol)
            {
                  m_Player2.Score = 1;
            }
            else
            {
                m_Player1.Score = 1;
            }
        }
        else
        {
            m_PlayerTurn = m_PlayerTurn == ePlayerTurn.Player1 ? ePlayerTurn.Player2 : ePlayerTurn.Player1;
        }

        return didPlayerlose;
        
    }

    public bool IsTie()
    {
        return m_EmptySquarsInBoard == 0;
    }

    private bool diagonalWin(eBoardSymbols i_SymbolToCheck)
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
                    howManyInReverseDiagonal--;
            }

            return howManyInDiagonal == 0 || howManyInReverseDiagonal == 0;
        }

        return false;
    }

    private bool rowWin(int i_Row, eBoardSymbols i_SymbolToCheck)
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

    private bool colWin(int i_Col, eBoardSymbols i_SymbolToCheck)
    {
        if (m_Board != null)
        {

            int howManyInCol = m_Board.GetLength(1);

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
                    m_Board[i, j] = eBoardSymbols.Empty;
                }
            }

            m_EmptySquarsInBoard = m_Board.GetLength(0) * m_Board.GetLength(1);
            m_PlayerTurn = ePlayerTurn.Player1;
        }
    }

    
    public eBoardSymbols[,]? CopyBoard()
    {
        if (m_Board != null)
        {
            eBoardSymbols[,] boardToCopy = new eBoardSymbols[m_Board.GetLength(1), m_Board.GetLength(0)];

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
        if (m_freeSquars.Count > 0)
        {
            index = generateRandomNumber(1, m_freeSquars.Count);
            string squar = m_freeSquars[index];
            string[] squarIndex = squar.Split(',');
            o_Row = int.Parse(squarIndex[0]);
            o_Col = int.Parse(squarIndex[1]);
        }

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
