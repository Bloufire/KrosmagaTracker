using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using NLog;

namespace SQLiteConnector
{
    public static class Connector
    {
        static SQLiteConnection _maConnexion;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static void CreateDatabase()
        {
            if (!File.Exists("krosmagaAddOn.db"))
            {
                try
                {
                    SQLiteConnection.CreateFile("krosmagaAddOn.db");
                }
                catch (Exception e)
                {
                    logger.Error("Error: SQLiteConnection.CreateFile(krosmagaAddOn.db) : " + e);
                }

                using (_maConnexion = new SQLiteConnection("Data Source = krosmagaAddOn.db; Version = 3;"))
                {
                    _maConnexion.Open();
                    string sql = "CREATE TABLE " + '"' + "Match" + '"' + " ( `idMatch` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, `opponentName` TEXT NOT NULL, `playerClasse` TEXT NOT NULL, `resultatMatch` INTEGER NOT NULL, `nbToursMatch` INTEGER NOT NULL, `Fk_deckId` INTEGER NOT NULL, `matchType` INTEGER NOT NULL, `date` TEXT, FOREIGN KEY(`Fk_deckId`) REFERENCES `Deck`(`idDeck`) )";
                    SQLiteCommand command = new SQLiteCommand(sql, _maConnexion);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        logger.Error("Error: SQL CREATE TABLE Match : " + e);
                    }
                    sql = "CREATE TABLE " + '"' + "Deck" + '"' + " ( `idDeck` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, `opponentClasse` TEXT NOT NULL )";
                    command = new SQLiteCommand(sql, _maConnexion);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        logger.Error("Error: SQL CREATE TABLE Deck : " + e);
                    }

                    sql = "CREATE TABLE " + '"' + "Card" + '"' + " ( `idCard` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, `realCardId` INTEGER NOT NULL, `Fk_deckId` INTEGER NOT NULL, FOREIGN KEY(`Fk_deckId`) REFERENCES `Deck`(`idDeck`) )";
                    command = new SQLiteCommand(sql, _maConnexion);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        logger.Error("Error: SQL CREATE TABLE Card : " + e);
                    }
                    _maConnexion.Close();
                }
            }
        }

        private static bool Open()
        {
            if (File.Exists("krosmagaAddOn.db"))
            {
                _maConnexion = new SQLiteConnection("Data Source = krosmagaAddOn.db; Version = 3;");
                _maConnexion.Open();
                return true;
            }
            return false;
        }

        private static void Close()
        {
            if(_maConnexion != null)
                _maConnexion.Close();
        }

        public static bool SaveMatchResult(string opponentClasse,
            List<int> cards,
            string opponentName,
            string playerClasse,
            int matchResult,
            int nbTours,
            int matchType,
            DateTime date)
        {
            string dateString = date.ToString("yyyy-MM-dd HH:mm:ss");

            if (Open())
            {
                string sql = "INSERT INTO Deck (opponentClasse) values ('" + opponentClasse + "')";
                SQLiteCommand commande = new SQLiteCommand(sql, _maConnexion);
                try
                {
                    commande.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    logger.Error("Error: INSERT INTO Deck (opponentClasse) : " + e);
                }


                sql = "SELECT last_insert_rowid()";
                commande = new SQLiteCommand(sql, _maConnexion);
                SQLiteDataReader reader = commande.ExecuteReader();
                reader.Read();

                int deckId = reader.GetInt32(0);

                foreach (var item in cards)
                {
                    sql = "INSERT INTO Card (realCardId, Fk_deckId) values (" + item + ", " + deckId + " )";
                    commande = new SQLiteCommand(sql, _maConnexion);
                    try
                    {
                        commande.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        logger.Error("Error: INSERT INTO Card (realCardId, Fk_deckId) : " + e);
                    }
                }

                sql = "INSERT INTO Match (opponentName, playerClasse, resultatMatch, nbToursMatch, Fk_deckId, matchType, date) values ('" + opponentName + "', '" + playerClasse + "', " + matchResult + ", " + nbTours + ", " + deckId + ", " + matchType + ", '" + dateString + "' )";
                commande = new SQLiteCommand(sql, _maConnexion);
                try
                {
                    commande.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    logger.Error("Error: INSERT INTO Match (opponentName, playerClasse, resultatMatch, nbToursMatch, Fk_deckId, matchType, date) : " + e);
                }
                Close();
                return true;
            }
            return false;
        }

        public static List<Match> GetMatches()
        {
            List<Match> listToReturn = new List<Match>();
            if(Open())
            {
                string sql = "SELECT * FROM Match";
                SQLiteCommand commande = new SQLiteCommand(sql, _maConnexion);
                SQLiteDataReader reader = commande.ExecuteReader();
                while (reader.Read())
                {
                    Deck deck = new Deck();
                    Match match = new Match();
                    match.IdMatch = reader.GetInt32(0);
                    match.MatchType = reader.GetInt32(6);
                    match.NbToursMatch = reader.GetInt32(4);
                    match.OpponentName = reader.GetString(1);
                    match.PlayerClasse = reader.GetString(2);
                    match.ResultatMatch = reader.GetInt32(3);

                    string dateString = reader.GetString(7);
                    match.Date = DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    int DeckId = reader.GetInt32(5);
                    sql = "SELECT * FROM Deck WHERE idDeck = " + DeckId;
                    SQLiteCommand commande2 = new SQLiteCommand(sql, _maConnexion);
                    SQLiteDataReader reader2 = commande2.ExecuteReader();
                    while(reader2.Read())
                    {
                        deck.IdDeck = DeckId;
                        deck.OpponentClasse = reader2.GetString(1);
                    }
                    sql = "SELECT * FROM Card WHERE Fk_deckId = " + DeckId;
                    SQLiteCommand commande3 = new SQLiteCommand(sql, _maConnexion);
                    SQLiteDataReader reader3 = commande3.ExecuteReader();
                    while(reader3.Read())
                    {
                        deck.CardsList.Add(new Card() { RealCardId = reader3.GetInt32(1) });
                    }
                    match.Deck = deck;
                    listToReturn.Add(match);
                }
            }
            Close();
            return listToReturn;
        }
    }
}
