﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Builders;
using AddOn_Krosmaga___Blou_fire.Data;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.Models;
using AddOn_Krosmaga___Blou_fire.ProducerConsumer;
using JsonCardsParser;
using SQLiteConnector;
using Match = AddOn_Krosmaga___Blou_fire.UIElements.Match;
using NLog;

namespace AddOn_Krosmaga___Blou_fire.Services
{
    public class TrackerCoreSrv : ObservableObject
    {
        #region Properties
        BlockingCollection<byte[]> _workQueue = new BlockingCollection<byte[]>(new ConcurrentQueue<byte[]>());
        private BinaryReader _binaryReader;
        private byte[] _lastMessage = new byte[1];
        public CardCollection CardsCollection { get; set; }
        public TrackerModel
        TrackerModel { get; set; } //Model qui sera à découper par page. Peut contenir toutes les données actuellement.
        public FiltersStatModel CurrentFiltersStatModel { get; set; }
        //Spécifiques orbes/orbes dorés/nécronomigore
        public int LastTurnOrb = 0;
        public string DateTimeOrb = "";
        public int LastTurnOrbGold = 0;
        public string DateTimeOrbGold = "";
        // Spécifique de multiple token (la Sadida Sylvine)
        public string TimeMultiLinked = "";
        public int NBMultiLinked = 0;
        private static Logger logger = LogManager.GetCurrentClassLogger(); // Logs

        #endregion


        #region Core

        public TrackerCoreSrv()
        {
            logger.Info("Tracker is starting");
            CardsCollection = new CardCollection { Collection = new JsonCard().ChargerCartes() };
            Helpers.Helpers.FirewallValidation();
            TrackerModel = new TrackerModel();
            UpdateMatchsWithFilterList();
            Connector.CreateDatabase();
            Producer producer = new Producer(_workQueue);
            StartService();
        }

        public void StartService()
        {
            var consumer = Task.Run(() =>
            {
                while (true)
                {
                    byte[] data = _workQueue.Take();
                    Krosmaga(0, data);
                }
            });
        }

        private void Krosmaga(int ID, byte[] data)
        {
            try
            {
                if (data.Length > 40 && _lastMessage.SequenceEqual(data.Skip(40)))
                    return;
                _lastMessage = data.Skip(40).ToArray();
                Stream f = new MemoryStream(data);
                _binaryReader = new BinaryReader(f);
                if (_binaryReader.BaseStream.Length >= 41)
                    _binaryReader.BaseStream.Position = 40;
                else
                    return;
                _binaryReader.ReadBytes(3);
                while (_binaryReader.BaseStream.Position < _binaryReader.BaseStream.Length && _binaryReader.ReadByte() != 0)
                {
                    _binaryReader.ReadBytes(3);
                    string messageId = Helpers.Helpers.ConcatHeader(_binaryReader);
                    _binaryReader.ReadByte();
                    uint size = Helpers.Helpers.ReadRawVarint32(_binaryReader);
                    byte[] send;
                    switch (messageId)
                    {
                        //case "104BE831BFC1F6EB11C8B453B7F257C499": // LeaderboardPersonnalEntryEvent
                        case "A743D4E99C61572311D93D8A99B0D00AB9": // StartOfTurnEvent
                            Builders.StartOfTurn startofturn = new Builders.StartOfTurn();
                            send = _binaryReader.ReadBytes((int)size);
                            startofturn.Decode(send);
                            logger.Info("Turn : " + startofturn.Turn + " / Player : " + startofturn.PlayerIndex);
                            UIActionStartOfTurn(startofturn);
                            break;
                        case "89438706FC2AE2CD11B3891BE848AD7887": // GameStartedEvent
                            Builders.GameStarted gamestarted = new Builders.GameStarted();
                            send = _binaryReader.ReadBytes((int)size);
                            gamestarted.Decode(send);
                            logger.Info("Game is starting - Type : " + gamestarted.GameType);
                            UIActionGameEventStarted(gamestarted);
	                      
							break;
                        case "1B4FF61A6FBC09E611F2CBA7E5FB5391BA": // GameFinishedEvent
                            Builders.GameFinished gamefinished = new Builders.GameFinished();
                            send = _binaryReader.ReadBytes((int)size);
                            gamefinished.Decode(send);
                            logger.Info("Game Finished - Winner : " + gamefinished.WinnerPlayer);
                            UIActionGameFinishedEvent(gamefinished);
	                       
							break;
                        case "98400741B1CB5A4A110FC4D2D51E2D4CA9": // GameEventsEvent
                            Builders.GameEvents gameevents = new Builders.GameEvents();
                            send = _binaryReader.ReadBytes((int)size);
                            /*string msgdata = "";
                            foreach (var item in send)
                            {
                                msgdata = msgdata + item;
                            }
                            logger.Info("send : " + msgdata);*/
                            gameevents.Decode(send);
                            logger.Trace("Game Event");
                            UIActionGameEventsEvent(gameevents);
                            break;
                        case "24454BD9B42E0A231174846DD1A86A7ABB": // PlayerAccountLoadedEvent
                            Builders.PlayerAccountLoaded playerAccountLoaded = new Builders.PlayerAccountLoaded();
                            send = _binaryReader.ReadBytes((int)size);
                            playerAccountLoaded.Decode(send);
                            logger.Info("Player Account Loaded - Nickname : " + playerAccountLoaded.Nickname);
                            break;
                        /*case "F64A6D942BA9B2A01139131B120F0A6494":
	                        test = true;
	                        break;*/
                        default:
                            //logger.Trace("Default of switch (messageId)");
                            if (_binaryReader.BaseStream.Position + size > _binaryReader.BaseStream.Length)
                                size = (uint)_binaryReader.BaseStream.Length - (uint)_binaryReader.BaseStream.Position - 5;
                            if (size != 0)
                                _binaryReader.BaseStream.Position = _binaryReader.BaseStream.Position + size;
                            break;
                    }
                    _binaryReader.ReadBytes(3);
                    //logger.Trace("Header : " + messageId + " - " + size);
                }
            }
            catch (Exception e)
            {
                logger.Error("Error : " + e);
                foreach (var item in data)
                    logger.Error("Error : " + item);
            }
        }

