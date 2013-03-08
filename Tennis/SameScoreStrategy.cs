namespace Tennis
{
    internal class SameScoreStrategy
    {
        public bool AppliesTo(TennisGame1 game)
        {
            return (game.m_score1 == game.m_score2);
        }

        public string CalculateScore(TennisGame1 game)
        {
            string score;
            switch (game.m_score1)
            {
                case 0:
                    score = "Love-All";
                    break;
                case 1:
                    score = "Fifteen-All";
                    break;
                case 2:
                    score = "Thirty-All";
                    break;
                case 3:
                    score = "Forty-All";
                    break;
                default:
                    score = "Deuce";
                    break;

            }
            return score;
        }
    }
}