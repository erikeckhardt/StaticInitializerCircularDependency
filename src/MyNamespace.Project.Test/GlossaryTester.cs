using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;

namespace MyNamespace.Project.Test {
	internal sealed class GlossaryTester {
		private static readonly MethodInfo s_jsonConvertDeserializeObjectMethod =
			typeof(JsonConvert)
				.GetMethods()
				.Single(m =>
					m.Name == nameof(JsonConvert.DeserializeObject)
						&& m.ContainsGenericParameters
						&& m.GetParameters()
							.Select(p => p.ParameterType)
							.SequenceEqual(new [] { typeof(string) })
				);

		[TestCaseSource(typeof(GlossaryTypeTestProvider))]
		public void WhenVariableShouldSerializeCorrectly(Glossary glossary) {
			var expected = $"{glossary.Id}";
			var actual = JsonConvert.SerializeObject(glossary);
			actual.ShouldBe(expected);
		}

		[TestCaseSource(typeof(GlossaryTypeTestProvider))]
		public void WhenPropertyShouldSerializeCorrectly(Glossary glossary) {
			var testClass = new { Glossary = glossary };
			var result = JsonConvert.SerializeObject(testClass);
			result.ShouldBe($@"{{""Glossary"":{glossary.Id}}}");
		}

		[TestCaseSource(typeof(GlossaryTypeTestProvider))]
		public void ShouldDeserializeFromNumberCorrectly(Glossary glossary) {
			var json = $"{glossary.Id}";
			Glossary actual =
				(Glossary) s_jsonConvertDeserializeObjectMethod
					.MakeGenericMethod(glossary.GetType())
					.Invoke(null, new object[] { json });

			actual.ShouldBe(glossary);
		}

		[TestCaseSource(typeof(GlossaryTypeTestProvider))]
		public void ShouldDeserializeFromObjectCorrectly(Glossary glossary) {
			var json = $@"{{""Value"":""{glossary.Value}"",""Id"":{glossary.Id}}}";
			Glossary actual =
				(Glossary) s_jsonConvertDeserializeObjectMethod
					.MakeGenericMethod(glossary.GetType())
					.Invoke(null, new object[] { json });

			actual.ShouldBe(glossary);
		}

		private sealed class GlossaryTypeTestProvider : IEnumerable<Glossary> {
			private static readonly HashSet<Glossary> s_glossaryTypes =
				new HashSet<Glossary>(
					typeof(Glossary)
						.Assembly
						.GetTypes()
						.Where(t => !t.IsNotPublic && t.IsSubclassOf(typeof(Glossary)))
						.OrderBy(DerivationDepth)
						.SelectMany(GlossaryHelper.GetDefinedValues)
				);

			private static int DerivationDepth(Type t) => t.BaseType == typeof(object) ? 1 : DerivationDepth(t.BaseType);

			public IEnumerator<Glossary> GetEnumerator() => s_glossaryTypes.GetEnumerator();
			IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) s_glossaryTypes).GetEnumerator();
		}
	}
}
