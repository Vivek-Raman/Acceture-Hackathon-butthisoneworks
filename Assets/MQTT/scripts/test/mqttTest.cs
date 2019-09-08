using UnityEngine;
using System.Collections.Generic;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;

using System;

public class mqttTest : MonoBehaviour
{
    public Grapher grapher;
    private int i = 0;
    private MqttClient client;

    void Start()
    {
        client = new MqttClient(IPAddress.Parse("40.71.204.206"), 1883, false, null);

        // register to message received 
        client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;

        string clientId = Guid.NewGuid().ToString();
        client.Connect(clientId, "yugvirparhar", "413216b05fcd4e9e94d134f860d32edb");
        Debug.Log("aa gaya yaha");

        // subscribe to the topic "/home/temperature" with QoS 2 
        client.Subscribe(new string[] { "DT2" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }

    void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        // collect data from this
        string message = System.Text.Encoding.UTF8.GetString(e.Message);
        Debug.Log("Received: " + message);
        int newTemp = Int32.Parse(message);
        grapher.AddValue(newTemp);
    }
}
