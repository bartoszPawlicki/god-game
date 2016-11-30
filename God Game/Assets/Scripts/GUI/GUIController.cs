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

        _player1Skills = GameObject.Find("Skills Player1").GetComponent<SkillsController>();
        _player2Skills = GameObject.Find("Skills Player2").GetComponent<SkillsController>();
        _godSkills = GameObject.Find("skills_god").GetComponent<GodSkillsController>();

        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        _gameTime = gameController.GetComponent<TimeManager>();
        _gameTimeText = GameObject.Find("Game Timer").GetComponent<Text>();

        RoundManager roundManager = gameController.GetComponent<RoundManager>();
        roundManager.OnLastRoundEnded += RoundManager_OnLastRoundEnded;
        roundManager.OnNewRoundStarted += RoundManager_OnNewRoundStarted;

        _roundText = GameObject.Find("Round").GetComponent<Text>();
        _roundText.gameObject.SetActive(false);
    }

    //Temporarty Start, I hope that Adrian will create powerfull control for text messages so ...
    private void RoundManager_OnNewRoundStarted(object sender, short roundNumber)
    {
        _isRoundTextAsctive = true;
        _roundText.text = "Round " + roundNumber;
        var timer = new Timer(1500);
        timer.Elapsed += GUIController_Elapsed;
        timer.Start();
    }

    private void GUIController_Elapsed(object sender, ElapsedEventArgs e)
    {
        _isRoundTextAsctive = false;
    }

    
    private void RoundManager_OnLastRoundEnded(object sender, System.EventArgs e)
    {
        _isRoundTextAsctive = true;
        _roundText.text = "Game Over";
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

        _gameTimeText.text = string.Format("{0:00}:{1:00}:{2:00}", _gameTime.TimeLeft.Minutes, _gameTime.TimeLeft.Seconds, _gameTime.TimeLeft.Milliseconds / 10);

        _roundText.gameObject.SetActive(_isRoundTextAsctive);
    }

    private PlayerController _player1;
    private PlayerController _player2;
    private GodController _god;

    private SkillsController _player1Skills;
    private SkillsController _player2Skills;
    private GodSkillsController _godSkills;

    private bool _isRoundTextAsctive = true;
    private Text _roundText;
    private Text _gameTimeText;
    private TimeManager _gameTime;
}
