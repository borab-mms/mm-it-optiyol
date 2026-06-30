using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Sterling;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Helpers.RedisHelper
{
    public delegate void MessageReceivedHandler(string channel, string message);

    public class PubSubHelper
    {
        private static readonly Lazy<ConnectionMultiplexer> lazyConnection =
                new Lazy<ConnectionMultiplexer>(() => CreateConnection());

        private static ISubscriber _redisSubscriber;
        public event MessageReceivedHandler? OnMessageReceived;
        public static IDatabase dbr;
        public static string ApplicationCode = "MediaMarktIT";
        public string channelName = "";
        public string fullChannel = "";
        private static int retryCount = 0;
        private const int MaxRetryCount = 3; // Maksimum yeniden deneme sayısı
        public PubSubHelper()
        {
            //fullChannel = $"{ApplicationCode}:{channelName}:{state}";
        }

        public static ConnectionMultiplexer redis => lazyConnection.Value;
        private static ConnectionMultiplexer CreateConnection()
        {
            var host = "10.166.118.116";
            var port = "6379";
            var password = "yZ7aUR8MqUELfdWE";
            var options = ConfigurationOptions.Parse($"{host}:{port},password={password},abortConnect=false");

            options.AbortOnConnectFail = false;
            options.SyncTimeout = 5000;
            options.ConnectTimeout = 5000;
            options.DefaultDatabase = 3;
            options.KeepAlive = 180;
            options.AllowAdmin = true;

            while (retryCount < MaxRetryCount)
            {
                try
                {
                    var connection = ConnectionMultiplexer.Connect(options);
                    dbr = connection.GetDatabase();
                    Console.WriteLine("Redis bağlantısı başarıyla kuruldu.");
                    return connection;
                }
                catch (Exception ex)
                {
                    retryCount++;
                    Console.WriteLine($"Redis bağlantı hatası: {ex.Message}. {retryCount}/{MaxRetryCount} deneme.");
                    System.Threading.Thread.Sleep(2000); // 2 saniye bekleyerek tekrar dene
                }
            }

            throw new Exception("Redis bağlantısı başarısız oldu. Maksimum deneme sayısına ulaşıldı.");
        }
        public static bool RedisConnect(string host, int port, string password, int dbNumber = 3)
        {
            try
            {
                host = "10.166.118.116";
                port = 6379;
                password = "yZ7aUR8MqUELfdWE";
                var options = ConfigurationOptions.Parse($"{host}:{port},password={password},abortConnect=false");

                options.AbortOnConnectFail = false;
                options.SyncTimeout = 5000;
                options.ConnectTimeout = 5000;
                options.DefaultDatabase = 3;
                options.KeepAlive = 180;
                options.AllowAdmin = true;

                while (retryCount < MaxRetryCount)
                {
                    try
                    {
                        var connection = ConnectionMultiplexer.Connect(options);
                        dbr = connection.GetDatabase();
                        Console.WriteLine("Redis bağlantısı başarıyla kuruldu.");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        retryCount++;
                        Console.WriteLine($"Redis bağlantı hatası: {ex.Message}. {retryCount}/{MaxRetryCount} deneme.");
                        System.Threading.Thread.Sleep(2000); // 2 saniye bekleyerek tekrar dene
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Redis bağlantı hatası: {ex.Message}");
            }
            return false;
        }
        public static bool CheckRedisConnect(string host, int port, string password, int dbNumber = 3)
        {
            return redis.IsConnected || RedisConnect(host, port, password, dbNumber);
        }
        public static void ConnectToRedis(string host, int Port, string Password, int dbNumber = 3)
        {
            try
            {
                //var options = ConfigurationOptions.Parse($"{host}:{Port},password={Password}");
                //options.AbortOnConnectFail = false;
                //options.SyncTimeout = 1000; // 1 saniye
                //options.ConnectTimeout = 1000; // 1 saniye
                //redis = ConnectionMultiplexer.Connect(string.Format("{0},allowAdmin=true", options));

                // Bağlantı kesildiğinde yeniden bağlantı kurmak için olayları yakala
                redis.ConnectionFailed += Connection_ConnectionFailed;
                redis.ConnectionRestored += Connection_ConnectionRestored;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
            }
        }
        static void Connection_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine($"Connection failed to Redis. Reason: {e.FailureType}, EndPoint: {e.EndPoint}");

            // Bağlantı başarısız olduğunda, bağlantıyı kapatıp yeniden başlatmayı dene
            DisposeAndReconnect();
        }
        static void Connection_ConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine($"Connection to Redis restored. EndPoint: {e.EndPoint}");
        }
        // Bağlantıyı Dispose edip yeniden bağlanmaya çalış
        static void DisposeAndReconnect()
        {
            try
            {
                if (redis != null)
                {
                    // Bağlantıyı düzgün bir şekilde kapat
                    redis.Dispose();
                    Console.WriteLine("Connection disposed.");
                }

                // Bağlantıyı yeniden başlat
                retryCount++;
                if (retryCount <= 3) // Maksimum 3 deneme
                {
                    Console.WriteLine($"Retrying to connect to Redis... Attempt {retryCount}");
                    ConnectToRedis("localhost", 6379, "1234");
                }
                else
                {
                    Console.WriteLine("Failed to reconnect after multiple attempts.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during reconnect: {ex.Message}");
            }
        }
        public static bool IsRedisConnect()
        {
            try
            {
                if (redis != null)
                {
                    if (redis.IsConnected)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }
        public static bool RedisDispose()
        {
            try
            {
                if (redis != null)
                {
                    if (redis.IsConnected)
                    {
                        redis.Dispose();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }
        public void InitSubscriber()
        {
            // Subscriber nesnesini al
            if (_redisSubscriber == null)
            {
                _redisSubscriber = redis.GetSubscriber();
            }
        }

        public static ISubscriber GetSubscriber()
        {
            // Subscriber nesnesini al
            if (_redisSubscriber == null)
            {
                _redisSubscriber = redis.GetSubscriber();
            }

            return _redisSubscriber;
        }
        public void SubscribeToChannel(string channelName)
        {
            try
            {
                _redisSubscriber.Subscribe(channelName, (channel, message) =>
                {
                    Console.WriteLine($"[{channel}] Yeni mesaj: {message}");

                    // Delegate çağırılıyor (Event varsa tetikle)
                    OnMessageReceived?.Invoke(channel, message);
                });

                Console.WriteLine($"{channelName} kanalına abone olundu.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Abone olma hatası: {ex.Message}");
            }
        }
        public void PublishMessage<T>(string channelName, T model)
        {
            try
            {
                string message = JsonConvert.SerializeObject(model);
                // Mesajı kanala yayınla
                long subscriberCount = _redisSubscriber.Publish(channelName, message);

                Console.WriteLine($"Mesaj gönderildi. Alıcı sayısı: {subscriberCount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Yayınlama hatası: {ex.Message}");
            }
        }
        public static PubSubHelper GetRedisDB()
        {
            return new PubSubHelper();
        }
        public static async Task<RedisResult> ExecuteScanAsync(int cursor, string pattern, int pageSize)
        {
            if (redis != null)
            {
                try
                {
                    // Redis veritabanını al
                    //var db = redis.GetDatabase();

                    // SCAN komutunu çalıştır ve sonucu döndür
                    return await dbr.ExecuteAsync("SCAN", cursor, "MATCH", pattern, "COUNT", pageSize);
                }
                catch (Exception ex)
                {
                    // Hata durumunda logging yapılabilir
                    Console.WriteLine($"SCAN operation failed: {ex.Message}");
                    return null;
                }
            }
            else
            {
                // Redis bağlantısı yoksa hata mesajı döndür
                Console.WriteLine("Redis connection is not established.");
                return null;
            }
        }
        public static async Task<List<RedisKey>> ExecuteKeysAsync(string pattern, int keyBatchSize)
        {
            if (redis != null)
            {
                try
                {
                    if (redis.IsConnected)
                    {
                        var server = redis.GetServer("localhost", 6379);

                        var keys = server.Keys(pattern: pattern, pageSize: keyBatchSize).ToList();
                        return keys;
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    // Hata durumunda logging yapılabilir
                    Console.WriteLine($"Keys operation failed: {ex.Message}");
                    return null;
                }
            }
            else
            {
                // Redis bağlantısı yoksa hata mesajı döndür
                Console.WriteLine("Redis connection is not established.");
                return null;
            }
        }
        public static async Task<long> ExecuteListLengthAsync(RedisKey key)
        {
            if (redis != null)
            {
                try
                {
                    if (redis.IsConnected)
                    {
                        long listLength = await dbr.ListLengthAsync(key);

                        return listLength;
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return new long(); ;
        }
        public static async Task<List<RedisValue>> ExecuteListRangeAsync(RedisKey key, long start, long end)
        {
            if (redis != null)
            {
                try
                {
                    if (redis.IsConnected)
                    {
                        var rangeItems = await dbr.ListRangeAsync(key, start, end);

                        return rangeItems.ToList();
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return new List<RedisValue>();
        }

    }
}
