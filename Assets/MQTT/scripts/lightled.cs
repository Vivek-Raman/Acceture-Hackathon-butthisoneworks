using UnityEngine;
using System.Collections;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;

using System;
using System.Collections.Generic;

public class lightled : MonoBehaviour {
    bool ledON1 = false;
    bool ledON2 = false;
    bool ledON3 = false;
    bool ledON4 = false;
    bool ledON5 = false;
    bool ledON6 = false;
    bool ledON7 = false;
    bool ledON8 = false;
    private MqttClient client;
    // Use this for initialization
    void Start () {
        // create client instance 
        client = new MqttClient(IPAddress.Parse("52.70.203.194"), 1883, false, null);

        // register to message received 
        // client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 

        string clientId = Guid.NewGuid().ToString();
        client.Connect(clientId, "yugvirparhar", "413216b05fcd4e9e94d134f860d32edb");
        Debug.Log("aa gaya yaha");
        // subscribe to the topic "/home/temperature" with QoS 2 
        client.Subscribe(new string[] { "yugvirparhar/f/led" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }
    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {

        Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message));
    }
    // Update is called once per frame


    void Send_Value(string s)
    {
        Debug.Log("sending..."+s);
        client.Publish("yugvirparhar/f/led", System.Text.Encoding.UTF8.GetBytes(s), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        Debug.Log("sent");
    }
    void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "LED1")
                {
                    if (!ledON1)
                    {
                        Send_Value("11");   
                        ledON1 = true;
                    }
                    else
                    {
                        Send_Value("10");
                        ledON1 = false; 
                    }
                }
                else if (hit.transform.tag == "LED2")
                {
                    if (!ledON2)
                    {
                        Send_Value("21");
                        ledON2 = true;
                    }
                    else
                    {
                        Send_Value("20");
                        ledON2 = false;
                    }
                }
                else if (hit.transform.tag == "LED3")
                {
                    if (!ledON3)
                    {
                        Send_Value("31");
                        ledON3 = true;
                    }
                    else
                    {
                        Send_Value("30");
                        ledON3 = false;
                    }
                }
                else if (hit.transform.tag == "LED4")
                {
                    if (!ledON4)
                    {
                        Send_Value("41");
                        ledON4 = true;
                    }
                    else
                    {
                        Send_Value("40");
                        ledON4 = false;
                    }
                }
                else if (hit.transform.tag == "LED5")
                {
                    if (!ledON5)
                    {
                        Send_Value("51");
                        ledON5 = true;
                    }
                    else
                    {
                        Send_Value("50");
                        ledON5 = false;
                    }
                }
                else if (hit.transform.tag == "LED6")
                {
                    if (!ledON6)
                    {
                        Send_Value("61");
                        ledON6 = true;
                    }
                    else
                    {
                        Send_Value("60");
                        ledON6 = false;
                    }
                }
                else if (hit.transform.tag == "LED7")
                {
                    if (!ledON7)
                    {
                        Send_Value("71");
                        ledON7 = true;
                    }
                    else
                    {
                        Send_Value("70");
                        ledON7 = false;
                    }
                }
                else if (hit.transform.tag == "LED8")
                {
                    if (!ledON8)
                    {
                        Send_Value("81");
                        ledON8 = true;
                    }
                    else
                    {
                        Send_Value("80");
                        ledON8 = false;
                    }
                }
            }

            }
        }

    }
