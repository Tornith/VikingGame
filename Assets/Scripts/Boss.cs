using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI bossHealthText;
    public GameObject artifactRetrievedGUI;

    private Health _health;
    private Enemy.Enemy _enemy;
    private GameManager _gameManager;
    private NavMeshAgent _navMeshAgent;
    
    private void Start()
    {
        _health = GetComponent<Health>();
        _enemy = GetComponent<Enemy.Enemy>();
        _gameManager = FindObjectOfType<GameManager>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        if (!_navMeshAgent.enabled) return;
        if (_health.health <= 0) return;

        var distance = Vector3.Distance(player.transform.position, transform.position);
        bossHealthText.gameObject.SetActive(distance < _enemy.viewDistance);
        bossHealthText.SetText($"Boss Health: {_health.health}");
    }
    
    private void OnDestroy()
    {
        bossHealthText.gameObject.SetActive(false);
        _gameManager.BossDefeated();
        artifactRetrievedGUI.GetComponent<FadeInFadeOut>().Fade();
    }
}