        private void UIActionStartOfTurn(StartOfTurn startofturn)
        {
            TrackerModel.CurrentTurn = startofturn.Turn;
            logger.Trace("UIActionStartOfTurn");
        }


        private void UIActionGameEventStarted(GameStarted value)
        {
            logger.Trace("UIActionGameEventStarted");
            logger.Info("Game Type : " + value.GameType + " / MyCardsCount : " + value.MyCardsCount + " / OpponentCardsCount : " + value.OpponentCardsCount + " / MyIndex : " + value.MyIndex + " / PlayersCount : " + value.PlayersCount);
            foreach (var item in value.PlayersList)
            {
                logger.Info("Nickname : " + item.Profile.Nickname + " / GodId : " + item.GodId + " / Index : " + item.Index);
            }
            //On initialise le "Own" positionnement.
            /* //Tracking inversé, pour debug
            if (value.MyIndex == 0)
                TrackerModel.MyIndex = 1;
            else
                TrackerModel.MyIndex = 0;
            */
            TrackerModel.MyIndex = value.MyIndex;
            //On charge les données du player selon mon index
            Data.Player player = value.PlayersList.FirstOrDefault(x => x.Index == TrackerModel.MyIndex);
            logger.Info(player.Profile.Nickname + " : Player " + player.Index + " / GodId : " + player.GodId);
            //On met à jour les données du model
            if (player != null)
            {
                TrackerModel.OwnPseudo = player.Profile.Nickname ?? "";
                TrackerModel.OwnWinsNb = player.Profile.VictoryCount;
                TrackerModel.OwnLosesNb = player.Profile.DefeatCount;
                TrackerModel.OwnLevel = player.Profile.Level;
                TrackerModel.OwnClasse = ((Enums.God)Enum.Parse(typeof(Enums.God), player.GodId.ToString())).ToString();
            }
            //On refait de 
            player = value.PlayersList.FirstOrDefault(x => x.Index != TrackerModel.MyIndex);
            logger.Info(player.Profile.Nickname + " : Player " + player.Index + " / GodId : " + player.GodId);
            if (player != null)
            {
                TrackerModel.VsPseudo = player.Profile.Nickname ?? "";
                TrackerModel.VsWinsNb = player.Profile.VictoryCount;
                TrackerModel.VsLosesNb = player.Profile.DefeatCount;
                TrackerModel.OpponentLevel = player.Profile.Level;
                TrackerModel.OpponentClasse = ((Enums.God)Enum.Parse(typeof(Enums.God), player.GodId.ToString())).ToString();
            }

            JsonCardsParser.Card unknown = CardsCollection.getCardById(-1);
            UIElements.DeckUI deckui = new UIElements.DeckUI(unknown, 1)
            {
                DrawTurn = 0,
                CardRelated = ""
            };

            if (TrackerModel.MyIndex == 0)
            {
                TrackerModel.OwnCardsInHand = 3;
                TrackerModel.OpponentCardsInHand = 4;
                TrackerModel.AddCardToCardInHand(deckui);
                TrackerModel.AddCardToCardInHand(deckui);
                TrackerModel.AddCardToCardInHand(deckui);
                TrackerModel.AddCardToCardInHand(deckui);
            }
            else
            {
                TrackerModel.OwnCardsInHand = 4;
                TrackerModel.OpponentCardsInHand = 3;
                TrackerModel.AddCardToCardInHand(deckui);
                TrackerModel.AddCardToCardInHand(deckui);
                TrackerModel.AddCardToCardInHand(deckui);
            }
            TrackerModel.GameType = value.GameType;
        }

