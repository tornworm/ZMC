using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
/// 

///   
///   
public class TreeCreate : EditorWindow
{
    ////三个属性
    //string tree = "";
    //string size = "";
    //GameObject thing;
    Vector3 pos;
    int size;
    //1.可序列化对象
    [SerializeField]
    protected List<Object> ObjList = new List<Object>();
    [SerializeField]
    protected List<int> count = new List<int>();
    //2.序列化对象及属性
    protected SerializedObject S_objList;
    protected SerializedProperty SP_objList;
    protected SerializedProperty SP_objList1;
    TreeCreate()
    {
        this.titleContent = new GUIContent("Create Tree");
    }
    protected void OnEnable()
    {
        //3.使用当前类初始化
        S_objList = new SerializedObject(this);
        S_objList = new SerializedObject(this);
        //4.获取当前类中可序列话的属性
        SP_objList = S_objList.FindProperty("ObjList");
        SP_objList1= S_objList.FindProperty("count");
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
        pos = EditorGUILayout.Vector3Field("pos", pos);
        GUILayout.Space(10);
        size = EditorGUILayout.IntField("size", size);




        //更新
        S_objList.Update();
        //检查是否有修改
        EditorGUI.BeginChangeCheck();
        //5.显示属性
        EditorGUILayout.PropertyField(SP_objList, true);
        EditorGUILayout.PropertyField(SP_objList1, true);
        //结束检查是否有修改
        if (EditorGUI.EndChangeCheck())
        {//提交修改
            S_objList.ApplyModifiedProperties();
        }



        EditorGUILayout.Space();
        //按钮
        if (GUILayout.Button("Create"))
        {
          
            for (int i = 0; i < ObjList.Count; i++)
            {
                for (int j = 0; j < count[i]; j++)
                {
                   
                    GameObject.Instantiate(ObjList[i], 
                   new Vector3(pos.x-size/2+Random.Range(0,size),pos.y,pos.z-size/2+Random.Range(0,size)), 
                   Quaternion.identity);
                }
               

            }


        }

        //结束竖直分布
        GUILayout.EndVertical();
    }
   
}