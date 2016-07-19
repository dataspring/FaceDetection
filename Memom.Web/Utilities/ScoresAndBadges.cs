using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.Common;
using System.Data.Entity;

using System.Data.Entity.Infrastructure;
using System.Configuration;

using MemomMvc52.Models;
using System.Data.SqlClient;

namespace MemomMvc52.Utilities
{
    public static class ScoresAndBadges
    {
        public static ScoreCard LeaderBoardResults(string UserCode, string LevelName, int PageStart, int PageLength)
        {

            ScoreCard playerScore = new ScoreCard();

            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["jhealthConnection"].ConnectionString);
            using (var db = new HpbDbContext(connection))
            {

                var cmd = db.Database.Connection.CreateCommand();

                cmd.CommandText = "SP_LeaderBoardHS_HPB";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                DbParameter param1 = cmd.CreateParameter();
                param1.DbType = DbType.String;
                param1.ParameterName = "@UserId";
                param1.Value = UserCode;
                cmd.Parameters.Add(param1);

                DbParameter param2 = cmd.CreateParameter();
                param2.DbType = DbType.String;
                param2.ParameterName = "@LevelName";
                param2.Value = LevelName;
                cmd.Parameters.Add(param2);

                DbParameter param3 = cmd.CreateParameter();
                param3.DbType = DbType.Int32;
                param3.ParameterName = "@PageStart";
                param3.Value = PageStart;
                cmd.Parameters.Add(param3);

                DbParameter param4 = cmd.CreateParameter();
                param4.DbType = DbType.Int32;
                param4.ParameterName = "@PageLength";
                param4.Value = PageLength;
                cmd.Parameters.Add(param4);

                try
                {
                    if (db.Database.Connection.State != ConnectionState.Open)
                        db.Database.Connection.Open();

                    var reader = cmd.ExecuteReader();

                    var rsCount = ((IObjectContextAdapter)db).ObjectContext.Translate<int>(reader);

                    playerScore.Count = rsCount.FirstOrDefault<int>();

                    reader.NextResult();

                    var rsScores = ((IObjectContextAdapter)db).ObjectContext.Translate<ScoreItem>(reader);

                    playerScore.LeaderboardScores = rsScores.ToArray<ScoreItem>().ToList();

                    reader.NextResult();

                    var rsPlayerScore = ((IObjectContextAdapter)db).ObjectContext.Translate<ScoreItem>(reader);

                    playerScore.PlayerScore = rsPlayerScore.FirstOrDefault<ScoreItem>();


                }
                catch (Exception exp)
                {
                    throw exp;
                }
                finally
                {
                    db.Database.Connection.Close();
                }

            }

            return playerScore;

        }

        public static PlayerBadges BadgeResults(string UserCode)
        {

            PlayerBadges playerBadges = new PlayerBadges();

            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["jhealthConnection"].ConnectionString);
            using (var db = new HpbDbContext(connection))
            {

                var cmd = db.Database.Connection.CreateCommand();

                cmd.CommandText = "SP_LeaderBoardBadges_HPB";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                DbParameter param1 = cmd.CreateParameter();
                param1.DbType = DbType.String;
                param1.ParameterName = "@UserId";
                param1.Value = UserCode;
                cmd.Parameters.Add(param1);

                try
                {
                    if (db.Database.Connection.State != ConnectionState.Open)
                        db.Database.Connection.Open();

                    var reader = cmd.ExecuteReader();

                    var rsBadges = ((IObjectContextAdapter)db).ObjectContext.Translate<Badge>(reader);

                    playerBadges.Badges = rsBadges.ToArray<Badge>().ToList();

                }
                catch (Exception exp)
                {
                    throw exp;
                }
                finally
                {
                    db.Database.Connection.Close();
                }

            }

