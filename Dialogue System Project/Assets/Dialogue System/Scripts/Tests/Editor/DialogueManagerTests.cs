using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Dialogue;

public class DialogueManagerTests 
{
    GameObject dm;
    DialogueManager manager;

    [SetUp]
    public void Setup()
    {
        dm = new GameObject();
        manager = dm.AddComponent<DialogueManager>();
    }

    [TearDown]
    public void Cleanup()
    {
        Object.DestroyImmediate(dm);
    }

    [Test]
    public void DialogueManagerFlagTests()
    {
        Assert.That(!manager.CheckFlag("foo"));

        manager.SetFlag("foo");
        manager.SetFlag("bar");

        Assert.That(manager.CheckFlag("foo"));
        Assert.That(manager.CheckFlag("bar"));

        manager.UnsetFlag("foo");

        Assert.That(!manager.CheckFlag("foo"));
        Assert.That(manager.CheckFlag("bar"));
        Object.DestroyImmediate(dm);
    }
}