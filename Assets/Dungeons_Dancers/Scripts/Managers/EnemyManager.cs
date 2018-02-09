using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [Header("Set Static Traps")]
    [SerializeField]
    private StaticTrapBehaviour[] staticTraps;
    [Header("Set Projectile Traps")]
    [SerializeField]
    private ProjectileTrapBehaviour[] projectileTraps;
    [Header("Set Moving Traps")]
    [SerializeField]
    private MovingTrapBehaviour[] movingTraps;

    private List<StaticTrapBehaviour> event1StaticTrap = new List<StaticTrapBehaviour>(), event2StaticTrap = new List<StaticTrapBehaviour>(), event3StaticTrap = new List<StaticTrapBehaviour>();
    private List<ProjectileTrapBehaviour> event1ProjectileTrap = new List<ProjectileTrapBehaviour>(), event2ProjectileTrap = new List<ProjectileTrapBehaviour>(), event3ProjectileTrap = new List<ProjectileTrapBehaviour>();
    private List<MovingTrapBehaviour> event1MovingTrap = new List<MovingTrapBehaviour>(), event2MovingTrap = new List<MovingTrapBehaviour>(), event3MovingTrap = new List<MovingTrapBehaviour>();

	// Use this for initialization
	void Start () {
        for (int i = 0; i <= staticTraps.Length - 1; i++)
        {
            switch (staticTraps[i].GetTrapBehaviour())
            {
                case 1:
                    event1StaticTrap.Add(staticTraps[i]);
                    break;
                case 2:
                    event2StaticTrap.Add(staticTraps[i]);
                    break;
                case 3:
                    event3StaticTrap.Add(staticTraps[i]);
                    break;
            }
        }
        for (int x = 0; x <= projectileTraps.Length - 1; x++)
        {
            switch (projectileTraps[x].GetTrapBehaviour())
            {
                case 1:
                    event1ProjectileTrap.Add(projectileTraps[x]);
                    break;
                case 2:
                    event2ProjectileTrap.Add(projectileTraps[x]);
                    break;
                case 3:
                    event3ProjectileTrap.Add(projectileTraps[x]);
                    break;
            }
        }
        for (int y = 0; y <= movingTraps.Length - 1; y++)
        {
            switch (movingTraps[y].GetTrapBehaviour())
            {
                case 1:
                    event1MovingTrap.Add(movingTraps[y]);
                    break;
                case 2:
                    event2MovingTrap.Add(movingTraps[y]);
                    break;
                case 3:
                    event3MovingTrap.Add(movingTraps[y]);
                    break;
            }
        }
	}

    public void TrapEvent1Behaviour()
    {
        for (int i = 0; i <= event1StaticTrap.Count - 1; i++)
        {
            if (!event1StaticTrap[i].GetActiveTrapEvent()) event1StaticTrap[i].ActiveTrap();
            else event1StaticTrap[i].DisableTrap();
        }

        for (int x = 0; x <= event1ProjectileTrap.Count - 1; x++)
        {
            if (!event1ProjectileTrap[x].GetActiveTrapEvent()) event1ProjectileTrap[x].ActiveTrap();
            else event1ProjectileTrap[x].DisableTrap();
        }

        for (int y = 0; y <= event1MovingTrap.Count - 1; y++)
        {
            if (!event1MovingTrap[y].GetActiveTrapEvent()) event1MovingTrap[y].ActiveTrap();
            else event1MovingTrap[y].DisableTrap();
        }
    }

    public void TrapEvent2Behaviour()
    {
        for (int i = 0; i <= event2StaticTrap.Count - 1; i++)
        {
            if (!event2StaticTrap[i].GetActiveTrapEvent()) event2StaticTrap[i].ActiveTrap();
            else event2StaticTrap[i].DisableTrap();
        }

        for (int x = 0; x <= event2ProjectileTrap.Count - 1; x++)
        {
            if (!event2ProjectileTrap[x].GetActiveTrapEvent()) event2ProjectileTrap[x].ActiveTrap();
            else event2ProjectileTrap[x].DisableTrap();
        }

        for (int y = 0; y <= event2MovingTrap.Count - 1; y++)
        {
            if (!event2MovingTrap[y].GetActiveTrapEvent()) event2MovingTrap[y].ActiveTrap();
            else event2MovingTrap[y].DisableTrap();
        }
    }

    public void TrapEvent3Behaviour()
    {
        for (int i = 0; i <= event3StaticTrap.Count - 1; i++)
        {
            if (!event3StaticTrap[i].GetActiveTrapEvent()) event3StaticTrap[i].ActiveTrap();
            else event3StaticTrap[i].DisableTrap();
        }

        for (int x = 0; x <= event3ProjectileTrap.Count - 1; x++)
        {
            if (!event3ProjectileTrap[x].GetActiveTrapEvent()) event3ProjectileTrap[x].ActiveTrap();
            else event3ProjectileTrap[x].DisableTrap();
        }

        for (int y = 0; y <= event3MovingTrap.Count - 1; y++)
        {
            if (!event3MovingTrap[y].GetActiveTrapEvent()) event3MovingTrap[y].ActiveTrap();
            else event3MovingTrap[y].DisableTrap();
        }
    }
}
