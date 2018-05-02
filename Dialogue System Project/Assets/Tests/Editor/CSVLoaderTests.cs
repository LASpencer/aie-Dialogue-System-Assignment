using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Dialogue;
using System.Collections.Generic;

public class CSVLoaderTests {

    //TODO write tests (make a test file, parse it correctly)
    CSVLoader loader;
    Dictionary<string, string> dict;

    [SetUp]
    public void Setup()
    {
        loader = AssetDatabase.LoadAssetAtPath<CSVLoader>("Assets/Tests/Editor/TestCSVLoader.asset");
        //TODO load test file as a resource
    }

    [TearDown]
    public void Cleanup()
    {

    }

    [Test]
    public void LoadsEntry()
    {
        dict = loader.LoadLanguage("en");
        Assert.AreEqual("Good day!", dict["greeting"]);
    }

    [Test]
    public void ParsesQuotesCorrectly()
    {
        dict = loader.LoadLanguage("en");
        Assert.AreEqual("It was all a \"misunderstanding\"", dict["misunderstanding"]);
        Assert.AreEqual("There was a small \"house\" on the hillside.", dict["house"]);
    }

    [Test]
    public void EscapesCommaInQuote()
    {
        dict = loader.LoadLanguage("en");
        Assert.AreEqual("There's something inspirational about it, one can be more self-critical, and it makes for better style.", dict["why_write"]);
        Assert.AreEqual("I could say: \"Well, it's like this, what with my meagre salary...\"", dict["excuse"]);
    }

    [Test]
    public void LoadsOtherLanguage()
    {
        dict = loader.LoadLanguage("de");
        Assert.AreEqual("Guten Tag!", dict["greeting"]);
        Assert.AreEqual("Es war alles ein \"Missverstaendnis\"", dict["misunderstanding"]);
        Assert.AreEqual("Es gab ein kleines \"Haus\" am Hang.", dict["house"]);
        Assert.AreEqual("Es gibt etwas Inspirierendes, man kann selbstkritischer sein und es macht einen besseren Stil", dict["why_write"]);
        Assert.AreEqual("Ich koennte sagen: \"Nun, es ist so, was mit meinem mageren Gehalt...\"", dict["excuse"]);
    }
}
