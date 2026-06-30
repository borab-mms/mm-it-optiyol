using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using MM.IT.Common.Models.MEX;
using Newtonsoft.Json.Linq;
using MM.IT.Common.Models.Sterling;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using MM.IT.Common.Extensions;
using Nancy.Bootstrapper;
using Microsoft.AspNetCore.Hosting.Server;

namespace MM.IT.Common.Helpers.RedisHelper;

public partial class RedisHelper
{
    public static IDatabase dbr;
    public static string ApplicationCode = "MediaMarktIT";
    public string redisKey = "";
    public string subKey = "";
    public int state = 0;
    private static int retryCount = 0;
    private const int MaxRetryCount = 3; // Maksimum yeniden deneme sayısı
    private static ISubscriber _redisSubscriber;
    private static readonly Lazy<ConnectionMultiplexer> lazyConnection =
        new Lazy<ConnectionMultiplexer>(() => CreateConnection());
    public RedisHelper(string _subKey, int _state = 0)
    {
        this.subKey = _subKey;
        this.state = _state;
        this.redisKey = GetRedisKey(_subKey, _state);
    }
    public static ConnectionMultiplexer redis => lazyConnection.Value;
    private static ConnectionMultiplexer CreateConnection()
    {
        //var host = "10.166.118.116";
        //var port = "6379";
        //var password = "yZ7aUR8MqUELfdWE";
        var host = "localhost";
        var port = "6379";
        var password = "1234";
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


    #region new 
    public static bool RedisConnect(string host, int port, string password, int dbNumber = 3)
    {
        try
        {
            //var host = "10.166.118.116";
            //var port = "6379";
            //var password = "yZ7aUR8MqUELfdWE";
            host = "localhost";
            port = 6379;
            password = "1234";
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
        if (redis.IsConnected)
        {
            return true;
        }
        else
        {
            return RedisConnect(host, port, password, dbNumber);
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

    #endregion

    #region MyRegion


    public static bool IsRedisConnect()
    {
        try
        {
            if (redis.IsConnected)
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public static string GetRedisKey(string subKey, int state = 0)
    {
        return string.Format("{0}:{1}:{2}",
                                ApplicationCode,
                                subKey, state.ToString());
    }
    public static RedisHelper GetRedisDB(string subKey, int state = 0)
    {
        return new RedisHelper(subKey, state);
    }
    public void SetHashKeyValue(string name, object value)
    {
        try
        {

            string _valueString = Newtonsoft.Json.Linq.JObject.FromObject(value).ToString();
            var _redisValue = dbr.HashSet(this.redisKey, new RedisValue(name), new RedisValue(_valueString));
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }
    }
    public async Task SetHashKeyValueAsync(string name, object value)
    {
        try
        {

            string _valueString = Newtonsoft.Json.Linq.JObject.FromObject(value).ToString();
            var _redisValue = await dbr.HashSetAsync(this.redisKey, new RedisValue(name), new RedisValue(_valueString));
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }
    }
    public static string RandomString(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public T GetHashKeyValue<T>(string name)
    {
        try
        {
            var _redisValue = dbr.HashGet(this.redisKey, new RedisValue(name));

            if (!_redisValue.HasValue) return default(T);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(_redisValue);
            //return ser.Deserialize<T>(_redisValue);
        }
        catch (Exception ex)
        {
            return default(T);
        }
    }
    public List<T> GetHashAllValues<T>()
    {
        try
        {
            var _redisValue = dbr.HashGetAll(this.redisKey);

            return _redisValue.Where(x => x.Value.HasValue)
                    .Select(x => Newtonsoft.Json.JsonConvert.DeserializeObject<T>(x.Value.ToString())).ToList();
        }
        catch (Exception ex)
        {
            return new List<T>();
        }
    }
    public T ListRightPopLeftPush<T>(int nextState = 1, int dbNumber = 3)
    {
        dbr = redis.GetDatabase(dbNumber);
        string valueString = dbr.ListRightPopLeftPush(this.redisKey, GetRedisKey(this.subKey, this.state + nextState));
        if (!string.IsNullOrEmpty(valueString))
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(valueString);
        else
            return default(T);
    }
    public T ListRightPopLeftPushWithKey<T>(string toFolder, int nextState = 1, int dbNumber = 3)
    {
        dbr = redis.GetDatabase(dbNumber);
        string valueString = dbr.ListRightPopLeftPush(this.redisKey, GetRedisKey(toFolder, this.state + nextState));
        if (!string.IsNullOrEmpty(valueString))
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(valueString);
        else
            return default(T);
    }
    public CustomerOrderRedisModelForError ListRightPopLeftPushWithKey(string toFolder, int nextState = 1, int dbNumber = 3)
    {
        var response = new CustomerOrderRedisModelForError();
        try
        {

            dbr = redis.GetDatabase(dbNumber);
            string valueString = dbr.ListRightPopLeftPush(this.redisKey, GetRedisKey(toFolder, this.state + nextState));
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerOrderRedisModel>(valueString);
            if (result != null)
            {
                response.CustomerOrderRedisModel = result;
            }
            else
            {
                response.Error = "Hata!";

            }
            return response;
        }
        catch (Exception ex)
        {
            response.Error = ex.Message + ex.InnerException;

            return response;
        }
    }
    public async Task<CustomerOrderRedisModelForError> ListRightPopLeftPushWithKeyAsync(string toFolder, int nextState = 1, int dbNumber = 3)
    {
        var response = new CustomerOrderRedisModelForError();
        try
        {

            dbr = redis.GetDatabase(dbNumber);
            string valueString = await dbr.ListRightPopLeftPushAsync(this.redisKey, GetRedisKey(toFolder, this.state + nextState));
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerOrderRedisModel>(valueString);
            if (result != null)
            {
                response.CustomerOrderRedisModel = result;
            }
            else
            {
                response.Error = "Hata!";

            }
            return response;
        }
        catch (Exception ex)
        {
            response.Error = ex.Message + ex.InnerException;

            return response;
        }
    }

    public void RightPushKey(object value, string jsonData)
    {
        var valueString = Newtonsoft.Json.Linq.JObject.FromObject(value);

        valueString.Add("jsonData", jsonData);

        dbr.ListRightPush(this.redisKey, valueString.ToString());
    }
    public void RightPushKey(object value)
    {
        string valueString = Newtonsoft.Json.Linq.JObject.FromObject(value).ToString();

        dbr.ListRightPush(this.redisKey, valueString);

    }
    public async Task RightPushKeyAsync(object value)
    {
        string valueString = Newtonsoft.Json.Linq.JObject.FromObject(value).ToString();

        await dbr.ListRightPushAsync(this.redisKey, valueString);

    }
    public void LeftPushKey(object value)
    {
        string valueString = Newtonsoft.Json.Linq.JObject.FromObject(value).ToString();

        dbr.ListLeftPush(this.redisKey, valueString);
    }
    public T ListRightPop<T>()
    {
        string valueString = dbr.ListRightPop(this.redisKey);
        if (!string.IsNullOrEmpty(valueString))
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(valueString);
        else
            return default(T);
    }
    public T ListLeftPop<T>()
    {
        try
        {
            string valueString = dbr.ListLeftPop(this.redisKey);

            if (!string.IsNullOrEmpty(valueString))
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(valueString);
        }
        catch (Exception ex)
        {
            var response = ex.Message;
        }
        return default(T);
    }
    public CustomerOrderRedisModelForError ListLeftPop()
    {
        var response = new CustomerOrderRedisModelForError();
        try
        {
            string valueString = dbr.ListLeftPop(this.redisKey);

            if (!string.IsNullOrEmpty(valueString))
            {

                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerOrderRedisModel>(valueString);
                if (result != null)
                {
                    response.CustomerOrderRedisModel = result;
                }
                else
                {
                    response.Error = "Hata1!";
                }
            }
            else
            {

                response.Error = "Hata2!";
            }
        }
        catch (Exception ex)
        {
            response.Error = ex.Message + ex.InnerException;
        }
        return response;
    }
    public async Task<CustomerOrderRedisModelForError> ListLeftPopAsync()
    {
        var response = new CustomerOrderRedisModelForError();
        try
        {
            string valueString = await dbr.ListLeftPopAsync(this.redisKey);

            if (!string.IsNullOrEmpty(valueString))
            {

                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerOrderRedisModel>(valueString);
                if (result != null)
                {
                    response.CustomerOrderRedisModel = result;
                }
                else
                {
                    response.Error = "Hata1!";
                }
            }
            else
            {

                response.Error = "Hata2!";
            }
        }
        catch (Exception ex)
        {
            response.Error = ex.Message + ex.InnerException;
        }
        return response;
    }

    /// <summary>
    /// İlgili key için belirtilen süre kadar bekleme ekler. Sonrasında otomatik silinir
    /// </summary>
    /// <param name="expireDate"></param>
    public void SetExpire(TimeSpan expireDate)
    {
        try
        {
            var _redisValue = dbr.KeyExpire(this.redisKey, expireDate);
        }
        catch (Exception ex)
        {
            var error = ex.InnerException.InnerException;
        }
    }
    /// <summary>
    /// List değerlerini belirtilen tipte geri döner
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public List<T> GetKeyValues<T>()
    {
        try
        {
            var _redisValue = dbr.HashGetAll(this.redisKey);

            return _redisValue.Where(x => x.Value.HasValue)
                    .Select(x => Newtonsoft.Json.JsonConvert.DeserializeObject<T>(x.Value.ToString())).ToList();

        }
        catch (Exception ex)
        {
            return new List<T>();
        }
    }

    public bool SetNotProcessingDataWithKey(string key, string toKey)
    {
        try
        {
            var keys = redis.GetServer($"localhost:6379")
             .Keys(database: 3, pattern: $"{key}")
             .ToArray();

            if (keys.Count() == 0)
            {
                return true;
            }
            foreach (var keyItem in keys)
            {
                string valueString = dbr.ListLeftPop(keyItem);

                if (!string.IsNullOrEmpty(valueString))
                {
                    dbr.ListRightPush(toKey, valueString);
                }
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }
    public bool DeleteKey()
    {
        var valueString = dbr.KeyDelete(this.redisKey);

        return valueString;
    }
    public bool DeleteHashKey(string key)
    {
        var valueString = dbr.HashDelete(this.redisKey, key);

        return valueString;
    }
    public async Task<bool> DeleteHashKeyAsync(string key)
    {
        var valueString = await dbr.HashDeleteAsync(this.redisKey, key);

        return valueString;
    }
    public bool IsRedisData()
    {
        //long count = dbr.SetLength(this.redisKey);
        //if (count == 0)
        //{
        //    return false;
        //}
        //return count > 0;
        try
        {
            // Anahtarın Redis'te var olup olmadığını kontrol et
            return dbr.KeyExists(this.redisKey);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hata oluştu: " + ex.Message);
            return false;
        }
    }
    public async Task<bool> IsRedisDataAsync()
    {
        //long count = dbr.SetLength(this.redisKey);
        //if (count == 0)
        //{
        //    return false;
        //}
        //return count > 0;
        try
        {
            // Anahtarın Redis'te var olup olmadığını kontrol et
            return await dbr.KeyExistsAsync(this.redisKey);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hata oluştu: " + ex.Message);
            return false;
        }
    }
    public bool IsRedisDataWithPattern(string pattern)
    {
        //var server = redis.GetServer("localhost", 6379);  // Redis sunucusunun adresi ve portu

        //// SCAN komutunu kullanarak anahtarları tarıyoruz
        //var keys = server.Keys(pattern: pattern); // "pattern" parametresi ile * deseni kullanıyoruz

        var getserver = redis.GetServer($"localhost:6379");

        var keys = getserver.Keys(database: 3, pattern: $"{pattern}")//
           .ToList();

        if (keys.Count() > 0)
        {
            return true;
        }
        return false;

    }

    public void SetStringKeyValue(object value, DateTime expires)
    {
        try
        {
            string text = JObject.FromObject(value).ToString();
            TimeSpan value2 = expires.Subtract(DateTime.UtcNow);
            dbr.StringSet(redisKey, text, value2);
        }
        catch (Exception)
        {
        }
    }
    public List<CustomerOrderRedisModel> GetKeyValuesByCreatedDate(DateTime dateTime)
    {
        try
        {
            var _redisValue = dbr.HashGetAll(this.redisKey);
            var result = _redisValue.Where(x => x.Value.HasValue)
                    .Select(x => Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerOrderRedisModel>(x.Value.ToString()));

            var myresult = result.Where(a => a.CreatedDate > dateTime).ToList();

            return myresult;

        }
        catch (Exception ex)
        {
            return new List<CustomerOrderRedisModel>();
        }
    }
    #endregion

}

