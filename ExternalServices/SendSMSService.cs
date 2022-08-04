using PayamakPanel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExternalServices
{
    public class SendSMSService
    {
        private string panelUserName = "jahani";
        private string panelPassword = "jahangold";
        private string penelPhone = "10000021";
        FaraApi faraApi = new FaraApi();

        public bool SendList(List<string> mobileNumber, string message)
        {

            var list = new List<List<string>>();
            var nSize = 400;
            for (int i = 0; i < mobileNumber.Count; i += nSize)
            {
                list.Add(mobileNumber.GetRange(i, Math.Min(nSize, mobileNumber.Count - i)));
            }
            PayamakPanel.Core.Models.Result result = new PayamakPanel.Core.Models.Result();
            foreach (var item in list)
            {
                result = faraApi.SendSms(panelUserName, panelPassword, penelPhone, string.Join(",", item), message);
            }
            if (result.RetStatus == 1)
                return true;
            else
            {
                return false;
            }
        }
        public bool Send(string mobileNumber, string message)
        {
            try
            {
                var result = faraApi.SendSms(panelUserName, panelPassword, penelPhone, mobileNumber, message);
                if (result.RetStatus == 1)
                    return true;
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

    }
}
