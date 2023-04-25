using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float lifeTime = 0.6f;
    public float minDist = 2f;
    public float maxDist = 3f;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        float direction = Random.rotation.eulerAngles.z;
        initialPosition = transform.position;
        float dist = Random.Range(minDist, maxDist);
        targetPosition = initialPosition + (Quaternion.Euler(0, 0, direction) * new Vector3(dist, dist, 0f));
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float fraction = lifeTime / 2f;

        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
        else if(timer > fraction)
        {
            textMesh.color = Color.Lerp(textMesh.color, Color.clear, (timer - fraction) / (lifeTime - fraction));
        }

        transform.position = Vector3.Lerp(initialPosition, targetPosition, Mathf.Sin(timer / lifeTime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / lifeTime));
    }

    public void SetDamageText(float damage)
    {
        textMesh.text = ((int)damage).ToString();
    }
}

// https://www.youtube.com/watch?v=I2j6mQpCrWE
