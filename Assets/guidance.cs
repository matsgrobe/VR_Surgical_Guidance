using UnityEngine;
using TMPro;

public class SurgicalGuidance : MonoBehaviour
{
    [Header("Tracking Points")]
    public Transform scalpelTip;
    public Transform[] allTumors; 
    
    [Header("UI & Visuals")]
    public TextMeshProUGUI distanceHUD;
    public LineRenderer guidanceLine;
    
    [Header("Settings")]
    public float successThreshold = 0.01f; 
    public float timeToReach = 0.5f;
    
    private int currentTargetIndex = 0;
    private bool allTargetsReached = false;
    private float dwellTimer = 0f;

void Update()
    {
        if (allTargetsReached) return;

        // EMERGENCY OVERRIDE
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            AdvanceTarget();
        }

        if (scalpelTip == null || allTumors == null || allTumors.Length == 0) return;

        Transform activeTarget = allTumors[currentTargetIndex];
        
        float distance = Vector3.Distance(scalpelTip.position, activeTarget.position);
        float distCm = distance * 100f;


        if (Time.frameCount % 30 == 0) {
            Debug.Log($"Logic: {distance} | Threshold: {successThreshold} | Target: {currentTargetIndex}");
        }

        distanceHUD.color = Color.black; 
        string progressStr = "";

        if (distance <= successThreshold) {
            dwellTimer += Time.deltaTime;
            float percent = Mathf.Clamp((dwellTimer / timeToReach) * 100f, 0f, 100f);
            progressStr = $"\n<color=green>HOLDING: {percent:F0}%</color>";

            if (dwellTimer >= timeToReach) {
                AdvanceTarget();
            }
        } else {
            dwellTimer = 0f; 
            progressStr = "\n<color=red>Searching...</color>";
        }

        distanceHUD.text = $"TARGET: {currentTargetIndex + 1}/3\nDIST: {distCm:F1} cm{progressStr}";

        if (guidanceLine != null) {
            guidanceLine.positionCount = 2;
            guidanceLine.SetPosition(0, scalpelTip.position);
            guidanceLine.SetPosition(1, activeTarget.position);
        }
    }

    void AdvanceTarget() {
        // Turn the current sphere green
        Renderer rend = allTumors[currentTargetIndex].GetComponent<Renderer>();
        if (rend != null) rend.material.color = Color.green;

        if (currentTargetIndex < allTumors.Length - 1) {
            currentTargetIndex++;
            dwellTimer = 0f;
            Debug.Log("Switched to Target " + (currentTargetIndex + 1));
        } else {
            allTargetsReached = true;
            distanceHUD.text = "<color=green>SURGERY COMPLETE!</color>";
            if (guidanceLine != null) guidanceLine.enabled = false;
        }
    }
}