            return playerBadges;

        }

        public static PlayerMetas BadgeMaster(string ClientCode)
        {

            //reuse to get all badge display info - as Badge class has both the display and score details together 
            PlayerMetas playerBadges = new PlayerMetas();

            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["jhealthConnection"].ConnectionString);
            using (var db = new HpbDbContext(connection))
            {

                var cmd = db.Database.Connection.CreateCommand();

                cmd.CommandText = "SP_GetBadgeDetails_HPB";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                DbParameter param1 = cmd.CreateParameter();
                param1.DbType = DbType.String;
                param1.ParameterName = "@ClientCode";
                param1.Value = ClientCode;
                cmd.Parameters.Add(param1);

                try
                {
                    if (db.Database.Connection.State != ConnectionState.Open)
                        db.Database.Connection.Open();

                    var reader = cmd.ExecuteReader();

                    var rsBadges = ((IObjectContextAdapter)db).ObjectContext.Translate<BadgeMeta>(reader);

                    playerBadges.BadgeMetas = rsBadges.ToArray<BadgeMeta>().ToList();

                }
                catch (Exception exp)
                {
                    throw exp;
                }
                finally
                {
                    db.Database.Connection.Close();
                }

            }

            return playerBadges;

        }

        public static ScoreItem LoggedInPlayerScore(string UserCode, string LevelName)
        {
            ScoreItem playerScore = new ScoreItem();
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["jhealthConnection"].ConnectionString);
            using (var db = new HpbDbContext(connection))
            {

                //var cmd = db.Database.Connection.CreateCommand();

                //cmd.CommandText = "SP_GetPlayerScore_HPB";

                //cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //DbParameter param1 = cmd.CreateParameter();
                //param1.DbType = DbType.String;
                //param1.ParameterName = "@UserCode";
                //param1.Value = UserCode;
                //cmd.Parameters.Add(param1);

                //try
                //{
                //    if (db.Database.Connection.State != ConnectionState.Open)
                //        db.Database.Connection.Open();

                //    var reader = cmd.ExecuteReader();

                //    var rsBadges = ((IObjectContextAdapter)db).ObjectContext.Translate<ScoreItem>(reader);

                //    playerScore = rsBadges.First();

                //}
                //catch (Exception exp)
                //{
                //    throw exp;
                //}
                //finally
                //{
                //    db.Database.Connection.Close();
                //}

                var cmd = db.Database.Connection.CreateCommand();

                cmd.CommandText = "SP_LeaderBoardHS_HPB";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                DbParameter param1 = cmd.CreateParameter();
                param1.DbType = DbType.String;
                param1.ParameterName = "@UserId";
                param1.Value = UserCode;
                cmd.Parameters.Add(param1);

                DbParameter param2 = cmd.CreateParameter();
                param2.DbType = DbType.String;
                param2.ParameterName = "@LevelName";
                param2.Value = LevelName;
                cmd.Parameters.Add(param2);

                DbParameter param3 = cmd.CreateParameter();
                param3.DbType = DbType.Int32;
                param3.ParameterName = "@PageStart";
                param3.Value = 0;
                cmd.Parameters.Add(param3);

                DbParameter param4 = cmd.CreateParameter();
                param4.DbType = DbType.Int32;
                param4.ParameterName = "@PageLength";
                param4.Value = 10;
                cmd.Parameters.Add(param4);

                try
                {
                    if (db.Database.Connection.State != ConnectionState.Open)
                        db.Database.Connection.Open();

                    var reader = cmd.ExecuteReader();

                    //var rsCount = ((IObjectContextAdapter)db).ObjectContext.Translate<int>(reader);

                    //playerScore.Count = rsCount.FirstOrDefault<int>();

                    reader.NextResult();

                    //var rsScores = ((IObjectContextAdapter)db).ObjectContext.Translate<ScoreItem>(reader);

                    //playerScore.LeaderboardScores = rsScores.ToArray<ScoreItem>().ToList();

                    reader.NextResult();

                    var rsPlayerScore = ((IObjectContextAdapter)db).ObjectContext.Translate<ScoreItem>(reader);

                    playerScore = rsPlayerScore.FirstOrDefault<ScoreItem>();


                }
                catch (Exception exp)
                {
                    throw exp;
                }
                finally
                {
                    db.Database.Connection.Close();
                }


            }

            return playerScore;

        }

    }
}