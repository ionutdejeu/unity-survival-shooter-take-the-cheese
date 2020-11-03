using System.Collections.Generic;
using UnityEngine;


namespace Assets.Core.UI
{

    /// <summary>
    /// Abstract class of a menu page
    /// 
    /// </summary>
    public abstract class MainMenu : MonoBehaviour
    {
        /// <summary>
        /// Current opened MenuPage
        /// </summary>
        protected IMainMenuPage mCurrentPage;

        /// <summary>
        /// Stack tracking the navigation stack for this specific page
        /// </summary>
        protected Stack<IMainMenuPage> mPageStack = new Stack<IMainMenuPage>();


        protected virtual void ChangePage(IMainMenuPage newPage)
        {
            DeactivateCurrentPage();
            ActivateCurrentPage(newPage);
        }

        /// <summary>
        /// Deactivat the current page if there's one set 
        /// </summary>
        protected void DeactivateCurrentPage()
        {
            if(mCurrentPage != null)
            {
                mCurrentPage.Hide();
            }
        }

        /// <summary>
        /// Activate a new page and place it on the navigation stack 
        /// </summary>
        /// <param name="newPage">The page to be activated</param>
        protected void ActivateCurrentPage(IMainMenuPage newPage)
        {
            mCurrentPage = newPage;
            mCurrentPage.Show();
            mPageStack.Push(mCurrentPage);
        }
        /// <summary>
        /// Goes back to a certain page
        /// </summary>
        /// <param name="backPage"></param>
        protected void SafeBack(IMainMenuPage backPage)
        {
            DeactivateCurrentPage();
            ActivateCurrentPage(backPage);
        }

        /// <summary>
        /// Goes back if possible
        /// </summary>
        public virtual void Back()
        {
            if (mPageStack.Count == 0)
            {
                return;
            }
            DeactivateCurrentPage();
            mPageStack.Pop();
            ActivateCurrentPage(mPageStack.Pop());
        }


        /// <summary>
		/// Goes back to a specified page if possible
		/// </summary>
		/// <param name="backPage">Page to go back to</param>
		public virtual void Back(IMainMenuPage backPage)
        {
            int count = mPageStack.Count;
            if (count == 0)
            {
                SafeBack(backPage);
                return;
            }

            for (int i = count - 1; i >= 0; i--)
            {
                IMainMenuPage currentPage = mPageStack.Pop();
                if (currentPage == backPage)
                {
                    SafeBack(backPage);
                    return;
                }
            }

            SafeBack(backPage);
        }
    }
}
