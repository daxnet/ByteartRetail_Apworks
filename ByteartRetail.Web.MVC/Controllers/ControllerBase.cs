using System;
using System.Web.Mvc;
using System.Web.Security;

namespace ByteartRetail.Web.MVC.Controllers
{
    /// <summary>
    /// 表示“控制器”Controller类型的基类型，所有Byteart Retail项目下的Controller都
    /// 应该继承于此基类型。
    /// </summary>
    public abstract class ControllerBase : Controller
    {
        #region Private Constants
        private const string SuccessPageAction = "SuccessPage";
        private const string SuccessPageController = "Home";
        #endregion

        #region Protected Properties
        /// <summary>
        /// 获取当前登录用户的ID值。
        /// </summary>
        protected Guid UserID
        {
            get
            {
                if (Session["UserID"] != null)
                    return (Guid)Session["UserID"];
                var membershipUser = Membership.GetUser();
                if (membershipUser != null)
                {
                    if (membershipUser.ProviderUserKey != null)
                    {
                        var id = new Guid(membershipUser.ProviderUserKey.ToString());
                        Session["UserID"] = id;
                        return id;
                    }
                }
                return Guid.Empty;
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// 将页面重定向到成功页面。
        /// </summary>
        /// <param name="pageTitle">需要在成功页面显示的成功信息。</param>
        /// <param name="action">成功信息显示后返回的Action名称。默认值：Index。</param>
        /// <param name="controller">成功信息显示后返回的Controller名称。默认值：Home。</param>
        /// <param name="waitSeconds">在成功页面停留的时间（秒）。默认值：3。</param>
        /// <returns>执行的Action Result。</returns>
        protected ActionResult RedirectToSuccess(string pageTitle, string action = "Index", string controller = "Home", int waitSeconds = 3)
        {
            return RedirectToAction(SuccessPageAction, SuccessPageController, new { pageTitle = pageTitle, retAction = action, retController = controller, waitSeconds = waitSeconds });
        }

        #endregion
    }
}