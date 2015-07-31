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
    using System.Collections.Generic;

    internal class JsObj
    {
        public List<string> Messages = new List<string>();

        public event EventHandler<ContinueEventArgs> OnContinue;

        public event EventHandler<ExceptionEventArgs> OnException; 

        public void PopupMsg(string msg, bool isContinue, string step)
        {
            Messages.Add(DateTime.Now.ToLocalTime() + "\t" + msg);

            if (isContinue)
            {
                if (OnContinue != null)
                {
                    OnContinue(this, new ContinueEventArgs(step));
                }
            }
            else
            {
                if (OnException != null)
                {
                    OnException(this, new ExceptionEventArgs(new Exception(msg)));
                }
            }


        }

        internal void ShowMessages()
        {
            using (var msgForm = new FrmMessages())
            {
                msgForm.Messages.AddRange(Messages);
                Messages.Clear();
                msgForm.ShowDialog();
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

    internal class ExceptionEventArgs : EventArgs
    {
        public Exception Ex { get; set; }

        public ExceptionEventArgs(Exception ex)
        {
            this.Ex = ex;
        }
    }
}
