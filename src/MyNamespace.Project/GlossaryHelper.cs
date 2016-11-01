using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace MyNamespace.Project {
	internal static class GlossaryHelper {
		private static IReadOnlyCollection<Glossary> GetGlossaryMembers(Type type)
			=> type
				.GetFields(BindingFlags.Static | BindingFlags.Public)
				.Where(f => type.IsAssignableFrom(f.FieldType))
				.Select(f => (Glossary) f.GetValue(null))
				.ToList()
				.AsReadOnly();

		public static IEnumerable<Glossary> GetDefinedValues(Type t) => GetGlossaryMembers(t);

		public static IReadOnlyDictionary<byte?, Glossary> GetDictionaryById(Type t) =>
			new ReadOnlyDictionary<byte?, Glossary>(
				GetGlossaryMembers(t)
					.ToDictionary(glossary => (byte?) glossary.Id)
			);
	}
}
