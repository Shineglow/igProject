using System.Collections;
using Characters;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AccelerationModuleTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void AccelerationTest()
    {
        var accelerationModule = new AccelerationModule();
        var playerStats = Resources.Load<CharacterStats>("Scriptables/PlayerCharacterStats");
        var rightSpeed = 0f;
        var speedFromAccelerationModule = 0f;

        for (var i = 0; i < 10; i++)
        {
            var deltaTime = i/10f;
            speedFromAccelerationModule = accelerationModule.GetActualSpeed(Vector2.right, deltaTime, playerStats);
            
            var normalizedSpeed = Mathf.Abs(rightSpeed / playerStats.MaxSeed);
            rightSpeed += playerStats.AccelerationMax * playerStats.Acceleration.Evaluate(normalizedSpeed) * deltaTime;
        }
        
        if(Mathf.Abs(rightSpeed - speedFromAccelerationModule) < 0.001f)
            Assert.Pass();
        else
            Assert.Fail();
    }
}
