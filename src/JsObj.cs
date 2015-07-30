// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsObj.cs" company="Jarrey, jar_bob@163.com">
//   Copyright © Jarrey, jar_bob@163.com
// </copyright>
// <summary>
//   Defines the JsObj type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SubmitSys
{
    using System;

    internal class JsObj
    {
        public event EventHandler<ContinueEventArgs> OnContinue;

        public void PopupMsg(string msg, bool isContinue, string step)
        {
            //MessageBox.Show(msg, Resources.InfoTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (OnContinue != null && isContinue)
            {
                OnContinue(this, new ContinueEventArgs(step));
            }
        }
    }

    internal class ContinueEventArgs : EventArgs
    {
        public StepStatus Step { get; set; }

        public ContinueEventArgs(string step)
        {
            var result = StepStatus.Init;
            Enum.TryParse(step, true, out result);
            this.Step = result;
        }
    }
}
