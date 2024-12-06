using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDialoguesSetUp : MonoBehaviour
{
    public GameObject _popUpPrefab;
    public GameObject _dialogPrefab;

    private string[] _popUpMsg = new string[3];
    private GameObject _popUpWindow;
    private GameObject _dialogWindow;

    private DialogHandler _dialogue1;

    public bool _showPopUp = false;
    private int _popUpPlayIdx = 0;
    public bool _startDialog1 = false;
    private void Awake()
    {
        DialogHandler.NameDialog _dialog_1_0= new DialogHandler.NameDialog("Chief", "In the name of ymir, cast thy blessings upon us! Nourish our earth! Nourish our people! In the name of Odin, cast thy powers upon us! Protect our people! Save our people! ");
        DialogHandler.NameDialog _dialog_1_1 = new DialogHandler.NameDialog(MCSelectionComponent.Instance.GetUserName(), "Yimir, odin, freya! Forcing their believes on me. What if I don’t believe in this so-called faith? What if I refuse to comply with the box, you raised me in? What if I want to decide for myself who to follow and who to believe in? And how about I found out who I am first?");
        DialogHandler.NameDialog _dialog_1_2 = new DialogHandler.NameDialog("Chief", "Sól and Máni cast thy light on your road! Höd and Hel cast thy shadow on theirs.");
        DialogHandler.NameDialog _dialog_1_3 = new DialogHandler.NameDialog(MCSelectionComponent.Instance.GetUserName(), "Here they go again! With their hatred against the new citizens of Bavaria! Maybe their rejection to new experiences, new cultures, and new religions, is a result of their stuck-up minds!");

        List<DialogHandler.NameDialog> _dialog_1 = new List<DialogComponent.NameDialog>();
        _dialog_1.Add(_dialog_1_0);
        _dialog_1.Add(_dialog_1_1);
        _dialog_1.Add(_dialog_1_2);
        _dialog_1.Add(_dialog_1_3);

        _dialogue1 = new DialogHandler(_dialog_1, false, false, false);

        _popUpMsg[0] = "Today is your coming of age ceremony";
        _popUpMsg[1] = "Escape the matrix and find yourself";
        _popUpMsg[2] = "Yggdrasil the origin of 9 worlds";

       
    }
    private void Update()
    {
        if(_startDialog1)
        {

            _dialogWindow = Instantiate(_dialogPrefab);
            _dialogWindow.GetComponent<DialogDisplayHandler>().Init(_dialogue1,true);
            _dialogue1.StartDialogue();

            _startDialog1 = false;
        }
        if(_showPopUp && _popUpPlayIdx<3)
        {
            _popUpWindow = Instantiate(_popUpPrefab);
            _popUpWindow.GetComponent<PopUpDialogueComponent>().Create(_popUpMsg[_popUpPlayIdx]);
            _popUpPlayIdx++;
            _showPopUp = false;
        }
    }
}
