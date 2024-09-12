using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Deliverance.GameState
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject loadingCanvas;
        [SerializeField] private Slider progressBar;

        public void Awake()
        {
            loadingCanvas.SetActive(false);
            SceneManager.LoadSceneAsync((int)Scenes.MainMenu, LoadSceneMode.Additive);
        }

        private List<AsyncOperation> _loadOperations = new();

        public void LoadGame()
        {
            progressBar.value = 0;
            loadingCanvas.SetActive(true);
            _loadOperations.Add(SceneManager.UnloadSceneAsync((int)Scenes.MainMenu));
            _loadOperations.Add(SceneManager.LoadSceneAsync((int)Scenes.Game, LoadSceneMode.Additive));
            StartCoroutine(GetSceneLoadProgress());
        }

        private float _totalProgress = 0;
        public IEnumerator GetSceneLoadProgress()
        {
            for (int i = 0; i < _loadOperations.Count; i++)
            {
                while (!_loadOperations[i].isDone)
                {
                    foreach (var operation in _loadOperations)
                    {
                        _totalProgress += operation.progress;
                    }

                    _totalProgress /= _loadOperations.Count;
                    //progressBar.value = _totalProgress;
                    yield return null;
                }
            }

            loadingCanvas.SetActive(false);
        }

        void Update()
        {
            progressBar.value = Mathf.MoveTowards(progressBar.value, _totalProgress, Time.deltaTime * 0.5f);
        }
    }
}