using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI
{
    public enum ServerRegionTypeEnum
    {
        US,
        TH
    }

    #region GAMEPLAY

    public enum ShotTypeEnum
    {
        UNKOWN = 0x01,
        NORMAL = 0x02,
        OB = 0x03,
        Sucess = 0x04,
        UNKOWN_19 = 0x19
    }

    #endregion

    public enum PlayerActionEnum
    {
        PLAYER_ACTION_ROTATION = 0x00, //ROTAÇÃO DO CHARACTER
        PLAYER_ACTION_UNK = 0x01,
        PLAYER_ACTION_APPEAR = 0x04, //APARECER
        PLAYER_ACTION_SUB = 0x05, //POSTURA DO PLAYER
        PLAYER_ACTION_MOVE = 0x06, //PLAYER SE MOVE
        PLAYER_ACTION_ANIMATION = 0x07, //ANIMAÇÃO DO CHARACTER
        PLAYER_ACTION_ANIMATION_2 = 0x08,
        PLAYER_ANIMATION_WITH_EFFECTS = 0x0A
    }

    public enum PlayerPostureEnum
    {
        PLAYER_ACTION_SUB_STAND = 0x00,
        PLAYER_ACTION_SUB_SIT = 0x01,
        PLAYER_ACTION_SUB_SLEEP = 0x02
    }

    public enum ChangeEquipmentEnumB : byte
    {
        GetCaddieSelected = 0x01,
        GetBallSelected = 0x02,
        GetClubSelected = 0x03,
        GetCharSelected = 0x04,
        GetMascotSelected = 0x05,
        GameStart = 0x07
    }

    public enum GameModeTypeEnum
    {
        GAME_MODE_FRONT = 0x00,
        GAME_MODE_BACK = 0x01,
        GAME_MODE_RANDOM = 0x02,
        GAME_MODE_SHUFFLE = 0x03,
        GAME_MODE_REPEAT = 0x04,
        GAME_MODE_SSC = 0x05

    }

    public enum GameTypeEnum
    {
        VERSUS_STROKE = 0x00,
        VERSUS_MATCH = 0x01,
        CHAT_ROOM = 0x02,
        GAME_TYPE_03 = 0x03,
        TOURNEY = 0x04, // 30 Players tournament
        TOURNEY_TEAM = 0x05, // 30 Players team tournament
        TOURNEY_GUILD = 0x06, // Guild battle
        PANG_BATTLE = 0x07, // Pang Battle
        GAME_TYPE_08 = 0x08, // Public My Room
        GAME_TYPE_09 = 0x09,
        GAME_TYPE_0A = 0x0A,
        GM_EVENT = 0x0B, //GM_EVENT grand prix
        GAME_TYPE_0C = 0x0C,
        GAME_TYPE_0D = 0x0D,
        CHIP_IN_PRACTICE = 0x0E, //pratice
        GAME_TYPE_0F = 0x0F, // Playing for the first time
        GAME_TYPE_10 = 0x10, // Learn with caddie
        GAME_TYPE_11 = 0x11, // Stroke
        SSC = 0x12, // This is Chaos!
        HOLE_REPEAT = 0x13, // This is Chaos!
        GRANDPRIX = 0x14, // Grand Prix
        Default = 0xFF
    }

    public enum GameMapTypeEnum
    {
        Blue_Lagoon = 0x00,
        Blue_Water = 0x01,
        Sepia_Wind = 0x02,
        Wind_Hill = 0x03,
        Wiz_Wiz = 0x04,
        West_Wiz = 0x05,
        Blue_Moon = 0x06,
        Silvia_Cannon = 0x07,
        Ice_Cannon = 0x08,
        White_Wiz = 0x09,
        Shinning_Sand = 0x0A,
        Pink_Wind = 0x0B,
        Deep_Inferno = 0x0D,
        Ice_Spa = 0x0E,
        Lost_Seaway = 0x0F,
        Eastern_Valley = 0x10,
        Special_Flag = 0x11,
        Ice_Inferno = 0x12,
        Wiz_City = 0x13,
        Abbot_Mine = 0x14,
        Grand_Zodiac = 0x40, //mapa especial 
        Unknown = 0x7F
    }

    public enum ChannelTypeEnum : int
    {
        All = 1,//todos os players
        Rookie = 2048,//somente rockie's
        Test = 512,//nao sei
        Beginer = 16,//somente beginner's
        Junior = 32//somente juniors

    }

    public enum LobbyActionEnum
    {
        Create = 1, //criar novos players
        DESTROY = 2, //Remover player
        UPDATE = 3, //update player
        LIST = 4 //list players
    }

    /// <summary>
    /// Ação de gerenciamento de salas
    /// </summary>
    public enum GameActionEnum
    {
        LIST = 0,
        CREATE = 1,
        DESTROY = 2,
        UPDATE = 3
    }

    public enum GameShopFlagEnum : int
    {
        BUY_SUCCESS = 0,
        BUY_FAIL = 1,
        PANG_NOTENOUGHT = 2,
        PASSWORD_WRONG = 3,
        ALREADY_HAVEITEM = 4,
        OUT_OF_TIME = 11,
        CANNOT_BUY_ITEM1 = 18,
        CANNOT_BUY_ITEM = 19,
        TOO_MUCH_ITEM = 21,
        COOKIE_NOTENOUGHT = 23,
        ITEM_EXPIRED = 35,
        ITEM_CANNOT_PURCHASE = 36,
        LEVEL_NOTENOUGHT = 44
    }

    //EX: block Lounge, block make room, block scratchy e papel shop e etc
    //0x28 aqui desabilita tudo no server, psq, gift, e etc, 8 + 32 gift mais psq
    public enum ServerFlag : long
    {
        Default = 0,
        Unknown = 1,
        CanNotPlay = 2,
        ShopAllOFF = 4,
        GiftShopOFF = 8,
        PapelShopOFF = 16,
        ShopLoungeOFF = 32,
        StrokeOFF = 64,
        MatchOFF = 128,
        TorneyOFF = 256,
        ShortGameOFF = 512,
        GuildBattleOFF = 1024,
        PangBattleOFF = 2048,
        ApproachOFF = 4096,
        LoungeOFF = 8192,
        ScratchyOFF = 16384,
        TickerOFF = 131072,
        MailBoxOFF = 262144,
        GrandZodiac = 524288,
        SinglePlay = 1048576,
        GrandPrix = 2097152,
        GuildOFF = 16777216,
        SpecialSuperCourse_OFF = 33554432,
        MemorialShopOFF = 268435456,
        CharMasteryOFF = 1073741824,
        CardSystemOFF = 4294967296,
        RecycleSystemOFF = 17179869184
    }

    public enum ServerProperty : int
    {
        Normal = 0,
        GP = 2048,
        /// <summary>
        /// Versão US, não funciona
        /// </summary>
        Natural = 128
    }

    public enum GameShopEnum : byte
    {
        /// <summary>
        /// Itens normais
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 
        /// </summary>
        Rental = 1
    }
}
