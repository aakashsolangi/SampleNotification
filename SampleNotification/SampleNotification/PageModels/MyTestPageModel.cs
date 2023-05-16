using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleNotification.PageModels
{
    public class MyTestPageModel : FreshBasePageModel
    {
        #region Private Properties

        public EventHandler OnSaved;

        #endregion Private Properties

        #region Constructor

        public MyTestPageModel() : base()
        {

        }

        #endregion Constructor

        #region Override Methods

        public override async void Init(object data)
        {
            base.Init(data);
        }

        public override async void ReverseInit(object returnedData)
        {

            base.ReverseInit(returnedData);
        }


        #endregion Public Properties

    }
}
