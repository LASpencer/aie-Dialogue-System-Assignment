using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class FieldManagerTests {

	[Test]
	public void FieldManagerFlagTests() {
        // Use the Assert class to test conditions.
        FieldManager fm = new FieldManager();

        // Flags not set are false
        Assert.That(!fm.CheckFlag("foo"));

        // Flags set are true
        fm.SetFlag("foo");
        fm.SetFlag("bar");
        Assert.That(fm.CheckFlag("foo"));
        Assert.That(fm.CheckFlag("bar"));
        Assert.That(!fm.CheckFlag("baz"));

        // Flags unset become false
        fm.UnsetFlag("foo");
        Assert.That(!fm.CheckFlag("foo"));
        Assert.That(fm.CheckFlag("bar"));

        // Clear clears flags
        fm.ClearFlags();
        Assert.That(!fm.CheckFlag("bar"));
    }

    [Test]
    public void FieldManagerNumberTests()
    {
        FieldManager fm = new FieldManager();

        // Numbers not set return 0?
        // HACK might change mind on this behaviour
        Assert.AreEqual(fm.GetNumber("Gold"), 0.0f);

        // Numbers can be set
        fm.SetNumber("Gold", 17);
        fm.SetNumber("Opinion", 25);
        Assert.AreEqual(fm.GetNumber("Gold"), 17.0f);
        Assert.AreEqual(fm.GetNumber("Opinion"), 25.0f);

        // Numbers can be deleted
        fm.ClearNumbers();
        // HACK might change mind on this behaviour
        Assert.AreEqual(fm.GetNumber("Gold"), 0.0f);

    }

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator FieldManagerTestsWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
