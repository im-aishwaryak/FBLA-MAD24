using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class SubjectButtonHandler : MonoBehaviour
{
    public string subjectName; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSubject()
    {
        UnityEngine.Debug.Log("when clicked subject is " + subjectName); 
        SubjectManager.onSubjectSelected(subjectName);
    }
}
