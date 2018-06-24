using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{

    public InputField emailLogin;
    public InputField passLogin;
    public InputField nameNew;
    public InputField emailNew;
    public InputField passNew;
    public GameObject errorPanel;
    public Text error;

    private void Awake()
    {
        PlayerPrefs.SetString("StringConn", "URI=file:" + Application.dataPath + "/Plugins/Users.s3db");
    }
    private void Start()
    {
    }

    public void Login(string sceneName)
    {
        if (emailLogin.text == "" || passLogin.text == "")
        {
            error.text = "No debe haber campos vacíos";
            Debug.LogError("No debe haber campos vacíos");
        }
        else
        {
            DataBase.User user = DataBase.GetUser(emailLogin.text, passLogin.text);
            if (user.Email != null)
            {
                Debug.Log("Bienvenido/a " + user.Name + ", tu útlimo puntaje fue: " + user.Score);
                PlayerPrefs.SetString("Name", user.Name);
                PlayerPrefs.SetString("Email", emailLogin.text);
                UIController.ChangeScene(sceneName);
            }
            else
            {
                error.text = "Error en los datos";
                Debug.LogError("Error en los datos");
            } 
        }
        UIController.OpenPanel(errorPanel);
    }
        
            
    

    public void CreateUser(string sceneName)
    {
        if(nameNew.text == "" || emailNew.text == "" || passNew.text == "")
        {
            error.text = "No debe haber campos vacíos";
            UIController.OpenPanel(errorPanel);
            Debug.LogError("No debe haber campos vacíos");
        }
        else
        {
            DataBase.InsertUser(nameNew.text, emailNew.text, passNew.text);
            if (DataBase.GetUser(emailNew.text, passNew.text).Name != null)
            {
                PlayerPrefs.SetString("Email", emailNew.text);
                Debug.Log("Usuario creado");
                UIController.ChangeScene(sceneName);
            }
        }
        
    }
   

    public void ListUsers()
    {
        ICollection<DataBase.User> users = DataBase.GetUsers();
        foreach (var user in users)
        {
            Debug.Log("value= " + user.Id + "  name = " + user.Name + "  Email = " + user.Email + " Pass = " + user.Pass + " Score = " + user.Score);
        }
    }
}
