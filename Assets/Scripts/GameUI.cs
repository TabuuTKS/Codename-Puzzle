using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [Header("Stamina Bar")]
    [SerializeField] Slider StaminaSlider;
    [SerializeField] Image StaminaSliderFill;
    [SerializeField] Color StaminaColorHigh;
    [SerializeField] Color StaminaColorLow;

    //private
    private const float RunDuration = 5f;
    private const float RunResetDelay = 700f;
    private float RunTimerElapsed = 0f;

    #region Singleton
    public static GameUI instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    private void Start()
    {
        StaminaSliderFill.color = StaminaColorHigh;
    }

    private void Update()
    {
        StaminaSlider.value = PlayerPrefs.Stamina;

        if (PlayerPrefs.Stamina < 0.3)
        {
            StaminaSliderFill.color = StaminaColorLow;
        }
        else
        {
            StaminaSliderFill.color = StaminaColorHigh;
        }
    }

    public void StaminaBar()
    {
        RunTimerElapsed = RunDuration;
        RunTimerElapsed -= Time.deltaTime;
        PlayerPrefs.Stamina -= (Time.deltaTime/10);
        if (RunTimerElapsed == 0f)
        {
            player.canRun = false;
        }
    }

    public IEnumerator ResetStamina()
    {
        yield return new WaitForSeconds(.5f);
        float elapsedTime = 0f;
        while (elapsedTime < RunResetDelay)
        {
            elapsedTime += Time.deltaTime;
            PlayerPrefs.Stamina = Mathf.Lerp(PlayerPrefs.Stamina, 1f, elapsedTime / RunResetDelay);
            yield return null;

        }
        PlayerPrefs.Stamina = 1f;
        player.canRun = true;
    }
}
