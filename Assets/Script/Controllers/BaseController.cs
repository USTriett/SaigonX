using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController
{
    public abstract void SendView(BaseView view);
    public abstract void HandleEvent();
}
