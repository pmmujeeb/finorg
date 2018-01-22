using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace FinOrg
{
	//	BIOS + CPU
	//	KEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\System\BIOS
	//		BaseBoardManufacturer
	//		BaseBoardProduct
	//		BaseBoardVersion
	//		SystemProductName
	//	HARDWARE\DESCRIPTION\System\CentralProcessor\0\ProcessorNameString
	public class Auth
	{
		public static void CheckLicense()
		{
			https://stackoverflow.com/questions/17292366/hashing-with-sha1-algorithm-in-c-sharp
			using (SqlConnection con = FinOrgForm.getSqlConnection())
			{
				con.Open();
				SqlDataAdapter adapter = new SqlDataAdapter(@"
				SELECT * FROM LicenseAuth;

				EXEC xp_instance_regread
				'HKEY_LOCAL_MACHINE',
				'HARDWARE\DESCRIPTION\System\CentralProcessor\0',
				'ProcessorNameString';

				EXEC xp_instance_regread
				'HKEY_LOCAL_MACHINE',
				'HARDWARE\DESCRIPTION\System\BIOS',
				'BaseBoardManufacturer';

				EXEC xp_instance_regread
				'HKEY_LOCAL_MACHINE',
				'HARDWARE\DESCRIPTION\System\BIOS',
				'BaseBoardProduct';

				EXEC xp_instance_regread
				'HKEY_LOCAL_MACHINE',
				'HARDWARE\DESCRIPTION\System\BIOS',
				'BaseBoardVersion';

				EXEC xp_instance_regread
				'HKEY_LOCAL_MACHINE',
				'HARDWARE\DESCRIPTION\System\BIOS',
				'SystemProductName';
				", con);

				DataSet _data = new DataSet();
				adapter.Fill(_data);

				string str = _data.Tables[0].Rows[0]["name"].ToString();
				for (int i = 1; i < _data.Tables.Count; i++)
				{
					foreach(DataRow r in _data.Tables[i].Rows)
					{
						str += r["Value"].ToString() + r["Data"].ToString();
					}
				}
				string hashed = Hash(str);
				if (_data.Tables[0].Rows[0]["license_key"].ToString() != hashed)
				{
					MessageBox.Show("Not licensed");
				} else
				{
					MessageBox.Show("Licensed");
				}
			}
		}

		static string Hash(string input)
		{
			using (SHA1Managed sha1 = new SHA1Managed())
			{
				var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
				var sb = new StringBuilder(hash.Length * 2);

				foreach (byte b in hash)
				{
					// can be "x2" if you want lowercase
					sb.Append(b.ToString("X2"));
				}

				return sb.ToString();
			}
		}
	}
}
