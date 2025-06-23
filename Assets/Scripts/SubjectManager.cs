using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SubjectManager
{

    public static string selectedSubject; 

    public static void onSubjectSelected(string subject)
    {

        SubjectManager.selectedSubject = subject;
        Debug.Log("subject manager subject = " + selectedSubject); 
    }
}
