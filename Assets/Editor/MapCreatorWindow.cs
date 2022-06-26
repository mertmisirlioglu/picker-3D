using UnityEngine;
using UnityEditor;


// I didnt have to time to complete.
public class MapCreatorWindow : EditorWindow
{
    private Texture2D headerSectionTexture;

    private Rect headerSection;

    private GUISkin skin;

    Color headerSectionColor = new Color(13f/255f,32f/255f,44f/255f,1f);

    [MenuItem("Window/Map Creator")]
    static void OpenWindow()
    {
        MapCreatorWindow window = (MapCreatorWindow) GetWindow((typeof(MapCreatorWindow)));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }

    private void OnEnable()
    {
        InitTextures();
        skin = Resources.Load<GUISkin>("GUIStyles/GUISkin");
    }

    void InitTextures()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
        headerSectionTexture.Apply();
    }

    private void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
    }

    void DrawLayouts()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = Screen.width;
        headerSection.height = 60;

        GUI.DrawTexture(headerSection,headerSectionTexture);
    }

    void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);
        GUILayout.Label("Map Creator", skin.GetStyle("Header"));

        GUILayout.EndArea();
    }
}
