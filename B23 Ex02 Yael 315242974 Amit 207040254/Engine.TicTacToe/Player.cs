using System;
namespace Engine.TicTacToe
{
	public struct Player
	{
		private readonly Symbol m_Symbol;
		private int m_Score;
		private readonly PlayerType m_PlayerType;

        public Player(Symbol i_Symbol, PlayerType i_PlayerType)
        {
            this.m_Symbol = i_Symbol;
            this.m_PlayerType = i_PlayerType;
            this.m_Score = 0;
        }

		public Symbol Symbol
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
                m_Score = value;
            }
        }


        public PlayerType PlayerType
        {
            get
            {
                return m_PlayerType;
            }
        }
    }
}

