using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;
using RAIN.BehaviorTrees;
using RAIN.Sensors;

public class wander : RAIN.Action.Action
{
	private int _previousPoint = 1;
	private RAIN.Path.RAINPathManager _path;
	private bool _getNext = true;
    public wander()
    {
        actionName = "wander";
    }

    public override RAIN.Action.Action.ActionResult Start(RAIN.Core.Agent agent, float deltaTime)
    {
		_path = (RAIN.Path.RAINPathManager)agent.PathManager;
        return RAIN.Action.Action.ActionResult.SUCCESS;
    }

    public override RAIN.Action.Action.ActionResult Execute(RAIN.Core.Agent agent, float deltaTime)
    {
		if(_getNext)
		{
			if(_previousPoint > 4)
				_previousPoint = 1;
			
			if(_path != null)
			{
				agent.LookTarget.TransformTarget = _path.waypointCollection.transform.FindChild (_previousPoint.ToString ()).transform;
			}
			
			_previousPoint = Random.Range (1,5);
			_getNext = false;
		}
		
		if(!agent.MoveTo (agent.LookTarget.TransformTarget.position, deltaTime))
		{
			return ActionResult.RUNNING;
		}
		else
		{
			_getNext = true;
		}
        return RAIN.Action.Action.ActionResult.SUCCESS;
    }

    public override RAIN.Action.Action.ActionResult Stop(RAIN.Core.Agent agent, float deltaTime)
    {
        return RAIN.Action.Action.ActionResult.SUCCESS;
    }
}