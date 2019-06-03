using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NucleonSpawner : MonoBehaviour
{
    public float timeBetweenSpawns;
    public float spawnDistance;
    public Nucleon[] nucleonPrefabs;
    float timeSinceLastSpawn;
    float timeSinceLastSpawnLocationChange;

    static readonly SpawnLocation[] methods =
    {
        UpSpawn,
        DownSpawn,
        LeftSpawn,
        RightSpawn,
        FrontSpawn,
        BackSpawn,
        RandomSpawn
    };

    SpawnLocation spawning;
    public SpawnMethod method;
    int method_index;
    public Text DirectionText;

    int nb_spawn = 0;
    public Text NbSpawnText;

    private void Awake()
    {
        method_index = (int)method;
    }

    void FixedUpdate()
    {

        if (timeSinceLastSpawnLocationChange > 5f)
        {
            timeSinceLastSpawnLocationChange = 0f;
            ++method_index;
            if (method_index >= methods.Length)
                method_index = 0;
        }

        spawning = methods[method_index];
        timeSinceLastSpawn += Time.deltaTime;
        timeSinceLastSpawnLocationChange += Time.deltaTime;
        if (timeSinceLastSpawn >= timeBetweenSpawns)
        {
            timeSinceLastSpawn -= timeBetweenSpawns;
            SpawnNucleon();
        }

        DirectionText.text = System.Enum.GetName(typeof(SpawnMethod), (SpawnMethod)method_index);
        NbSpawnText.text = ""+nb_spawn;
    }

    void SpawnNucleon()
    {
        Nucleon prefab = nucleonPrefabs[Random.Range(0, nucleonPrefabs.Length)];
        Nucleon spawn = Instantiate<Nucleon>(prefab);
        spawn.transform.localPosition = spawning() * spawnDistance;
        spawn.transform.SetParent(transform);
        ++nb_spawn;
    }

    static Vector3 UpSpawn() { return Vector3.up ; }
    static Vector3 DownSpawn() { return Vector3.down ; }
    static Vector3 LeftSpawn() { return Vector3.left; }
    static Vector3 RightSpawn() { return Vector3.right; }
    static Vector3 FrontSpawn() { return Vector3.forward; }
    static Vector3 BackSpawn() { return Vector3.back; }
    static Vector3 RandomSpawn() { return Random.onUnitSphere ; ; }
}
