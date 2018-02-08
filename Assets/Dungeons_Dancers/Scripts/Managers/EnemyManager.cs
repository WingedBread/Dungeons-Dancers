using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class EnemyManager : MonoBehaviour {

    private GameManager gameManager;
    [SerializeField]
    private StaticTrapBehaviour[] staticTraps;
    [SerializeField]
    private ProjectileTrapBehaviour[] projectileTraps;
    [SerializeField]
    private MovingTrapBehaviour[] movingTraps;

    private List<StaticTrapBehaviour> event1StaticTrap, event2StaticTrap, event3StaticTrap;
    private List<ProjectileTrapBehaviour> event1ProjectileTrap, event2ProjectileTrap, event3ProjectileTrap;
    private List<MovingTrapBehaviour> event1MovingTrap, event2MovingTrap, event3MovingTrap;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        for (int i = 0; i <= staticTraps.Length-1; i++)
        {
            for (int x = 0; x <= projectileTraps.Length - 1; x++)
            {
                for (int y = 0; y <= movingTraps.Length - 1; y++)
                {
                    switch (staticTraps[i].GetTrapBehaviour())
                    {
                        case 1:
                            Koreographer.Instance.RegisterForEvents("Trap1Event", StaticTrap1BeatBehaviour);
                            event1StaticTrap.Add(staticTraps[i]);
                            event1ProjectileTrap.Add(projectileTraps[x]);
                            event1MovingTrap.Add(movingTraps[y]);
                            break;
                        case 2:
                            Koreographer.Instance.RegisterForEvents("Trap2Event", StaticTrap2BeatBehaviour);
                            event2StaticTrap.Add(staticTraps[i]);
                            event2ProjectileTrap.Add(projectileTraps[x]);
                            event2MovingTrap.Add(movingTraps[y]);
                            break;
                        case 3:
                            Koreographer.Instance.RegisterForEvents("Trap3Event", StaticTrap3BeatBehaviour);
                            event3StaticTrap.Add(staticTraps[i]);
                            event3ProjectileTrap.Add(projectileTraps[x]);
                            event3MovingTrap.Add(movingTraps[y]);
                            break;
                    }
                }
            }
        }
		
	}

    void StaticTrap1BeatBehaviour(KoreographyEvent kTrapEvent)
    {
        if (gameManager.GetGameStatus())
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
    }
    void StaticTrap2BeatBehaviour(KoreographyEvent kTrapEvent)
    {
        if (gameManager.GetGameStatus())
        {
            for (int i = 0; i <= event1StaticTrap.Count - 1; i++)
            {
                if (!event2StaticTrap[i].GetActiveTrapEvent()) event2StaticTrap[i].ActiveTrap();
                else event2StaticTrap[i].DisableTrap();
            }

            for (int x = 0; x <= event1ProjectileTrap.Count - 1; x++)
            {
                if (!event2ProjectileTrap[x].GetActiveTrapEvent()) event2ProjectileTrap[x].ActiveTrap();
                else event2ProjectileTrap[x].DisableTrap();
            }

            for (int y = 0; y <= event1MovingTrap.Count - 1; y++)
            {
                if (!event2MovingTrap[y].GetActiveTrapEvent()) event2MovingTrap[y].ActiveTrap();
                else event2MovingTrap[y].DisableTrap();
            }
        }
    }
    void StaticTrap3BeatBehaviour(KoreographyEvent kTrapEvent)
    {
        if (gameManager.GetGameStatus())
        {
            for (int i = 0; i <= event1StaticTrap.Count - 1; i++)
            {
                if (!event3StaticTrap[i].GetActiveTrapEvent()) event3StaticTrap[i].ActiveTrap();
                else event3StaticTrap[i].DisableTrap();
            }

            for (int x = 0; x <= event1ProjectileTrap.Count - 1; x++)
            {
                if (!event3ProjectileTrap[x].GetActiveTrapEvent()) event3ProjectileTrap[x].ActiveTrap();
                else event1ProjectileTrap[x].DisableTrap();
            }

            for (int y = 0; y <= event1MovingTrap.Count - 1; y++)
            {
                if (!event3MovingTrap[y].GetActiveTrapEvent()) event3MovingTrap[y].ActiveTrap();
                else event3MovingTrap[y].DisableTrap();
            }
        }
    }

}
