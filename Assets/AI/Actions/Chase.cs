using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

public class Chase : RAIN.Action.Action
{
	private UnityEngine.GameObject _goHero = null;
	int count = 0;
    public Chase()
    {
        actionName = "Chase";
    }

    public override RAIN.Action.Action.ActionResult Start(RAIN.Core.Agent agent, float deltaTime)
    {
		Debug.Log ("I am wandering");
		if(actionContext.ContextItemExists ("playerLoc") && actionContext.ContextItemExists ("canSee"))
		{
			_goHero = actionContext.GetContextItem<GameObject>("playerLoc");
			actionContext.SetContextItem ("canSee", 1);
		}
        return RAIN.Action.Action.ActionResult.SUCCESS;
    }

    public override RAIN.Action.Action.ActionResult Execute(RAIN.Core.Agent agent, float deltaTime)
    {
		if(_goHero != null)
		{
			agent.LookAt (_goHero.transform.position, deltaTime);
			agent.MoveTarget.VectorTarget = _goHero.transform.position;
			if(!agent.Move (deltaTime))
			{
				RAIN.Sensors.RaycastSensor s = (RAIN.Sensors.RaycastSensor)agent.GetSensor ("Sensor");
				if(!s.CanSee (_goHero))
				{
					if(actionContext.ContextItemExists ("canSee"))
					{
						actionContext.SetContextItem ("canSee", 0);
						agent.Mind.CancelInvoke ("Move");
						agent.Mind.Reset ();
						return RAIN.Action.Action.ActionResult.SUCCESS;
					}
				}
				return ActionResult.RUNNING;
			}
		}
        return RAIN.Action.Action.ActionResult.SUCCESS;
    }

    public override RAIN.Action.Action.ActionResult Stop(RAIN.Core.Agent agent, float deltaTime)
    {
        return RAIN.Action.Action.ActionResult.SUCCESS;
    }
}