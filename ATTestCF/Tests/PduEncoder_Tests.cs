using System.Collections.Generic;
using System.Text;
using ATTestCF.Sms;

namespace ATTestCF.Tests
{
	public class PduEncoder_Tests
	{
		public void Test()
		{
			var pdu = EnumerableHelper.Single(PduEncoder.Encode(new Sms.Sms("+420776813221", "How are you?")));
			Assert.Equal("0011000C912470671823120000AA0CC8F71D14969741F977FD07", pdu);

			pdu = EnumerableHelper.Single(PduEncoder.Encode(new Sms.Sms("+123456789123", new string('x', 160))));
			Assert.Equal("0011000C912143658719320000AAA0783C1E8FC7E3F1783C1E8FC7E3F1783C1E8FC7E3F1783C1E8FC7E3F1783C1E8FC7E3F1783C1E8FC7E3F1783C1E8FC7E3F1783C1E8FC7E3F1783C1E8FC7E3F1783C1E8FC7E3F1783C1E8FC7E3F1783C1E8FC7E3F1783C1E8FC7E3F1783C1E8FC7E3F1783C1E8FC7E3F1783C1E8FC7E3F1783C1E8FC7E3F1783C1E8FC7E3F1783C1E8FC7E3F1783C1E8FC7E3F1", pdu);
		}

		public void CanEncodeLongMessage()
		{
			var pdus = EnumerableHelper.ToArray(PduEncoder.Encode(new Sms.Sms("+123456789123", "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.")));

			Assert.Equal(3, pdus.Length);

			var pdu1 = pdus[0];
			Assert.Equal("0041000C912143658719320000A0050003000301986F79B90D4AC3E7F53688FC66BFE5A0799A0E0AB7CB741668FC76CFCB637A995E9783C2E4343C3D4F8FD3EE33A8CC4ED359A079990C22BF41E5747DDE7E9341F4721BFE9683D2EE719A9C26D7DD74509D0E6287C56F791954A683C86FF65B5E06B5C36777181466A7E3F5B0AB4A0795DDE936284C06B5D3EE741B642FBBD3E1360B14AFA7E7", pdu1);
		}

		public void GetMessageBytesAsHexString_ShortMessage()
		{
			Assert.Equal("E8329BFD06DDDF723619", PduEncoder.GetMessageBytesAsHexString("hello world", false));
		}

		public void GetMessageBytesAsHexString_LongMessage()
		{
			Assert.Equal("D06536FB0DBABFE56C32", PduEncoder.GetMessageBytesAsHexString("hello world", true));
		}

		public void EncodeErr()
		{
			var pdus = EnumerableHelper.ToArray(PduEncoder.Encode(new Sms.Sms("+123456789123", "From its origin in southwestern Tibet as the Yarlung Tsangpo River, it flows across southern Tibet to break through the Himalayas in great gorges and into Arunachal Pradesh (India) where it is known as Dihang.[3] It flows southwest through the Assam Valley as Brahmaputra and south through Bangladesh as the Jamuna (not to be mistaken with Yamuna of India). In the vast Ganges Delta it merges with the Padma, the main distributary of the Ganges, then the Meghna, before emptying into the Bay of Bengal.")));

			Assert.Equal(4, pdus.Length);
		}
	}
}