        private void UIActionGameFinishedEvent(GameFinished value)
        {
            logger.Trace("UIActionGameFinishedEvent");
            List<int> cardsToSave = new List<int>();
            foreach (var item in TrackerModel.Deck)
            {
                for (int i = 0; i < item.CardCount; i++)
                    cardsToSave.Add(item.Card.Id);
            }

            Connector.SaveMatchResult(TrackerModel.OpponentClasse, cardsToSave, TrackerModel.VsPseudo,
                TrackerModel.OwnClasse, value.WinnerPlayer == TrackerModel.MyIndex ? 1 : 0, TrackerModel.CurrentTurn, (int)TrackerModel.GameType,
                DateTime.Now);
            TrackerModel.OpponentPlayedCards.Clear();
            TrackerModel.CardAlreadyPlayed.Clear();
            TrackerModel.NbFleau = 0;
            TrackerModel.CardIdsByTurn.Clear();
            TrackerModel.OpponentCardsInHand = 0;
            TrackerModel.VsWinsNb = 0;
            TrackerModel.VsLosesNb = 0;
            TrackerModel.VsPseudo = "";
			TrackerModel.OpponentClasse = String.Empty;
            TrackerModel.CurrentTurn = 0;
            TrackerModel.OpponentLevel = 0;
            UpdateMatchsWithFilterList();
            TrackerModel.ClearCardToDeck();
            TrackerModel.ClearDeckInfinites();
            TrackerModel.ClearDeckKrosmiques();
            TrackerModel.ClearCardInHand();
            TrackerModel.ClearOwnCardsInHand();
        }

        private void UIActionGameEventsEvent(Builders.GameEvents value)
        {
            logger.Trace("UIActionGameEventsEvent");
            foreach (var item in value.EventsList)
            {
                if (item.EventType == Enums.EventType.TURN_ENDED)
                    logger.Info("Turn Ended");
                IterateEvents(item);
            }
        }

