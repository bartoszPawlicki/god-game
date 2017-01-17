using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour {
    private Text ammoText;
    private int _ammo;
    public int ammo
    {
        get { return _ammo; }
        set
        {
            _ammo = value; ammoText.text = "" + value;
        }
    }
	void Start () {
        ammoText = GetComponent<Text>();
	}
}
