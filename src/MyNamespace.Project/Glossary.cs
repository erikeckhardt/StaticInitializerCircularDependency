using Newtonsoft.Json;

namespace MyNamespace.Project {
	/// <summary>
	/// A base class for pseudo-enums that is guaranteed to supply a Value property. Serializes as a byte.
	/// </summary>
	[JsonConverter(typeof(GlossaryConverter))]
	public abstract class Glossary {
		protected Glossary(string value, byte id) {
			Value = value;
			Id = id;
		}

		public string Value { get; }
		public byte Id { get; }

		public override string ToString() => $"{GetType().Name} {Id}: {Value}";
	}
}
