using System;

namespace Tiny {
	public static class Json {
		public static T Decode<T>(this string json) {
			if (string.IsNullOrEmpty(json)) return default(T);
			object jsonObj = JsonParser.ParseValue(json);
			if (jsonObj == null) return default(T);
			return JsonMapper.DecodeJsonObject<T>(jsonObj);
		}

        public static object Decode(this string json, Type type)
        {
            if (string.IsNullOrEmpty(json)) return null;
            object jsonObj = JsonParser.ParseValue(json);
            if (jsonObj == null) return null;
            return JsonMapper.DecodeJsonObject(jsonObj, type);
        }

        public static string Encode(this object value, bool pretty = false) {
			JsonBuilder builder = new JsonBuilder(pretty);
			JsonMapper.EncodeValue(value, builder);
			return builder.ToString();
		}
	}
}

