using PangyaAPI.BinaryModels;
using System;
using System.Collections.Generic;

namespace PangyaAPI.Repository.Models
{
    public class Game
    {
        public List<HoleInfo> HolesInfo { get; set; }

        public List<Player> Players = new List<Player>();

        public byte Un { get; set; }
        /// <summary>
        /// Ocorre quando o player termina a partida por completo
        /// </summary>
        public bool GameCompleted { get; set; } = false;

        /// <summary>
        /// Nome da Sala
        /// </summary>
        public string RoomTitle { get; set; }
        //Senha da Sala
        public string Password { get; set; }


        /// <summary>
        /// 00 (False) = Disabled, 01 (True) = Enabled
        /// </summary>
        public uint NaturalMode { get; set; }

        /// <summary>
        /// 00 = Stroke / 01 = Match / 
        /// </summary>
        public GameTypeEnum Mode { get; set; }

        /// <summary>
        /// Quantidade de players que poderá jogar
        /// </summary>
        public byte MaxPlayers { get; set; }

        /// <summary>
        /// Quantidade de Holes (03 / 06 / 09 / 18)
        /// </summary>
        public byte Holes { get; set; }

        /// <summary>
        /// Tempo por jogada (40sec / 60sec / 90sec / 120sec)
        /// </summary>
        public uint TimeSec { get; set; }

        /// <summary>
        /// Tempo por Torneio (30min / 40min / 50min)
        /// </summary>
        public uint TimeMin { get; set; }

        public byte Time30S { get; set; }

        /// <summary>
        /// Mapa
        /// </summary>
        public GameMapTypeEnum Course { get; set; }

        /// <summary>
        /// 00 = Front, 01 = Back, 02 = Random, 03 = Suffle
        /// </summary>
        public GameModeTypeEnum HoleOrder { get; set; }
        /// <summary>
        /// Game_Key
        /// </summary>
        public byte[] RoomKey { get; set; }
        /// <summary>
        /// ID do proprietario da sala
        /// </summary>
        public int Owner_ID { get; set; }
        /// <summary>
        /// ?????
        /// </summary>
        /// 
        public GameTypeEnum SpecialFlag { get; set; } = GameTypeEnum.Default;

        /// <summary>
        /// Artefato> dar xp, pangs, aumenta chuva etc
        /// </summary>
        public uint Artifact { get; set; }
        public byte FIDle { get; internal set; } = 0;
        /// <summary>
        /// Fecha o Hole(Hole_Repeted)
        /// </summary>
        public uint LockHole { get; internal set; }
        /// <summary>
        /// Numero do hole, usado no practice
        /// </summary>
        public byte HoleNumber { get; internal set; } = 0;
        public uint Trophy { get; set; } = 0;
        /// <summary>
        /// Numero da Sala
        /// </summary>
        public ushort GameId { get; set; }

        /// <summary>
        /// Sala de Evento,  true(evento), false(sem evento)
        /// </summary>
        public bool GMEvent { get; set; } = false;

        /// <summary>
        /// Sala Iniciou, true(sala ainda nao iniciou) false(sala iniciou)
        /// </summary>
        public Boolean GameStarted { get; set; } = false;

        public bool GP { get; set; } = false;

        public uint GPTypeID { get; set; }

        public uint GPTypeIDA { get; set; }

        public uint GPTime { get; set; }

        public Game()
        {
        }

        /// <summary>
        /// Envia um packet para todos os Players
        /// </summary>
        /// <param name="packet">packet</param>
        public void SendToAll(PangyaBinaryWriter packet)
        {
            Players.ForEach(p => p.SendResponse(packet));
        }

        public void SendToAll(byte[] packet)
        {
            var result = new PangyaBinaryWriter();
            result.Write(packet);

            SendToAll(result);
        }


        public void BuildCreateHole()
        {
            this.HolesInfo = new List<HoleInfo>(18);

            for (var i = 0; i < 18; i++)
            {
                int randNum = new Random().Next();
                byte Pos = (byte)new Random().Next(1, 3);
                ushort WP = (ushort)new Random().Next(0, 8);
                ushort WD = (ushort)new Random().Next(255);
                ushort WT = (ushort)(new Random().Next(0, 3));
                var hole = new HoleInfo()
                {
                    GameInfo = this,
                    Index = randNum,
                    Hole = (byte)(i + 1),
                    Weather = WT,
                    WindPower = WP,
                    WindDirection = WD,
                    Course = (byte)Course,
                    Pos = Pos,
                    ModeType = HoleOrder
                };

                HolesInfo.Add(hole);
            }
        }


