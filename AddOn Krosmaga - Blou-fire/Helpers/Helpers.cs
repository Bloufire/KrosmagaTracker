using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NetFwTypeLib;
using NLog.Fluent;

namespace AddOn_Krosmaga___Blou_fire.Helpers
{
	public class Helpers
	{
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
					fwPolicy2.Rules.Remove(rule.Name);
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
	}
}