using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameConfiguration : MonoBehaviour
    {
        public enum Game
        {
            Landfill,
            Park,
            Recycle
        }
        public Game _typeGame;
        public float timeHoldingLandfill { get; private set; }
        public float timeHoldingBox{get;private set;}
        public float timeLeavingBox{ get;private set;}
        public int hardBoxPercentaje{get;private set;}
        public int duration{get;private set;}
        public int filterWindow { get; private set; }
        public float multMovX { get; private set; }
        public float multMovZ { get; private set; }

        //Park Game
        int numberBox;
        int speed;
        int timeToAppearMoreBox;
        public static GameConfiguration Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
            ReadJSONData();
        }
       
        public int NumberBox => numberBox;
        public int SpeedBox => speed;
        public int TimeToAppearMoreBox => timeToAppearMoreBox;
        public void ReadJSONData()
        {
            string path;
            string jsonString;
            JSONNode data;
            switch (_typeGame)
            {
                case Game.Landfill:
                     path = Application.streamingAssetsPath + "/CirculoDeContenedores.txt";
                     jsonString = File.ReadAllText(path);
                     data = JSON.Parse(jsonString);

                    timeHoldingBox = data["CirculoDeContenedores"][0]["tiempoParaCogerCaja"].AsFloat;                  
                    timeHoldingLandfill = data["CirculoDeContenedores"][0]["tiempoParaAbrirBasurero"].AsFloat;

                    GetCommonDataFromFile(data, "CirculoDeContenedores");
                    break;
                case Game.Park:
                     path = Application.streamingAssetsPath + "/NaturalezaDeLosResiduos.txt";
                     jsonString = File.ReadAllText(path);
                     data = JSON.Parse(jsonString);

                    numberBox = data["NaturalezaDeLosResiduos"][0]["numeroCajas"].AsInt;
                    speed = data["NaturalezaDeLosResiduos"][0]["velocidadCajas"].AsInt;
                    timeToAppearMoreBox = data["NaturalezaDeLosResiduos"][0]["tiempoParaAparecerNuevasCajas"].AsInt;

                    GetCommonDataFromFile(data, "NaturalezaDeLosResiduos");
                    break;
                case Game.Recycle:
                     path = Application.streamingAssetsPath + "/ViajeAlCentroDeReciclaje.txt";
                     jsonString = File.ReadAllText(path);
                     data = JSON.Parse(jsonString);

                    timeHoldingBox = data["ViajeAlCentroDeReciclaje"][0]["tiempoParaCogerCaja"].AsFloat;
                    timeLeavingBox = data["ViajeAlCentroDeReciclaje"][0]["tiempoParaDejarCaja"].AsFloat;
                    speed = 4;

                    GetCommonDataFromFile(data, "ViajeAlCentroDeReciclaje");
                    break;
            }                   
        }

        private void GetCommonDataFromFile(JSONNode data,string fileName)
        {
            hardBoxPercentaje = data[fileName][0]["porcentajeCajaDificil"].AsInt;
            duration = data[fileName][0]["duracionJuego"].AsInt;
            filterWindow = data[fileName][0]["ventanaFiltro"].AsInt;
            multMovX = data[fileName][0]["multiplicadorEjeX"].AsFloat;
            multMovZ = data[fileName][0]["multiplicadorEjeZ"].AsFloat;
        }
    }
}