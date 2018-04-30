using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigPanel : MonoBehaviour {

    public static GameObject instance;

    Canvas canvas;

    //Controles
    InputField input_teamA;
    InputField input_teamB;
    Dropdown dropdown_level;

    //Botones
    Button button_close;
    Button button_ok;

    void Awake() {

        Transform contentTransform = transform.Find("layout_content");

        input_teamA = contentTransform.Find("input_team_a").GetComponent<InputField>();
        input_teamB = contentTransform.Find("input_team_b").GetComponent<InputField>();
        dropdown_level = contentTransform.Find("dropdown_level").GetComponent<Dropdown>();

        button_close = transform.Find("button_close").GetComponent<Button>();
        button_ok = transform.Find("button_ok").GetComponent<Button>();

        button_close.onClick.AddListener(Close);
        button_ok.onClick.AddListener(Apply);
    }

    public void Close() {
        Destroy(gameObject);
    }

    public void Apply() {

        bool thereIsError = false;

        //Revisar el campo del equipo A
        if(input_teamA.text.Trim().Equals("") == false) {
            PlayManager.instance.teamA.name = input_teamA.text.Trim();
            input_teamA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        } else {
            input_teamA.GetComponent<Image>().color = new Color32(255, 113, 113, 255);
            thereIsError = true;
        }

        //Revisar el campo del equipo B
        if (input_teamB.text.Trim().Equals("") == false) {
            PlayManager.instance.teamB.name = input_teamB.text.Trim();
            input_teamB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        } else {
            input_teamB.GetComponent<Image>().color = new Color32(255, 113, 113, 255);
            thereIsError = true;
        }

        //Aplicar el nivel
        PlayManager.instance.level = int.Parse(dropdown_level.captionText.text);

        //Si no hay errores, aplicar la configuración
        if (!thereIsError) {
            Debug.Log("Se aplicó la configuración: " + PlayManager.instance.teamA.name + ", " + PlayManager.instance.teamB.name + ", nivel " + PlayManager.instance.level);
            Close();
        }

    }

    public static GameObject Show() {
        if(instance == null) {
            Canvas canvas = FindObjectOfType<Canvas>();
            GameObject configPanelPrefab = Resources.Load("config_panel") as GameObject;
            instance = Instantiate(configPanelPrefab, canvas.transform, false);
            return instance;
        } else {
            return instance;
        }
    }
}
