using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core_MVC.CustomSessionExtensions
{
    public static class CustomSessions
    {
        /// <summary>
        /// Saving CLR object in session 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public static void SetObject<T>(this ISession session, string key, T data)
        {
            session.SetString(key, JsonSerializer.Serialize(data));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);// return a blank instance
            }
            else
            {
                // deserialize
                return JsonSerializer.Deserialize<T>(data);
            }
        }

    }
}


