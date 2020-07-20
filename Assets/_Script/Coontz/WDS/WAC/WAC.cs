using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WAC : MonoBehaviour
{
    public GameObject[] FCS;
    public GameObject Launcher;
    public RectTransform screen;
    public RectTransform screen2;
    
    public GameObject FCS4EnableLamp;
    public GameObject FCS4TrackLamp;
    public GameObject FCS4SelectButton;
    public GameObject FCS4ResetButton;
    public GameObject FCS4AssignLamp;
    public GameObject FCS4LostLamp;
    public GameObject FCS4GuidingLamp;
    public GameObject FCS5EnableLamp;
    public GameObject FCS5TrackLamp;
    public GameObject FCS5SelectButton;
    public GameObject FCS5ResetButton;
    public GameObject FCS5AssignLamp;
    public GameObject FCS5LostLamp;
    public GameObject FCS5GuidingLamp;
    public GameObject launcherAlignLamp;
    public GameObject[] luncherLoadingLamp;
    public GameObject[] luncherReadyLamp; 
    public GameObject loadSelectDial;
    public GameObject[] loadSelectLamp;
    public GameObject loadOnceButton;
    public GameObject loadContSwitch;
    public GameObject loadOnceLamp;
    public GameObject loadContLamp;
    public GameObject salvoSelectDial;
    public GameObject salvoIntevalDial;
    public GameObject fireKeyButton;


    RectTransform HI;
    RectTransform heading;
    RectTransform heading2;
    RectTransform targetSymbol1;
    RectTransform targetIP1;
    RectTransform targetSymbol2;
    RectTransform targetIP2;
    WDSComputer computer;
    GameObject ownship;
    GMFCS fcs4;
    GMFCS fcs5;
    MissileLauncher launcherController;

    bool loadOnce = false;
    bool loadCont = false;
    float launchInterval = 0;
    float fireTime;

    void Start()
    {
        heading = screen.transform.GetChild(0).GetComponent<RectTransform>();
        heading2 = screen2.transform.GetChild(0).GetComponent<RectTransform>();
        targetSymbol1 = screen.transform.GetChild(2).GetComponent<RectTransform>();
        targetIP1 = screen.transform.GetChild(3).GetComponent<RectTransform>();
        targetSymbol2 = screen2.transform.GetChild(2).GetComponent<RectTransform>();
        targetIP2 = screen2.transform.GetChild(3).GetComponent<RectTransform>();
        ownship = GameObject.FindGameObjectWithTag("Player");
        fcs4 = FCS[0].GetComponent<GMFCS>();
        fcs5 = FCS[1].GetComponent<GMFCS>();
        launcherController = Launcher.GetComponent<MissileLauncher>();
    }

    // Update is called once per frame 295
    void Update()
    {
        var scale = 80 * 1.852f * 1000f;
        if(fcs4.trackingTarget != null){
            var arrivalTime = Vector3.Distance(fcs4.trackingTarget.transform.position,ownship.transform.position) / (fcs4.trackingTarget.GetComponent<Rigidbody>().velocity.magnitude + 2400 * 0.0514f);
            var predictionPos = fcs4.trackingTarget.transform.position + arrivalTime * fcs4.trackingTarget.GetComponent<Rigidbody>().velocity;
            targetSymbol1.transform.localPosition = new Vector3(fcs4.trackingTarget.transform.position.x / (scale / 10) * 550f,fcs4.trackingTarget.transform.position.z / (scale / 10) * 550f,0f);
            targetIP1.transform.localPosition = new Vector3(predictionPos.x / (scale / 10) * 550f,predictionPos.z / (scale / 10) * 550f,0f);
        } else {
            targetSymbol1.transform.localPosition = new Vector3(0,0,10f);
            targetIP1.transform.localPosition = new Vector3(0,0,10f);
        }
        if(fcs4.enable){
            FCS4EnableLamp.GetComponent<Lamp>().enable = true;
        } else {
            FCS4EnableLamp.GetComponent<Lamp>().enable = false;
        }
        if(fcs4.tracking){
            FCS4TrackLamp.GetComponent<Lamp>().enable = true;
        } else {
            FCS4TrackLamp.GetComponent<Lamp>().enable = false;
        }
        if(fcs4.isGuiding){
            FCS4GuidingLamp.GetComponent<Lamp>().enable = true;
        } else {
            FCS4GuidingLamp.GetComponent<Lamp>().enable = false;
        }
        if(fcs4.isLost){
            FCS4LostLamp.GetComponent<Lamp>().enable = true;
        } else {
            FCS4LostLamp.GetComponent<Lamp>().enable = false;
        }
        if(FCS4SelectButton.GetComponent<ButtonSwitch>().pushed){
            launcherController.FCS = FCS[0];
        }
        if(FCS4ResetButton.GetComponent<ButtonSwitch>().pushed){
            launcherController.FCS = null;
        }
        if(launcherController.FCS == FCS[0]){
            FCS4AssignLamp.GetComponent<Lamp>().enable = true;
        } else {
            FCS4AssignLamp.GetComponent<Lamp>().enable = false;
        }

        if(fcs5.trackingTarget != null){
            var arrivalTime = Vector3.Distance(fcs5.trackingTarget.transform.position,ownship.transform.position) / (fcs5.trackingTarget.GetComponent<Rigidbody>().velocity.magnitude + 2400 * 0.0514f);
            var predictionPos = fcs5.trackingTarget.transform.position + arrivalTime * fcs5.trackingTarget.GetComponent<Rigidbody>().velocity;
            targetSymbol2.transform.localPosition = new Vector3(fcs5.trackingTarget.transform.position.x / (scale / 10) * 550f,fcs5.trackingTarget.transform.position.z / (scale / 10) * 550f,0f);
            targetIP2.transform.localPosition = new Vector3(predictionPos.x / (scale / 10) * 550f,predictionPos.z / (scale / 10) * 550f,0f);
        } else {
            targetSymbol2.transform.localPosition = new Vector3(0,0,10f);
            targetIP2.transform.localPosition = new Vector3(0,0,10f);
        }
        if(fcs5.enable){
            FCS5EnableLamp.GetComponent<Lamp>().enable = true;
        } else {
            FCS5EnableLamp.GetComponent<Lamp>().enable = false;
        }
        if(fcs5.tracking){
            FCS5TrackLamp.GetComponent<Lamp>().enable = true;
        } else {
            FCS5TrackLamp.GetComponent<Lamp>().enable = false;
        }
        if(fcs5.isGuiding){
            FCS5GuidingLamp.GetComponent<Lamp>().enable = true;
        } else {
            FCS5GuidingLamp.GetComponent<Lamp>().enable = false;
        }
        if(fcs5.isLost){
            FCS5LostLamp.GetComponent<Lamp>().enable = true;
        } else {
            FCS5LostLamp.GetComponent<Lamp>().enable = false;
        }
        if(FCS5SelectButton.GetComponent<ButtonSwitch>().pushed){
            launcherController.FCS = FCS[1];
        }
        if(FCS5ResetButton.GetComponent<ButtonSwitch>().pushed){
            launcherController.FCS = null;
        }
        if(launcherController.FCS == FCS[1]){
            FCS5AssignLamp.GetComponent<Lamp>().enable = true;
        } else {
            FCS5AssignLamp.GetComponent<Lamp>().enable = false;
        }

        if(loadContSwitch.GetComponent<Switch>().position == 1){
            loadCont = true;
        } else {
            loadCont = false;
        }
        if(loadCont){
            if(loadSelectDial.GetComponent<DialSwitch>().position == 1 && !launcherController.loaded[0] && !launcherController.loaded[1]){
                if(!launcherController.isLoading[0]){
                    launcherController.loadMissile(0);
                }
                if(!launcherController.isLoading[1]){
                    launcherController.loadMissile(1);
                }
            }
            if(loadSelectDial.GetComponent<DialSwitch>().position == 0 && !launcherController.loaded[0]){
                if(!launcherController.isLoading[0]){
                    launcherController.loadMissile(0);
                }
            }
            if(loadSelectDial.GetComponent<DialSwitch>().position == 2 && !launcherController.loaded[1]){
                if(!launcherController.isLoading[1]){
                    launcherController.loadMissile(1);
                }
            }
        }
        if(loadOnceButton.GetComponent<ButtonSwitch>().pushed && !loadCont){
            loadOnce = true;
        }
        if(loadSelectDial.GetComponent<DialSwitch>().position == 1 && launcherController.loaded[0] &&launcherController.loaded[1]){
            loadOnce = false;
        }
        if(loadSelectDial.GetComponent<DialSwitch>().position == 0 && launcherController.loaded[0]){
            loadOnce = false;
        }
        if(loadSelectDial.GetComponent<DialSwitch>().position == 2 && launcherController.loaded[1]){
            loadOnce = false;
        }
        if(loadSelectDial.GetComponent<DialSwitch>().position == 1 && !launcherController.loaded[0] && !launcherController.loaded[1] && loadOnce){
            if(!launcherController.isLoading[0]){
                launcherController.loadMissile(0);
            }
            if(!launcherController.isLoading[1]){
                launcherController.loadMissile(1);
            }
        }
        if(loadSelectDial.GetComponent<DialSwitch>().position == 0 && !launcherController.loaded[0] && loadOnce){
            if(!launcherController.isLoading[0]){
                launcherController.loadMissile(0);
            }
        }
        if(loadSelectDial.GetComponent<DialSwitch>().position == 2 && !launcherController.loaded[1] && loadOnce){
            if(!launcherController.isLoading[1]){
                launcherController.loadMissile(1);
            }
        }
        for(int cnt = 0;cnt < 3;cnt++){
            if(loadSelectDial.GetComponent<DialSwitch>().position == cnt){
                loadSelectLamp[cnt].GetComponent<Lamp>().enable = true;
            } else{
                loadSelectLamp[cnt].GetComponent<Lamp>().enable = false;
            }
        }
        if(loadOnce){
            loadOnceLamp.GetComponent<Lamp>().enable = true;
        } else{
            loadOnceLamp.GetComponent<Lamp>().enable = false;
        }
        if(loadCont){
            loadContLamp.GetComponent<Lamp>().enable = true;
        } else{
            loadContLamp.GetComponent<Lamp>().enable = false;
        }

        launchInterval = salvoIntevalDial.GetComponent<DialSwitch>().position * 50 + 100;
        launchInterval = launchInterval / 1000;
        if(fireKeyButton.GetComponent<ButtonSwitch>().pushed){
            if(salvoSelectDial.GetComponent<DialSwitch>().position == 1){
                if(launcherController.loaded[0]){
                    fireTime = Time.time;
                    launcherController.launchMissile(0);
                }
                if(launcherController.loaded[1] && !launcherController.loaded[0] && (Time.time - fireTime) > launchInterval){
                    launcherController.launchMissile(1);
                }
            }
            if(salvoSelectDial.GetComponent<DialSwitch>().position == 0 && launcherController.loaded[0]){
                launcherController.launchMissile(0);
            }
            if(salvoSelectDial.GetComponent<DialSwitch>().position == 2 && launcherController.loaded[1]){
                launcherController.launchMissile(1);
            }
        }
        for(int cnt = 0;cnt < 2;cnt++){
            luncherLoadingLamp[cnt].GetComponent<Lamp>().enable = launcherController.isLoading[cnt];
            luncherReadyLamp[cnt].GetComponent<Lamp>().enable = launcherController.loaded[cnt];
        }
        launcherAlignLamp.GetComponent<Lamp>().enable = launcherController.aligned;


        heading.transform.localEulerAngles = new Vector3(0,0,-1f * ownship.GetComponent<ShipMove>().currentAngle - 90);
        heading2.transform.localEulerAngles = new Vector3(0,0,-1f * ownship.GetComponent<ShipMove>().currentAngle - 90);
    }
}
