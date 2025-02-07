using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;

public class DataCollector : MonoBehaviour
{
    public string folderPath;
    public string game;
    public string user;

    public TrackedPoseDriver head;
    public TrackedPoseDriver leftController;
    public TrackedPoseDriver rightController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DataInfo dataInfo = new DataInfo();
        dataInfo.time = Time.time;
        Device headset = new Device();
        headset.position = new Position(head.transform.position);
        headset.rotation = new Rotation(head.transform.rotation);
        dataInfo.headset = headset;
        Device left = new Device();
        left.position = new Position(leftController.transform.position);
        left.rotation = new Rotation(leftController.transform.rotation);
        dataInfo.leftController = left;
        Device right = new Device();
        right.position = new Position(rightController.transform.position);
        right.rotation = new Rotation(rightController.transform.rotation);
        dataInfo.rightController = right;
        string path = folderPath + "/" + game + "_" + user + ".json";
        if (!File.Exists(path)) File.AppendAllText(path, "[");
        else File.AppendAllText(path, ",");
        File.AppendAllText(path, JsonConvert.SerializeObject(dataInfo));
    }

    private void OnApplicationQuit()
    {
        string path = folderPath + "/" + game + "_" + user + ".json";
        File.AppendAllText(path, "]");
        string jsonString = File.ReadAllText(path);
        List<DataInfo> results = JsonConvert.DeserializeObject<List<DataInfo>>(jsonString);
    }
}

[System.Serializable]
public class DataInfo
{
    public float time { get; set; }
    public Device headset { get; set; }
    public Device leftController { get; set; }
    public Device rightController { get; set; }
}

[System.Serializable]
public class Device
{
    public Position position { get; set; }
    public Rotation rotation { get; set; }
}

[System.Serializable]
public class Position
{
    public Position(Vector3 position)
    {
        x = position.x;
        y = position.y;
        z = position.z;
    }
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
}

[System.Serializable]
public class Rotation
{
    public Rotation(Quaternion quaternion)
    {
        x = quaternion.x;
        y = quaternion.y;
        z = quaternion.z;
        w = quaternion.w;
    }
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float w { get; set; }
}