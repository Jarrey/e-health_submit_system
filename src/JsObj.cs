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

    using CefSharp;

    using SubmitSys.DAL;

    internal class JsObj
    {
        public List<string> Messages = new List<string>();

        public event EventHandler<ContinueEventArgs> OnContinue;

        public event EventHandler<ExceptionEventArgs> OnException;

        public event EventHandler<MessageEventArgs> OnLogin;

        public void Login(string msg, string accountType)
        {
            if (OnLogin != null)
            {
                OnLogin(this, new MessageEventArgs(msg, accountType));
            }
        }

        public void Continue(string parameter)
        {
            if (OnContinue != null)
            {
                OnContinue(this, new ContinueEventArgs("init", parameter));
            }
        }

        public void WriteBack(string id, string tableType)
        {
            using (var db = new DBUpdateFlag())
            {
                db.UpdateFlag(id, tableType);
            }
        }

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

        public string Parameter { get; set; }

        public ContinueEventArgs(string step)
        {
            StepStatus result;
            Enum.TryParse(step, true, out result);
            this.Step = result;
        }

        public ContinueEventArgs(string step, string parameter)
        {
            StepStatus result;
            Enum.TryParse(step, true, out result);
            this.Step = result;
            this.Parameter = parameter;
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

    internal class MessageEventArgs : EventArgs
    {
        public string Message { get; set; }

        public AccountTypes Account { get; set; }

        public MessageEventArgs(string msg, string type)
        {
            this.Message = msg;
            AccountTypes result;
            Enum.TryParse(type, true, out result);
            this.Account = result;
        }
    }
}
