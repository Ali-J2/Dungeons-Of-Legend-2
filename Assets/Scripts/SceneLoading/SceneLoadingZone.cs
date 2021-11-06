using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SheetCodes;

namespace DungeonBrickStudios
{
    public class SceneLoadingZone : MonoBehaviour
    {
        [SerializeField] private SubTriggerCollider sceneTriggerArea = default;
        [SerializeField] private List<SceneIdentifier> scenesToLoad = default;


        private void Awake()
        {
            sceneTriggerArea.onTriggerEnter += OnEvent_SceneTriggerEntered;
        }

        private void OnEvent_SceneTriggerEntered(Collider col)
        {
            if (!col.CompareTag("Player"))
                return;

            GameSceneManager.instance.HandleSceneLoading(scenesToLoad);
        }
    }
}
