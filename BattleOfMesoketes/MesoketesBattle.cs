using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfMesoketes
{
    /// <summary>
    /// 
    /// </summary>
    public class MesoketesBattle : IMesoketesBattle
    {
        private Dictionary<string, Side> Sides = null;
        private int SuccessCount = 0;
        private bool _disposed = false;

        /// <summary>
        /// Validates the entered battles input details
        /// Handles cases : 1. battle's, 2. Invalid Side's, 3. Invalid Strength's
        /// </summary>
        /// <param name="input"></param>
        /// <param name="message"></param>
        /// <returns>Valid/Invalid</returns>
        public bool ValidateInput(string input, ref string message)
        {
            bool result = false;

            try
            {
                string [] battles = input.Split(new char [] { Constants.BATTLEDELIMETER }, StringSplitOptions.None);

                if (battles.Length <= 0)
                {
                    message = "No battles entered";
                    return result;
                }

                for (int i = 0; i < battles.Length; i++)
                {
                    string [] fight = battles [i].Split(new char [] { Constants.FIGHTDELIMETER }, StringSplitOptions.None);

                    if (fight.Length != 3)
                    {
                        message = string.Format("battle: {0} is not an Valid format. (Sample format -> T2-S-4)", (i + 1));
                        return result;
                    }
                    if (!IsValidSide(fight [1]))
                    {
                        message = string.Format("battle: {0}, Side: {1} is InValid. (Valid Sides -> N,S,W,E)", (i + 1), fight [1]);
                        return result;
                    }
                    try
                    {
                        Convert.ToInt32(fight [2]);
                    }
                    catch (Exception)
                    {
                        message = string.Format("battle: {0}, Strength: {1} is Invalid. (Strength should be an integer like 5,6)", (i + 1), fight [2]);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return result;
            }

            return true;
        }

        /// <summary>
        /// Initiaze Sides hashtable
        /// </summary>
        public void InitializeSides()
        {
            Sides = new Dictionary<string, Side>();

            Sides.Add("N", new N());
            Sides.Add("E", new E());
            Sides.Add("W", new W());
            Sides.Add("S", new S());
        }

        /// <summary>
        /// Get the successful battle won from the battles of days supplied
        /// </summary>
        /// <param name="battlesOfDays"></param>
        /// <returns>Successful battle count</returns>
        public int GetSuccessfulBattlesCount(string [] battlesOfDays)
        {
            foreach (string item in battlesOfDays)
            {
                string [] battles = item.Split(new char [] { Constants.BATTLEDELIMETER }, StringSplitOptions.None);

                foreach (string battle in battles)
                {
                    string [] fight = battle.Split(new char [] { Constants.FIGHTDELIMETER }, StringSplitOptions.None);

                    BattleResult(fight [1], Convert.ToInt32(fight [2]));
                }

                UpdateMinHeightInSides();
            }

            return SuccessCount;
        }

        /// <summary>
        /// Check for the input side is Valid
        /// </summary>
        /// <param name="side"></param>
        /// <returns>Exists or not</returns>
        public bool IsValidSide(string side)
        {
            return Sides.ContainsKey(side.ToUpper());
        }

        /// <summary>
        /// Get the Side object if valid and not empty
        /// </summary>
        /// <param name="side"></param>
        /// <returns>Returns object of T type</returns>
        public Side GetSide(string side)
        {
            #region defensive

            if (string.IsNullOrEmpty(side))
                throw new Exception("Side is null or empty");

            #endregion defensive

            if (!IsValidSide(side))
                throw new Exception("Invalid Side");

            return Sides [side.ToUpper()];
        }

        /// <summary>
        /// Compute the battle result and correspondingly updates battle successCount and the height of resp Side
        /// </summary>
        /// <param name="side"></param>
        /// <param name="strength"></param>
        public void BattleResult(string side, int strength)
        {
            Side t = GetSide(side);

            if (t != null)
            {
                if (t.Height >= strength)
                {
                    t.Height -= strength;
                }
                else
                {
                    t.Height = 0;
                    SuccessCount += 1;
                }

                t.SideAffected = true;
                t.TempHeight += strength;

                UpdateSide(t, side);
            }
        }

        /// <summary>
        /// Updates the corresponding side with new height which needs to be modified
        /// </summary>
        /// <param name="t"></param>
        /// <param name="side"></param>
        public void UpdateSide(Side t, string side)
        {
            if (IsValidSide(side))
            {
                Sides [side] = t;
            }
        }

        /// <summary>
        /// Updates the height to new minimal height.
        /// </summary>
        public void UpdateMinHeightInSides()
        {
            foreach (KeyValuePair<string, Side> side in Sides)
            {
                if (side.Value.SideAffected)
                {
                    side.Value.Height = side.Value.TempHeight;
                    side.Value.TempHeight = 0;
                    side.Value.SideAffected = false;
                }
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
                    Sides = null;
                }

                _disposed = true;
            }
        }

        #endregion Dispose
    }
}
