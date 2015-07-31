// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StepStatus.cs" company="Jarrey, jar_bob@163.com">
//   Copyright © Jarrey, jar_bob@163.com
// </copyright>
// <summary>
//   Defines the StepStatus type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SubmitSys
{
    /// <summary> The step status. </summary>
    public enum StepStatus
    {
        Init,

        OpenDocTabForNew,
        ClickNewDoc,
        OpenDocTabForModify,
        ClickModifyDoc,

        OpenDocTabForBMod,
        ClickBMod,

        OpenDocTabForWifeCheck,
        ClickWifeCheck,

        OpenDocTabForWifeBasicInfo,
        ClickWifeBasicInfo,

        OpenDocTabForWifeClinical,
        ClickWifeClinical,

        /* --------------------------------------- */

        OpenDocTabForHusCheck,
        ClickHusCheck,

        OpenDocTabForHusBasicInfo,
        ClickHusBasicInfo
    }
}
