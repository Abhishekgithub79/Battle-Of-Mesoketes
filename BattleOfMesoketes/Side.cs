using System;

namespace BattleOfMesoketes
{
    public class Side
    {
        public int Height { get; set; }

        public int TempHeight { get; set; }

        public bool SideAffected { get; set; }
    }

    /// <summary>
    /// North Side
    /// </summary>
    public class N : Side
    { }

    /// <summary>
    /// East Side
    /// </summary>
    public class E : Side
    { }

    /// <summary>
    /// West Side
    /// </summary>
    public class W : Side
    { }

    /// <summary>
    /// South Side
    /// </summary>
    public class S : Side
    { }
}
