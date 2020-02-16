using Dapper;
using PangyaAPI.Repository.Models;
using System.Data;
using System.Data.SqlClient;

namespace PangyaAPI.Repository
{
    public class GuildRepository
    {
        private readonly string _connectionString;

        public GuildRepository()
        {
            _connectionString = Settings.Default.ConnectionString;
        }

        public Guild GetById(int uid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection.QuerySingleOrDefault<Guild>("ProcGuildGetPlayerData", new { UID = uid },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Verifica se o GuildName está disponível para uso
        /// </summary>
        /// <param name="name">Nome da Guild</param>
        /// <returns>Disponível (true) / Indisponível (false)</returns>
        public bool GuildNameAvaliable(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection.QuerySingleOrDefault<bool>("ProcGuildNameAvailable", new { GUILDNAME = name },
                    commandType: CommandType.StoredProcedure) == true ?
                    false : //Indisponível 
                    true;  //Disponível
            }
        }

        /// <summary>
        /// Cria nova Guild
        /// </summary>
        /// <param name="uid">Usuário ID Leader</param>
        /// <param name="guildName">Nome da Guild</param>
        /// <param name="guildIntro">Introdução da Guild</param>
        /// <returns>Retorna código resultado: 10 = PLAYER IS IN GUILD,  0 = SUCCESSFULLY CREATED GUILD, 9 = GUILD NAME IS ALREADY EXISTED, 2 = TRANSACTION ERROR</returns>
        public int CreateGuild(int uid, string guildName, string guildIntro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection.QuerySingleOrDefault<int>("USP_GUILD_CREATE", new { UID = uid, GUILDNAME = guildName, GUILDINTRO = guildIntro },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
