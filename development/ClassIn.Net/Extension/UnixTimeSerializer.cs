using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Beyova;

namespace EF.E1Technology.EEO
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    public class UnixTimeSerializer : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime?);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var unixTotalSeconds = reader.Value.SafeToString().ObjectToNullableInt64();
            if (unixTotalSeconds.HasValue)
            {
                return CommonExtension.UnixMillisecondsToDateTime(unixTotalSeconds * 1000, DateTimeKind.Utc);
            }

            return null;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dateTime = value as DateTime?;
            if (dateTime.HasValue)
            {
                writer.WriteValue((long)(dateTime.Value.ToUnixMillisecondsDateTime() / 1000));
            }
            else
            {
                writer.WriteNull();
            }
        }
    }
}
