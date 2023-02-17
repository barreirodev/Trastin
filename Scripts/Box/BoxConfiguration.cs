using System.Collections.Generic;
using UnityEngine;

namespace Box
{
    [CreateAssetMenu(menuName = "Create box configuration", fileName = "BoxConfiguration", order = 1)]
    public class BoxConfiguration : ScriptableObject
    {
        public enum TypeGame
        {
            Landfill,
            Park,
            Recycle
        }
        public TypeGame _typeGame;
        public Vector3 _position;
        public BoxMediator _prefab;

        [Header("Landfill")]
        public BoxConfigurationDetails _boxEasyDetailsOrganic;
        public BoxConfigurationDetails _boxHardDetailsOrganic;
        public BoxConfigurationDetails _boxEasyDetailsYellow;
        public BoxConfigurationDetails _boxHardDetailsYellow;
        public BoxConfigurationDetails _boxEasyDetailsBlue;
        public BoxConfigurationDetails _boxEasyDetailsGreen;
        public BoxConfigurationDetails _boxEasyDetailsGrey;
        public BoxConfigurationDetails _boxHardDetailsGrey;

        [Header("Park")]
        public BoxConfigurationDetails _boxEasyDetailsBiodegradable;
        public BoxConfigurationDetails _boxHardDetailsBiodegradable;
        public BoxConfigurationDetails _boxEasyDetailsNonBiodegradable;
        public BoxConfigurationDetails _boxHardDetailsNonBiodegradable;

        [Header("Recycle")]
        public BoxConfigurationDetails _boxEasyDetailsVegetal;
        public BoxConfigurationDetails _boxHardDetailsVegetal;   
        public BoxConfigurationDetails _boxEasyDetailsAnimal;
        public BoxConfigurationDetails _boxHardDetailsAnimal;
        public BoxConfigurationDetails _boxEasyDetailsSintetic;
        public BoxConfigurationDetails _boxHardDetailsSintetic;
        public BoxConfigurationDetails _boxEasyDetailsUniversal;
        public BoxConfigurationDetails _boxHardDetailsUniversal;
        public BoxConfigurationDetails _boxEasyDetailsFossil;
        public BoxConfigurationDetails _boxHardDetailsFossil;
        public BoxConfigurationDetails _boxEasyDetailsMineral;
        public BoxConfigurationDetails _boxHardDetailsMineral;
        public bool _disappearOnPickUp;
        public enum Movement
        {
            None,
            zMovement,
            xMovement
        }
        public Movement _typeMovement;

        public Vector3 Position() => _position;
        public Vector3 Position(Vector3 newPosition) => _position = newPosition;
        public BoxMediator Prefab => _prefab;
        public TypeGame Game => _typeGame;
        public void InitializeNumberOfImagesUsed(Dictionary<BoxConfigurationDetails, List<int>> numberOfImagesUsed)
        {
            switch (_typeGame)
            {
                case TypeGame.Landfill:
                    numberOfImagesUsed.Add(_boxHardDetailsOrganic, new List<int>());
                    numberOfImagesUsed.Add(_boxEasyDetailsOrganic, new List<int>());
                    numberOfImagesUsed.Add(_boxHardDetailsYellow, new List<int>());
                    numberOfImagesUsed.Add(_boxEasyDetailsYellow, new List<int>());
                    numberOfImagesUsed.Add(_boxEasyDetailsBlue, new List<int>());
                    numberOfImagesUsed.Add(_boxEasyDetailsGreen, new List<int>());
                    numberOfImagesUsed.Add(_boxHardDetailsGrey, new List<int>());
                    numberOfImagesUsed.Add(_boxEasyDetailsGrey, new List<int>());
                    break;
                case TypeGame.Park:
                    numberOfImagesUsed.Add(_boxHardDetailsNonBiodegradable, new List<int>());
                    numberOfImagesUsed.Add(_boxEasyDetailsNonBiodegradable, new List<int>());
                    numberOfImagesUsed.Add(_boxHardDetailsBiodegradable, new List<int>());
                    numberOfImagesUsed.Add(_boxEasyDetailsBiodegradable, new List<int>());

                    break;
                case TypeGame.Recycle:
                    numberOfImagesUsed.Add(_boxHardDetailsVegetal, new List<int>());
                    numberOfImagesUsed.Add(_boxEasyDetailsVegetal, new List<int>());
                    numberOfImagesUsed.Add(_boxHardDetailsAnimal, new List<int>());
                    numberOfImagesUsed.Add(_boxEasyDetailsAnimal, new List<int>());
                    numberOfImagesUsed.Add(_boxHardDetailsSintetic, new List<int>());
                    numberOfImagesUsed.Add(_boxEasyDetailsSintetic, new List<int>());
                    numberOfImagesUsed.Add(_boxHardDetailsUniversal, new List<int>());
                    numberOfImagesUsed.Add(_boxEasyDetailsUniversal, new List<int>());
                    numberOfImagesUsed.Add(_boxHardDetailsFossil, new List<int>());
                    numberOfImagesUsed.Add(_boxEasyDetailsFossil, new List<int>());
                    numberOfImagesUsed.Add(_boxHardDetailsMineral, new List<int>());
                    numberOfImagesUsed.Add(_boxEasyDetailsMineral, new List<int>());
                    break;
            }
        }
        public BoxConfigurationDetails GetBox(bool neededNonBiodegradable, bool hard)
        {
            switch (_typeGame)
            {
                case TypeGame.Landfill:
                    int typeLandfill = Random.Range(0,5);

                        switch (typeLandfill)
                        {
                            case 0:
                                if (hard) return _boxHardDetailsOrganic;
                                else return _boxEasyDetailsOrganic;
                            case 1:
                                if (hard) return _boxHardDetailsYellow;
                                else return _boxEasyDetailsYellow;
                            case 2:
                                return _boxEasyDetailsBlue;
                            case 3:
                                return _boxEasyDetailsGreen;
                            case 4:
                                if (hard) return _boxHardDetailsGrey;
                                else return _boxEasyDetailsGrey;
                        }
                break;

                case TypeGame.Park:
                    if (neededNonBiodegradable)

                        if (hard) return _boxHardDetailsNonBiodegradable;
                        else return _boxEasyDetailsNonBiodegradable;
                        
                    else

                        if (hard) return _boxHardDetailsBiodegradable;                
                        else return _boxEasyDetailsBiodegradable;

                case TypeGame.Recycle:
                    int typeOrigin = Random.Range(0, 6);

                    switch (typeOrigin)
                    {
                        case 0:
                            if (hard) return _boxHardDetailsVegetal;
                            else return _boxEasyDetailsVegetal;
                        case 1:
                            if (hard) return _boxHardDetailsAnimal;
                            else return _boxEasyDetailsAnimal;
                        case 2:
                            if (hard) return _boxHardDetailsSintetic;
                            else return _boxEasyDetailsSintetic;
                        case 3:
                            if (hard) return _boxHardDetailsUniversal;
                            else return _boxEasyDetailsUniversal;
                        case 4:
                            if (hard) return _boxHardDetailsFossil;
                            else return _boxEasyDetailsFossil;
                        case 5:
                            if (hard) return _boxHardDetailsMineral;
                            else return _boxEasyDetailsMineral;
                    }
                    break;
            }
            return null;
        }

        public bool DisappearOnPickUp => _disappearOnPickUp;
        public Movement TypeMovement => _typeMovement;

    }
}