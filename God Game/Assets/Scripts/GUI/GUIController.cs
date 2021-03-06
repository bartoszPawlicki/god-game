﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Timers;
using Assets.Scripts;
using System;

public class GUIController : MonoBehaviour
{
    public ProgressBar progressBar;
    public GameObject mainMenu;
    public static ProgressBar pBar;

    // Use this for initialization
    void Awake()
    {
        pBar = progressBar;
    }
    void Update()
    {
        if(Input.GetButtonDown("Menu"))
        {
            mainMenu.SetActive(!mainMenu.activeSelf);
        }
    }
    // Use this for initialization
    void Start ()
    {
        _player1 = GameObject.Find("Player1").GetComponent<PlayerController>();
        _player1Bullet = GameObject.Find("Player1").GetComponent<BulletControler>();
        _player2 = GameObject.Find("Player2").GetComponent<PlayerController>();
        _player2Bullet = GameObject.Find("Player2").GetComponent<BulletControler>();

        _god = GameObject.Find("God").GetComponent<GodController>();
        _totemActivator = GameObject.FindGameObjectWithTag("Totem").GetComponent<TotemActivator>();

        _player1Skills = GameObject.Find("Skills Player1").GetComponent<SkillsController>();
        _player2Skills = GameObject.Find("Skills Player2").GetComponent<SkillsController>();
        _godSkills = GameObject.Find("skills_god").GetComponent<GodSkillsController>();

        GameController gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gameController.OnGameEnd += gameController_OnGameEnd;

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

        //_godPride = GameObject.FindGameObjectWithTag("GroundGod").GetComponent<GodPride>();

        _player1Text = GameObject.Find("HP Player 1").GetComponent<Text>();
        _player2Text = GameObject.Find("HP Player 2").GetComponent<Text>();

        _gameEnd = transform.FindChild("GameEnd").GetComponent<Text>();
    }

    private void gameController_OnGameEnd(object sender, Winner w)
    {
        _gameEnd.text = w.ToString() + " Win !!!";
        
        mainMenu.SetActive(true);
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
        int time;
        if (roundNumber == 1)
            time = 2500;
        else
            time = 1500;
        var timer = new Timer(time) { AutoReset = false };
        timer.Elapsed += GUIController_Elapsed;
        timer.Start();
    }

    private void TotemActivator_OnTotemCaptured(object sender, System.EventArgs e)
    {
        _isRoundTextAsctive = true;
        _roundText.text = "Totem has been captured";

        var timer = new Timer(1500) { AutoReset = false };
        timer.Elapsed += GUIController_Elapsed; ;
        timer.Start();
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
        _player1Skills.SlingshotCooldown = _player1Bullet.ReloadValue;
        _player1Skills.SprintCooldown = _player1.SprintCooldownValue;

        _player2Skills.ThrowCooldown = _player2.ThrowCooldownValue;
        _player2Skills.RopeCooldown = _player2.RopeCooldownValue;
        _player2Skills.SlingshotCooldown = _player2Bullet.ReloadValue;
        _player2Skills.SprintCooldown = _player2.SprintCooldownValue;

        _godSkills.ThunderCooldown = _god.ThunderCooldownValue;
        _godSkills.WaterGeyserCooldown = _god.WaterGeyserCooldownValue;
        _godSkills.GlobalWindCooldown = _god.GlobalWindCooldownValue;

        _roundText.gameObject.SetActive(_isRoundTextAsctive);

        _player1Text.text = _player1.HP.ToString();
        _player2Text.text = _player2.HP.ToString();
    }
    private PlayerController _player1;
    private PlayerController _player2;
    private GodController _god;

    private SkillsController _player1Skills;
    private SkillsController _player2Skills;
    private GodSkillsController _godSkills;
    private BulletControler _ammo1;
    private BulletControler _ammo2;

    private bool _isRoundTextAsctive;
    private Text _roundText;
    
    private CameraController _cameraController;
    private GodPride _godPride;
    private TotemActivator _totemActivator;

    private BulletControler _player1Bullet;
    private BulletControler _player2Bullet;

    private Text _player1Text;
    private Text _player2Text;

    private Text _gameEnd;
}
