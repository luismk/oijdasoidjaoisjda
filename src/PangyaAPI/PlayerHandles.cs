using PangyaAPI.BinaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI
{
    public partial class Player
    {
        /// <summary>
        /// Envia cookies do player
        /// </summary>
        public void HandleCookies()
        {
            Response.Write(new byte[] { 0x96, 0x00, });
            Response.WriteUInt64(999321); //cookies inicias
            Response.WriteUInt64(0);
            SendResponse();
        }

        public void HandlePangs()
        {
            Response.Write(new byte[] { 0xC8, 0x00, });
            Response.WriteUInt64(888321);
            Response.WriteUInt64(0);
            this.SendResponse();
        }

        public void SendResponse(PangyaBinaryWriter response)
        {
            SendResponse(response.GetBytes());
        }

        public void SendNotice(string notice)
        {
            var response = new PangyaBinaryWriter();

            response.Write(new byte[] { 0x40, 0x00, 0x07 });
            response.WritePStr(Member_Old.Nickname);
            response.WritePStr(notice);

            SendResponse(response);
        }
    }
}