        public byte[] GetGameInformation()
        {
            var result = new PangyaBinaryWriter();

            result.WriteStr(RoomTitle, 64); //ok
            result.Write((byte)(Password.Length > 0 ? 0 : 1)); //senha
            result.Write((byte)(GameStarted ? 0 : 1));
            result.Write((byte)0);//Orange
            result.Write(MaxPlayers); //maxplayer in room ok, 30 players
            result.Write((byte)Players.Count); //conta quantidade de players na sala //Players.GetAll().Count
            //result.Write(RoomKey, 17);//ultimo byte é zero

            result.Write(RoomKey);
            result.Write((byte)0x00);
            result.Write((byte)(Mode == GameTypeEnum.VERSUS_STROKE ? 30 : 0)); //Time30S
            result.Write(Holes);
            result.Write((byte)Mode);
            result.Write(GameId);
            result.Write((byte)HoleOrder);
            result.Write((byte)Course);
            result.Write(TimeSec);
            result.Write(TimeMin);
            result.Write(Trophy); // trophy typeid
            result.Write(FIDle); //Idle
            result.Write((byte)(GMEvent ? 1 : 0)); //GM Event 0(OFF), ON(1)
            result.WriteEmptyBytes(66); //bytes referente a guild 
            result.Write(0);// rate pang 
            result.Write(0);// rate chuva ?
            result.Write(Owner_ID); //meu uid
            result.Write((byte)SpecialFlag);
            result.Write(Artifact);//artefato
            result.Write(NaturalMode);//natural mode
            result.Write(GPTypeID);//Grand Prix 1
            result.Write(GPTypeIDA);//Grand Prix 2
            result.Write(GPTime);//Grand Time
            result.Write((uint)(GP ? 1 : 0));// grand prix active

            return result.GetBytes();
        }

        /// <summary>
        /// 48 00
        /// </summary>
        /// <returns></returns>
        public byte[] CreateVS()
        {
            var result = new PangyaBinaryWriter();

            result.Write(new byte[]
            {
                0x48, 0x00,0x00,
                0xFF, 0xFF,
                (byte)Players.Count //PlayersCount
            });

            int i = 1;
            foreach (var player in Players)
            {
                player.GameSlot.SlotNumber = i;

                result.Write(player.ConnectionId);

                if (Mode == GameTypeEnum.TOURNEY)
                {
                    result.Write(player.GetGameInfoCAMP());
                }
                else if (Mode == GameTypeEnum.VERSUS_STROKE)
                {
                    result.Write(player.GetGameInfoVSMatch());

                    //VS MATCH MODE --- No modo camp não tem.
                    result.Write(player.Characters.GetCurrentCharacterData());
                }

                i++;
            }

            result.Write((byte)0);

            //SendToAll(result.GetBytes());

            return result.GetBytes();
        }

        public byte[] CreateChatMode(Player p)
        {
            p.GameSlot.SlotNumber = Players.Count + 1;

            var result = new PangyaBinaryWriter();

            result.Write(new byte[]
            {
                0x48, 0x00,0x00,
                0xFF, 0xFF,
                (byte)Players.Count //PlayersCount
            });

            //int i = 1;
            //foreach (var player in Players)
            //{

            result.Write(p.ConnectionId);

            result.Write(p.GetGameInfoVSMatch());
            result.Write(p.Characters.GetCurrentCharacterData());

            //i++;
            //}

            result.Write((byte)0);

            //SendToAll(result.GetBytes());

            return result.GetBytes();
        }


        public void CreateChatModeList(Player p)
        {
            foreach (var player in Players)
            {
                var result = new PangyaBinaryWriter();

                result.Write(new byte[]
                {
                0x48, 0x00,0x07, //LIST
                0xFF, 0xFF,
                (byte)Players.Count //PlayersCount
                });

                result.Write(player.ConnectionId);

                result.Write(player.GetGameInfoVSMatch());
                result.Write(player.Characters.GetCurrentCharacterData());

                result.Write(0);

                p.SendResponse(result);
            }
        }

        public void CreateVSStroke()
        {

            int i = 1;
            foreach (var player in Players)
            {
                var result = new PangyaBinaryWriter();

                result.Write(new byte[]
                {
                    0x48, 0x00,0x03,
                    0xFF, 0xFF,
                });

                player.GameSlot.SlotNumber = i;

                result.Write(player.ConnectionId);
                result.Write(player.ConnectionId);

                result.Write(player.GetGameInfoVSMatch_StrokeGame());
                i++;

                //Envia para todos
                Players.ForEach(p => p.SendResponse(result.GetBytes()));

                //player.SendResponse(result.GetBytes());
            }
        }

        //private void SendToAll(byte[] message)
        //{
        //    Players.ForEach(p => p.SendResponse(message));
        //}

    }
}
