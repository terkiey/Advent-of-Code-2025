using System.Security.Cryptography;
using System.Text;

namespace AoC.Days;

internal class SecurityDoorPasswordGenerator
{
	public string GenerateFromInput(string doorId)
	{
		int index = 0;
		StringBuilder sb = new();
		while (true)
		{
			string hash = GetMd5Hash(doorId + index);
			
			if (hash[..5] == "00000")
			{
				sb.Append(hash[5]);
			}
			if (sb.Length == 8)
			{
				return sb.ToString();
			}
			index++;
		}
	}

	public string GenerateInspiredFromInput(string doorId)
	{
		int index = 0;
		char[] password = new char[8];
		int hits = 0;
		while(true)
		{
			string hash = GetMd5Hash(doorId + index);
			int passwordIndex = -1;
			if (hash[..5] == "00000")
			{
				if (int.TryParse(hash[5].ToString(), out passwordIndex) && passwordIndex >= 0 && passwordIndex <= 7 && password[passwordIndex] == default)
				{
					password[passwordIndex] = hash[6];
					hits++;
					if (hits == 8)
					{
						return new(password);
					}
				}
			}
			index++;
		}
	}

	private string GetMd5Hash(string input)
	{
		MD5 md5 = MD5.Create();
		byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
		return Convert.ToHexString(hashBytes);
	}
}
