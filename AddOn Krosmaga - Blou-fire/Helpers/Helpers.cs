using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using AddOn_Krosmaga___Blou_fire.Enums;
using AddOn_Krosmaga___Blou_fire.UIElements;
using JsonCardsParser;
using NetFwTypeLib;
using NLog;
using Card = SQLiteConnector.Card;

namespace AddOn_Krosmaga___Blou_fire.Helpers
{
    public class Helpers
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static bool TryOpenUrl(string url, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "")
        {
            try
            {
                // Log.Info("[Helper.TryOpenUrl] " + url, memberName, sourceFilePath);
                Process.Start(url);
                return true;
            }
            catch (Exception e)
            {
                // Log.Error("[Helper.TryOpenUrl] " + e, memberName, sourceFilePath);
                logger.Error("Error: Process.Start(url) : " + e);
                return false;
            }
        }

        public static void FirewallValidation()
        {
            Type tNetFwPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2) Activator.CreateInstance(tNetFwPolicy2);
            var currentProfiles = fwPolicy2.CurrentProfileTypes;

            bool _exist = false;

            foreach (INetFwRule rule in fwPolicy2.Rules)
            {
                if (rule.Name.IndexOf("KrosmagaAddOn: " + System.IO.Path.GetFileName(Assembly.GetEntryAssembly().Location)) != -1)
                {
                    try
                    {
                        fwPolicy2.Rules.Remove(rule.Name);
                    }
                    catch (Exception e)
                    {
                        logger.Error("Error: fwPolicy2.Rules.Remove(rule.Name) : " + e);
                    }

                    break;
                }
            }
            if (!_exist)
            {
                INetFwRule firewallRule = (INetFwRule) Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
                firewallRule.Enabled = true;
                firewallRule.InterfaceTypes = "All";
                firewallRule.ApplicationName = Assembly.GetEntryAssembly().Location;
                firewallRule.Name = "KrosmagaAddOn: " + System.IO.Path.GetFileName(Assembly.GetEntryAssembly().Location);
                INetFwPolicy2 firewallPolicy = (INetFwPolicy2) Activator.CreateInstance
                    (Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                firewallRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
                firewallPolicy.Rules.Add(firewallRule);
            }
        }

        public static string ConcatHeader(BinaryReader b)
        {
            string toReturn = "";

            for (int i = 0; i < 17; i++)
            {
                toReturn += b.ReadByte().ToString("X2");
            }

            return toReturn.ToUpper();
        }

        public static uint ReadRawVarint32(BinaryReader b)
        {
            if (b.BaseStream.Position + 5 > b.BaseStream.Length)
            {
                return SlowReadRawVarint32(b);
            }
            int num = (int) b.ReadByte();
            if (num < 128)
            {
                return (uint) num;
            }
            int num2 = num & 127;
            if ((num = (int) b.ReadByte()) < 128)
            {
                num2 |= num << 7;
            }
            else
            {
                num2 |= (num & 127) << 7;
                if ((num = (int) b.ReadByte()) < 128)
                {
                    num2 |= num << 14;
                }
                else
                {
                    num2 |= (num & 127) << 14;
                    if ((num = (int) b.ReadByte()) < 128)
                    {
                        num2 |= num << 21;
                    }
                    else
                    {
                        num2 |= (num & 127) << 21;
                        num2 |= (num = (int) b.ReadByte()) << 28;
                        if (num >= 128)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                if (ReadRawByte(b) < 128)
                                {
                                    return (uint) num2;
                                }
                            }
                        }
                    }
                }
            }
            return (uint) num2;
        }

        public static uint SlowReadRawVarint32(BinaryReader b)
        {
            int num = (int) ReadRawByte(b);
            if (num < 128)
            {
                return (uint) num;
            }
            int num2 = num & 127;
            if ((num = (int) ReadRawByte(b)) < 128)
            {
                num2 |= num << 7;
            }
            else
            {
                num2 |= (num & 127) << 7;
                if ((num = (int) ReadRawByte(b)) < 128)
                {
                    num2 |= num << 14;
                }
                else
                {
                    num2 |= (num & 127) << 14;
                    if ((num = (int) ReadRawByte(b)) < 128)
                    {
                        num2 |= num << 21;
                    }
                    else
                    {
                        num2 |= (num & 127) << 21;
                        num2 |= (num = (int) ReadRawByte(b)) << 28;
                        if (num >= 128)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                if (ReadRawByte(b) < 128)
                                {
                                    return (uint) num2;
                                }
                            }
                        }
                    }
                }
            }
            return (uint) num2;
        }

        private static byte ReadRawByte(BinaryReader b)
        {
            if (b.BaseStream.Position == b.BaseStream.Length)
                return 0;
            return b.ReadByte();
        }

        public static Color GetClassColor(string argKey, bool b)
        {
            Enum.TryParse(argKey, out ClassEnum enumClass);
            switch ((int)enumClass)
            {
                case 1:
                    return GetColorFromHex("#B02511"); // IOP
                case 2:
                    return GetColorFromHex("#474D1A"); // CRA
                case 3:
                    return GetColorFromHex("#C05343"); // ENI
                case 4:
                    return GetColorFromHex("#78128D"); // ECA
                case 5:
                    return GetColorFromHex("#A87E00"); // ENU
                case 6:
                    return GetColorFromHex("#434D67"); // SRAM
                case 7:
                    return GetColorFromHex("#29387E"); // XEL
                case 8:
                    return GetColorFromHex("#382F25"); // SACRI
                case 10:
                    return GetColorFromHex("#848A00"); // SADI
                default:
                    return GetColorFromHex("#734744"); // NEUTRE
            }
        }

        private static Color GetColorFromHex(string hexColor)
        {
            return (Color) ColorConverter.ConvertFromString(hexColor);
        }



        public static List<DeckUI> TransformCardListToDeckUiList(List<Card> valueCardsList)
        {
            CardCollection cards;
            cards = new CardCollection();
            cards.Collection = new JsonCard().ChargerCartes();
            var valueToReturn = new List<DeckUI>();
            foreach (var card in valueCardsList)
            {
                var cardToAdd = cards.getCardById(card.RealCardId);
                //Si la carte existe déjà dans la liste , on 

                if (valueToReturn.Find(x => x.Card == cardToAdd) == null)
                {
                    var deckui = new DeckUI(cardToAdd, 1);
                    valueToReturn.Add(deckui);
                }
                else
                {
                    valueToReturn.Find(x => x.Card == cardToAdd).CardCount = ++valueToReturn.Find(x => x.Card == cardToAdd).CardCount;
                }
            }

            return valueToReturn;
        }
    }
}