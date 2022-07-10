namespace GameLib
{
    public class GameZone
    {
        public int UserId { get; }
        public FieldCell[][] GameField { get; }

        public GameZone(int userId, FieldCell[][] gameField)
        {
            UserId = userId;
            GameField = gameField;
        }
    }
}