        private void IterateEvents(GameEvent value)
        {
            List<Builders.EventsManager.CardMovedEvent> list = new List<Builders.EventsManager.CardMovedEvent>();
            JsonCardsParser.Card card = CardsCollection.getCardById(-1); //'unknown
            JsonCardsParser.Card cardRelated = CardsCollection.getCardById(-1); //'unknown
            if (value.EventType == Enums.EventType.CARD_MOVED)
            {
                logger.Trace("Enums.EventType.CARD_MOVED");
                Builders.EventsManager.CardMovedEvent cardMoved = new Builders.EventsManager.CardMovedEvent(value);
                //logger.Info("card : " + cardMoved.Card + " / TradingCard : " + cardMoved.TradingCard + " / RelatedTradingCard : " + cardMoved.RelatedTradingCard + " / CardLocationFrom : " + cardMoved.CardLocationFrom + " / CardLocationTo : " + cardMoved.CardLocationTo + " / ConcernedFighter : " + cardMoved.ConcernedFighter);
                if (cardMoved.ConcernedFighter < 2) // Cette variable est utilisé pour stocker d'autres infos que les joueurs
                {
                    // Si on connait la carte, on récupère les infos
                    // 45 = prisme pioche / 589 = prisme fléau / 190 = prisme PA
                    if (cardMoved.TradingCard > 0 && cardMoved.TradingCard != 45 && cardMoved.TradingCard != 589 && cardMoved.TradingCard != 190)
                        card = CardsCollection.getCardById((int)cardMoved.TradingCard);
                    // Fléaux
                    if (cardMoved.RelatedTradingCard == 589)
                        card = CardsCollection.getCardById((int)1565);
                    // Sinon, on la considère unknown
                    if (card == null)
                        card = CardsCollection.getCardById(-1);
                    logger.Info("Player : " + cardMoved.ConcernedFighter + " => Card Name : " + card.Name + " From : " + cardMoved.CardLocationFrom + " To : " + cardMoved.CardLocationTo + " / Related to : " + cardMoved.RelatedTradingCard);
                    //*************************************************************************************************
                    //*************************************************************************************************
                    // Quand une carte arrive dans la main adverse
                    //*************************************************************************************************
                    //*************************************************************************************************
                    if ((cardMoved.ConcernedFighter != TrackerModel.MyIndex && cardMoved.CardLocationTo == Enums.CardLocation.OwnHand) ||
                        (cardMoved.ConcernedFighter == TrackerModel.MyIndex && cardMoved.CardLocationTo == Enums.CardLocation.OpponentHand))
                    {
                        TrackerModel.OpponentCardsInHand += 1;
                        logger.Info("Opponent Hand : " + TrackerModel.OpponentCardsInHand);
                        if (card.Id == 1565) // Si c'est un fléaux, on le compte
                        {
                            TrackerModel.NbFleau += 1;
                            logger.Info("Curse + 1 / Total : " + TrackerModel.NbFleau);
                        }
                        if (card.Id == -1)  // Si la carte est inconnu, on essaye de la deviner (token, ...)
                        {
                            // Si on connait la carte qui a fait piocher la nouvelle carte
                            if (cardMoved.RelatedTradingCard > 0 && cardMoved.RelatedTradingCard != 45 && cardMoved.RelatedTradingCard != 589) // 45 = prisme pioche / 589 =  fléau
                            {
                                cardRelated = CardsCollection.getCardById((int)cardMoved.RelatedTradingCard);
                                if (cardRelated.LinkedCards.Count() == 1) //Si une seul carte est lié, on peut supposer que c'est cette carte
                                    card = CardsCollection.getCardById((int)cardRelated.LinkedCards[0]);
                                else if(cardRelated.LinkedCards.Count() > 1) // Si plusieurs cartes sont liés (sylevine forherbe)
                                {
                                    if (NBMultiLinked == 0)
                                        TimeMultiLinked = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                                    if (TimeMultiLinked == DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString())
                                    {
                                        card = CardsCollection.getCardById((int)cardRelated.LinkedCards[NBMultiLinked]);
                                        NBMultiLinked = NBMultiLinked + 1;
                                    }
                                    else
                                    {
                                        TimeMultiLinked = "";
                                        NBMultiLinked = 0;
                                    }
                                }
                                //Orbe
                                if (cardRelated.Name.LongCount() > 0 && cardRelated.Name.IndexOf("necrome") >= 0)
                                {
                                    card = CardsCollection.getCardById((int)870);
                                    LastTurnOrb = TrackerModel.OpponentCardsInHand;
                                    DateTimeOrb = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                                }
                            }
                            // Orbe doré
                            if (LastTurnOrb > 0)
                            {
                                if (DateTimeOrb == DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString())
                                {
                                    if (cardMoved.RelatedTradingCard == 0 && LastTurnOrb == (TrackerModel.OpponentCardsInHand + 2))
                                    {
                                        card = CardsCollection.getCardById((int)605);
                                        LastTurnOrbGold = TrackerModel.OpponentCardsInHand;
                                        DateTimeOrbGold = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                                    }
                                }
                                else
                                {
                                    LastTurnOrb = 0;
                                    DateTimeOrb = "";
                                }
                            }
                            // Nécronomigore
                            if (LastTurnOrbGold > 0)
                            {
                                if (DateTimeOrbGold == DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString())
                                {
                                    if (cardMoved.RelatedTradingCard == 0 && LastTurnOrbGold == (TrackerModel.OpponentCardsInHand + 1))
                                    {
                                        card = CardsCollection.getCardById((int)659);
                                    }
                                }
                                else
                                {
                                    LastTurnOrbGold = 0;
                                    DateTimeOrbGold = "";
                                }
                            }
                        } // Fin Si la carte est inconnu, on essaye de la deviner (token, ...)
                        // On envoi les éléments à la vue
                        string _cardrelated = "";
                        if (cardRelated.Id != -1 && card.Id == -1)
                            _cardrelated = cardRelated.UIName;
                        UIElements.DeckUI deckui = new UIElements.DeckUI(card, 1)
                        {
                            DrawTurn = TrackerModel.CurrentTurn,
                            IdObject = cardMoved.Card,
                            CardRelated = _cardrelated
                        };
                        TrackerModel.AddCardToCardInHand(deckui);
                    }

                    //*************************************************************************************************
                    //*************************************************************************************************
                    // Quand une carte quitte la main adverse
                    //*************************************************************************************************
                    //*************************************************************************************************
                    if ((cardMoved.ConcernedFighter != TrackerModel.MyIndex && cardMoved.CardLocationFrom == Enums.CardLocation.OwnHand) ||
                        (cardMoved.ConcernedFighter == TrackerModel.MyIndex && cardMoved.CardLocationFrom == Enums.CardLocation.OpponentHand))
                    {
                        TrackerModel.OpponentCardsInHand -= 1;
                        logger.Info("Opponent Hand : " + TrackerModel.OpponentCardsInHand);
                        var cardInHandToRemove = TrackerModel.CardsInHand.FirstOrDefault(x => x.IdObject == cardMoved.Card);
                        if (cardInHandToRemove == null) // Si on ne retrouve pas la carte, c'est probablement l'une des cartes du mulligan
                            cardInHandToRemove = TrackerModel.CardsInHand.FirstOrDefault(x => x.Card.Id == -1 && x.DrawTurn == 0);
                        if (cardInHandToRemove != null)
                        {
                            if (cardInHandToRemove.Card.Id == 1565) // Si c'est un fléaux, on le décompte
                            {
                                TrackerModel.NbFleau -= 1;
                                logger.Info("Curse - 1 / Total : " + TrackerModel.NbFleau);
                            }
                            TrackerModel.RemoveCardFromCardInHand(cardInHandToRemove);
                        }
                        else
                            logger.Info("Error Card in hand not found : cardInHandToRemove == null / card : " + card.Name);
                    }
                }
                else
                    logger.Trace("Strange, Dofus / Prisme / ... ? - CardLocationFrom : " + cardMoved.CardLocationFrom + " => CardLocationTo : " + cardMoved.CardLocationTo + " / ConcernedFighter : " + cardMoved.ConcernedFighter + " / RelatedTradingCard : " + cardMoved.RelatedTradingCard);
                //Si une carte de ladversaire se "déplace"
                if ((cardMoved.CardLocationFrom == Enums.CardLocation.OwnHand && cardMoved.CardLocationTo == Enums.CardLocation.Playground ||
                     cardMoved.CardLocationFrom == Enums.CardLocation.OwnHand && cardMoved.CardLocationTo == Enums.CardLocation.OwnGraveyard ||
                     cardMoved.CardLocationFrom == Enums.CardLocation.OpponentPile && cardMoved.CardLocationTo == Enums.CardLocation.OpponentGraveyard ||
                     cardMoved.CardLocationFrom == Enums.CardLocation.OwnPile && cardMoved.CardLocationTo == Enums.CardLocation.OwnGraveyard) &&
                     cardMoved.ConcernedFighter != TrackerModel.MyIndex)
                {
                    logger.Trace("Moved 1");
                    TrackerModel.OpponentPlayedCards.Add(CardsCollection.getCardById((int)cardMoved.TradingCard));
                    card = CardsCollection.getCardById((int)cardMoved.TradingCard);
                    UIElements.DeckUI deckUI;
                    if (!TrackerModel.CardAlreadyPlayed.Any(x => x.CardCount == cardMoved.Card))
                    {
                        logger.Trace("Moved 1.1");
                        deckUI = TrackerModel.Deck.FirstOrDefault(x => x.Card == card);
	                   
						if (deckUI != null)
                        {
                            logger.Trace("Moved 1.1.1");
                            deckUI.CardCount++;
                        }
                        else
                        {
                            logger.Trace("Moved 1.1.2");

                            deckUI = new UIElements.DeckUI(card, 1);
                            try
                            {
                                deckUI.DrawTurn = TrackerModel.CardIdsByTurn.First(x => x.Key == cardMoved.Card).Value;
                            }
                            catch (Exception e)
                            {
                                deckUI.DrawTurn = 0;
                                //logger.Error("Error: deckUI.DrawTurn : " + e);
                            }
							deckUI.PlayedTurn.Add(TrackerModel.CurrentTurn);

                            //logger.Info($"Contenu du TrackerModel.Deck {string.Join($"{Environment.NewLine}", TrackerModel.Deck)}");
							TrackerModel.AddCardToDeck(deckUI);
                        }
					}
                    else
                        logger.Trace("Moved 1.2");
				}
				if (cardMoved.CardLocationTo == Enums.CardLocation.Playground ||
                    cardMoved.CardLocationTo == Enums.CardLocation.OwnGraveyard ||
                    cardMoved.CardLocationTo == Enums.CardLocation.OpponentGraveyard ||
                    cardMoved.CardLocationTo == Enums.CardLocation.OpponentHand ||
                    cardMoved.CardLocationTo == Enums.CardLocation.OwnHand)
                {
                    logger.Trace("Moved 2");
                    if (!TrackerModel.CardIdsByTurn.Any(x => x.Key == cardMoved.Card))
                    {
                        logger.Trace("Moved 2.1");
                        TrackerModel.CardIdsByTurn.Add(new KeyValuePair<int, int>(cardMoved.Card, TrackerModel.CurrentTurn));
                    }
                    if (!TrackerModel.CardAlreadyPlayed.Any(x => x.CardCount == cardMoved.Card))
                    {
                        logger.Trace("Moved 2.2");
                        card = null;
                        if (cardMoved.CardLocationFrom == Enums.CardLocation.Nowhere &&
                            cardMoved.CardLocationTo == Enums.CardLocation.OwnHand && cardMoved.ConcernedFighter != TrackerModel.MyIndex)
                        {
                            logger.Trace("Moved 2.2.1");
                            if (cardMoved.RelatedTradingCard == 589)
                            {
                                logger.Trace("Moved 2.2.1.1");
                                card = new JsonCardsParser.Card() { Id = 1565 };
                            }
                        }
                        else
                        {
                            logger.Trace("Moved 2.2.2");
                            if (cardMoved.TradingCard == 1565)
                            {
                                logger.Trace("Moved 2.2.2.1");
                                card = new JsonCardsParser.Card() { Id = 1565 };
                            }
                            else
                            {
                                logger.Trace("Moved 2.2.2.2");
                                card = CardsCollection.getCardById((int)cardMoved.TradingCard);
                            }
                        }
                        if (card != null)
                        {
                            logger.Trace("Moved 2.2.3");
                            TrackerModel.CardAlreadyPlayed.Add(new UIElements.DeckUI(card, cardMoved.Card));
                        }
                    }
                }
                if ((cardMoved.CardLocationTo == Enums.CardLocation.OwnHand && cardMoved.ConcernedFighter == TrackerModel.MyIndex) ||
                    (cardMoved.CardLocationTo == Enums.CardLocation.OpponentHand && cardMoved.ConcernedFighter != TrackerModel.MyIndex))
                {
                    logger.Trace("Moved 4");
                    TrackerModel.OwnCardsInHand += 1;
                    logger.Info("Own Hand : " + TrackerModel.OwnCardsInHand);
                }
                if ((cardMoved.CardLocationFrom == Enums.CardLocation.OwnHand && cardMoved.ConcernedFighter == TrackerModel.MyIndex) ||
                    (cardMoved.CardLocationFrom == Enums.CardLocation.OpponentHand &&
                     cardMoved.ConcernedFighter != TrackerModel.MyIndex))
                {
                    logger.Trace("Moved 6");
                    TrackerModel.OwnCardsInHand -= 1;
                    logger.Info("Own Hand : " + TrackerModel.OwnCardsInHand);
                }
                if (cardMoved.ConcernedFighter == TrackerModel.MyIndex)
                {
                    logger.Trace("Moved 7");
                    if ((cardMoved.CardLocationFrom == Enums.CardLocation.OpponentGraveyard &&
                         cardMoved.CardLocationTo == Enums.CardLocation.OpponentHand) ||
                        (cardMoved.CardLocationFrom == Enums.CardLocation.Playground &&
                         cardMoved.CardLocationTo == Enums.CardLocation.OpponentHand) ||
                        (cardMoved.CardLocationFrom == Enums.CardLocation.Nowhere &&
                         cardMoved.CardLocationTo == Enums.CardLocation.OpponentHand))
                    {
                        logger.Trace("Moved 7.1");
                        card = card = CardsCollection.getCardById((int)cardMoved.TradingCard);
                        if (card == null)
                        {
                            logger.Trace("Moved 7.1.1");
                            if (cardMoved.TriggeredEvents.Any(x => x.EventType == Enums.EventType.PLAYER_CARD_COST_MODIFIED))
                            {
                                logger.Trace("Moved 7.1.1.1");
                                card = CardsCollection.getCardById(cardMoved.TriggeredEvents
                                    .First(x => x.EventType == Enums.EventType.PLAYER_CARD_COST_MODIFIED).RelatedTradingCardId);
                            }
                        }
                        if (card != null)
                        {
                            logger.Trace("Moved 7.1.2");
                            //var carte = TrackerModel.CardsInHand.FirstOrDefault(x => x.IdObject == cardMoved.Card);
                            //if (carte != null)
                            //    TrackerModel.RemoveCardFromCardInHand(carte);
                            //UIElements.DeckUI deckUI = new UIElements.DeckUI(card, 1)
                            //{
                            //    DrawTurn = TrackerModel.CurrentTurn,
                            //    IdObject = cardMoved.Card
                            //};
                            //TrackerModel.AddCardToCardInHand(deckUI);
                        }
                    }
                }
                else
                {
                    logger.Trace("Moved 8");
                    if ((cardMoved.CardLocationFrom == Enums.CardLocation.Playground &&
                         cardMoved.CardLocationTo == Enums.CardLocation.OwnHand) ||
                        (cardMoved.CardLocationFrom == Enums.CardLocation.OwnGraveyard &&
                         cardMoved.CardLocationTo == Enums.CardLocation.OwnHand) ||
                        (cardMoved.CardLocationFrom == Enums.CardLocation.OpponentGraveyard &&
                         cardMoved.CardLocationTo == Enums.CardLocation.OwnHand))
                    {
                        logger.Trace("Moved 8.1");
                        if (cardMoved.CardLocationFrom == Enums.CardLocation.OpponentGraveyard &&
                            cardMoved.CardLocationTo == Enums.CardLocation.OwnHand)
                        {
                            logger.Trace("Moved 8.1.1");
                            UIElements.DeckUI deckUi = TrackerModel.CardAlreadyPlayed.FirstOrDefault(x => x.CardCount == cardMoved.Card);
                            card = deckUi.Card;
                        }
                        else
                        {
                            logger.Trace("Moved 8.1.2");
                            card = CardsCollection.getCardById((int)cardMoved.TradingCard);
                        }
                        if (card != null)
                        {
                            logger.Trace("Moved 8.1.3");
                            //UIElements.DeckUI deckUI = new UIElements.DeckUI(card, 1)
                            //{
                            //    DrawTurn = TrackerModel.CurrentTurn,
                            //    IdObject = cardMoved.Card
                            //};
                            //TrackerModel.AddCardToCardInHand(deckUI);
                        }
                    }
                }/*
                if (cardMoved.CardLocationFrom == Enums.CardLocation.OpponentHand &&
                    cardMoved.CardLocationTo == Enums.CardLocation.Nowhere && cardMoved.ConcernedFighter == TrackerModel.MyIndex)
                {
                    logger.Trace("Moved 9");
                    UIElements.DeckUI deckui = TrackerModel.CardAlreadyPlayed.FirstOrDefault(x => x.CardCount == cardMoved.Card);
                    if (deckui != null && deckui.Card.Id == 1565)
                    {
                        logger.Trace("Moved 9.1");
                        TrackerModel.NbFleau -= 1;
                        logger.Info("Curse - 1 / Total : " + TrackerModel.NbFleau);
                    }
                }*/
            }
            /*
            else if (value.EventType == Enums.EventType.CARD_TO_BE_PLAYED)
            {
                logger.Trace("Enums.EventType.CARD_TO_BE_PLAYED");
                var eventCardMoved = value.TriggeredEvents.FirstOrDefault(x => x.EventType == Enums.EventType.CARD_MOVED);
                if (eventCardMoved != null)
                {
                    Builders.EventsManager.CardMovedEvent cardMoved = new Builders.EventsManager.CardMovedEvent(eventCardMoved);
                    if (cardMoved.CardLocationFrom == Enums.CardLocation.OwnHand &&
                        cardMoved.CardLocationTo == Enums.CardLocation.Nowhere && cardMoved.ConcernedFighter != TrackerModel.MyIndex)
                    {
                        if (value.UInt1 == 1565)
                        {
                            TrackerModel.NbFleau -= 1;
                            logger.Info("Curse - 1 / Total : " + TrackerModel.NbFleau);
                        }
                    }
                }
            }
            else if (value.EventType == Enums.EventType.A_O_E_ACTIVATED)
            {
                logger.Trace("Enums.EventType.A_O_E_ACTIVATED");
                var eventCardMoved = value.TriggeredEvents.FirstOrDefault(x => x.EventType == Enums.EventType.CARD_MOVED);
                if (eventCardMoved != null && TrackerModel.ActualFleauxIds.Contains(value.Int1) && eventCardMoved.Int1 != TrackerModel.MyIndex)
                {
                    TrackerModel.NbFleau += 1;
                    logger.Info("Curse + 1 / Total : " + TrackerModel.NbFleau);
                }
            }
            else if (value.EventType == Enums.EventType.TURN_STARTED)
            {
                logger.Trace("Enums.EventType.TURN_STARTED");
                var listOfEvents = value.TriggeredEvents.Where(x => x.UInt1 == 589);
                if (listOfEvents != null)
                {
                    foreach (var item in listOfEvents)
                    {
                        Builders.EventsManager.NewAOEEvent newAOE = new Builders.EventsManager.NewAOEEvent(item);
                        if (UIDatas.ActualFleauxIds.Count >= 4)
						    UIDatas.ActualFleauxIds.Dequeue();
                        if (newAOE.TradingCardId == 589)
                        {
                            TrackerModel.ActualFleauxIds.Enqueue(newAOE.ConcernedFightObject);
                        }
                    }
                }
            }
            else if (value.EventType == Enums.EventType.NEW_A_O_E)
            {
                logger.Trace("Enums.EventType.NEW_A_O_E");
                Builders.EventsManager.NewAOEEvent newAOE = new Builders.EventsManager.NewAOEEvent(value);
                if (UIDatas.ActualFleauxIds.Count >= 4)
				    UIDatas.ActualFleauxIds.Dequeue();
                if (newAOE.TradingCardId == 589)
                {
                    TrackerModel.ActualFleauxIds.Enqueue(newAOE.ConcernedFightObject);
                }
            }
            else if (value.EventType == Enums.EventType.EFFECT_STOPPED)
            {
                logger.Trace("Enums.EventType.EFFECT_STOPPED");
                //if (value.UInt1 == 589)
                //{
                //	UIDatas.ActualFleauxIds.Enqueue(value.Int1);
                //}
            }*/
            foreach (var item in value.TriggeredEvents)
            {
                logger.Trace("Event Type Triggered : " + item.EventType);
                IterateEvents(item);
            }
        }


        //Permet le rafraichissement de la page d'historique
        public void UpdateMatchsWithFilterList()
        {
            TrackerModel.FilteredGames = new List<Match>();
            var tempList = new List<Match>();
            var sqlMatches = Connector.GetMatches();
            foreach (var item in sqlMatches)
            {
                TrackerModel.FilteredGames.Add(new UIElements.Match(item));
            }
            TrackerModel.FilteredGames = StartFiltreMatches();
        }

        private List<Match> StartFiltreMatches()
        {
            if (CurrentFiltersStatModel == null) return TrackerModel.FilteredGames;

            //if (CurrentFiltersStatModel.SelectedClass != ClassEnum.None)
            //	TrackerModel.FilteredGames.RemoveAll(x => x.PlayerClassName != CurrentFiltersStatModel.SelectedClass.ToString());
            //if (CurrentFiltersStatModel.SelectedVsClass != ClassEnum.None)
            //	TrackerModel.FilteredGames.RemoveAll(x => x.OppenentClassName != CurrentFiltersStatModel.SelectedVsClass.ToString());
            return TrackerModel.FilteredGames;
        }

        #endregion


    }
}