using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyNamespace.Project {
	internal sealed class GlossaryConverter : JsonConverter {
		public override bool CanConvert(Type objectType) => objectType == typeof(Glossary);
		public override bool CanWrite => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => JToken.FromObject(((Glossary) value).Id).WriteTo(writer);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
			byte id;
			if (reader.TokenType == JsonToken.Integer) {
				id = (byte) JToken.ReadFrom(reader);
			} else {
				id = (byte) JObject.Load(reader)["Id"];
			}
			return GlossaryHelper.GetDictionaryById(objectType)[id];
		}
	}
}
