﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinFormsMvp;
namespace Dianzhu.CSClient.Presenters
{
   public  class MainViewPresenter:Presenter<Views.ViewsContracts.IMainView>
    {
       public MainViewPresenter(Views.ViewsContracts.IMainView view)
           : base(view)
       { }
    }
}