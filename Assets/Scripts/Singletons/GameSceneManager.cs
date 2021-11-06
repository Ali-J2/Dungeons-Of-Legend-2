using System;
using System.Collections.Generic;
using UnityEngine;
using SheetCodes;
using UnityEngine.SceneManagement;

namespace DungeonBrickStudios
{
    public class GameSceneManager : MonoBehaviour
    {
        public static GameSceneManager instance { get; private set; }

        [SerializeField] private List<SceneIdentifier> scenesToLoadOnStart;
        private List<SceneIdentifier> currentlyLoadedScenes;

        private void Awake()
        {
            instance = this;
            currentlyLoadedScenes = new List<SceneIdentifier>();
            HandleScenesOnStart();
        }

        public void HandleSceneLoading(List<SceneIdentifier> scenesToLoad)
        {
            // remove unwanted scenes
            for (int i = currentlyLoadedScenes.Count - 1; i >= 0; i--)
            {
                if (scenesToLoad.Contains(currentlyLoadedScenes[i]))
                    continue;

                UnloadScene(currentlyLoadedScenes[i]);
            }

            // Add desired scenes if not already loaded
            foreach (SceneIdentifier scene in scenesToLoad)
            {
                if (currentlyLoadedScenes.Contains(scene))
                    continue;

                LoadScene(scene);
            }
        }

        private void LoadScene(SceneIdentifier scene)
        {
            if (currentlyLoadedScenes.Contains(scene))
                return;

            currentlyLoadedScenes.Add(scene);
            SceneManager.LoadSceneAsync(scene.GetRecord().Name, LoadSceneMode.Additive);
        }

        private void UnloadScene(SceneIdentifier scene)
        {
            if (!currentlyLoadedScenes.Contains(scene))
                return;

            currentlyLoadedScenes.Remove(scene);
            SceneManager.UnloadSceneAsync(scene.GetRecord().Name);
        }

        private void HandleScenesOnStart()
        {
            foreach (SceneIdentifier scene in scenesToLoadOnStart)
            {
                if (SceneLoaded(scene.GetRecord().Name))
                    continue;

                LoadScene(scene);
            }
        }

        private bool SceneLoaded(string sceneName)
        {
            bool sceneOpen = false;

            for (int i = 0; i < SceneManager.sceneCount; i++)
                if (SceneManager.GetSceneAt(i).name == sceneName)
                    sceneOpen = true;

            return sceneOpen;
        }
    }
}
