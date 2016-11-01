using System.Diagnostics;

namespace MyNamespace.Project {
	public sealed class Derived : Parent {
		public Derived(string value, byte id) : base(value, id) {}

		private static readonly bool isConstructed = Construct();

		private static bool Construct() {
			Debug.WriteLine($"Statically constructing {nameof(Derived)}");
			return true;
		}

		public new static readonly Derived F = new Derived("F", 4);
		public new static readonly Derived O = new Derived("O", 5);
		public new static readonly Derived W = new Derived("W", 6);
		public new static readonly Derived D = new Derived("D", 7);
		public new static readonly Derived X = new Derived("X", 8);
		public new static readonly Derived Z = new Derived("Z", 9);
		public new static readonly Derived S = new Derived("S", 10);
		public new static readonly Derived K = new Derived("K", 11);
		public new static readonly Derived Q = new Derived("Q", 12);
		public new static readonly Derived Y = new Derived("Y", 13);
	}
}
