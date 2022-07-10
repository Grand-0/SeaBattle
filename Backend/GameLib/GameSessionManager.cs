namespace GameLib
{
    public class GameSessionManager
    {
        private GameZone _zoneFirstPlayer { get; }
        private GameZone _zoneSecondPlayer { get; }
        public int CurrentUser { get; set; }

        public GameSessionManager(int firstUserId, int secondUserId, FieldCell[][] firstUserZone, FieldCell[][] secondUserZone)
        {
            _zoneFirstPlayer = new GameZone(firstUserId, firstUserZone);
            _zoneSecondPlayer = new GameZone(secondUserId, secondUserZone);
        }

        public void ChangeCurrentUser()
        {
            if (CurrentUser == _zoneFirstPlayer.UserId)
            {
                CurrentUser = _zoneSecondPlayer.UserId;
            }
            else
            {
                CurrentUser = _zoneFirstPlayer.UserId;
            }
        }

        public string MakeShot(int x, int y)
        {
            string status = "";

            if (CurrentUser == _zoneFirstPlayer.UserId)
            {
                _zoneSecondPlayer.GameField[x][y].isMarked = true;
                status = MakeShotExtended(_zoneSecondPlayer.GameField, x, y);
            }
            else
            {
                _zoneFirstPlayer.GameField[x][y].isMarked = true;
                status = MakeShotExtended(_zoneFirstPlayer.GameField, x, y);
            }

            ChangeCurrentUser();

            return status;
        }

        private string MakeShotExtended(FieldCell[][] field, int x, int y)
        {
            switch (field[x][y].CurrentShip)
            {
                case ShipClassification.TorpedoBoat:
                    return "Потопил!";
                case ShipClassification.Destroyer:
                    return CheckShipStatus(ShipClassification.Destroyer, field, x, y);
                case ShipClassification.Cruiser:
                    return CheckShipStatus(ShipClassification.Cruiser, field, x, y);
                case ShipClassification.Battleship:
                    return CheckShipStatus(ShipClassification.Battleship, field, x, y);
                default:
                    return "Промах";
            }
        }

        private string CheckShipStatus(ShipClassification attackedShip, FieldCell[][] field, int x, int y)
        {
            int health = (int)attackedShip - 1;

            if (health == 0)
            {
                return "Потопил!";
            }

            int healthCounter = 0;

            CheckForHorizontal(attackedShip, field, x, y, health, ref healthCounter);
            CheckForVertical(attackedShip, field, x, y, health, ref healthCounter);

            int resultHealth = health - healthCounter;

            if (resultHealth > 0)
            {
                return "Подбил!";
            }
            else
            {
                return "Потопил!";
            }
        }

        private void CheckForVertical(ShipClassification attackedShip, FieldCell[][] field, int x, int y, int health, ref int healthCounter)
        {
            for (int i = y; i < y + health; i++)
            {
                if (i == 5)
                {
                    break;
                }

                if (field[x][i].CurrentShip == attackedShip && field[x][i].isMarked)
                {
                    healthCounter++;
                }

                if (field[x][i].CurrentShip == ShipClassification.Zero)
                {
                    break;
                }
            }

            for (int i = y; i > 0; i--)
            {
                if (field[x][i].CurrentShip == attackedShip && field[x][i].isMarked)
                {
                    healthCounter++;
                }

                if (field[x][i].CurrentShip == ShipClassification.Zero)
                {
                    break;
                }
            }
        }

        private void CheckForHorizontal(ShipClassification attackedShip, FieldCell[][] field, int x, int y, int health, ref int healthCounter)
        {
            for (int i = x; i < x + health; i++)
            {
                if (i == 5)
                {
                    break;
                }

                if (field[i][y].CurrentShip == attackedShip && field[i][y].isMarked)
                {
                    healthCounter++;
                }

                if (field[i][y].CurrentShip == ShipClassification.Zero)
                {
                    break;
                }
            }

            for (int i = x; i > 0; i--)
            {
                if (field[i][y].CurrentShip == attackedShip && field[i][y].isMarked)
                {
                    healthCounter++;
                }

                if (field[i][y].CurrentShip == ShipClassification.Zero)
                {
                    break;
                }
            }
        }
    }
}
