using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    [SerializeField] private bool isPlayer = true;
    [SerializeField] private Material playerMaterial;
    [SerializeField] private Material enemyMaterial;
    
    private void Start()
    {
        CreateCharacterModel();
    }
    
    private void CreateCharacterModel()
    {
        if (isPlayer)
        {
            CreatePlayerModel();
        }
        else
        {
            CreateEnemyModel();
        }
    }
    
    private void CreatePlayerModel()
    {
        // الرأس (أزرق)
        GameObject head = CreateCube(new Vector3(0, 0.4f, 0), new Vector3(0.3f, 0.4f, 0.3f), Color.blue);
        head.transform.parent = transform;
        
        // العيون (بيضاء)
        GameObject leftEye = CreateCube(new Vector3(-0.1f, 0.55f, 0.15f), new Vector3(0.1f, 0.1f, 0.05f), Color.white);
        leftEye.transform.parent = transform;
        
        GameObject rightEye = CreateCube(new Vector3(0.1f, 0.55f, 0.15f), new Vector3(0.1f, 0.1f, 0.05f), Color.white);
        rightEye.transform.parent = transform;
        
        // الجسم (أزرق داكن)
        GameObject body = CreateCube(new Vector3(0, 0, 0), new Vector3(0.4f, 0.5f, 0.3f), new Color(0, 0, 0.7f));
        body.transform.parent = transform;
        
        // الأطراف اليسرى (أزرق فاتح)
        GameObject leftArm = CreateCube(new Vector3(-0.25f, 0.1f, 0), new Vector3(0.2f, 0.4f, 0.2f), new Color(0.5f, 0.5f, 1f));
        leftArm.transform.parent = transform;
        
        GameObject leftLeg = CreateCube(new Vector3(-0.15f, -0.4f, 0), new Vector3(0.2f, 0.4f, 0.2f), new Color(0.2f, 0.2f, 0.8f));
        leftLeg.transform.parent = transform;
        
        // الأطراف اليمنى (أزرق فاتح)
        GameObject rightArm = CreateCube(new Vector3(0.25f, 0.1f, 0), new Vector3(0.2f, 0.4f, 0.2f), new Color(0.5f, 0.5f, 1f));
        rightArm.transform.parent = transform;
        
        GameObject rightLeg = CreateCube(new Vector3(0.15f, -0.4f, 0), new Vector3(0.2f, 0.4f, 0.2f), new Color(0.2f, 0.2f, 0.8f));
        rightLeg.transform.parent = transform;
    }
    
    private void CreateEnemyModel()
    {
        // الرأس (أحمر)
        GameObject head = CreateCube(new Vector3(0, 0.45f, 0), new Vector3(0.35f, 0.45f, 0.35f), Color.red);
        head.transform.parent = transform;
        
        // القرنان (أسود)
        GameObject leftHorn = CreateCube(new Vector3(-0.15f, 0.85f, 0.1f), new Vector3(0.1f, 0.3f, 0.1f), Color.black);
        leftHorn.transform.parent = transform;
        
        GameObject rightHorn = CreateCube(new Vector3(0.15f, 0.85f, 0.1f), new Vector3(0.1f, 0.3f, 0.1f), Color.black);
        rightHorn.transform.parent = transform;
        
        // العيون (حمراء مظلمة)
        GameObject leftEye = CreateCube(new Vector3(-0.12f, 0.6f, 0.17f), new Vector3(0.1f, 0.12f, 0.06f), new Color(0.8f, 0, 0));
        leftEye.transform.parent = transform;
        
        GameObject rightEye = CreateCube(new Vector3(0.12f, 0.6f, 0.17f), new Vector3(0.1f, 0.12f, 0.06f), new Color(0.8f, 0, 0));
        rightEye.transform.parent = transform;
        
        // الفم (أسود مظلم)
        GameObject mouth = CreateCube(new Vector3(0, 0.35f, 0.17f), new Vector3(0.2f, 0.08f, 0.05f), new Color(0.3f, 0, 0));
        mouth.transform.parent = transform;
        
        // الجسم (أحمر داكن)
        GameObject body = CreateCube(new Vector3(0, 0, 0), new Vector3(0.45f, 0.55f, 0.35f), new Color(0.6f, 0, 0));
        body.transform.parent = transform;
        
        // الأطراف اليسرى (أحمر فاتح)
        GameObject leftArm = CreateCube(new Vector3(-0.28f, 0.1f, 0), new Vector3(0.22f, 0.45f, 0.22f), new Color(0.9f, 0.3f, 0.3f));
        leftArm.transform.parent = transform;
        
        GameObject leftLeg = CreateCube(new Vector3(-0.18f, -0.4f, 0), new Vector3(0.22f, 0.45f, 0.22f), new Color(0.5f, 0, 0));
        leftLeg.transform.parent = transform;
        
        // الأطراف اليمنى (أحمر فاتح)
        GameObject rightArm = CreateCube(new Vector3(0.28f, 0.1f, 0), new Vector3(0.22f, 0.45f, 0.22f), new Color(0.9f, 0.3f, 0.3f));
        rightArm.transform.parent = transform;
        
        GameObject rightLeg = CreateCube(new Vector3(0.18f, -0.4f, 0), new Vector3(0.22f, 0.45f, 0.22f), new Color(0.5f, 0, 0));
        rightLeg.transform.parent = transform;
    }
    
    private GameObject CreateCube(Vector3 position, Vector3 scale, Color color)
    {
        GameObject cube = new GameObject();
        cube.transform.localPosition = position;
        cube.transform.localScale = scale;
        
        MeshFilter meshFilter = cube.AddComponent<MeshFilter>();
        meshFilter.mesh = Resources.GetBuiltinResource<Mesh>("Cube.fbx");
        
        MeshRenderer meshRenderer = cube.AddComponent<MeshRenderer>();
        Material mat = new Material(Shader.Find("Standard"));
        mat.color = color;
        meshRenderer.material = mat;
        
        return cube;
    }
}
