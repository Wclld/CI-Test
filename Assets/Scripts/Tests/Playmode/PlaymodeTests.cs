using System;
using System.Collections;

using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playmode
{
    public class PlaymodeTests
    {
        [UnityTest]
        public IEnumerator PlaymodeTestsWithEnumeratorPasses()
        {
            yield return new WaitForSeconds(5);

            Assert.Pass();
        }
    }
}
