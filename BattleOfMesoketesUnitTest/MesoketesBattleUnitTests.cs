using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BattleOfMesoketes;

namespace BattleOfMesoketesUnitTest
{
    [TestClass]
    public class MesoketesBattleUnitTests : IDisposable
    {
        private IMesoketesBattle _mesoketesBattle = null;
        private bool _disposed = false;

        public MesoketesBattleUnitTests()
        {
            _mesoketesBattle = new MesoketesBattle();
            _mesoketesBattle.InitializeSides();
        }

        [TestMethod]
        public void ValidateInput_TestForNoBattleEntered()
        {
            string input = string.Empty;
            string message = string.Empty;
                        
            bool result = _mesoketesBattle.ValidateInput(input, ref message);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateInput_TestForInvalidFightEntered()
        {
            string input = "T1-W";
            string message = string.Empty;

            bool result = _mesoketesBattle.ValidateInput(input, ref message);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateInput_TestForInvalidSideEntered()
        {
            string input = "T1-R-3";
            string message = string.Empty;

            bool result = _mesoketesBattle.ValidateInput(input, ref message);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateInput_TestForInvalidStrengthEntered()
        {
            string input = "T1-R-A";
            string message = string.Empty;

            bool result = _mesoketesBattle.ValidateInput(input, ref message);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetSuccessfulBattlesCount_SampleTest1()
        {
            string [] battlesOfDays = new string [Constants.NUMBEROFDAYS];

            battlesOfDays[0] = "T1-N-3;T2-S-2;T3-E-5;T4-W-4";
            battlesOfDays[1] = "T1-N-4;T2-S-2;T3-E-3";
            battlesOfDays[2] = "T1-N-2;T2-S-3";

            int successCount = _mesoketesBattle.GetSuccessfulBattlesCount(battlesOfDays);

            Assert.AreEqual<int>(6, successCount);
        }

        [TestMethod]
        public void GetSuccessfulBattlesCount_SampleTest2()
        {
            string [] battlesOfDays = new string [Constants.NUMBEROFDAYS];

            battlesOfDays [0] = "T1-N-3;T2-S-4;T3-W-2";
            battlesOfDays [1] = "T1-E-4;T2-N-3;T3-S-2";
            battlesOfDays [2] = "T1-W-3;T2-E-5;T3-N-2";

            int successCount = _mesoketesBattle.GetSuccessfulBattlesCount(battlesOfDays);

            Assert.AreEqual<int>(6, successCount);
        }

        [TestMethod]
        public void GetSuccessfulBattlesCount_SampleTest3()
        {
            string [] battlesOfDays = new string [Constants.NUMBEROFDAYS];

            battlesOfDays [0] = "T1-N-3;T2-S-4;T3-W-2";
            battlesOfDays [1] = "T1-N-3;T2-S-4;T3-W-2";
            battlesOfDays [2] = "T1-N-3;T2-S-4;T3-W-2";

            int successCount = _mesoketesBattle.GetSuccessfulBattlesCount(battlesOfDays);

            Assert.AreEqual<int>(3, successCount);
        }

        [TestMethod]
        public void IsValidSide_TestForValidSide()
        {
            bool result = _mesoketesBattle.IsValidSide("N");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidSide_TestForInValidSide()
        {
            bool result = _mesoketesBattle.IsValidSide("A");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetSide_TestForInstanceOfN()
        {
            var result = _mesoketesBattle.GetSide("N");

            Assert.IsInstanceOfType(result, typeof(N));
        }

        [TestMethod]
        public void GetSide_TestForInstanceOfS()
        {
            var result = _mesoketesBattle.GetSide("S");

            Assert.IsInstanceOfType(result, typeof(S));
        }

        [TestMethod]
        public void GetSide_TestForInstanceOfE()
        {
            var result = _mesoketesBattle.GetSide("E");

            Assert.IsInstanceOfType(result, typeof(E));
        }

        [TestMethod]
        public void GetSide_TestForInstanceOfW()
        {
            var result = _mesoketesBattle.GetSide("W");

            Assert.IsInstanceOfType(result, typeof(W));
        }

        [TestMethod]
        public void GetSide_TestForSideEmpty()
        {
            try
            {
                _mesoketesBattle.GetSide(string.Empty);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Side is null or empty");
            }
        }

        [TestMethod]
        public void GetSide_TestForInvalidSide()
        {
            try
            {
                _mesoketesBattle.GetSide("A");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Invalid Side");
            }
        }

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _mesoketesBattle = null;
                }

                _disposed = true;
            }
        }

        #endregion Dispose
    }
}
