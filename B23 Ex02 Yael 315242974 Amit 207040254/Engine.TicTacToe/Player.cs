using System;

namespace Engine.Reverse.TicTacToe;

public struct Player
{
    private readonly eBoardSymbols m_Symbol;
    private int m_Score;
    private ePlayerType? m_PlayerType;

    public Player(eBoardSymbols i_Symbol, ePlayerType i_PlayerType)
    {
        this.m_Symbol = i_Symbol;
        this.m_PlayerType = i_PlayerType;
        this.m_Score = 0;
    }

    public eBoardSymbols Symbol
    {
        get
        {
            return m_Symbol;
        }

    }


    public int Score
    {
        get
        {
            return m_Score;
        }
        set
        {
            m_Score += value;
        }
    }


    public ePlayerType PlayerType
    {
        get
        {
            return m_PlayerType.Value;
        }
    }

    

}


