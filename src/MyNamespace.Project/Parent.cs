using System.Diagnostics;

namespace MyNamespace.Project {
	public class Parent : Glossary {
		protected Parent(string value, byte id) : base(value, id) { }

		private static readonly bool isConstructed = Construct();

		private static bool Construct() {
			Debug.WriteLine($"Statically constructing {nameof(Parent)}");
			return true;
		}

		public bool IsDerived => this is Derived;
		public static readonly Parent A = new Parent("A", 1);
		public static readonly Parent B = new Parent("B", 2);
		public static readonly Parent I = new Parent("I", 3);
		public static readonly Parent F = Derived.F;
		public static readonly Parent O = Derived.O;
		public static readonly Parent W = Derived.W;
		public static readonly Parent D = Derived.D;
		public static readonly Parent X = Derived.X;
		public static readonly Parent Z = Derived.Z;
		public static readonly Parent S = Derived.S;
		public static readonly Parent K = Derived.K;
		public static readonly Parent Q = Derived.Q;
		public static readonly Parent Y = Derived.Y;
		public static readonly Parent U = new Parent("U", 0);
	}
}
