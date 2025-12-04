using NUnit.Framework;
using UnityEngine;


namespace Tests.EditMode
{
    public class EditorModeTests
    {
        [TestCase(1, "TestName_1")]
        [TestCase(2, "TestName_2")]
        [TestCase(3, "TestName_3")]
        [TestCase(4, "TestName_4")]
        [TestCase(5, "TestName_5")]
        public void CheckPlayerPrefsInt(int prefsValue, string keyName)
        {
            PlayerPrefs.SetInt(keyName, prefsValue);
            
            Assert.IsTrue(PlayerPrefs.GetInt(keyName) == prefsValue, $"{keyName} value does not match!");
            PlayerPrefs.DeleteKey(keyName);
        }

        [TestCase( "TestName_1")]
        [TestCase( "TestName_2")]
        [TestCase( "TestName_3")]
        [TestCase( "TestName_4")]
        [TestCase( "TestName_5")]
        public void CheckPlayerPrefsClear(string keyName)
        {
            Assert.IsFalse(PlayerPrefs.HasKey(keyName), $"{keyName} is not cleared!");
        }
    }
}
