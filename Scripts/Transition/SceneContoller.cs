using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class SceneContoller : Singleton<SceneContoller>
{
    public GameObject playerPrefab;
    GameObject player;
    NavMeshAgent PlayerAgent;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void TransitonDestination(TransitonPoint transitonPoint)
    {
        switch(transitonPoint.transitionType)
        {
            case TransitonPoint.TransitionType.SameScene:
                StartCoroutine(Transition(SceneManager.GetActiveScene().name, transitonPoint.destinationTag));
                break;
            case TransitonPoint.TransitionType.DifferentScene:
                StartCoroutine(Transition(transitonPoint.sceneName, transitonPoint.destinationTag));
                break;
        }
    }

    IEnumerator Transition(string sceneName, TransitonDestination.DestinationTag destinationTag)
    {
        if(SceneManager.GetActiveScene().name != sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield return Instantiate(playerPrefab, GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
            yield break; 
        }
        else
        {
            player = GameManager.Instance.playerStats.gameObject;
            PlayerAgent = player.GetComponent<NavMeshAgent>();
            PlayerAgent.enabled = false;
            player.transform.SetPositionAndRotation(GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
            PlayerAgent.enabled = true;
            yield return null;
        }
    }

    private TransitonDestination GetDestination(TransitonDestination.DestinationTag destinationTag)
    {
        var entrances = FindObjectsOfType<TransitonDestination>();
        for (int i = 0; i < entrances.Length; i++)
        {
            if (entrances[i].destinationTag == destinationTag)
                return entrances[i];
        }

        return null;
    }
}
