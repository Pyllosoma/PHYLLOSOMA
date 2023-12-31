﻿using System;
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
                arrow.GetComponent<ProjectileTestWeapon>().Shoot(null,transform.forward,10f);
            }
        }
    }
}