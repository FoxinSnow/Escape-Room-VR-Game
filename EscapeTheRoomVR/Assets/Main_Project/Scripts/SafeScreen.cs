﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeScreen : MonoBehaviour {

    public Transform scr0, scr1, scr2, scr3, scr4, scr5, scr6;
    //C = 10, E = 11, O = 12, R = 13, T = 14, x = 15, * = 16, # = 17;
    public Texture tex0, tex1, tex2, tex3, tex4, tex5, tex6, tex7,
        tex8, tex9, tex10, tex11, tex12, tex13, tex14, tex15, tex16, tex17;
    public Transform fingerPrint;
    public Texture fpTexture;
    private Transform[] scrs = new Transform[7]; //screens
    private Texture[] texs = new Texture[18]; //textures
    private int currentScr = 0; //0 ~ 6, mark the next screen should use
    private int[] password = new int[4];
    private int[] input = new int[7]
        {-1, -1, -1, -1, -1, -1, -1}; //7 is the max length, only check first 4 digit
    private float second = 0;//time to test whether to sleep
    private static bool wOrS = false; //false = sleep, true = wake
    private static bool passC = false; //use for test whether fingerprint can be used

    // Use this for initialization
    void Start () {
        scrs[0] = scr0; scrs[1] = scr1; scrs[2] = scr2; scrs[3] = scr3;
        scrs[4] = scr4; scrs[5] = scr5; scrs[6] = scr6;
        texs[0] = tex0; texs[1] = tex1; texs[2] = tex2; texs[3] = tex3;
        texs[4] = tex4; texs[5] = tex5; texs[6] = tex6; texs[7] = tex7;
        texs[8] = tex8; texs[9] = tex9; texs[10] = tex10; texs[11] = tex11;
        texs[12] = tex12; texs[13] = tex13; texs[14] = tex14; texs[15] = tex15;
        //wait for add 16 and 17
        //.
        //.
        password[0] = 1;
        password[1] = 1;
        password[2] = 1;
        password[3] = 1;
	}
	
	// Update is called once per frame
	void Update () {

    }

    //display numbers, n = 0 ~ 9, or 16, 17
    private void inputNum(int n){
        //current screen will display the input number
        scrs[currentScr].GetComponent<Renderer>().material.mainTexture = texs[n];
        input[currentScr] = n; //the input number will be stored for check
        currentScr++; //the current scr move to next one 
    }

    //delete the last number
    private void deleteNum(){
        scrs[currentScr - 1].GetComponent<Renderer>().material.mainTexture = texs[15];
        input[currentScr - 1] = -1;
    }

    private void cancel(){
        //reset changed variables
        resetInput();
        sleep();
    }

    //check whether password is correct
    private void checkPassword(){
        if (input[4] != -1){  //if there is more than 4 digit, wrong
            passwordWrong();
            return;
        } else { //else if there is only 4 digit
            for (int i = 0; i < 4; i++) {
                if (password[i] != input[i]){ //if any one is not correct, wrong
                    passwordWrong();
                    return;
                }
            }
            passwordCorrect();
        }
    }

    private void passwordCorrect(){
        Debug.Log("CORRECT");
        scrs[0].GetComponent<Renderer>().material.mainTexture = texs[10];//c
        scrs[1].GetComponent<Renderer>().material.mainTexture = texs[12];//o
        scrs[2].GetComponent<Renderer>().material.mainTexture = texs[13];//r
        scrs[3].GetComponent<Renderer>().material.mainTexture = texs[13];//r
        scrs[4].GetComponent<Renderer>().material.mainTexture = texs[11];//e
        scrs[5].GetComponent<Renderer>().material.mainTexture = texs[10];//c
        scrs[6].GetComponent<Renderer>().material.mainTexture = texs[14];//t
        passC = true; //ready for finger print
    }

    private void passwordWrong(){
        Debug.Log("WRONG");
        allX();
        //reset to wait new input
        resetInput();
        //after 2 second slepp the screen
        second += Time.deltaTime;
        Debug.Log(second);
        if (second > 2.0f){
            sleep();
        }
    }

    private void getFingerPrint(){
        if(passC.Equals(true)){
            fingerPrint.GetComponent<Renderer>().material.mainTexture = fpTexture;
        }
    }

    private void wakeUp(){
        allX();
        wOrS = true;
    }

    private void sleep(){
        allEmpty();
        wOrS = false;
    }

    private void resetInput(){
        //reset the input array
        for (int i = 0; i < 6; i++)
        {
            input[i] = -1;
        }
        //reset the current scr
        currentScr = 0;
        passC = false;
    }

    //n = 0 ~ 17
    //main function
    public void pressButton(int n){
        //if not wake up, wake up first
        if(wOrS.Equals(false)){
            wakeUp();
        }
        //0 1 2 3 4 5 6 7 8 9
        //delete = 10; cancel = 11; enter = 12;
        //* = 16, # = 17; 16 17 to be same with texture index 
        switch (n){
            case 0: break;
            case 1: break;
            case 2: break;
            case 3: break;
            case 4: break;
            case 5: break;
            case 6: break;
            case 7: break;
            case 8: break;
            case 9: break;
            case 10: break;
            case 11:
                cancel();
                break;
            case 12: break;
            case 16: break;
            case 17: break;
        }
    }

    private void allX(){
        scrs[0].GetComponent<Renderer>().material.mainTexture = texs[15];
        scrs[1].GetComponent<Renderer>().material.mainTexture = texs[15];
        scrs[2].GetComponent<Renderer>().material.mainTexture = texs[15];
        scrs[3].GetComponent<Renderer>().material.mainTexture = texs[15];
        scrs[4].GetComponent<Renderer>().material.mainTexture = texs[15];
        scrs[5].GetComponent<Renderer>().material.mainTexture = texs[15];
        scrs[6].GetComponent<Renderer>().material.mainTexture = texs[15];
    }

    private void allEmpty(){
        scrs[0].GetComponent<Renderer>().material.mainTexture = null;
        scrs[1].GetComponent<Renderer>().material.mainTexture = null;
        scrs[2].GetComponent<Renderer>().material.mainTexture = null;
        scrs[3].GetComponent<Renderer>().material.mainTexture = null;
        scrs[4].GetComponent<Renderer>().material.mainTexture = null;
        scrs[5].GetComponent<Renderer>().material.mainTexture = null;
        scrs[6].GetComponent<Renderer>().material.mainTexture = null;
    }
}