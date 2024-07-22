using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserModels : BaseModel
{
    private DTOPlayer dtoPlayer;

    public override void OnFinishedWork()
    {
        EventBus.Execute("OnFinishedEvent");
    }
}
