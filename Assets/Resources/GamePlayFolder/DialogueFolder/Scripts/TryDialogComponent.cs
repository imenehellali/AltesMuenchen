using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryDialogComponent : MonoBehaviour
{
   public List<DialogHandler.NameDialog> _dialogues=new List<DialogHandler.NameDialog>();
    public bool _dialogueEnd = false;
    public bool _dialogueStart = false;
    public bool _dialogueISPlaying = false;
    private DialogHandler _obj;
    public DialogDisplayHandler _displayDialPref;
    DialogDisplayHandler _display;
   public void Create()
    {

        //To Change
        _dialogues.Add(new DialogHandler.NameDialog("chief", "In the name of ymir, cast thy blessings upon us! Nourish our earth! Nourish our people! In the name of Odin, cast thy powers upon us! Protect our people! Save our people! "));
        _dialogues.Add(new DialogHandler.NameDialog("MC", "Yimir, odin, freya! Forcing their believes on me. What if I don’t believe in this so-called faith? What if I refuse to comply with the box, you raised me in? What if I want to decide for myself who to follow and who to believe in? And how about I found out who I am first?"));
        _dialogues.Add(new DialogHandler.NameDialog("chief", "Sól and Máni cast thy light on your road! Höd and Hel cast thy shadow on theirs."));
        _dialogues.Add(new DialogHandler.NameDialog("MC", "Here they go again! With their hatred against the new citizens of Bavaria! Maybe your rejection to new experiences, new cultures, and new religions, is a result of their stuck-up minds!"));

        _obj = new DialogHandler(_dialogues, _dialogueStart, _dialogueEnd, _dialogueISPlaying);
        _display =Instantiate(_displayDialPref, null);
        _display.gameObject.GetComponent<DialogHandler>().SetDialogComponent(_obj);

    }

    public void StartDialogue()
    {
        _display.gameObject.SetActive(true);
        _obj.StartDialogue();
    }

}
