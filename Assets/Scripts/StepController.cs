using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;


public class StepController : Singleton<StepController>
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<Step> steps;
    [SerializeField] private float totalStepTransitionTime;

    private int currentStepId;
    private Vector3 PlayerStartPosition;
    private float currentStepTransitionTime;
    private bool isTransitioning;
    private Step prevStep;

    private void Update()
    {
        CheckBallDistance();
    }

    private void FixedUpdate()
    {
        if (isTransitioning)
        {
            currentStepTransitionTime += Time.fixedDeltaTime;
            UpdateStepTransition();
            if (currentStepTransitionTime >= totalStepTransitionTime)
            {
                FinishStepTransition();
            }
        }
    }

    private void CheckBallDistance()
    {
        if (isTransitioning) return;
        float distance = Vector3.Distance(player.transform.position, steps[currentStepId].transform.position);
        if (distance >= steps[currentStepId].MaxBallDistance)
        {
            RestartScene();
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private Step ActivateNextStep(Vector3 nextStepPosition)
    {
        prevStep = steps[currentStepId];
        currentStepId = (currentStepId + 1) % steps.Count;
        steps[currentStepId].Activate(nextStepPosition);
        return steps[currentStepId];
    }

    public void BeginStepTransition(Vector3 nextStepPosition)
    {
        Step nextStep = ActivateNextStep(nextStepPosition);
        StepRotator.GetInstance().SetNewStep(nextStep.gameObject);
        StepRotator.GetInstance().IsRotationEnabled = false;

        PlayerStartPosition = player.transform.position;
        currentStepTransitionTime = 0;
        isTransitioning = true;
    }

    private void UpdateStepTransition()
    {
        player.transform.position = Vector3.Lerp(PlayerStartPosition,
            steps[currentStepId].PlayerPlacingPoint.transform.position,
            currentStepTransitionTime / totalStepTransitionTime);
    }

    private void FinishStepTransition()
    {
        isTransitioning = false;
        prevStep.Deactivate();
        StepRotator.GetInstance().IsRotationEnabled = true;
    }
}
