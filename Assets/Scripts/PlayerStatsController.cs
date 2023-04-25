using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]
public class BasicInfoChar {
    public BasicStats baseInfo;
    public TypeCharacter typeChar;
}

public class PlayerStatsController : MonoBehaviour {

    // Padrão Singleton
    public static PlayerStatsController intance;

    public int xpMultiply = 1;
    public float xpFirstLevel = 100; // XP para o próximo level
    public float difficultFactor = 1.5f; //fator de dificuldade do level
    public List<BasicInfoChar> baseInfoChars;


    void Start() {
        intance = this;
        DontDestroyOnLoad(gameObject);
        //SceneManager.LoadScene("GamePlay");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) //A adiciona 100 de Xp
            AddXp(100);
        if (Input.GetKeyDown(KeyCode.R)) //R zera
            PlayerPrefs.DeleteAll();
    }
        // Método para XP
        public static void AddXp(float xpAdd) {
            float newXp = (GetCurrentXp() + xpAdd) * PlayerStatsController.intance.xpMultiply;
            while (newXp >= GetNextXp()) {
                newXp -= GetNextXp();
                AddLevel();

            }

            PlayerPrefs.SetFloat("currentXp", newXp);
        }

        public static float GetCurrentXp() {
            return PlayerPrefs.GetFloat("currentXp");
        }

        //Método para level atual
        public static int GetCurrentLevel() {
            return PlayerPrefs.GetInt("currentLevel");
        }

        public static void AddLevel() {
            int newLevel = GetCurrentLevel() + 1;
            PlayerPrefs.SetInt("currentLevel", newLevel);
        }

        public static float GetNextXp() {
            return PlayerStatsController.intance.xpFirstLevel * (GetCurrentLevel() + 1) * PlayerStatsController.intance.difficultFactor;
        }
        // Tipos de Personagem
        public static TypeCharacter GetTypeCharacter() {

            int typeId = PlayerPrefs.GetInt("TypeCharacter");

            if (typeId == 0)
                return TypeCharacter.Guerreiro;
            else if (typeId == 1)
                return TypeCharacter.Mago;
            else if (typeId == 2)
                return TypeCharacter.Arqueiro;

            return TypeCharacter.Guerreiro;
        }

        public static void SetTypeCharacter(TypeCharacter newType) {
            PlayerPrefs.SetInt("TypeCharacter", (int)newType);

        }

        public BasicStats GetBasicStats(TypeCharacter type) {
            foreach (BasicInfoChar info in baseInfoChars) {
                if (info.typeChar == type)
                    return info.baseInfo;
            }

            return baseInfoChars[0].baseInfo;
        }
}



    
