namespace Darts.Core.Tests
{
    internal class PlayerTest
    {
        [Test]
        public void PlayerThrows_ValidThrow_DecreasesTempScore()
        {
            Player dummy = new Player("Player", 50);

            Throw dartThrow = new Throw(20, 2);


            dummy.PlayerThrows(dartThrow);

            Assert.That(dummy.tempScore, Is.EqualTo(10));

        }
        [Test]
        public void PlayerThrows_InValidThrow_RaisesTOOMUCHFLAG()
        {
            Player dummy = new Player("Player", 10);

            Throw dartThrow = new Throw(20, 2);


            dummy.PlayerThrows(dartThrow);

            Assert.That(dummy.tooMuchFlag, Is.True);

        }
    }
}

