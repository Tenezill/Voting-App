using System;
using System.Linq;
using StackExchange.Redis;
using Dapper;
using Npgsql;

namespace worker
{
    class Program
    {
        static void Main(string[] args)
        {
            var psqlHost = Environment.GetEnvironmentVariable("PSQL_HOST") ?? "db";
            var psqlPwd = Environment.GetEnvironmentVariable("PSQL_PWD");
            var psqlConnectionString = $"Host={psqlHost};Username=postgres;Password=${psqlPwd};Database=postgres";

            // connect to redis
            var redisHost = Environment.GetEnvironmentVariable("REDIS_HOST") ?? "redis";
            var redis = ConnectionMultiplexer.Connect(redisHost);
            var redisDb = redis.GetDatabase();

            // infinite loop start
            while (true)
            {

                // query redis
                var votesVal = redisDb.ListRightPop("votes");

                // result?
                if (votesVal.HasValue)
                {
                    Console.WriteLine($"Got value from REDIS: {votesVal.ToString()}");

                    // connect to db
                    using (var dbConnection = new NpgsqlConnection(psqlConnectionString))
                    {
                        dbConnection.Open();
                        // consolidate
                        var currentVote = dbConnection.Query<VoteResult>("select").First();

                        // write changes to db
                    }
                }

                // pause 3sec
                System.Threading.Thread.Sleep(3000);
                // loop end
            }
        }
    }
}

class VoteResult
{
    public int Cats { get; set; }
    public int Dogs { get; set; }
}