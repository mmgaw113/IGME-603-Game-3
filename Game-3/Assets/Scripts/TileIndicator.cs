using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 rotSpeed;
    public float oscAmount;
    public float waveLength = 2f;
    public float amplitude = 2f;

    private Vector3 currentPos;
    private Vector3 oscDir;

    public GameObject blueAttackVFX;
    public GameObject redAttackVFX;
    void Start()
    {
        currentPos = transform.position;
        oscDir = new Vector3(0f, 1f, 0f);

        redAttackVFX.SetActive(false);
        blueAttackVFX.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotSpeed * Time.deltaTime);

        Vector3 targetPosition = currentPos + new Vector3(0f, 1f * oscAmount, 0f);
        float offset = Mathf.Sin(Time.time * waveLength) * amplitude;
        Vector3 osc = offset * oscDir;

        transform.position = targetPosition + osc;
    }

    public IEnumerator PlayAttackEffect(bool isPlayer1)
    {
        if (isPlayer1)
        {
            blueAttackVFX.SetActive(true);
            yield return new WaitForSeconds(1f);
            blueAttackVFX.SetActive(false);
        }
        else
        {
            redAttackVFX.SetActive(true);
            yield return new WaitForSeconds(1f);
            redAttackVFX.SetActive(false);
        }
    }
}
