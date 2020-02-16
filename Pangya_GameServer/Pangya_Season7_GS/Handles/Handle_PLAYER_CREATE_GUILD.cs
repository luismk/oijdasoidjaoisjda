using Pangya_Season7_GS.Handles_Packet;
using PangyaAPI;
using PangyaAPI.BinaryModels;
using PangyaAPI.Repository;
using System;

namespace Pangya_Season7_GS.Handles
{
    public class Handle_PLAYER_CREATE_GUILD : HandleBase<Packet_PLAYER_CREATE_GUILD>
    {
        public Handle_PLAYER_CREATE_GUILD(Player player) : base(player)
        {
            Handle();
        }

        private void Handle()
        {
            var result = (new GuildRepository()).CreateGuild(Player.Member_Old.UID, PacketResult.GuildName, PacketResult.GuildInfo);

            switch (result)
            {
                case 10: //PLAYER IS IN GUILD
                    Player.Send(new byte[] { 0xB5, 0x01, 0xee, 0xd4, 0x00, 0x00 });
                    break;


                case 9: //GUILD NAME IS ALREADY EXISTED
                    Player.Send(new byte[] { 0xB5, 0x01, 0xf3, 0xd2, 0x00, 0x00 });
                    break;

                case 2: //TRANSACTION ERROR
                    Player.Send(new byte[] { 0xB5, 0x01, 0x00, 0x00, 0x00, 0x00 });
                    break;

                case 0: //SUCCESSFULLY CREATED GUILD
                    {
                        var response = new PangyaBinaryWriter();

                        //// Delete Guild Creator
                        //var warehouse = player.Inventory.Remove(436207919, 1, false);

                        // Successfully Created
                        response.Write(new byte[] { 0xC5, 0x00 });
                        response.Write((byte)1);
                        response.Write(436207919);
                        response.Write(1);//count?
                        response.Write(1);

                        Player.SendResponse(response);

                        response = new PangyaBinaryWriter();
                        response.Write(new byte[] { 0xB5, 0x01 });
                        response.Write(1);// Status Successfully
                        Player.SendResponse(response);

                        //Reload player in guild
                        Player.SendResponse(GuildInfo());
                    }
                    break;
            }

            //if(true) //Item que cria guild
            //{
            //    Player.SendResponse(new byte[] { 0xB5, 0x01, 0xf1, 0xd2, 0x00, 0x00 });
            //}
            //else if(true)
            //{
            //    Player.Send(new byte[] { 0xB5, 0x01, 0xf1, 0xd2, 0x00, 0x00 });
            //}
        }

        private byte[] GuildInfo()
        {
            var response = new PangyaBinaryWriter();
            response.Write(new byte[] { 0xBF, 0x01 });
            response.Write((uint)1);

            response.Write(Player.Guild.GUILD_INDEX);//GUILD ID?
            response.WriteStr(Player.Guild.GUILD_NAME, 17);//GUILD NAME(TH > 21, US 17)
            response.Write((UInt32)0); // guild level point ok
            response.Write((UInt32)0); //guild pangs OK  
            response.Write((UInt32)Player.Guild.GUILD_TOTAL_MEMBER); //guild total member ok
            response.WriteStr(Player.Guild.GUILD_IMAGE, 9); //GuilD_IMAGE
            response.WriteEmptyBytes(3);
            response.WriteStr(Player.Guild.GUILD_NOTICE, 0x65);//GUILD_Notice
            response.WriteStr(Player.Guild.GUILD_INTRODUCING, 101);//GUILD_INTRODUCING
            response.Write((UInt32)Player.Guild.GUILD_POSITION);// Guild Position
            response.Write((UInt32)Player.Guild.GUILD_LEADER_UID); //// Guild Leader UID?
            response.WriteStr(Player.Guild.GUILD_LEADER_NICKNAME, 22); //guild leader nickname OK
            response.Write(PangyaAPI.Tools.Utils.ToPangyaDateTime(Player.Guild.GUILD_CREATE_DATE?.Date ?? DateTime.Now)); //PlayerData.GuildData.GUILD_CREATE_DATE
            return response.GetBytes();
        }
    }
}
