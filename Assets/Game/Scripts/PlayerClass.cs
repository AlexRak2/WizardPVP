using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{

    public WizardClass cl_class;

    public List<WizardClassObject> g_classes;

    [Header("UI")]
    public GameObject classSelector;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GetComponent<CameraMovement>().allowInput = false;
    }

    public void SelectClass(int c)
    {
        cl_class = (WizardClass)c;
        UpdateClass();
        classSelector.SetActive(false);
        GetComponent<CameraMovement>().allowInput = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void UpdateClass()
    {
        HideAllExcept(cl_class);


    }


    private void HideAllExcept(WizardClass c)
    {
        foreach (var item in g_classes)
        {
            if (item.wizardClass == c)
            {
                item.classPlayerModel.SetActive(true);
            }
            else
            {
                item.classPlayerModel.SetActive(false);
            }
        }
    }
}

public enum WizardClass
{
    Ancient = 0,
    Zephir,
    Pyro,
    Electra,
}

[System.Serializable]
public struct WizardClassObject 
{
    public WizardClass wizardClass;
    public GameObject classPlayerModel;
    [SerializeField] private ConfigurableJoint[] joints;

}
