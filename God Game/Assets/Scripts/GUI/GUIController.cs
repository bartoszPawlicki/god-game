using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Timers;

public class GUIController : MonoBehaviour
{
    public ProgressBar progressBar;
    public static ProgressBar pBar;
    // Use this for initialization
    void Awake()
    {
        pBar = progressBar;
    }
    // Use this for initialization
    void Start ()
    {
        _player1 = GameObject.Find("Player1").GetComponent<PlayerController>();
        _player2 = GameObject.Find("Player2").GetComponent<PlayerController>();
        _god = GameObject.Find("God").GetComponent<GodController>();
        _totemActivator = GameObject.FindGameObjectWithTag("Totem").GetComponent<TotemActivator>();

        _player1Skills = GameObject.Find("Skills Player1").GetComponent<SkillsController>();
        _player2Skills = GameObject.Find("Skills Player2").GetComponent<SkillsController>();
        _godSkills = GameObject.Find("skills_god").GetComponent<GodSkillsController>();

        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");

        RoundManager roundManager = gameController.GetComponent<RoundManager>();
        roundManager.OnLastRoundEnded += RoundManager_OnLastRoundEnded;
        roundManager.OnNewRoundStarted += RoundManager_OnNewRoundStarted;

        _roundText = GameObject.Find("Round").GetComponent<Text>();
        _roundText.gameObject.SetActive(false);

        _totemActivator.OnTotemCapured += TotemActivator_OnTotemCaptured;

        _god.OnThunderSkillChosen += _god_OnThunderSkillChosen;
        _god.OnWaterGeyserSkillChosen += _god_OnWaterGeyserSkillChosen;
        _god.OnGlobalWindSKillChosen += _god_OnGlobalWindSKillChosen;

        _cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();

        _godPride = GameObject.FindGameObjectWithTag("God").GetComponent<GodPride>();
    }

    private void _god_OnThunderSkillChosen(object sender, System.EventArgs e)
    {
        _godSkills.ThunderSkill.SetSelected(true);
        _godSkills.WaterGeyserSkill.SetSelected(false);
        _godSkills.GlobalWindSkill.SetSelected(false);
    }
    private void _god_OnWaterGeyserSkillChosen(object sender, System.EventArgs e)
    {
        _godSkills.ThunderSkill.SetSelected(false);
        _godSkills.WaterGeyserSkill.SetSelected(true);
        _godSkills.GlobalWindSkill.SetSelected(false);
    }
    private void _god_OnGlobalWindSKillChosen(object sender, System.EventArgs e)
    {
        _godSkills.ThunderSkill.SetSelected(false);
        _godSkills.WaterGeyserSkill.SetSelected(false);
        _godSkills.GlobalWindSkill.SetSelected(true);
    }

    //Temporarty Start, I hope that Adrian will create powerfull control for text messages so ...

    private void RoundManager_OnNewRoundStarted(object sender, short roundNumber)
    {
        _isRoundTextAsctive = true;
        _roundText.text = "Round " + roundNumber;
        var timer = new Timer(1500) { AutoReset = false };
        timer.Elapsed += GUIController_Elapsed;
        timer.Start();
    }

    private void TotemActivator_OnTotemCaptured(object sender, System.EventArgs e)
    {
        _isRoundTextAsctive = true;
        _roundText.text = "Totem has been captured";
        Debug.Log("Totem captured trigger");
    }
    private void GUIController_Elapsed(object sender, ElapsedEventArgs e)
    {
        _isRoundTextAsctive = false;
    }
    
    private void RoundManager_OnLastRoundEnded(object sender, System.EventArgs e)
    {
        _isRoundTextAsctive = true;
        _roundText.text = "Game Over \n";
        if (_godPride.godPride > 0)
            _roundText.text += "God Wins!";
        else
            _roundText.text += "Players Wins!";

    }
    //Temporary End
    void OnGUI()
    {
        _player1Skills.ThrowCooldown = _player1.ThrowCooldownValue;
        _player1Skills.RopeCooldown = _player1.RopeCooldownValue;
        _player2Skills.ThrowCooldown = _player2.ThrowCooldownValue;
        _player2Skills.RopeCooldown = _player2.RopeCooldownValue;

        _godSkills.ThunderCooldown = _god.ThunderCooldownValue;
        _godSkills.WaterGeyserCooldown = _god.WaterGeyserCooldownValue;
        _godSkills.GlobalWindCooldown = _god.GlobalWindCooldownValue;

        _roundText.gameObject.SetActive(_isRoundTextAsctive);
    }

    private PlayerController _player1;
    private PlayerController _player2;
    private GodController _god;

    private SkillsController _player1Skills;
    private SkillsController _player2Skills;
    private GodSkillsController _godSkills;

    private bool _isRoundTextAsctive;
    private Text _roundText;
    
    private CameraController _cameraController;
    private GodPride _godPride;
    private TotemActivator _totemActivator;
}
