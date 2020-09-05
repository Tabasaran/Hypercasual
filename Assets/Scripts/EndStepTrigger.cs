using UnityEngine;
using UnityEngine.UI;

public class EndStepTrigger : MonoBehaviour
{
    public Text score;

    private void Start()
    {
        score = GameObject.Find("Score").GetComponent<Text>();
    }
    private void OnTriggerEnter(Collider other)
    {
        EndStep();
        if (score != null)
            score.text = (int.Parse(score.text) + 1).ToString();
    }
    
    private void EndStep()
    {
        Vector3 nextStepPosition = transform.position + Vector3.down * 15;
        StepController.GetInstance().BeginStepTransition(nextStepPosition);
    }
}