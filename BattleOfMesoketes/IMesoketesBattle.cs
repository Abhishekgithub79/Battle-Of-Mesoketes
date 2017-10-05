using System;

namespace BattleOfMesoketes
{
    public interface IMesoketesBattle : IDisposable
    {
        bool ValidateInput(string input, ref string message);
        void InitializeSides();
        int GetSuccessfulBattlesCount(string [] battlesOfDays);
        bool IsValidSide(string side);
        Side GetSide(string side);
        void BattleResult(string side, int strength);
        void UpdateSide(Side t, string side);
        void UpdateMinHeightInSides();
    }
}