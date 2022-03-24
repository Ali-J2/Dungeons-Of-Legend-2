using System;
using System.Collections.Generic;
using UnityEngine;

namespace SheetCodes
{
	//Generated code, do not edit!

	public static class ModelManager
	{
        private static Dictionary<DatasheetType, LoadRequest> loadRequests;

        static ModelManager()
        {
            loadRequests = new Dictionary<DatasheetType, LoadRequest>();
        }

        public static void InitializeAll()
        {
            DatasheetType[] values = Enum.GetValues(typeof(DatasheetType)) as DatasheetType[];
            foreach(DatasheetType value in values)
                Initialize(value);
        }
		
        public static void Unload(DatasheetType datasheetType)
        {
            switch (datasheetType)
            {
                case DatasheetType.Scene:
                    {
                        if (sceneModel == null || sceneModel.Equals(null))
                        {
                            Log(string.Format("Sheet Codes: Trying to unload model {0}. Model is not loaded.", datasheetType));
                            break;
                        }
                        Resources.UnloadAsset(sceneModel);
                        sceneModel = null;
                        LoadRequest request;
                        if (loadRequests.TryGetValue(DatasheetType.Scene, out request))
                        {
                            loadRequests.Remove(DatasheetType.Scene);
                            request.resourceRequest.completed -= OnLoadCompleted_SceneModel;
							foreach (Action<bool> callback in request.callbacks)
								callback(false);
                        }
                        break;
                    }
                case DatasheetType.Avatar:
                    {
                        if (avatarModel == null || avatarModel.Equals(null))
                        {
                            Log(string.Format("Sheet Codes: Trying to unload model {0}. Model is not loaded.", datasheetType));
                            break;
                        }
                        Resources.UnloadAsset(avatarModel);
                        avatarModel = null;
                        LoadRequest request;
                        if (loadRequests.TryGetValue(DatasheetType.Avatar, out request))
                        {
                            loadRequests.Remove(DatasheetType.Avatar);
                            request.resourceRequest.completed -= OnLoadCompleted_AvatarModel;
							foreach (Action<bool> callback in request.callbacks)
								callback(false);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        public static void Initialize(DatasheetType datasheetType)
        {
            switch (datasheetType)
            {
                case DatasheetType.Scene:
                    {
                        if (sceneModel != null && !sceneModel.Equals(null))
                        {
                            Log(string.Format("Sheet Codes: Trying to Initialize {0}. Model is already initialized.", datasheetType));
                            break;
                        }

                        sceneModel = Resources.Load<SceneModel>("ScriptableObjects/Scene");
                        LoadRequest request;
                        if (loadRequests.TryGetValue(DatasheetType.Scene, out request))
                        {
                            Log(string.Format("Sheet Codes: Trying to initialize {0} while also async loading. Async load has been canceled.", datasheetType));
                            loadRequests.Remove(DatasheetType.Scene);
                            request.resourceRequest.completed -= OnLoadCompleted_SceneModel;
							foreach (Action<bool> callback in request.callbacks)
								callback(true);
                        }
                        break;
                    }
                case DatasheetType.Avatar:
                    {
                        if (avatarModel != null && !avatarModel.Equals(null))
                        {
                            Log(string.Format("Sheet Codes: Trying to Initialize {0}. Model is already initialized.", datasheetType));
                            break;
                        }

                        avatarModel = Resources.Load<AvatarModel>("ScriptableObjects/Avatar");
                        LoadRequest request;
                        if (loadRequests.TryGetValue(DatasheetType.Avatar, out request))
                        {
                            Log(string.Format("Sheet Codes: Trying to initialize {0} while also async loading. Async load has been canceled.", datasheetType));
                            loadRequests.Remove(DatasheetType.Avatar);
                            request.resourceRequest.completed -= OnLoadCompleted_AvatarModel;
							foreach (Action<bool> callback in request.callbacks)
								callback(true);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        public static void InitializeAsync(DatasheetType datasheetType, Action<bool> callback)
        {
            switch (datasheetType)
            {
                case DatasheetType.Scene:
                    {
                        if (sceneModel != null && !sceneModel.Equals(null))
                        {
                            Log(string.Format("Sheet Codes: Trying to InitializeAsync {0}. Model is already initialized.", datasheetType));
                            callback(true);
                            break;
                        }
                        if(loadRequests.ContainsKey(DatasheetType.Scene))
                        {
                            loadRequests[DatasheetType.Scene].callbacks.Add(callback);
                            break;
                        }
                        ResourceRequest request = Resources.LoadAsync<SceneModel>("ScriptableObjects/Scene");
                        loadRequests.Add(DatasheetType.Scene, new LoadRequest(request, callback));
                        request.completed += OnLoadCompleted_SceneModel;
                        break;
                    }
                case DatasheetType.Avatar:
                    {
                        if (avatarModel != null && !avatarModel.Equals(null))
                        {
                            Log(string.Format("Sheet Codes: Trying to InitializeAsync {0}. Model is already initialized.", datasheetType));
                            callback(true);
                            break;
                        }
                        if(loadRequests.ContainsKey(DatasheetType.Avatar))
                        {
                            loadRequests[DatasheetType.Avatar].callbacks.Add(callback);
                            break;
                        }
                        ResourceRequest request = Resources.LoadAsync<AvatarModel>("ScriptableObjects/Avatar");
                        loadRequests.Add(DatasheetType.Avatar, new LoadRequest(request, callback));
                        request.completed += OnLoadCompleted_AvatarModel;
                        break;
                    }
                default:
                    break;
            }
        }

        private static void OnLoadCompleted_SceneModel(AsyncOperation operation)
        {
            LoadRequest request = loadRequests[DatasheetType.Scene];
            sceneModel = request.resourceRequest.asset as SceneModel;
            loadRequests.Remove(DatasheetType.Scene);
            operation.completed -= OnLoadCompleted_SceneModel;
            foreach (Action<bool> callback in request.callbacks)
                callback(true);
        }

		private static SceneModel sceneModel = default;
		public static SceneModel SceneModel
        {
            get
            {
                if (sceneModel == null)
                    Initialize(DatasheetType.Scene);

                return sceneModel;
            }
        }
        private static void OnLoadCompleted_AvatarModel(AsyncOperation operation)
        {
            LoadRequest request = loadRequests[DatasheetType.Avatar];
            avatarModel = request.resourceRequest.asset as AvatarModel;
            loadRequests.Remove(DatasheetType.Avatar);
            operation.completed -= OnLoadCompleted_AvatarModel;
            foreach (Action<bool> callback in request.callbacks)
                callback(true);
        }

		private static AvatarModel avatarModel = default;
		public static AvatarModel AvatarModel
        {
            get
            {
                if (avatarModel == null)
                    Initialize(DatasheetType.Avatar);

                return avatarModel;
            }
        }
		
        private static void Log(string message)
        {
            Debug.LogWarning(message);
        }
	}
	
    public struct LoadRequest
    {
        public readonly ResourceRequest resourceRequest;
        public readonly List<Action<bool>> callbacks;

        public LoadRequest(ResourceRequest resourceRequest, Action<bool> callback)
        {
            this.resourceRequest = resourceRequest;
            callbacks = new List<Action<bool>>() { callback };
        }
    }
}
