using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework.Core.Data
{
    [CreateAssetMenu(menuName = "Data/MapSelectionData" , fileName = "MapSelectionData")]
    public class MapSelectionData : ScriptableObject
    {
        public List<MapInfo> Maps;

    }

}

[Serializable] 
public struct MapInfo
{
    public Texture2D MapThumbnail;
    public string MapName;
    public string SceneName;
}
