using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

class MaskTextureGeneratorWindow : EditorWindow
{
    [MenuItem("Window/MaskHelper/MaskTextureGenerator")]
    static void OpenWindow()
    {
        MaskTextureGeneratorWindow ww = GetWindow<MaskTextureGeneratorWindow>();
        ww.Show(true);
    }

    public Texture2D targetTexture;

    void OnGUI()
    {
        targetTexture = (Texture2D) EditorGUILayout.ObjectField("Image", targetTexture, typeof (Texture2D), false);

        if (GUILayout.Button("Gen Mask")) {
            CreateMaskTexture();
        }
    }

    private void CreateMaskTexture()
    {
        if (targetTexture == null)
            return;

        int w = targetTexture.width;
        int h = targetTexture.height;
        Texture2D mask = new Texture2D(w, h, TextureFormat.RGB24, false);
        var pixels = targetTexture.GetPixels();
        for (int i = 0; i < pixels.Length; i++) {
            int x = i % w;
            int y = i / w;
            float a = pixels[i].a;
            mask.SetPixel(x, y, new Color(a, a, a, 1.0f));
        }
        byte[] maskPNG = mask.EncodeToPNG();
        if (maskPNG != null) {
            string targetPath = AssetDatabase.GetAssetPath(targetTexture.GetInstanceID());
            string dirName  = System.IO.Path.GetDirectoryName(targetPath);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(targetPath) + "Mask.png";
            string newPath  = System.IO.Path.Combine(dirName, fileName);
            System.IO.File.WriteAllBytes(newPath, maskPNG);
        }
        DestroyImmediate(mask);
        AssetDatabase.Refresh();
        targetTexture = null;
    }
}
