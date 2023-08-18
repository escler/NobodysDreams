using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// FINAL -Lucrecia Ferrari- se aplican Enums para el uso de queue, setea constantes de los tutoriales, si se mete en la queue o si cierra el que esta y abre el actual,
// ademas de setear cada parte donde se necesita del dialogo. Uso de Encapsulamiento (private) para que no se modifiquen los elementos por otros. Se usa en: metodos, bools, floats.

// DialogType.Tutorial: Se encolan
// DialogType.Instant: No se encolan y se muestran en el momento
public enum DialogType
{
    Tutorial,
    Instant
}

// Una DialogKey por cada situacion que requiera un dialogo
public enum DialogKey
{
    IntroductionGuide,
    Flashlight,
    RulerCube,
    FirstCheckpoint,
    Boosters,
    ChocolatesDown,
    Ghost,
    LittleGhost,
    CandyWheel,
    Glasses,
    Pills,
    BottleOfPills,
    FakeWindow,
    RealWindow,
    Level2,
    BallPickable,
    Bucket,
    Practice,
    FirstDiana,
    Walls,
    BookLocked,
    OreosRotation,
    BoardRotation,
    CarefullGhosts,
    DuckWay,
    ToTheBed,
    ShouldDown,
    QuestClowns,
    NoseAdivination,
    SearchRudolf,
    EnableLetter,
    Pieces,
    Searching,
    Victory,
    EnableEquip,
    DownDoor,
    GoToTheFinal
}

public class DialogManager : MonoBehaviour
{
    // Definicion de los dialogos, usamos STRUCTS para agrupar los datos de los dialogos del tutorial
    [System.Serializable]
    private struct DialogEntry
    {
        [SerializeField] internal DialogKey key;
        [SerializeField] internal DialogType type;
        [SerializeField, Multiline] internal string text;

        internal bool shown;
    }

    // Variables seteables desde el inspector
    [SerializeField] AudioSource dialogAudioSource;
    [SerializeField] AudioClip[] wawas;
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private float dialogTime = 7f;
    [SerializeField] private DialogEntry[] dialogEntries;

    private Queue<int> dialogQueue = new Queue<int>();
    private float t = 0f;
    private bool showingInstantDialog = false;

    int randomNumber;
    string lastDialog, actualDialog;

    // Metodo principal para indicarle al manager que muestre un nuevo dialogo
    public void ShowDialog(DialogKey key)
    {
        actualDialog = key.ToString();
        randomNumber = UnityEngine.Random.Range(0, wawas.Length -1);

        // Busqueda de la definicion del dialogo por key
        int idx = Array.FindIndex(dialogEntries, dialogEntry => dialogEntry.key == key);
        // Si no fue encontrado, salimos
        if (idx == -1)
        {
            return;
        }

        DialogEntry dialogEntry = dialogEntries[idx];

        // Si ya fue mostrado, salimos
        if (dialogEntry.shown)
        {
            return;
        }

        // Lo marcamos como mostrado
        dialogEntry.shown = true;

        if (dialogEntry.type == DialogType.Tutorial)
        {
            // DialogType.Tutorial: se encola
            dialogQueue.Enqueue(idx);
        }
        else
        {
            // DialogType.Instant: se muestra instantaneamente
            ShowDialog(dialogEntry);
        }
    }

    private void ShowDialog(DialogEntry dialogEntry)
    {
        // Seteo del timer
        t = dialogTime;
        // Seteamos si estamos mostrando un DialogType.Instant
        showingInstantDialog = dialogEntry.type == DialogType.Instant;
        // Seteo del texto
        dialogBox.GetComponent<TextMeshProUGUI>().text = dialogEntry.text;
        // Habilitacion del dialogBox (UI)
        dialogBox.SetActive(true);
    }

    private void HideDialog()
    {
        // Si NO se estaba mostrando un DialogType.Instant hay que remover el index de la cola, unicamente despues de que fue mostrado
        if (!showingInstantDialog)
        {
            dialogQueue.Dequeue();
        }
        // Deshabilitacion del dialogBox (UI)
        dialogBox.SetActive(false);
    }

    private void Update()
    {

        float dt = Time.deltaTime;

        if(actualDialog != lastDialog)
        {
            dialogAudioSource.PlayOneShot(wawas[randomNumber]);
            lastDialog = actualDialog;
        }

        if (t > 0f)
        {
            t = Mathf.Max(t - dt, 0f);
            if (t == 0f)
            {
                // Solo ocultamos el dialogo cuando se cumple el tiempo
                HideDialog();
            }
            return;
        }

        // Mostramos el primer dialogo en la cola (si existe al menos uno)
        if (dialogQueue.Count > 0)
        {

            int idx = dialogQueue.Peek();
            ShowDialog(dialogEntries[idx]);
        }
    }
}
