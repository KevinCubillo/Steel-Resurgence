using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField]
    List<Vector3> positions;

    [SerializeField]
    Vector3 currPosition;

    [SerializeField]
    Color NodeColor;

    private bool _gameStarted;

    // Start is called before the first frame update
    void Start()
    {
        _gameStarted = true;
        currPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (!_gameStarted && transform.hasChanged) {
            currPosition = transform.position;
        }

        for (int i = 0; i < positions.Count; i++) {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(positions[i] + currPosition, 0.5f);
            if (i < positions.Count - 1) {
                Gizmos.color = Color.grey;
                Gizmos.DrawLine(positions[i] + currPosition, positions[i + 1] + currPosition);
            }
        }
    }
}
