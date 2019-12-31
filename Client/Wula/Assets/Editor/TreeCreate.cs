using System.Collections;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
/// 

///   
///   
public class TreeCreate : EditorWindow
{
    //三个属性
    string tree = "";
    string size = "";
    GameObject thing;
    TreeCreate()
    {
        this.titleContent = new GUIContent("Create Tree");
    }

    //unity界面按钮
    [MenuItem("Dead fish/造树")]
    static void ShowWindow()
    {
        //打开这个类
        GetWindow(typeof(TreeCreate));
    }
    //绘制编辑器拓展界面
    void OnGUI()
    {
        //垂直分布
        EditorGUILayout.BeginVertical();

        //格式与标题
        GUILayout.Space(10);
        GUI.skin.label.fontSize = 24;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("Create Tree");

        GUILayout.Space(10);
        //输入树数量
        tree = EditorGUILayout.TextField("tree", tree);
        //输入范围
        GUILayout.Space(10);
        size = EditorGUILayout.TextField("size", size);
        //输入目标对象
        GUILayout.Space(10);
        thing = (GameObject)EditorGUILayout.ObjectField("Object", thing, typeof(GameObject), true);


        EditorGUILayout.Space();
        //按钮
        if (GUILayout.Button("Create"))
        {
            for (int i = 0; i < int.Parse(tree); i++)
            {
                GameObject.Instantiate(thing);
            }
           
     
        }
     
        //结束竖直分布
        GUILayout.EndVertical();
    }
   
}