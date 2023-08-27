using System;
using Runtime.Weapons;
using UnityEngine;

namespace Tests.Weapons
{
    public class ArrowShooter : MonoBehaviour
    {
        public GameObject Arrow;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                var arrow = Instantiate(Arrow);
                arrow.transform.position = transform.position;
                arrow.transform.rotation = transform.rotation;
                arrow.GetComponent<GravityProjectileWeapon>().Shot(new Vector3(0,0,0),45f);
            }
        }
    }
}