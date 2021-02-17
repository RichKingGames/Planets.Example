using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Mass;
    private void OnDestroy()
    {
        //GameObject.Find("Spawner2").GetComponent<ObjectMap>().RemoveFromMap(this.gameObject);
    }
}
