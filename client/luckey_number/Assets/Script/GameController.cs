using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class GameController : MonoBehaviour
{
    static SocketIOComponent socketIO;
    public InputField inputNumber;
    public InputField inputName;
    public Text statusBar;
    public GameObject panelWin;
    public GameObject panelHaveWin;

    private bool gameStart = false;
    private bool moreThan = false;
    private bool lessThan = false;
    
    // Start is called before the first frame update
    void Start()
    {
        panelHaveWin.SetActive(false);
        panelWin.SetActive(false);
        socketIO = GetComponent<SocketIOComponent>();
        socketIO.On("open", OnConnected);
        socketIO.On("youWin", OnWin);
        socketIO.On("moreThan", OnNumberMoreLuckeyNumber);
        socketIO.On("lessThan", OnNumberLessLuckeyNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            statusBar.text = "Please enter your name and number";
            if (moreThan)
            {
                statusBar.text = "Number more than luckeynumber";
            }else if (lessThan)
            {
                statusBar.text = "Number less than luckeynumber";
            }
        }
    }

    private void OnConnected(SocketIOEvent obj)
    {
        Debug.Log("connected");
        gameStart = true;
    }
    void OnWin(SocketIOEvent obj)
    {
        Debug.Log("Win");
        panelWin.SetActive(true);

    }
    void OnNumberMoreLuckeyNumber(SocketIOEvent obj)
    {
        lessThan = false;
        moreThan = true;
    }
    void OnNumberLessLuckeyNumber(SocketIOEvent obj)
    {
        moreThan = false;
        lessThan = true;
    }
    public void ClickSend()
    {
        Debug.Log("Sended");
        if (inputName != null && inputNumber != null)
        {
            JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
            jSONObject.AddField("name", inputName.text);
            jSONObject.AddField("number", inputNumber.text);
            socketIO.Emit("message", jSONObject);

        }
    }
    public void ResetNumber()
    {
        Debug.Log("resetnumber");
        socketIO.Emit("resetnumber");
        panelWin.SetActive(false);
    }
    
    public void CloseGame()
    {
        
    }
}
